using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerScanner
{
    class ReservedKeywordScanner : Scanner
    {
        public ReservedKeywordScanner(string FilePath) : base(FilePath)
        {
        }

        public ReservedKeywordScanner(StringBuilder Code) : base(Code)
        {
        }

        protected override void StateStart(char c)
        {
            switch (c)
            {
                // Used to ignore strings
                case '"':
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideBrackets);
                    break;

                case '+':
                    this.HandleOperator('+');
                    break;

                case '-':
                    this.HandleOperator('-');
                    break;

                case '*':
                    this.HandleOperator('*');
                    break;

                case '/':
                    this.HandleOperator('/');
                    break;

                case '<':
                    this.HandleOperator('<');
                    break;

                case '=':
                    this.HandleOperator('=');
                    break;

                case '&':
                    this.HandleOperator('&');
                    break;

                case '!':
                    this.HandleOperator('!');
                    break;

                case ';':
                    this.CurrentSectionLength = 1;
                    this.EndAndReplaceCurrentSection("<;>", true);
                    this.ChangeState(this.StateStart);
                    break;

                case '(':
                    this.CurrentSectionLength = 1;
                    this.EndAndReplaceCurrentSection("<(>", true);
                    this.ChangeState(this.StateStart);
                    break;

                case ')':
                    this.CurrentSectionLength = 1;
                    this.EndAndReplaceCurrentSection("<)>", true);
                    this.ChangeState(this.StateStart);
                    break;

                case ':':
                    this.CurrentSectionLength = 1;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateColon);
                    break;

                case '.':
                    this.CurrentSectionLength = 1;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateDot);
                    break;

                case ' ':
                case '\n':
                    this.CurrentSectionLength = 0;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateWhiteSpace);
                    break;


                default:
                    this.CurrentIndex++;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateInsideBrackets(char c)
        {
            switch (c)
            {
                case '"':
                    this.CurrentIndex++;
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentIndex++;
                    this.ChangeState(this.StateInsideBrackets);
                    break;
            }
        }

        private void HandleOperator(char o)
        {
            this.CurrentSectionLength = 1;
            this.EndAndReplaceCurrentSection("<op," + o + ">", true);
            this.ChangeState(this.StateStart);
        }

        private void StateColon(char c)
        {
            switch (c)
            {
                case '=':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<:=>", true);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<:>", false);
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateDot(char c)
        {
            switch (c)
            {
                case '.':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<..>", true);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateWhiteSpace(char c)
        {
            switch (c)
            {
                case 'i':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateI);
                    break;

                case 'd':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateD);
                    break;

                case 'v':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateV);
                    break;

                case 'f':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateF);
                    break;

                case 'e':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateE);
                    break;

                case 'r':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateR);
                    break;

                case 'b':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateB);
                    break;

                case 'p':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateP);
                    break;
                case 's':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateS);
                    break;

                case 'a':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateA);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateI(char c)
        {
            switch (c)
            {
                case 'n':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateIN);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateIN(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<in>", false);
                    this.ChangeState(this.StateStart);
                    break;

                case 't':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateINT);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateINT(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<type,int>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateD(char c)
        {
            switch (c)
            {
                case 'o':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateDO);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateDO(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<do>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateV(char c)
        {
            switch (c)
            {
                case 'a':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateVA);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateVA(char c)
        {
            switch (c)
            {
                case 'r':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateVAR);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateVAR(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<var_ident>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateF(char c)
        {
            switch (c)
            {
                case 'o':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateFO);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateFO(char c)
        {
            switch (c)
            {
                case 'r':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateFOR);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateFOR(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<for>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateE(char c)
        {
            switch (c)
            {
                case 'n':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateEN);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateEN(char c)
        {
            switch (c)
            {
                case 'd':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateEND);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateEND(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<end>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateR(char c)
        {
            switch (c)
            {
                case 'e':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateRE);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateRE(char c)
        {
            switch (c)
            {
                case 'a':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateREA);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateREA(char c)
        {
            switch (c)
            {
                case 'd':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateREAD);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateREAD(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<read>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateB(char c)
        {
            switch (c)
            {
                case 'o':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateBO);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateBO(char c)
        {
            switch (c)
            {
                case 'o':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateBOO);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateBOO(char c)
        {
            switch (c)
            {
                case 'l':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateBOOL);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateBOOL(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<type,bool>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateP(char c)
        {
            switch (c)
            {
                case 'r':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StatePR);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StatePR(char c)
        {
            switch (c)
            {
                case 'i':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StatePRI);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StatePRI(char c)
        {
            switch (c)
            {
                case 'n':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StatePRIN);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StatePRIN(char c)
        {
            switch (c)
            {
                case 't':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StatePRINT);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StatePRINT(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<print>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateS(char c)
        {
            switch (c)
            {
                case 't':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateST);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateST(char c)
        {
            switch (c)
            {
                case 'r':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateSTR);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateSTR(char c)
        {
            switch (c)
            {
                case 'i':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateSTRI);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateSTRI(char c)
        {
            switch (c)
            {
                case 'n':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateSTRIN);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateSTRIN(char c)
        {
            switch (c)
            {
                case 'g':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateSTRING);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateSTRING(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<type,string>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateA(char c)
        {
            switch (c)
            {
                case 's':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateAS);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateAS(char c)
        {
            switch (c)
            {
                case 's':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateASS);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateASS(char c)
        {
            switch (c)
            {
                case 'e':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateASSE);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateASSE(char c)
        {
            switch (c)
            {
                case 'r':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateASSER);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateASSER(char c)
        {
            switch (c)
            {
                case 't':
                    this.CurrentSectionLength++;
                    this.CurrentIndex++;
                    this.ChangeState(this.StateASSERT);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }

        private void StateASSERT(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case ';':
                case '<':
                case '(':
                case '[':
                    this.CurrentSectionLength++;
                    this.EndAndReplaceCurrentSection("<assert>", false);
                    this.ChangeState(this.StateStart);
                    break;

                default:
                    this.CurrentSectionLength = 0;
                    this.ChangeState(this.StateStart);
                    break;
            }
        }
    }
}
