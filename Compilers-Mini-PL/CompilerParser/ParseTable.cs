using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{
    class ParseTable
    {
        private Dictionary<ParseTableKey, TagType> Table;
        private CFGReader Reader;

        public ParseTable(CFGReader reader)
        {
            this.Table = new Dictionary<ParseTableKey, TagType>();
            this.Reader = reader;
            this.Reader.Read();
        }

        public void Construct()
        {

        }
    }
}
