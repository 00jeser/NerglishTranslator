using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NerglishTranslator
{
    public static class Static
    {
        public static List<string> en = new List<string>(File.ReadAllLines("en.txt"));
        public static List<string> ner = new List<string>(File.ReadAllLines("ner.txt"));
    }
}
