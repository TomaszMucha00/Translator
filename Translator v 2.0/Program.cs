using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator_v_2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in Core.IndexsOfSplitSignF(Core.IndexsOfSplitSign))
            {
                Console.WriteLine(item);
            }

            int c=1;

            foreach (var item in Data.textToTranslate)
            {
                Console.WriteLine(c.ToString() + " " + item.ToString());
                c++;
            }

            Core.TextToSplitCharTab();
            Core.DeleteEmptyCharTab(ref Core.TabAfterSignsSplit);
            Core.SetTabOfElement(ref Core.TabOfElement);
            

            foreach (var item in Core.TabOfElement)
            {
                Console.WriteLine("??" + item.TextFragment + "\t  " + item.DataType + "\t \t " + item.SyntaxError);
            }


        }
    }

}
