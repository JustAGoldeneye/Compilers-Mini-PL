using System;
using Compilers_Mini_PL.IO;
using Compilers_Mini_PL.CompilerScanner;

namespace Compilers_Mini_PL
{
    class Program
    {
        static void Main(string[] args)
        {
            Scanner sc = new Scanner(@"C:\Users\Eemeli\Documents\YLIOPISTO\Compilers\Compilers-Mini-PL\Math.MiniPL");
            Console.WriteLine(sc);
        }
    }
}
