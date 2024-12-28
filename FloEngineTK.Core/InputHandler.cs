using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FloEngineTK.Core.Input
{
    public static class InputHandler
    {
        private static HashSet<Keys> _pressedKeys = new HashSet<Keys>();
        private static HashSet<Keys> _releasedKeys = new HashSet<Keys>();

        public static void Initialize(GameWindow gameWindow)
        {
            gameWindow.KeyDown += OnKeyDown;
            gameWindow.KeyUp += OnKeyUp;
        }

        private static void OnKeyDown(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Add(args.Key);
            _releasedKeys.Remove(args.Key);
        }

        private static void OnKeyUp(KeyboardKeyEventArgs args)
        {
            _releasedKeys.Add(args.Key);
            _pressedKeys.Remove(args.Key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return _pressedKeys.Contains(key);
        }
        public static bool IsKeyUp(Keys key)
        {
            return _releasedKeys.Contains(key);
        }

        public static IEnumerable<Keys> GetPressedKeys()
        {
            return _pressedKeys; 
        }

    }
}
