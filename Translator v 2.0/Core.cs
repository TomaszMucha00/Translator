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
                foreach (var item2 in SplitSigns)
                {
                    string tempString = "";
                    foreach (var temp2 in item)
                    {
                        tempString += temp2;
                    }
                    Console.WriteLine(item.ToString() == item2.ToString());
                    if (tempString==item2.ToString())
                    {
                        Element temp = new Element(item);
                        temp.DataType = temp.SplitSignDict[item2.ToString()];
                        temp.SyntaxError = false;
                        TabOfElement.Add(temp);
                    }
                }
            }
        }

        #endregion
    }
}
