using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{
    class ParseRule // Not in use right now
    {
        public TagType Parent { get; set; }
        public List<TagType> Rule { get; set; }

    public ParseRule(TagType parent)
        {
            this.Parent = parent;
            this.Rule = new List<TagType>();
        }

        public void AddToRule(TagType addition)
        {
            this.Rule.Add(addition);
        }

        public override bool Equals(object obj)
        {
            return obj is ParseRule rule &&
                   Parent == rule.Parent;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Parent);
        }
    }
}
