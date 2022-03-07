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
                case ' ':
                case '\n':
                    this.CurrentSectionLength = 1;
                    this.EndAndReplaceCurrentSection("", true);
                    this.ChangeState(this.StateStart);
                    break;

                // case

                default:
                    this.CurrentIndex++;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }
    }
}
