using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerScanner
{
    class MainScanner : Scanner
    {
        public MainScanner(string FilePath) : base(FilePath)
        {
        }

        public MainScanner(StringBuilder Code) : base(Code)
        {
        }

        protected override void StateStart(char c)
        {
            switch (c)
            {
                // Used to ignore existing tags
                case '<':
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideTag);
                    break;

                case ' ':
                case '\n':
                    this.CurrentSectionLength = 1;
                    this.EndAndReplaceCurrentSection("", true);
                    this.ChangeState(this.StateStart);
                    break;

                case '"':
                    this.CurrentSectionLength = 1;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideQuotation);
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    this.CurrentSectionLength = 1;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateNumber);
                    break;

                // Variable names
                default:
                    this.CurrentSectionLength = 1;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideVariableName);
                    break;
            }
        }

        private void StateInsideTag(char c)
        {
            switch (c)
            {
                case '>':
                    this.CurrentIndex++;
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideTag);
                    break;
            }
        }

        private void StateInsideQuotation(char c)
        {
            switch (c)
            {
                case '"':
                    this.CurrentSectionLength++;
                    string tag = "<string," + this.Code.ToString()
                        .Substring(this.CurrentIndex - this.CurrentSectionLength + 2, this.CurrentSectionLength - 2) + ">";
                    this.EndAndReplaceCurrentSection(tag, true);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideQuotation);
                    break;
            }
        }

        private void StateNumber(char c)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateNumber);
                    break;

                default:
                    this.CurrentSectionLength++;
                    string tag = "<int," + this.Code.ToString()
                        .Substring(this.CurrentIndex - this.CurrentSectionLength + 1, this.CurrentSectionLength - 1) + ">";
                    this.EndAndReplaceCurrentSection(tag, false);
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateInsideVariableName(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case '<':
                    this.CurrentSectionLength++;
                    string tag = "<var," + this.Code.ToString()
                        .Substring(this.CurrentIndex - this.CurrentSectionLength + 1, this.CurrentSectionLength - 1) + ">";
                    this.EndAndReplaceCurrentSection(tag, false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideVariableName);
                    break;
            }
        }
    }
}
