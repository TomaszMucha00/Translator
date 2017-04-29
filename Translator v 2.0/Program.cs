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
                //Console.WriteLine(item);
            }

            int c=1;

            foreach (var item in Data.textToTranslate)
            {
               // Console.WriteLine(c.ToString() + " " + item.ToString());
                c++;
            }

            Core.TextToSplitCharTab();
            Core.DeleteEmptyCharTab(ref Core.TabAfterSignsSplit);
            Core.SetTabOfElement(ref Core.TabOfElement);

            Core.IsIdentyficator(ref Core.TabOfElement);
            Core.IsFullId(ref Core.TabOfElement);
            Core.DoubleOrInt(ref Core.TabOfElement);
            Core.SplitNumberAndId(ref Core.TabOfElement);
            Core.ErrorControl(ref Core.TabOfElement);
            Core.DoubleOrInt(ref Core.TabOfElement);
            Core.ErrorControl(ref Core.TabOfElement);
            Core.ErrorUpgradeDouble(ref Core.TabOfElement);
            Core.ErrorUpgradeInteger(ref Core.TabOfElement);
            Core.ErrorUpdateId(ref Core.TabOfElement);
            Core.ErrorUpdateCheckId(ref Core.TabOfElement);
            Core.ErrorUpdateIdExtraSymbol(ref Core.TabOfElement);

            for (int i = 0; i <= Core.TabOfElement.FindIndex(x=>x.SyntaxError); i++)
            {
                string s = new string(Core.TabOfElement[i].TextFragment);
                Console.WriteLine("Element: {0,15}                    Data type:  {1,22}                 Lexical error: {2,30}", s,Core.TabOfElement[i].DataType, Core.TabOfElement[i].SyntaxError);
            }
            
        }
    }

}
