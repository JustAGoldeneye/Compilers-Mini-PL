using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{

    class CFG
    {
        private Dictionary<TagType, TagType[]> ParseRules;
        private CFGReader Reader;
        public CFG(CFGReader reader)
        {
            this.ParseRules = new Dictionary<TagType, TagType[]>();
            this.Reader = reader;
            this.Reader.Read();
        }

        public void Construct()
        {
            List<TagType>[] unconvertedRules = this.Reader.GetCFG();
            for (int i = 0; i < unconvertedRules.Length; i++)
            {
                this.ParseRules.Add(unconvertedRules[i][0], unconvertedRules[i].GetRange(1, unconvertedRules[i].Count - 1).ToArray());
            }
        }

        public TagType[] GetParseRule(TagType tag)
        {
            TagType[] parseRule;
            if (this.ParseRules.TryGetValue(tag, out parseRule))
            {
                return parseRule;
            } else
            {
                throw new Exception("No parse rule for the tag " + tag + " exists.");
            }
        }

        public TagType[] GetNonTerminals()
        {
            return this.ParseRules.Keys.ToArray();
        }
    }
}
