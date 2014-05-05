using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.Bird;
using Engine.World;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Utilities;
using Utilities.ObjModel;
using Utilities.Shapes;

namespace GraphicsEngine.Scene
{
    public class WorldScene : IDisposable
    {
        private readonly GameWindow _scene;
        private readonly World _world;
        private readonly IList<Bird> _birds; 
        private Vector3 _rotation = new Vector3(0, 0 ,0);
        private readonly ObjModel _birdObj = new ObjModel("HUMBIRD.obj");
        private readonly ObjModel _treeObj = new ObjModel("tree.obj");
        private readonly Camera.Camera _camera = new Camera.Camera();
        

        public WorldScene(World world, IList<Bird> birds)
        {
            _world = world;
            _birds = birds;
            _scene = new GameWindow(Convert.ToInt32(_world.WindowResolution.X), Convert.ToInt32(_world.WindowResolution.Y), new GraphicsMode(32, 32, 0, 16), "Bird Simulator");
            _scene.Load += Load;
            _scene.Resize += Resize;
            _scene.UpdateFrame += UpdateFrame;
            _scene.RenderFrame += RenderFrame;
            _scene.VSync = VSyncMode.On;
        }

        public void StartRendering()
        {
            _scene.Run(_world.RenderFps);
        }

        public void Dispose()
        {
            _scene.Dispose();
        }

        private void Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);

            //GL.Enable(EnableCap.Lighting);
            //GL.Enable(EnableCap.Light0);
            //GL.Enable(EnableCap.ColorMaterial);
            //GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            //var ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };

            //var diffuseLight = new float[] { 80, 80, 80, 1.0f };
            //var specularLight = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            //var position = new float[] { -2f, -2f, -2f, 1.0f };

            //GL.LightModel(LightModelParameter.LightModelAmbient, ambient);
            //GL.Light(LightName.Light0, LightParameter.Ambient, ambient);
            //GL.Light(LightName.Light0, LightParameter.Diffuse, diffuseLight);
            //GL.Light(LightName.Light0, LightParameter.Position, position);

            GL.ShadeModel(ShadingModel.Smooth);
        }

        private void Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, _scene.Width, _scene.Height);
        }

        private void UpdateFrame(object sender, FrameEventArgs e)
        {
            if (_scene.Keyboard[Key.Escape])
            {
                _scene.Exit();
            }
            if (_scene.Keyboard[Key.A])
            {
                _rotation.Y += _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.W])
            {
                _camera.MoveForward();
            }
            if (_scene.Keyboard[Key.S])
            {
                _camera.MoveBackward();
            }
            if (_scene.Keyboard[Key.D])
            {
                _rotation.Y -= _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.U])
            {
                _rotation.X += _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.J])
            {
                _rotation.X -= _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.H])
            {
                _rotation.Y += _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.K])
            {
                _rotation.Y -= _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.Y])
            {
                _rotation.Z += _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.I])
            {
                _rotation.Z -= _world.RotationAngle;
            }
        }

        private void RenderFrame(object sender, FrameEventArgs e)
        {   
            InitNextFrame();
            PerformTransformations();
            RenderWorldEdges();
            RenderObjects();
            
            _scene.SwapBuffers();
        }

        private void InitNextFrame()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
           
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var m = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver3, _scene.Width / _scene.Height, 0.001f, 5000);
            GL.LoadMatrix(ref m);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.LoadMatrix(ref _camera.LookAt);
        }

        private void PerformTransformations()
        {
            GL.Rotate(_rotation.X, 1, 0, 0);
            GL.Rotate(_rotation.Y, 0, 1, 0);
            GL.Rotate(_rotation.Z, 0, 0, 1);
        }

        private void RenderWorldEdges()
        {
            var world = new Cube(_world.WorldSize, Color.Yellow);
            world.Render();

            var outerWorld = new Cube(1024, Color.Red);
            outerWorld.Render();
        }

        private void RenderObjects()
        {
            GL.PushMatrix();
            GL.Color3(Color.DarkGreen);
            GL.Scale(new Vector3(0.05f));
            GL.Translate(-_world.WorldSize, -_world.WorldSize, -_world.WorldSize);
            GL.Translate(-10, -10, -10);
            _treeObj.Render();
            GL.PopMatrix();


            GL.PushMatrix();
            GL.Color3(Color.DarkGreen);
            GL.Scale(new Vector3(0.05f));
            // GL.Translate(-_world.WorldSize.X, -_world.WorldSize.X, -_world.WorldSize.X);
            //GL.Translate(0, 0, 0);
            _treeObj.Render();
            GL.PopMatrix();

            GL.Color3(Color.Cyan);

            foreach (var bird in _birds)
            {
                GL.PushMatrix();
                GL.Translate(-_world.WorldSize, -_world.WorldSize, -_world.WorldSize);
                GL.Translate(bird.Position.X, bird.Position.Y, bird.Position.Z);
                GL.Scale(new Vector3(2.0f));
                _birdObj.Render();
                GL.PopMatrix();
            }
        }
    }
}
