using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilers_Mini_PL.IO;

namespace Compilers_Mini_PL.CompilerScanner
{
    class Scanner
    {
        private FileReader Reader;
        private StringBuilder Code;

        public Scanner(String FilePath)
        {
            this.Reader = new FileReader(FilePath);
            this.Code = new StringBuilder(this.Reader.ToString());
        }

        public override string ToString()
        {
            return this.Code.ToString();
        }
    }
}
