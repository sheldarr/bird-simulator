using System;
using System.Collections.Generic;
using System.Drawing;
using BirdSimulator.Bird;
using BirdSimulator.World;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GraphicsEngine.Scene
{
    public class WorldScene : IDisposable
    {
        private GameWindow _scene;
        private World _world;
        private IList<Bird> _birds; 
        private Vector3 _rotation = new Vector3(0, 0 ,0);
        

        public WorldScene(World world, IList<Bird> birds)
        {
            _world = world;
            _birds = birds;
            _scene = new GameWindow(Convert.ToInt32(_world.WindowResolution.X), Convert.ToInt32(_world.WindowResolution.Y), GraphicsMode.Default, "Bird Simulator");
            _scene.Load += Load;
            _scene.Resize += Resize;
            _scene.UpdateFrame += UpdateFrame;
            _scene.RenderFrame += RenderFrame;
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
            _scene.VSync = VSyncMode.On;
            GL.Enable(EnableCap.DepthTest);
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
            if (_scene.Keyboard[Key.W])
            {
                _rotation.X += _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.S])
            {
                _rotation.X -= _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.A])
            {
                _rotation.Y += _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.D])
            {
                _rotation.Y -= _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.Q])
            {
                _rotation.Z += _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.E])
            {
                _rotation.Z -= _world.RotationAngle;
            }
            if (_scene.Keyboard[Key.O])
            {
                _world.WorldScale = Vector3.Subtract(_world.WorldScale, new Vector3(0.01f));
            }
            if (_scene.Keyboard[Key.P])
            {
                _world.WorldScale = Vector3.Add(_world.WorldScale, new Vector3(0.01f));
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


            var cameraPos = new Vector3(1, 0, 1);
            var cameraTarget = new Vector3(0, 0, 0);
            var cameraUp = new Vector3(0, 1, 0);
            var lookat = Matrix4.LookAt(cameraPos, cameraTarget, cameraUp);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.LoadMatrix(ref lookat);

            GL.MatrixMode(MatrixMode.Projection);
            //GL.Frustum(0, 0, 1024, 768, 0, 1000);

            GL.LoadIdentity();
        }

        private void PerformTransformations()
        {
            GL.Rotate(_rotation.X, 1, 0, 0);
            GL.Rotate(_rotation.Y, 0, 1, 0);
            GL.Rotate(_rotation.Z, 0, 0, 1);
            GL.Scale(_world.WorldScale);
        }

        private void RenderWorldEdges()
        {
            GL.Color3(Color.Yellow);

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(-0.75, 0.75, -0.75);
            GL.Vertex3(0.75, 0.75, -0.75);
            GL.Vertex3(0.75, -0.75, -0.75);
            GL.Vertex3(-0.75, -0.75, -0.75);
            GL.Vertex3(-0.75, 0.75, -0.75);

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(-0.75, 0.75, 0.75);
            GL.Vertex3(0.75, 0.75, 0.75);
            GL.Vertex3(0.75, -0.75, 0.75);
            GL.Vertex3(-0.75, -0.75, 0.75);
            GL.Vertex3(-0.75, 0.75, 0.75);

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(-0.75, 0.75, 0.75);
            GL.Vertex3(0.75, 0.75, 0.75);
            GL.Vertex3(0.75, 0.75, -0.75);
            GL.Vertex3(-0.75, 0.75, -0.75);
            GL.Vertex3(-0.75, 0.75, 0.75);

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(0.75, -0.75, 0.75);
            GL.Vertex3(0.75, 0.75, 0.75);
            GL.Vertex3(0.75, 0.75, -0.75);
            GL.Vertex3(0.75, -0.75, -0.75);
            GL.Vertex3(0.75, -0.75, 0.75);

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(-0.75, -0.75, 0.75);
            GL.Vertex3(-0.75, 0.75, 0.75);
            GL.Vertex3(-0.75, 0.75, -0.75);
            GL.Vertex3(-0.75, -0.75, -0.75);
            GL.Vertex3(-0.75, -0.75, 0.75);

            GL.End();

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(-0.75, -0.75, 0.75);
            GL.Vertex3(0.75, -0.75, 0.75);
            GL.Vertex3(0.75, -0.75, -0.75);
            GL.Vertex3(-0.75, -0.75, -0.75);

            GL.End();

            GL.Color3(Color.Red);

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(-640, 640, -640);
            GL.Vertex3(640, 640, -640);
            GL.Vertex3(640, -640, -640);
            GL.Vertex3(-640, -640, -640);
            GL.Vertex3(-640, 640, -640);

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(-640, 640, 640);
            GL.Vertex3(640, 640, 640);
            GL.Vertex3(640, -640, 640);
            GL.Vertex3(-640, -640, 640);
            GL.Vertex3(-640, 640, 640);

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(-640, 640, 640);
            GL.Vertex3(640, 640, 640);
            GL.Vertex3(640, 640, -640);
            GL.Vertex3(-640, 640, -640);
            GL.Vertex3(-640, 640, 640);

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(640, -640, 640);
            GL.Vertex3(640, 640, 640);
            GL.Vertex3(640, 640, -640);
            GL.Vertex3(640, -640, -640);
            GL.Vertex3(640, -640, 640);

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(-640, -640, 640);
            GL.Vertex3(-640, 640, 640);
            GL.Vertex3(-640, 640, -640);
            GL.Vertex3(-640, -640, -640);
            GL.Vertex3(-640, -640, 640);

            GL.End();

            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(-640, -640, 640);
            GL.Vertex3(640, -640, 640);
            GL.Vertex3(640, -640, -640);
            GL.Vertex3(-640, -640, -640);
            GL.Vertex3(-640, -640, 640);

            GL.End();
        }

        private void RenderObjects()
        {
            GL.Color3(Color.Cyan);

            foreach (var bird in _birds)
            {
                GL.Begin(PrimitiveType.Points);

                GL.Vertex3(bird.Position.X, bird.Position.Y, bird.Position.Z);

                GL.End();
            }
        }
    }
}
