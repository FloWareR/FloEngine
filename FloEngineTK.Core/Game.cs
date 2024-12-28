using FloEngineTK.Core.Input;
using FloEngineTK.Core.Management;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Threading.Channels;

namespace FloEngineTK.Core
{
    public abstract class Game
    {
        protected string WindowTitle { get; set; }
        protected uint IntialWindowWidth { get; set; }
        protected uint IntialWindowHeight { get; set; }

        private GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
        private NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;

        public Game(string windowTitle, uint intialWindowWidth, uint intialWindowHeight)
        {
            WindowTitle = windowTitle;
            IntialWindowWidth = intialWindowWidth;
            IntialWindowHeight = intialWindowHeight;
            _nativeWindowSettings.ClientSize = new Vector2i((int)intialWindowWidth, (int)intialWindowHeight);
            _nativeWindowSettings.Title = windowTitle;
            _nativeWindowSettings.API = ContextAPI.OpenGL;

            _gameWindowSettings.UpdateFrequency = 60.0;   
        }

        public void Run()
        {
            using var gameWindow = DisplayManager.Instance.CreateGameWindow(_gameWindowSettings, _nativeWindowSettings);

            InputHandler.Initialize(gameWindow);

            Time time = new();
            gameWindow.Load += LoadContent;
            gameWindow.UpdateFrame += (FrameEventArgs eventArgs) =>
            {
                double gameTime = eventArgs.Time;
                time.deltaTime = TimeSpan.FromSeconds(gameTime);
                time.time += TimeSpan.FromSeconds(gameTime);
                Update(time);
            };
            gameWindow.RenderFrame += (FrameEventArgs eventArgs) =>
            {
                Render(time);
                gameWindow.SwapBuffers();
            };

            gameWindow.Resize += (ResizeEventArgs) =>
            {
                GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y);
            };
            Initialize(gameWindow);
            gameWindow.Run();
        }

        protected abstract void Initialize(GameWindow gameWindow);
        protected abstract void LoadContent();
        protected abstract void Update(Time time);
        protected abstract void Render(Time time);
    }
}
