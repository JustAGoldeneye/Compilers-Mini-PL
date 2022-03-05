using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.IO
{
    class FileReader
    {
        private readonly string FileContents;
        private int CurrentIndex;

        public FileReader(string FilePath)
        {
            this.FileContents = System.IO.File.ReadAllText(FilePath);
            this.CurrentIndex = -1;
        }

        public char GetNextChar()
        {
            if (this.CurrentIndex < FileContents.Length - 1)
            {
                this.CurrentIndex++;
            } else {
                this.CurrentIndex = 0;
            }
            return FileContents[CurrentIndex];
        }

        public void Reset()
        {
            this.CurrentIndex = -1;
        }

        public override string ToString()
        {
            return this.FileContents;
        }
    }
}
