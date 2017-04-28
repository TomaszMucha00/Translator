using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator_v_2._0
{
    class Element
    {
        public char[] TextFragment { get; set; }
        public bool SyntaxError { get; set; }
        public string DataType { get; set; }

        public Dictionary<string, string> SplitSignDict = new Dictionary<string, string>();

        public Element(char[] _TextFragment)
        {
            SplitSignDict.Add("+", "Add operator");
            SplitSignDict.Add("-", "Subtraction operator");
            SplitSignDict.Add("*", "Multiplication operator");
            SplitSignDict.Add("/", "Division operator");
            SplitSignDict.Add("(", "Opening bracket");
            SplitSignDict.Add(")", "Closing bracket");
            TextFragment = _TextFragment;
        }

    }
}
