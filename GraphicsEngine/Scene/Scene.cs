﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Engine.Bird;
using Engine.World;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Utilities.Interfaces;
using Utilities.Models;
using Utilities.Shapes;
using Utilities.Text;

namespace GraphicsEngine.Scene
{
    public class Scene : IDisposable
    {
        private readonly GameWindow _scene;
        private readonly World _world;
        private readonly IList<IRenderable> _objects = new List<IRenderable>(); 

        private readonly Camera.Camera _camera = new Camera.Camera();
        private Vector3 _rotation = new Vector3(0, 0, 0);

        private readonly IDictionary<string, Text> _messages = new Dictionary<string, Text>();

        private readonly WorldCube _worldCube;

        public Scene(World world, IEnumerable<Bird> birds)
        {
            _world = world;

            birds.ToList().ForEach(bird => _objects.Add(new BirdModel(bird, _world.WorldSize)));

            var random = new Random();
            for (var i = 0; i < 20; i++)
            {
                _objects.Add(new TreeModel(new Vector3((float)random.NextDouble() * _world.WorldSize * 2, 0, (float)random.NextDouble() * _world.WorldSize * 2), _world.WorldSize));
            }

            _scene = new GameWindow(Convert.ToInt32(_world.WindowResolution.X), Convert.ToInt32(_world.WindowResolution.Y), new GraphicsMode(32, 32, 0, 24), "Bird Simulator");
            _scene.Load += Load;
            _scene.Resize += Resize;
            _scene.UpdateFrame += UpdateFrame;
            _scene.RenderFrame += RenderFrame;
            _scene.VSync = VSyncMode.On;

            _worldCube = new WorldCube(_world.WorldSize*2);

            //_messages.Add("MousePosition", new Text(new Size((int)_world.WindowResolution.X, (int)_world.WindowResolution.Y), new Size(480, 20), new Point(0, 0), "Mouse Position "));
            //_messages.Add("MouseInput", new Text(new Size((int)_world.WindowResolution.X, (int)_world.WindowResolution.Y), new Size(480, 20), new Point(0, 20), "Mouse Input"));
            _messages.Add("CameraPosition", new Text(new Size((int)_world.WindowResolution.X, (int)_world.WindowResolution.Y), new Size(480, 20), new Point(0, 40), "Camera Position"));
            //_messages.Add("CameraDirection", new Text(new Size((int)_world.WindowResolution.X, (int)_world.WindowResolution.Y), new Size(480, 20), new Point(0, 60), "Camera Direction"));
            //_messages.Add("CameraTarget", new Text(new Size((int)_world.WindowResolution.X, (int)_world.WindowResolution.Y), new Size(480, 20), new Point(0, 80), "Camera Target"));
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
            GL.ShadeModel(ShadingModel.Smooth);

            //GL.Enable(EnableCap.Lighting);
            //GL.Enable(EnableCap.Light0);
            //GL.Enable(EnableCap.ColorMaterial);
            //GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            //var ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };

            //var diffuseLight = new float[] { 80, 80, 80, 1.0f };
            //var specularLight = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            //var position = new float[] { -10, -10, -10, 10 };

            //GL.LightModel(LightModelParameter.LightModelAmbient, ambient);
            //GL.Light(LightName.Light0, LightParameter.Ambient, ambient);
            //GL.Light(LightName.Light0, LightParameter.Diffuse, diffuseLight);
            //GL.Light(LightName.Light0, LightParameter.Position, position);      
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
            _camera.SetTarget((_scene.Mouse.X - (_world.WindowResolution.X/2))/(_world.WindowResolution.X/2),
                (_scene.Mouse.Y - (_world.WindowResolution.Y/2))/(_world.WindowResolution.Y/2));
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

        private void UpdateMessages()
        {
            //_messages["MousePosition"].Content = String.Format("Mouse Positon: ({0}, {1})", _scene.Mouse.X, _scene.Mouse.Y);
            //_messages["MouseInput"].Content = String.Format("Mouse Input: ({0}, {1})",
            //    (_scene.Mouse.X - (_world.WindowResolution.X / 2)) / (_world.WindowResolution.X / 2) * 180 * -1,
            //    -(_scene.Mouse.Y - (_world.WindowResolution.Y / 2)) / (_world.WindowResolution.Y / 2) * 180 * -1);
            //_messages["MouseInput"].UpdateText();
            _messages["CameraPosition"].Content = String.Format("Camera Positon: ({0}, {1}, {2})", _camera.Position.X,
                _camera.Position.Y, _camera.Position.Z);
            _messages["CameraPosition"].UpdateText();
            //_messages["CameraDirection"].Content = String.Format("Camera Direction: ({0}, {1}, {2})", _camera.Direction.X,
            //    _camera.Direction.Y, _camera.Direction.Z);
            //_messages["CameraTarget"].Content = String.Format("Camera Target: ({0}, {1}, {2})", _camera.Target.X,
            //    _camera.Target.Y, _camera.Target.Z);
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
            _worldCube.Render();

            foreach (var message in _messages)
            {
                message.Value.Draw();
            }

            foreach (var obj in _objects)
            {
                GL.PushMatrix();
                GL.Translate(-_world.WorldSize, -_world.WorldSize, -_world.WorldSize);
                obj.Render();
                GL.PopMatrix();
            }
            UpdateMessages();
        }
    }
}
