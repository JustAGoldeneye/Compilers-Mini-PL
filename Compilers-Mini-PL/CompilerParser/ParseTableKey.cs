using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{
    class ParseTableKey
    {
        public TagType NonTerminal { get; set; }
        public TagType Terminal { get; set; }

        public ParseTableKey(TagType nonTerminal, TagType terminal)
        {
            this.NonTerminal = nonTerminal;
            this.Terminal = terminal;
        }

        public override bool Equals(object obj)
        {
            return obj is ParseTableKey key &&
                   NonTerminal == key.NonTerminal &&
                   Terminal == key.Terminal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NonTerminal, Terminal);
        }
    }
}
