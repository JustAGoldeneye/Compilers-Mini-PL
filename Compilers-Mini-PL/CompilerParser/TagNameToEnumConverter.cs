using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{
    class TagNameToEnumConverter
    {
        public readonly Dictionary<string, TagType> TagsToEnums;
        public TagNameToEnumConverter()
        {
            this.TagsToEnums = new Dictionary<string, TagType>();
            this.TagsToEnums.Add("var", TagType.VAR);
            this.TagsToEnums.Add("for", TagType.FOR);
            this.TagsToEnums.Add("end", TagType.END);
            this.TagsToEnums.Add("in", TagType.IN);
            this.TagsToEnums.Add("do", TagType.DO);
            this.TagsToEnums.Add("read", TagType.READ);
            this.TagsToEnums.Add("print", TagType.PRINT);
            this.TagsToEnums.Add("int", TagType.INT);
            this.TagsToEnums.Add("string", TagType.STRING);
            this.TagsToEnums.Add("bool", TagType.BOOL);
            this.TagsToEnums.Add("assert", TagType.ASSERT);
            this.TagsToEnums.Add("+", TagType.PLUS);
            this.TagsToEnums.Add("-", TagType.MINUS);
            this.TagsToEnums.Add("*", TagType.STAR);
            this.TagsToEnums.Add("/", TagType.SLASH);
            this.TagsToEnums.Add("<", TagType.LESSERTHAN);
            this.TagsToEnums.Add("=", TagType.EQUALS);
            this.TagsToEnums.Add("&", TagType.AMPERSAND);
            this.TagsToEnums.Add("!", TagType.EXCLAMATIONMARK);
            this.TagsToEnums.Add(":", TagType.COLON);
            this.TagsToEnums.Add(";", TagType.SEMICOLON);
            this.TagsToEnums.Add("(", TagType.LEFTBRACKET);
            this.TagsToEnums.Add(")", TagType.RIGHTBRACKET);
            this.TagsToEnums.Add(".", TagType.DOT);
            this.TagsToEnums.Add("..", TagType.DOUBLEDOT);
            this.TagsToEnums.Add(":=", TagType.ASSIGNEMNT);
            //this.TagsToEnums.Add("prog", TagType.PROG);
            this.TagsToEnums.Add("stmts", TagType.STMTS);
            this.TagsToEnums.Add("stmts'", TagType.STMTSX);
            this.TagsToEnums.Add("stmt", TagType.STMT);
            this.TagsToEnums.Add("stmt'", TagType.STMTX);
            this.TagsToEnums.Add("expr", TagType.EXPR);
            this.TagsToEnums.Add("expr'", TagType.EXPRX);
            this.TagsToEnums.Add("opnd", TagType.OPND);
            this.TagsToEnums.Add("op", TagType.OP);
            this.TagsToEnums.Add("unary_opnd", TagType.UNARYOPEND);
            this.TagsToEnums.Add("type", TagType.TYPE);
            this.TagsToEnums.Add("var_ident", TagType.VARIDENT);
            //this.TagsToEnums.Add("ident", TagType.IDENT);
            this.TagsToEnums.Add("reserved_keyword", TagType.RESERVEDKEYWORD);
            //this.TagsToEnums.Add("cfg_(", TagType.CFGPRIORITYSTART);
            //this.TagsToEnums.Add("cfg_)", TagType.CFGPRIORITYEND);
            //this.TagsToEnums.Add("cfg_|", TagType.CFGOR);
            //this.TagsToEnums.Add("cfg_$", TagType.CFGLINEEND);
            //this.TagsToEnums.Add("cfg__", TagType.CFGEMPTY);
        }

        public TagType Convert(string name)
        {
            TagType tagType;
            if (this.TagsToEnums.TryGetValue(name, out tagType))
            {
                return tagType;
            } else
            {
                throw new Exception("No TagType " + name + " exists.");
            }
        }
    }
}
