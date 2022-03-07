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
        protected StringBuilder Code;
        protected int CurrentIndex;
        protected int CurrentSectionLength;

        public Scanner(string FilePath)
        {
            this.Code = new StringBuilder(System.IO.File.ReadAllText(FilePath));
            this.Code.Insert(0, '\n');
            this.Code.Append('\n');
            this.CurrentIndex = 0;
            this.CurrentSectionLength = 0;
        }

        public Scanner(StringBuilder Code)
        {
            this.Code = Code;
            this.CurrentIndex = 0;
            this.CurrentSectionLength = 0;
        }

        public void Run()
        {
            this.ResetPosition();
            this.StateStart(this.Code[this.CurrentIndex]);
        }


        protected void ResetPosition()
        {
            this.CurrentIndex = 0;
            this.CurrentSectionLength = 0;
        }
        
        public override string ToString()
        {
            return this.Code.ToString();
        }

        public StringBuilder ExportCode()
        {
            return new StringBuilder(this.ToString());
        }

        protected virtual void StateStart(char c)
        {
        }

        protected void EndAndReplaceCurrentSection(string replacingText, bool removeCurrentChar)
        {
            //Console.WriteLine(this + "\n");
            if (removeCurrentChar)
            {
                this.Code.Remove(this.CurrentIndex - this.CurrentSectionLength + 1, this.CurrentSectionLength);

            } else
            {
                this.Code.Remove(this.CurrentIndex - this.CurrentSectionLength + 1, this.CurrentSectionLength - 1);
            }

            this.Code.Insert(this.CurrentIndex - this.CurrentSectionLength + 1, replacingText);

            this.CurrentIndex = this.CurrentIndex - this.CurrentSectionLength + 1 + replacingText.Length;
            this.CurrentSectionLength = 0;
        }

        private bool IsAtTheEnd()
        {
            return this.CurrentIndex < this.Code.Length;
        }

        protected void ChangeState(Action<char> stateAction)
        {
            if (!IsAtTheEnd())
            {
                return;
            }
            stateAction(this.Code[this.CurrentIndex]);
        }
    }
}
