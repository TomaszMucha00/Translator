using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator_v_2._0
{
    class Core
    {
        #region Fields
        public static List<char[]> TabAfterSignsSplit = new List<char[]>();
        public static List<int> IndexsOfSplitSign = new List<int>();
        public static List<Element> TabOfElement = new List<Element>();
        public static char[] SplitSigns = { '+', '-', '*', '/', '(', ')' };
        #endregion

        #region Metods

        #region Dictionary
        //public static void DictionaryInit()
        //{
        //    SignsErrorDict.Add("+", "+");
        //    SignsErrorDict.Add("+", "-");
        //    SignsErrorDict.Add("+", "*");
        //    SignsErrorDict.Add("+", "/");
        //    SignsErrorDict.Add("-", "+");
        //    SignsErrorDict.Add("-", "-");
        //    SignsErrorDict.Add("-", "*");
        //    SignsErrorDict.Add("-", "/");
        //    SignsErrorDict.Add("*", "+");
        //    SignsErrorDict.Add("*", "-");
        //    SignsErrorDict.Add("*", "*");
        //    SignsErrorDict.Add("*", "/");
        //    SignsErrorDict.Add("/", "+");
        //    SignsErrorDict.Add("/", "-");
        //    SignsErrorDict.Add("/", "*");
        //    SignsErrorDict.Add("/", "/");
        //    SignsErrorDict.Add("(", "+");
        //    SignsErrorDict.Add("(", "+");
        //    SignsErrorDict.Add("(", "+");
        //    SignsErrorDict.Add("(", "+");
        //    SignsErrorDict.Add(")", "+");
        //    SignsErrorDict.Add(")", "+");
        //    SignsErrorDict.Add(")", "+");
        //    SignsErrorDict.Add(")", "+");
        //}

        #endregion

        private static bool Empty(char[] c)
        {
            return c.Count()==0;
        }

        public static int CountSplitSign()
        {
            char[] TextToTranslateAsArray = Data.textToTranslate.ToCharArray();

            int SplitSignCounter = 0;

            foreach (var textSign in TextToTranslateAsArray)
            {
                foreach (var splitSign in SplitSigns)
                {
                    if (textSign==splitSign)
                    {
                        SplitSignCounter++;
                    }
                }
            }
            return SplitSignCounter;
        }

        public static List<int> IndexsOfSplitSignF(List<int> ListOfIndexsSplitSign)
        {
            char[] TextToTranslateAsArray = Data.textToTranslate.ToCharArray();

                foreach (var splitSign in SplitSigns)
                {
                    for (int i = 0; i < TextToTranslateAsArray.Length; i++)
                    {
                        if (TextToTranslateAsArray[i] == splitSign)
                        {
                            ListOfIndexsSplitSign.Add(i);
                        }
                    }
                    
                }

            ListOfIndexsSplitSign.Sort();

            return ListOfIndexsSplitSign;
        }

        public static List<char[]> TextToSplitCharTab()
        {
            TabAfterSignsSplit.Add(Data.textToTranslate.Substring(0, IndexsOfSplitSign[0]).ToCharArray());
            for (int i = 0; i+1 < IndexsOfSplitSign.Count; i++)
            {
              
                TabAfterSignsSplit.Add(Data.textToTranslate[IndexsOfSplitSign[i]].ToString().ToCharArray());
                TabAfterSignsSplit.Add(Data.textToTranslate.Substring(IndexsOfSplitSign[i]+1, IndexsOfSplitSign[i+1] - IndexsOfSplitSign[i] - 1).ToCharArray());
            }
            TabAfterSignsSplit.Add(Data.textToTranslate.Substring(IndexsOfSplitSign.Last(), 1).ToCharArray());
            TabAfterSignsSplit.Add(Data.textToTranslate.Substring(IndexsOfSplitSign.Last()+1, Data.textToTranslate.Length- IndexsOfSplitSign.Last()-2).ToCharArray());
            return TabAfterSignsSplit;
        }

        public static void DeleteEmptyCharTab(ref List<char[]> TabAfterSignsSplit)
        {
            TabAfterSignsSplit.RemoveAll(Empty);
        }

        public static void SetTabOfElement(ref List<Element> TabOfElement)
        {
            foreach (var item in TabAfterSignsSplit)
            {
                bool isSplitSign = false;
                foreach (var item2 in SplitSigns)
                {
                    string tempString = "";
                    foreach (var temp2 in item)
                    {
                        tempString += temp2;
                    }
                    if (tempString==item2.ToString())
                    {
                        Element temp = new Element(item);
                        temp.DataType = temp.SplitSignDict[item2.ToString()];
                        temp.SyntaxError = false;
                        TabOfElement.Add(temp);
                        isSplitSign = true;
                    }
                }
                if (!isSplitSign)
                {
                    Element temp = new Element(item);
                    temp.DataType = "TempNull";
                    temp.SyntaxError = false;
                    TabOfElement.Add(temp);
                }
            }

            TabOfElement.RemoveAt(TabOfElement.Count-1);
        }

        public static bool ErrorControlManyPoint( Element TestingObject)
        {
            bool b1 = (TestingObject.DataType == "Integer") || (TestingObject.DataType == "Double" )||( TestingObject.DataType == "Identyficator");

            int counter = 0;

            foreach (var item in TestingObject.TextFragment)
            {
                if (b1 && item == '.')
                {
                    counter++;
                }   
            }
            if (counter>1)
            {
                return false;
            }

            return true;
        }

        public static bool ErrorControlPointOnTheBegining( Element TestingObject)
        {
            bool b1 = TestingObject.DataType == "Integer" || TestingObject.DataType == "Double" || TestingObject.DataType == "Identyficator";

            if (b1 && TestingObject.TextFragment.First() == '.')
            {
                return false;
            }
            return true;
        }

        public static bool ErrorControlPointOnTheEnd( Element TestingObject)
        {
            bool b1 = TestingObject.DataType == "Integer" || TestingObject.DataType == "Double" || TestingObject.DataType == "Identyficator";

            if (b1 && TestingObject.TextFragment.Last() == '.')
            {
                return false;
            }
            return true;
        }

        public static bool ErrorControlNotAllowedSymbol( Element TestingObject)
        {

            bool b1 = TestingObject.DataType == "Integer" || TestingObject.DataType == "Double" || TestingObject.DataType == "Identyficator";

            foreach (var item in TestingObject.TextFragment)
            {
                bool b2 = (item > 96 && item < 123) || (item > 47 && item < 58) || item == '.';
                if (b1&&!b2)
                {
                    return false;
                }
            }


            return true;
        }

        public static bool ErrorIdHaveDot(Element TestingObject)
        {
            int counter = 0;
            foreach (var item in TestingObject.TextFragment)
            {
                if (item == '.')
                {
                    counter++;
                }
            }

            if (TestingObject.DataType == "Identyficator" && counter!=0)
            {
                return false;
            }
            return true;
        }

        public static void ErrorControl(ref List<Element> TestingObject)
        {
            foreach (var item in TestingObject)
            {
                if(!(
                ErrorControlManyPoint(item)&&
                ErrorControlPointOnTheBegining(item) &&
                ErrorControlPointOnTheEnd(item) &&
                ErrorControlNotAllowedSymbol(item)&&
                ErrorIdHaveDot(item) ))
                {
                    item.SyntaxError = true;
                }
            }
        }

        public static void IsIdentyficator(ref List<Element> TabOfElement)
        {
            bool id = false;
            foreach (var element in TabOfElement)
            {                
                foreach (var sign in (new String(element.TextFragment).ToLower().ToCharArray()))
                {
                    if (sign>96 && sign<123)
                    {
                        id = true;
                    }
                }
                if (id)
                {
                    element.DataType = "Contain id";
                }
                id = false;
            }
        }

        public static void IsFullId(ref List<Element> TabOfElement)
        {
            foreach (var element in TabOfElement)
            {
                if (element.DataType == "Contain id")
                {
                    if (element.TextFragment.First()>96 && element.TextFragment.First() <123)
                    {
                        element.DataType = "Identyficator";
                    }
                    else
                    {
                        element.DataType = "To split";
                    }
                }
            }
        }

        public static void DoubleOrInt(ref List<Element> TabOfElement)
        {
            foreach (var element in TabOfElement)
            {
                if (element.DataType == "TempNull")
                {

                    int counter = 0;
                    foreach (var item in element.TextFragment)
                    {
                        if (item == '.')
                        {
                            counter++;
                        }  
                    }
                    if (counter != 0)
                    {
                        element.DataType = "Double";
                    }
                    else
                    {
                        element.DataType = "Integer";
                    }
                }
            }
        }

        public static int FindFirstLetter(char[] cTab)
        {
            for (int i = 0; i < cTab.Count(); i++)
            {
                if (cTab[i]> 96 && cTab[i] < 123)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void SplitNumberAndId(ref List<Element> TabOfElement)
        {
            while(!(TabOfElement.Where(x => x.DataType == "To split").Count() == 0))
            {
                Element temp;
                int tempInt = TabOfElement.FindIndex(x => x.DataType == "To split");
                temp = TabOfElement[TabOfElement.FindIndex(x => x.DataType == "To split")];
                TabOfElement.RemoveAt(TabOfElement.FindIndex(x => x.DataType == "To split"));
                char[] cTab1 = new char[FindFirstLetter(temp.TextFragment)];
                char[] cTab2 = new char[temp.TextFragment.Count() - FindFirstLetter(temp.TextFragment)];
                for (int i = 0; i <= FindFirstLetter(temp.TextFragment)-1; i++)
                {
                    cTab1[i] = temp.TextFragment[i];
                }
                for (int i = FindFirstLetter(temp.TextFragment); i < temp.TextFragment.Count(); i++)
                {
                    cTab2[i-FindFirstLetter(temp.TextFragment)] = temp.TextFragment[i];
                }
                Element e1 = new Element(cTab1);
                Element e2 = new Element(cTab2);
                e1.DataType = "TempNull";
                e2.DataType = "Identyficator";
                e1.SyntaxError = false;
                e2.SyntaxError = false;
                List<Element> tempList = new List<Element>();
                tempList.Add(e1);
                tempList.Add(e2);
                TabOfElement.InsertRange(tempInt, tempList);


            }            
        }

        public static void ErrorUpgradeDouble(ref List<Element> TabOfElement)
        {            
            foreach (var element in TabOfElement.Where(x=>(x.DataType=="Double")&&(x.SyntaxError)).ToList())
            {
                int index = 0;
                int counter = 0;
                for (int i = 0; i < element.TextFragment.Length; i++)
                {
                    if (element.TextFragment[i]=='.')
                    {
                        counter++;
                    }
                    if (counter == 2)
                    {
                        index = i;
                        break;
                    }
                }
                int tempInt = TabOfElement.FindIndex(x => (x.DataType == "Double")&&x.SyntaxError);
                char[] cTab1 = new char[index];
                char[] cTab2 = new char[element.TextFragment.Count()-index];
                for (int i = 0; i <= index - 1; i++)
                {
                    cTab1[i] = element.TextFragment[i];
                }
                for (int i = index; i < element.TextFragment.Count(); i++)
                {
                    cTab2[i - index] = element.TextFragment[i];
                }
                Element e1 = new Element(cTab1);
                Element e2 = new Element(cTab2);
                e1.DataType = "Double";
                e2.DataType = "Error";
                e1.SyntaxError = false;
                e2.SyntaxError = true;
                List<Element> tempList = new List<Element>();
                tempList.Add(e1);
                tempList.Add(e2);
                TabOfElement.RemoveAt(tempInt);
                TabOfElement.InsertRange(tempInt, tempList);
            }
        }

        public static void ErrorUpgradeInteger(ref List<Element> TabOfElement)
        {
            foreach (var element in TabOfElement.Where(x=>x.DataType=="Integer").Where(x=>x.SyntaxError).ToList())
            {
                int index = 0;
                for (int i = 0; i < element.TextFragment.Length; i++)
                {
                    if ((element.TextFragment[i] < 48) || (element.TextFragment[i] > 57))
                    {
                        index = i;
                        break;
                    }
                }
                int tempInt = TabOfElement.FindIndex(x => (x.DataType == "Integer") && x.SyntaxError);
                char[] cTab1 = new char[index];
                char[] cTab2 = new char[element.TextFragment.Count() - index];
                for (int i = 0; i <= index - 1; i++)
                {
                    cTab1[i] = element.TextFragment[i];
                }
                for (int i = index; i < element.TextFragment.Count(); i++)
                {
                    cTab2[i - index] = element.TextFragment[i];
                }
                Element e1 = new Element(cTab1);
                Element e2 = new Element(cTab2);
                e1.DataType = "Integer";
                e2.DataType = "Error";
                e1.SyntaxError = false;
                e2.SyntaxError = true;
                List<Element> tempList = new List<Element>();
                tempList.Add(e1);
                tempList.Add(e2);
                TabOfElement.RemoveAt(tempInt);
                TabOfElement.InsertRange(tempInt, tempList);
            }
        }

        public static void ErrorUpdateId(ref List<Element> TabOfElement)
        {
            foreach (var element in TabOfElement.Where(x => x.DataType == "Identyficator").Where(x => x.SyntaxError).ToList())
            {
                int index = 0;
                for (int i = 0; i < element.TextFragment.Length; i++)
                {
                    if ((element.TextFragment[i] == '.'))
                    {
                        index = i;
                        break;
                    }
                }
                int tempInt = TabOfElement.FindIndex(x => (x.DataType == "Identyficator") && x.SyntaxError);
                char[] cTab1 = new char[index];
                char[] cTab2 = new char[element.TextFragment.Count() - index];
                for (int i = 0; i <= index - 1; i++)
                {
                    cTab1[i] = element.TextFragment[i];
                }
                for (int i = index; i < element.TextFragment.Count(); i++)
                {
                    cTab2[i - index] = element.TextFragment[i];
                }
                Element e1 = new Element(cTab1);
                Element e2 = new Element(cTab2);
                e1.DataType = "Identyficator";
                e2.DataType = "Error";
                e1.SyntaxError = false;
                e2.SyntaxError = true;
                List<Element> tempList = new List<Element>();
                tempList.Add(e1);
                tempList.Add(e2);
                TabOfElement.RemoveAt(tempInt);
                TabOfElement.InsertRange(tempInt, tempList);
            }
        }

        public static void ErrorUpdateCheckId(ref List<Element> TabOfElement)
        {
            foreach (var element in TabOfElement.Where(x=>x.DataType=="Identyficator"))
            {
                foreach (var item in element.TextFragment)
                {
                    if (item<48||(item>57&&item<97)||item>122)
                    {
                        element.SyntaxError = true;
                        break;
                    }
                }
            }
        }

        public static void ErrorUpdateIdExtraSymbol(ref List<Element> TabOfElement)
        {
            foreach (var element in TabOfElement.Where(x => x.DataType == "Identyficator").Where(x => x.SyntaxError).ToList())
            {
                int index = 0;
                for (int i = 0; i < element.TextFragment.Length; i++)
                {
                    if (element.TextFragment[i] < 48 || (element.TextFragment[i] > 57 && element.TextFragment[i] < 97) || element.TextFragment[i] > 122)
                    {
                        index = i;
                        break;
                    }
                }
                int tempInt = TabOfElement.FindIndex(x => (x.DataType == "Identyficator") && x.SyntaxError);
                char[] cTab1 = new char[index];
                char[] cTab2 = new char[element.TextFragment.Count() - index];
                for (int i = 0; i <= index - 1; i++)
                {
                    cTab1[i] = element.TextFragment[i];
                }
                for (int i = index; i < element.TextFragment.Count(); i++)
                {
                    cTab2[i - index] = element.TextFragment[i];
                }
                Element e1 = new Element(cTab1);
                Element e2 = new Element(cTab2);
                e1.DataType = "Identyficator";
                e2.DataType = "Error";
                e1.SyntaxError = false;
                e2.SyntaxError = true;
                List<Element> tempList = new List<Element>();
                tempList.Add(e1);
                tempList.Add(e2);
                TabOfElement.RemoveAt(tempInt);
                TabOfElement.InsertRange(tempInt, tempList);
            }
        }
        #endregion
    }
}
