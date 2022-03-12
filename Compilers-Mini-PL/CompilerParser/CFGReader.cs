using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{
    class CFGReader
    {
        private List<TagType>[] CFG;
        private List<TagType>[] FirstSets;
        private List<TagType>[] FollowSets;
        private TagNameToEnumConverter Converter;
        private bool AlreadyRead;

        public CFGReader(TagNameToEnumConverter converter)
        {
            this.CFG = new List<TagType>[10];
            this.FirstSets = new List<TagType>[10];
            this.FollowSets = new List<TagType>[7];
            this.Converter = converter;
            this.AlreadyRead = false;

            int i = 0;
            while (i < this.CFG.Length)
            {
                this.CFG[i] = new List<TagType>();
                this.FirstSets[i] = new List<TagType>();
                if (i < this.FollowSets.Length)
                {
                    this.FollowSets[i] = new List<TagType>();
                }
                i++;
            }
        }

        public void Read()
        {
            if (!this.AlreadyRead)
            {
                this.ReadCFG();
                this.ReadFirstSets();
                this.ReadFollowSets();
                this.AlreadyRead = true;
            }
        }

        private void ReadCFG()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\Eemeli\Documents\YLIOPISTO\Compilers\Compilers-Mini-PL\MiniPL-CFG.txt");

            int groupIndex = 0;

            bool insideTag = false;
            int tagContentLength = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (!insideTag)
                {
                    if (text[i].Equals(';'))
                    {
                        groupIndex++;
                    }
                    else if (text[i].Equals('_'))
                    {
                        this.CFG[groupIndex].Add(TagType.CFGEMPTY);
                    }
                    else if (text[i].Equals('|'))
                    {
                        this.CFG[groupIndex].Add(TagType.CFGOR);
                    }
                    else if (text[i].Equals('('))
                    {
                        this.CFG[groupIndex].Add(TagType.CFGPRIORITYSTART);
                    }
                    else if (text[i].Equals(')'))
                    {
                        this.CFG[groupIndex].Add(TagType.CFGPRIORITYEND);
                    }
                    else if (text[i].Equals('<') || text[i].Equals('"'))
                    {
                        tagContentLength = 0;
                        insideTag = true;
                    }

                } else {
                    if (text[i].Equals('>') || text[i].Equals('"'))
                    {
                        this.CFG[groupIndex].Add(this.Converter.Convert(text.Substring(i - tagContentLength, tagContentLength)));
                        insideTag = false;
                    } else
                    {
                        tagContentLength++;
                    }
                }
            }
        }

        private void ReadFirstSets()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\Eemeli\Documents\YLIOPISTO\Compilers\Compilers-Mini-PL\MiniPL-FirstSets.txt");
            this.ReadSet(text, this.FirstSets);
        }

        private void ReadFollowSets()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\Eemeli\Documents\YLIOPISTO\Compilers\Compilers-Mini-PL\MiniPL-FollowSets .txt");
            this.ReadSet(text, this.FollowSets);
        }

        private void ReadSet(string text, List<TagType>[] target)
        {
            int groupIndex = 0;

            bool insideTag = true;
            int tagContentLength = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (!insideTag)
                {
                    if (text[i].Equals('\n'))
                    {
                        groupIndex++;
                        tagContentLength = 0;
                        insideTag = true;
                    }
                    else if (text[i].Equals(' '))
                    {
                        tagContentLength = 0;
                        insideTag = true;
                    }

                }
                else
                {
                    if (text[i].Equals(','))
                    {
                        string tag = text.Substring(i - tagContentLength, tagContentLength);
                        if (tag.Equals("$"))
                        {
                            target[groupIndex].Add(TagType.CFGLINEEND);

                        } else if (tag.Equals("_"))
                        {
                            target[groupIndex].Add(TagType.CFGEMPTY);

                        } else
                        {
                            target[groupIndex].Add(this.Converter.Convert(tag));
                        }

                        insideTag = false;
                    }
                    else
                    {
                        tagContentLength++;
                    }
                }
            }
        }

        public List<TagType>[] GetCFG()
        {
            return this.CFG;
        }

        public List<TagType>[] GetFirstSets()
        {
            return this.FirstSets;
        }

        public List<TagType>[] GetFollowSets()
        {
            return this.FollowSets;
        }

    }
}
