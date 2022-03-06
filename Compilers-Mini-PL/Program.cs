using System;
using Compilers_Mini_PL.IO;
using Compilers_Mini_PL.CompilerScanner;

namespace Compilers_Mini_PL
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Eemeli\Documents\YLIOPISTO\Compilers\Compilers-Mini-PL\GiveCommented.MiniPL";

            Scanner commentScanner = new CommentScanner(filePath);
            //Console.WriteLine(commentScanner);
            commentScanner.Run();
            Console.Write("\n");
            Console.WriteLine(commentScanner);
        }
    }
}
