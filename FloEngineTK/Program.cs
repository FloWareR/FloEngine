using FloEngineTK.Implementations;
using FloEngineTK.Core;

namespace FloEngineTK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game testGame = new ObjectTest("DaveTheWizard", 1920, 1080);
            testGame.Run();
        }
    }
}
