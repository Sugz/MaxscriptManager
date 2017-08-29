using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxscriptManager.Src
{
    public static class MConstants
    {
        public static readonly string InvalidPath = "The path is not valid.";
        public static readonly string[] ComDelimiters = { "--", "/*", "*/" };
        public static readonly string[] ClassDef = { "struct", "rollout", "fn", "function", "attributes", "macroscript", "parameters" };
        public static readonly string[] BlueWords = { "global", "local", "if", "then", "else", "do", "as", "and", "not", "while", "for", "to", "return", "try", "catch" };
        public static readonly string[] LightBlueWords = { "undefined", "true", "false"  };

    }
}
