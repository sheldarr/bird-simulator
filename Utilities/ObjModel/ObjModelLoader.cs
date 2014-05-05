using System.Collections.Generic;
using System.IO;
using OpenTK;
using Utilities.ObjModel.ObjPrimitives;

namespace Utilities.ObjModel
{
    public class ObjModelLoader
    {
        private static List<Vector3> _vertices;
        private static List<Vector3> _normals;
        private static List<Vector2> _texCoords;
        private static Dictionary<ObjVertex, int> _objVerticesIndexDictionary;
        private static List<ObjVertex> _objVertices;
        private static List<ObjTriangle> _objTriangles;
        private static List<ObjQuad> _objQuads;

        private static readonly char[] SplitCharacters = { ' ' };
        private static readonly char[] FaceParamaterSplitter = { '/' };

        public static void Load(ObjModel model, string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                Load(model, streamReader);
                streamReader.Close();
            }
        }

        static void Load(ObjModel model, TextReader textReader)
        {
            _vertices = new List<Vector3>();
            _normals = new List<Vector3>();
            _texCoords = new List<Vector2>();
            _objVerticesIndexDictionary = new Dictionary<ObjVertex, int>();
            _objVertices = new List<ObjVertex>();
            _objTriangles = new List<ObjTriangle>();
            _objQuads = new List<ObjQuad>();

            string line;
            while ((line = textReader.ReadLine()) != null)
            {
                line = line.Trim(SplitCharacters);
                line = line.Replace("  ", " ");

                string[] parameters = line.Split(SplitCharacters);

                switch (parameters[0])
                {
                    case "p": // Point
                        break;

                    case "v": // Vertex
                        var x = float.Parse(parameters[1]);
                        var y = float.Parse(parameters[2]);
                        var z = float.Parse(parameters[3]);
                        _vertices.Add(new Vector3(x, y, z));
                        break;

                    case "vt": // TexCoord
                        var u = float.Parse(parameters[1]);
                        var v = float.Parse(parameters[2]);
                        _texCoords.Add(new Vector2(u, v));
                        break;

                    case "vn": // Normal
                        var nx = float.Parse(parameters[1]);
                        var ny = float.Parse(parameters[2]);
                        var nz = float.Parse(parameters[3]);
                        _normals.Add(new Vector3(nx, ny, nz));
                        break;

                    case "f":
                        switch (parameters.Length)
                        {
                            case 4:
                                var objTriangle = new ObjTriangle
                                {
                                    Index0 = ParseFaceParameter(parameters[1]),
                                    Index1 = ParseFaceParameter(parameters[2]),
                                    Index2 = ParseFaceParameter(parameters[3])
                                };
                                _objTriangles.Add(objTriangle);
                                break;

                            case 5:
                                var objQuad = new ObjQuad
                                {
                                    Index0 = ParseFaceParameter(parameters[1]),
                                    Index1 = ParseFaceParameter(parameters[2]),
                                    Index2 = ParseFaceParameter(parameters[3]),
                                    Index3 = ParseFaceParameter(parameters[4])
                                };
                                _objQuads.Add(objQuad);
                                break;
                        }
                        break;
                }
            }

            model.Vertices = _objVertices.ToArray();
            model.Triangles = _objTriangles.ToArray();
            model.Quads = _objQuads.ToArray();

            _objVerticesIndexDictionary = null;
            _vertices = null;
            _normals = null;
            _texCoords = null;
            _objVertices = null;
            _objTriangles = null;
            _objQuads = null;
        }

        static int ParseFaceParameter(string faceParameter)
        {
            var vertex = new Vector3();
            var texCoord = new Vector2();
            var normal = new Vector3();

            var parameters = faceParameter.Split(FaceParamaterSplitter);

            var vertexIndex = int.Parse(parameters[0]);

            if (vertexIndex < 0)
            {
                vertexIndex = _vertices.Count + vertexIndex;
            }
            else
            {
                vertexIndex = vertexIndex -1;
            }

            vertex = _vertices[vertexIndex];

            if (parameters.Length > 1)
            {
                var texCoordIndex = int.Parse(parameters[1]);
                if (texCoordIndex < 0) texCoordIndex = _texCoords.Count + texCoordIndex;
                else texCoordIndex = texCoordIndex - 1;
                texCoord = _texCoords[texCoordIndex];
            }

            if (parameters.Length > 2)
            {
                var normalIndex = int.Parse(parameters[2]);
                if (normalIndex < 0) normalIndex = _normals.Count + normalIndex;
                else normalIndex = normalIndex - 1;
                normal = _normals[normalIndex];
            }

            return FindOrAddObjVertex(ref vertex, ref texCoord, ref normal);
        }

        static int FindOrAddObjVertex(ref Vector3 vertex, ref Vector2 texCoord, ref Vector3 normal)
        {
            var newObjVertex = new ObjVertex {Vertex = vertex, TexCoord = texCoord, Normal = normal};

            int index;
            if (_objVerticesIndexDictionary.TryGetValue(newObjVertex, out index))
            {
                return index;
            }
            else
            {
                _objVertices.Add(newObjVertex);
                _objVerticesIndexDictionary[newObjVertex] = _objVertices.Count - 1;
                return _objVertices.Count - 1;
            }
        }
    }
}
