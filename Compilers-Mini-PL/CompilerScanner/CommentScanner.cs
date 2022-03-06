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

        public override void Run()
        {
            this.CurrentIndex = 0;
            this.CurrentSectionLength = 0;
            this.StateStart(this.Code[this.CurrentIndex]);
        }

        private void StateStart(char c)
        {
            switch (c)
            {
                case '/':
                    this.CurrentSectionLength++;
                    this.ChangeState(this.StateSlash);
                    break;

                default:
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
                    this.ChangeState(this.StateInsideLineComment);
                    break;

                case '*':
                    this.CurrentSectionLength++;
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
                    this.EndAndReplaceCurrentSection("", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength++;
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
                    this.ChangeState(this.StateStarInsideBlockComment);
                    break;

                default:
                    this.CurrentSectionLength++;
                    this.ChangeState(this.StateInsideBlockComment);
                    break;
            }
        }

        private void StateStarInsideBlockComment(char c)
        {
            switch (c)
            {
                case '/':
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
