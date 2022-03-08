using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{
    class TagTextToObjectConverter
    {
        private readonly string Text;
        private List<Tag> Tags;
        private TagNameToEnumConverter Converter;

        public TagTextToObjectConverter(string text, bool isFilePath, TagNameToEnumConverter converter)
        {
            if (isFilePath)
            {
                this.Text = System.IO.File.ReadAllText(text);
            } else
            {
                this.Text = text;
            }
            this.Tags = new List<Tag>();
            this.Converter = converter;
        }

        public void Convert()
        {
            int sectionStartIndex = -1;
            string currentTagName = "";
            bool insideTag = false;
            for (int i = 0; i < this.Text.Length; i++)
            {
                if (!insideTag)
                {
                    if (this.Text[i].Equals('<'))
                    {
                        sectionStartIndex = i + 1;
                        insideTag = true;
                    }
                } else
                {
                    if (this.Text[i].Equals(','))
                    {
                        currentTagName = this.Text.Substring(sectionStartIndex, i - sectionStartIndex);
                        sectionStartIndex = i + 1;
                    } else if (this.Text[i].Equals('>'))
                    {
                        if (currentTagName.Equals(""))
                        {
                            this.SaveTag(this.Text.Substring(sectionStartIndex, i - sectionStartIndex), "");
                        } else
                        {
                            this.SaveTag(currentTagName, this.Text.Substring(sectionStartIndex, i - sectionStartIndex));
                        }
                        currentTagName = "";
                        insideTag = false;
                    }
                }
            }
        }

        private void SaveTag(string name, string data)
        {
            this.Tags.Add(new Tag(name, data, this.Converter));
        }

        public List<Tag> GetTags()
        {
            return this.Tags;
        }
    }
}
