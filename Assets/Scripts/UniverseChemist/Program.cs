using System;
using System.Linq;

namespace chemist
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Game.New();
            game.Disassemble(game.GetFirstElementWithName("Water"));
            game.AddElementToWorkspace("Water");
            game.Combine(game.GetFirstElementWithName("Fire"), game.GetFirstElementWithName("Water"));

            game.Save("saveFile.xml");

            var g2 = Game.FromSaveFile("saveFile.xml");
        }
    }
}
