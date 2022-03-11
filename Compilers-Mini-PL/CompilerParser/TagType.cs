using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers_Mini_PL.CompilerParser
{
    enum TagType
    {
        VAR,
        FOR,
        END,
        IN,
        DO,
        READ,
        PRINT,
        INT,
        STRING,
        BOOL,
        ASSERT,
        PLUS,
        MINUS,
        STAR,
        SLASH,
        LESSERTHAN,
        EQUALS,
        AMPERSAND,
        EXCLAMATIONMARK,
        COLON,
        SEMICOLON,
        LEFTBRACKET,
        RIGHTBRACKET,
        DOT,
        DOUBLEDOT,
        ASSIGNEMNT,
        //PROG, // use stmts instead
        STMTS,
        STMTSX,
        STMT,
        STMTX,
        EXPR,
        EXPRX,
        OPND,
        OP,
        UNARYOPEND,
        TYPE,
        VARIDENT,
        //IDENT, // use varindent instead
        RESERVEDKEYWORD,
        CFGPRIORITYSTART,
        CFGPRIORITYEND,
        CFGOR,
        CFGLINEEND,
        CFGEMPTY
    }
}
