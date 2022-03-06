using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilers_Mini_PL.IO;

namespace Compilers_Mini_PL.CompilerScanner
{
    abstract class Scanner
    {
        protected StringBuilder Code { get; }
        protected int CurrentIndex;
        protected int CurrentSectionLength;

        public Scanner(string FilePath)
        {
            this.Code = new StringBuilder(System.IO.File.ReadAllText(FilePath));
            this.CurrentIndex = 0;
            this.CurrentSectionLength = 0;
        }

        public Scanner(StringBuilder Code)
        {
            this.Code = Code;
            this.CurrentIndex = 0;
            this.CurrentSectionLength = 0;
        }

        public virtual void Run()
        {
        }


        public override string ToString()
        {
            return this.Code.ToString();
        }

        protected void EndAndReplaceCurrentSection(string replacingText, bool removeCurrentChar)
        {
            //Console.WriteLine(this + "\n");
            if (removeCurrentChar)
            {
                this.Code.Remove(this.CurrentIndex - this.CurrentSectionLength, this.CurrentSectionLength + 1);
            } else
            {
                this.Code.Remove(this.CurrentIndex - this.CurrentSectionLength, this.CurrentSectionLength);
            }
            this.CurrentIndex -= this.CurrentSectionLength;
            this.CurrentSectionLength = 0;
        }

        private bool IsAtTheEnd()
        {
            return this.CurrentIndex < this.Code.Length;
        }

        protected void ChangeState(Action<char> stateAction)
        {
            this.CurrentIndex++;
            if (!IsAtTheEnd())
            {
                return;
            }
            stateAction(this.Code[this.CurrentIndex]);
        }
    }
}
