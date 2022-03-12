using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{
    class ParseTable
    {
        private Dictionary<ParseTableKey, TagType[]> Table;
        private Dictionary<TagType, List<TagType[]>> SeparateParseRules;
        private CFG Grammar;

        public ParseTable(CFG grammar)
        {
            this.Table = new Dictionary<ParseTableKey, TagType[]>();
            this.SeparateParseRules = new Dictionary<TagType, List<TagType[]>>();
            this.Grammar = grammar;
            this.Grammar.Construct();
        }

        public void ConstructSeparateParseRules()
        {
            TagType[] parents = this.Grammar.GetParents();
            for (int i = 0; i < parents.Length; i++)
            {
                TagType[] ruleTagsForParent = this.Grammar.GetParseRulesFor(parents[i]);
                this.SeparateParseRules.Add(parents[i], new List<TagType[]>());

                int ruleStart = 0;
                int j = 0;
                //Console.WriteLine("\n\n" + parents[i] + ":::");
                while (j < ruleTagsForParent.Length)
                {
                    //Console.Write("|||" + ruleTagsForParent[j] + "|||");
                    if (ruleTagsForParent[j] == TagType.CFGOR)
                    {
                        this.AddToSeparateParseRules(ruleTagsForParent, ruleStart, j, parents[i]);
                        ruleStart = j;
                    }
                    j++;
                    //Console.Write("\n");
                }
                this.AddToSeparateParseRules(ruleTagsForParent, ruleStart, j - 1, parents[i]);
            }
        }

        private void AddToSeparateParseRules(TagType[] ruleTags, int start, int end, TagType parentTag)
        {
            if (ruleTags[start] == TagType.CFGOR)
            {
                start++;
            }
            if (ruleTags[end] == TagType.CFGOR)
            {
                end--;
            }

            TagType[] rule = new TagType[end - start + 1];
            for (int i = start; i <= end; i++)
            {
                rule[i - start] = ruleTags[i];
                //Console.Write(rule[i - start] + " ");
            }
            if (this.SeparateParseRules.TryGetValue(parentTag, out List<TagType[]> ruleList))
            {
                ruleList.Add(rule);
                /*for (int i = 0; i < rule.Length; i++)
                {
                    Console.WriteLine(rule[i]);
                }
                Console.WriteLine("\n");*/
            }
            else
            {
                throw new Exception("No parse rule for the tag " + parentTag + " was found.");
            }
        }
        // Tee uus construct metodi ja muuta rajoitetumpi privateksi
        // Ignore taulua tehtäessä CFGEMPTY
        // poista kommentit parsetaulusta

        public void PrintParseTable()
        {
            foreach (KeyValuePair<TagType, List<TagType[]>> entry in this.SeparateParseRules)
            {
                Console.Write("\n\n" + entry.Key + "\n");
                for (int i = 0; i < entry.Value.Count; i++)
                {
                    Console.Write("\n");
                    for (int j = 0; j < entry.Value[i].Length; j++)
                    {
                        Console.Write(entry.Value[i][j] + " ");
                    }
                }
            }
        }
    }
}
