using FloEngineTK.Engine;
using FloEngineTK.Core;

namespace FloEngineTK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game testGame = new TestGame("DaveTheWizard", 1280, 720);
            testGame.Run();
        }
    }
}
