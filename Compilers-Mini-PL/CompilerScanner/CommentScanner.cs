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
        public CommentScanner(string FilePath) : base(FilePath)
        {
        }
        
        public CommentScanner(StringBuilder Code) : base(Code)
        {
        }

        protected override void StateStart(char c)
        {
            switch (c)
            {
                case '/':
                    this.CurrentSectionLength = 1;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateSlash);
                    break;

                default:
                    this.CurrentIndex++;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateSlash(char c)
        {
            switch (c)
            {
                case '/':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideLineComment);
                    break;

                case '*':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideBlockComment);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateInsideLineComment(char c)
        {
            switch (c)
            {
                case '\n':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideLineComment);
                    break;
            }
        }

        private void StateInsideBlockComment(char c)
        {
            switch (c)
            {
                case '*':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateStarInsideBlockComment);
                    break;

                default:
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideBlockComment);
                    break;
            }
        }

        private void StateStarInsideBlockComment(char c)
        {
            switch (c)
            {
                case '/':
                    this.CurrentSectionLength++; // Necessary also when removeCurrentChar (a personal note, please ignore)
                    this.EndAndReplaceCurrentSection("", true);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength++;
                    this.ChangeState(this.StateInsideBlockComment);
                    break;
            }
        }
    }
}
