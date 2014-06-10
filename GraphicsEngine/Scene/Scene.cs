using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Engine.Anomaly;
using Engine.Bird;
using Engine.ConfigurationLoader;
using Engine.Strategies;
using Engine.World;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Utilities.Anomaly;
using Utilities.Interfaces;
using Utilities.Models;
using Utilities.Shapes;
using Utilities.Text;

namespace GraphicsEngine.Scene
{
    public class Scene : IDisposable
    {
        private readonly GraphicsSettings _graphicsSettings;
        private readonly GameWindow _scene;
        private readonly World _world;
        private readonly List<IRenderable> _objects = new List<IRenderable>();
        private readonly List<IRenderable> _anomalies = new List<IRenderable>(); 

        private readonly Camera.Camera _camera;
        private readonly Text _debugText;

        private readonly WorldCube _worldCube;

        public Scene(GraphicsSettings graphicsSettings, World world, IEnumerable<Bird> birds, IEnumerable<Anomaly> anomalies)
        {
            _graphicsSettings = graphicsSettings;
            _scene = new GameWindow(Convert.ToInt32(_graphicsSettings.WindowResolution.X), Convert.ToInt32(_graphicsSettings.WindowResolution.Y), new GraphicsMode(32, 32, 0, 0), "Bird Simulator");
            _scene.Load += Load;
            _scene.Resize += Resize;
            _scene.UpdateFrame += UpdateFrame;
            _scene.RenderFrame += RenderFrame;
            _scene.VSync = VSyncMode.On;

            _world = world;
            _worldCube = new WorldCube(_world.WorldSize * 2);

            _camera = new Camera.Camera(_graphicsSettings.CameraSpeed, _graphicsSettings.MaxVerticalAngle, _graphicsSettings.MaxHorizontalAngle,
                _graphicsSettings.CameraPosition, _graphicsSettings.CameraDirection, birds.First(bird => bird._strategy.GetType() == typeof(VectorFlight)));
           
            birds.ToList().ForEach(bird => _objects.Add(new BirdModel(bird, _world.WorldSize)));
            anomalies.ToList().ForEach(anomaly => _anomalies.Add(new AnomalyModel(anomaly)));
            //RandomizeTrees();

            _debugText = new Text(new Size((int)_graphicsSettings.WindowResolution.X, (int)_graphicsSettings.WindowResolution.Y), new Size(240, 40), new Point(0, 0), String.Empty);
        }

        private void RandomizeTrees()
        {
            var random = new Random();
            for (var i = 0; i < _world.NumberOfTrees; i++)
            {
                _objects.Add(new TreeModel(new Vector3((float) random.NextDouble()*_world.WorldSize*2, 0, 
                        (float) random.NextDouble()*_world.WorldSize*2), _world.WorldSize));
            }
        }

        public void StartRendering()
        {
            _scene.Run(_graphicsSettings.Fps);
        }

        public void Dispose()
        {
            _scene.Dispose();
        }

        private void Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.ShadeModel(ShadingModel.Smooth);      
        }

        private void Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, _scene.Width, _scene.Height);
        }

        private void UpdateFrame(object sender, FrameEventArgs e)
        {
            HandleMouse();
            HandleKeyboard();
        }

        private void HandleMouse()
        {
            _camera.SetTarget((_scene.Mouse.X - (_graphicsSettings.WindowResolution.X / 2)) / (_graphicsSettings.WindowResolution.X / 2),
                (_scene.Mouse.Y - (_graphicsSettings.WindowResolution.Y / 2)) / (_graphicsSettings.WindowResolution.Y / 2));
        }

        private void HandleKeyboard()
        {
            if (_scene.Keyboard[Key.Escape])
            {
                _scene.Exit();
            }
            if (_scene.Keyboard[Key.A])
            {
                _camera.MoveLeft();
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
                _camera.MoveRight();
            }
            if (_scene.Keyboard[Key.R])
            {
                _debugText.UpdateText();
            }
        }

        private void UpdateDebugMessage()
        {
            var debugMessage = new StringBuilder();
            debugMessage.AppendFormat("CP:({0:F}, {1:F}, {2:F})\n", _camera.Position.X,
                _camera.Position.Y, _camera.Position.Z);
            debugMessage.AppendFormat("CD:({0:F}, {1:F}, {2:F})\n", _camera.Direction.X,
                _camera.Direction.Y, _camera.Direction.Z);
            _debugText.Content = debugMessage.ToString();
        }

        private void RenderFrame(object sender, FrameEventArgs e)
        {
            InitNextFrame();
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

        private void RenderObjects()
        {

            GL.PushMatrix();
            GL.Translate(_camera.FollowingBird.Position.X - _world.WorldSize / 2, _camera.FollowingBird.Position.Y - _world.WorldSize / 2, _camera.FollowingBird.Position.Z - _world.WorldSize / 2);
            _worldCube.Render();
            GL.PopMatrix();
      
            _debugText.Render();

            foreach (var anomaly in _anomalies)
            {
                GL.PushMatrix();
                GL.Translate(-_world.WorldSize, -_world.WorldSize, -_world.WorldSize);
                anomaly.Render();
                GL.PopMatrix();
            }

            foreach (var obj in _objects)
            {
                GL.PushMatrix();
                GL.Translate(-_world.WorldSize, -_world.WorldSize, -_world.WorldSize);
                obj.Render();
                GL.PopMatrix();
            }
            UpdateDebugMessage();
        }
    }
}
