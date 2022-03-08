using System;
using System.Text;
using Compilers_Mini_PL.IO;
using Compilers_Mini_PL.CompilerScanner;
using Compilers_Mini_PL.CompilerParser;


namespace Compilers_Mini_PL
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Eemeli\Documents\YLIOPISTO\Compilers\Compilers-Mini-PL\GiveCommented.MiniPL";

            Scanner commentScanner = new CommentScanner(filePath);
            Console.Write("------\nOriginal\n\n");
            Console.WriteLine(commentScanner);
            commentScanner.Run();
            Console.Write("\n------\nComment Scanner\n\n");
            Console.WriteLine(commentScanner);

            Scanner RKScanner = new KeywordScanner(commentScanner.ExportCode());
            RKScanner.Run();
            Console.Write("\n------\nReserved Keyword Scanner\n\n");
            Console.WriteLine(RKScanner);

            Scanner mainScanner = new MainScanner(RKScanner.ExportCode());
            mainScanner.Run();
            Console.Write("\n------\nMain Scanner\n\n");
            Console.WriteLine(mainScanner);

            /*StringBuilder testSB = new StringBuilder(System.IO.File.ReadAllText(filePath));
            //Console.WriteLine(testSB);
            for (int i = 0; i < testSB.Length; i++)
            {
                Console.WriteLine(testSB[i]);
            }
            /*testSB.Remove(23, 1);
            Console.WriteLine("\n+++++\n");
            Console.WriteLine(testSB);*/

            /*TagNameToEnumConverter converter = new TagNameToEnumConverter();
            Console.WriteLine(converter.Convert("+"));
            //Console.WriteLine(converter.Convert("apple"));
            Tag tag1 = new Tag("int", "999", converter);
            Tag tag2 = new Tag("-", converter);
            Console.WriteLine(tag1);
            Console.WriteLine(tag2);*/
        }
    }
}
