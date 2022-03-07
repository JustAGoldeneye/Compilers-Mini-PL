using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.IO
{
    // THIS CLASS IS NOT CURRENTLY IN USE!!! and most likely will never be
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

        // Old code from ReservedKeywordScanner
        /*private void CheckKeywords()
        {
            if (this.CurrentMatchesKeyword("in"))
            {
                this.ApplyToKeyword("in");
                return;
            }
            if (this.CurrentMatchesKeyword("do"))
            {
                this.ApplyToKeyword("do");
                return;
            }
            if (this.CurrentMatchesKeyword("var"))
            {
                this.ApplyToKeyword("var");
                return;
            }
            if (this.CurrentMatchesKeyword("for"))
            {
                this.ApplyToKeyword("for");
                return;
            }
            if (this.CurrentMatchesKeyword("end"))
            {
                this.ApplyToKeyword("end");
                return;
            }
            if (this.CurrentMatchesKeyword("int"))
            {
                this.ApplyToKeyword("int");
                return;
            }
            if (this.CurrentMatchesKeyword("read"))
            {
                this.ApplyToKeyword("read");
                return;
            }
            if (this.CurrentMatchesKeyword("bool"))
            {
                this.ApplyToKeyword("bool");
                return;
            }
            if (this.CurrentMatchesKeyword("print"))
            {
                this.ApplyToKeyword("print");
                return;
            }
            if (this.CurrentMatchesKeyword("string"))
            {
                this.ApplyToKeyword("string");
                return;
            }
            if (this.CurrentMatchesKeyword("assert"))
            {
                this.ApplyToKeyword("assert");
                return;
            }
            this.CurrentIndex++;
            this.ChangeState(this.StateStart);
        }

        private bool CurrentMatchesKeyword(string keyword)
        {
            if (this.Code.Length - this.CurrentIndex >= keyword.Length)
            {
                for (int i = this.CurrentIndex; i < keyword.Length; i++)
                {
                    if (!this.Code[i].Equals(keyword[i]))
                    {
                        return false;
                    }
                }
                return true;
            } else
            {
                return false;
            }
        }

        private void ApplyToKeyword(string keyword)
        {
            this.CurrentIndex += keyword.Length;
            this.EndAndReplaceCurrentSection("<" + keyword + ">", true);
            this.ChangeState(this.StateStart);
        }*/
    }
}
