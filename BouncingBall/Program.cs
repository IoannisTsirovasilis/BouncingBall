using System;

namespace BouncingBall
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BouncingBall())
                game.Run();
        }
    }
}
