using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{
    class Tag
    {
        private readonly TagType Name;
        private readonly string Data;

        public Tag(TagType name)
        {
            this.Name = name;
            this.Data = "";
        }

        public Tag(TagType name, string data)
        {
            this.Name = name;
            this.Data = data;
        }

        public Tag(string name, TagNameToEnumConverter converter)
        {
            this.Name = converter.Convert(name);
            this.Data = "";
        }

        public Tag(string name, string data, TagNameToEnumConverter converter)
        {
            this.Name = converter.Convert(name);
            this.Data = data;
        }

        public TagType GetName()
        {
            return this.Name;
        }

        public string GetData()
        {
            return this.Data;
        }

        public override string ToString()
        {
            if (this.Data.Equals(""))
            {
                return this.Name.ToString();

            }
            return this.Name + ": " + this.Data;
        }
    }
}
