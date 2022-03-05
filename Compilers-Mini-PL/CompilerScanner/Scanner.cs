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
        protected FileReader Reader;
        protected StringBuilder Code;

        public Scanner(String FilePath)
        {
            this.Reader = new FileReader(FilePath);
            this.Code = new StringBuilder(this.Reader.ToString());
        }

        public Scanner(StringBuilder Code, FileReader Reader)
        {
            this.Reader = Reader;
            this.Reader.Reset();
            this.Code = Code;
        }

        public virtual void ScanFile()
        {
            Scanner commentScanner = new CommentScanner(this.Code, this.Reader);
            commentScanner.ScanFile();

            // Scan with both scanners
        }

        public override string ToString()
        {
            return this.Code.ToString();
        }
    }
}
