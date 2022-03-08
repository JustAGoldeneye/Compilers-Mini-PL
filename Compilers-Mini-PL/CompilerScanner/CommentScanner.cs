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
        //private int CurrentBlockCommentsOpen;
        public CommentScanner(string FilePath) : base(FilePath)
        {
            //this.CurrentBlockCommentsOpen = 0;
        }
        
        public CommentScanner(StringBuilder Code) : base(Code)
        {
            //this.CurrentBlockCommentsOpen = 0;
        }

        protected override void StateStart(char c)
        {
            //this.CurrentBlockCommentsOpen = 0;
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
                    //this.CurrentBlockCommentsOpen = 1;
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

                 /*case '/':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateSlashInsideBlockComment);
                    break;*/

                default:
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideBlockComment);
                    break;
            }
        }

        /*private void StateSlashInsideBlockComment(char c)
        {
            //Console.WriteLine(this.CurrentBlockCommentsOpen);
            switch (c)
            {
                case '*':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.CurrentBlockCommentsOpen++;
                    this.ChangeState(this.StateInsideBlockComment);
                    break;

                default:
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideBlockComment);
                    break;
            }
        }*/

        private void StateStarInsideBlockComment(char c)
        {
            //Console.WriteLine(this.CurrentBlockCommentsOpen);
            switch (c)
            {
                case '/':
                    /*this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    if (this.CurrentBlockCommentsOpen > 1)
                    {
                        this.CurrentBlockCommentsOpen--;
                        this.ChangeState(this.StateInsideBlockComment);
                    } else
                    {
                        this.CurrentBlockCommentsOpen = 0;
                        this.EndAndReplaceCurrentSection("", true);
                        this.ChangeState(this.StateStart);
                    }*/
                    this.CurrentSectionLength++; // Necessary also when removeCurrentChar (a personal note, please ignore)
                    //this.CurrentBlockCommentsOpen--;
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
