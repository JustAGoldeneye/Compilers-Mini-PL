using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilers_Mini_PL.IO;

namespace Compilers_Mini_PL.CompilerScanner
{
    class CommentScanner : Scanner
    {
        private int CurrentIndex;

        public CommentScanner(string FilePath) : base(FilePath)
        {
            this.CurrentIndex = 0;
        }
        
        public CommentScanner(StringBuilder Code, FileReader Reader) : base(Code, Reader)
        {
            this.CurrentIndex = 0;
        }

        public override void ScanFile()
        {
            /*int i = 0;
            while (i < base.Code.Length)
            {
                char currentChar = base.Reader.GetNextChar();
                switch (currentChar)
                {
                    case '/':
                        Console.WriteLine("/");
                        break;

                    default:
                        break;

                }

                i++; // make proper increase for every condition OR keep this and make it add extra i for every character removal (save the original StringBuilder) OR change string builder every step to avoid chaging this (necessary in any case?)
            }*/

            this.CurrentIndex = 0;
            this.StateStart(base.Code[this.CurrentIndex]);
        }

        private bool IsAtTheEnd()
        {
            return this.CurrentIndex < base.Code.Length;
        }

        // Impement as its own class?
        private void ChangeState(Action<char> stateAction)
        {
            this.CurrentIndex++;
            if (!IsAtTheEnd())
            {
                return;
            }
            stateAction(base.Code[this.CurrentIndex]);
        }

        private void StateStart(char c)
        {
            switch (c)
            {
                case '/':
                    Console.WriteLine("/");
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void State1(char c)
        {

        }

        // just use the count opening commments solutions to advance in the projet
    }
}
