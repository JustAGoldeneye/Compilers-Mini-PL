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
        private Dictionary<TagType, TagType[]> FirstSets;
        private Dictionary<TagType, TagType[]> FollowSets;
        private CFGReader Reader;
        private bool Constructed;

        public CFG(CFGReader reader)
        {
            this.ParseRules = new Dictionary<TagType, TagType[]>();
            this.FirstSets = new Dictionary<TagType, TagType[]>();
            this.FollowSets = new Dictionary<TagType, TagType[]>();
            this.Reader = reader;
            this.Reader.Read();
            this.Constructed = false;
        }

        public void Construct()
        {
            if (this.Constructed)
            {
                return;
            }
            this.ConstructTagTypesDictionary(this.Reader.GetCFG(), this.ParseRules);
            this.ConstructTagTypesDictionary(this.Reader.GetFirstSets(), this.FirstSets);
            this.ConstructTagTypesDictionary(this.Reader.GetFollowSets(), this.FollowSets);
            this.Constructed = true;
        }

        private void ConstructTagTypesDictionary(List<TagType>[] unconverted, Dictionary<TagType, TagType[]> dictionary)
        {
            for (int i = 0; i < unconverted.Length; i++)
            {
                dictionary.Add(unconverted[i][0], unconverted[i].GetRange(1, unconverted[i].Count - 1).ToArray());
            }
        }

        public TagType[] GetParseRulesFor(TagType tag)
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

        public TagType[] GetParents()
        {
            return this.ParseRules.Keys.ToArray();
        }

        public TagType[] GetFirstTerminalsFor(TagType tagType)
        {
            if (this.FirstSets.TryGetValue(tagType, out TagType[] terminals))
            {
                return terminals;
            } else
            {
                throw new Exception("No first terminals for " + tagType + " were found.");
            }
        }

        public TagType[] GetFollowTerminals(TagType tagType)
        {
            if (this.FollowSets.TryGetValue(tagType, out TagType[] terminals))
            {
                return terminals;
            }
            else
            {
                throw new Exception("No follow terminals for " + tagType + " were found.");
            }
        }

        public CFGReader GetReader()
        {
            return this.Reader;
        }
    }
}
