using System;
using System.Drawing;
using BirdSimulator.World;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GraphicsEngine.Scene
{
    public class WorldScene : IDisposable
    {
        private GameWindow _scene = new GameWindow(1024, 768);
        private World _world;
        private WorldConfiguration _worldConfiguration;
        private Vector3 _rotation = new Vector3(0, 0 ,0);
        

        public WorldScene(World world, WorldConfiguration worldConfiguration)
        {
            _world = world;
            _worldConfiguration = worldConfiguration;
            _scene.Load += Load;
            _scene.Resize += Resize;
            _scene.UpdateFrame += UpdateFrame;
            _scene.RenderFrame += RenderFrame;
        }

        public void StartRendering()
        {
            _scene.Run(_worldConfiguration.RenderFps);
        }

        public void Dispose()
        {
            _scene.Dispose();
        }

        private void Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            _scene.VSync = VSyncMode.On;
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
                _rotation.X += _worldConfiguration.RotationAngle;
            }
            if (_scene.Keyboard[Key.S])
            {
                _rotation.X -= _worldConfiguration.RotationAngle;
            }
            if (_scene.Keyboard[Key.A])
            {
                _rotation.Y += _worldConfiguration.RotationAngle;
            }
            if (_scene.Keyboard[Key.D])
            {
                _rotation.Y -= _worldConfiguration.RotationAngle;
            }
            if (_scene.Keyboard[Key.Q])
            {
                _rotation.Z += _worldConfiguration.RotationAngle;
            }
            if (_scene.Keyboard[Key.E])
            {
                _rotation.Z -= _worldConfiguration.RotationAngle;
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
            
        }

        private void PerformTransformations()
        {
            GL.Rotate(_rotation.X, 1, 0, 0);
            GL.Rotate(_rotation.Y, 0, 1, 0);
            GL.Rotate(_rotation.Z, 0, 0, 1);
            GL.Scale(_worldConfiguration.WorldScale);
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
        }

        private void RenderObjects()
        {
            GL.Color3(Color.Yellow);

            foreach (var bird in _world.Birds)
            {
                GL.Begin(PrimitiveType.Points);

                GL.Vertex3(bird.Position.X, bird.Position.Y, bird.Position.Z);

                GL.End();
            }
        }
    }
}
