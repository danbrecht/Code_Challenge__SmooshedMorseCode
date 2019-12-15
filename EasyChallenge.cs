using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// https://www.reddit.com/r/dailyprogrammer/comments/cmd1hb/20190805_challenge_380_easy_smooshed_morse_code_1/
/// </summary>

namespace Code_Challenge__SmooshedMorseCode
{
    static class EasyChallenge
    {
        internal static void Challenge()
        {
            DisplayConversion("sos");
            DisplayConversion("daily");
            DisplayConversion("programmer");
            DisplayConversion("bits");
            DisplayConversion("three");
        }

        internal static void Bonus()
        {
            Dictionary<string, string> dictWordAndCode;

            dictWordAndCode = LoadWordsAndCodes();

            Bonus1(dictWordAndCode);
            Bonus2(dictWordAndCode);
            Bonus3(dictWordAndCode);
            Bonus4(dictWordAndCode);
            Bonus5(dictWordAndCode);
        }

        /// <summary>
        /// --.---.---.-- is one of five 13-character sequences that does not appear in the encoding of any word. Find the other four
        /// </summary>
        private static void Bonus5(Dictionary<string, string> dictWordAndCode)
        {
            List<string> lstDistinceCodes;
            string strCode = "";

            Console.WriteLine("13-character sequences that do not appear in the encoding of any word:");

            lstDistinceCodes = dictWordAndCode.Values.Distinct().ToList();

            // recurse to check all possible 13 character dash-dot combinations
            AddDotOrDash(strCode, lstDistinceCodes, '-');
            AddDotOrDash(strCode, lstDistinceCodes, '.');
        }

        private static void AddDotOrDash(string strCode, List<string> lstDistinceCodes, char chDashDot)
        {
            strCode += chDashDot;

            if (strCode.Length == 13)
            {
                if (!lstDistinceCodes.Any(p => p.Contains(strCode)))
                    Console.WriteLine("\t" + strCode);
                return;
            }

            AddDotOrDash(strCode, lstDistinceCodes, '-');
            AddDotOrDash(strCode, lstDistinceCodes, '.');
        }

        /// <summary>
        /// Find the only 13-letter word that encodes to a palindrome
        /// </summary>
        private static void Bonus4(Dictionary<string, string> dictWordAndCode)
        {
            string strTemp;

            foreach (var pair in dictWordAndCode)
            {
                if (pair.Key.Length != 13)
                    continue;

                strTemp = pair.Value;

                while (strTemp.Length != 0)
                {
                    if (strTemp.First() != strTemp.Last())
                        break;

                    strTemp = strTemp.Substring(1, strTemp.Length - 2);
                } 

                if (strTemp.Length == 0)
                {
                    Console.WriteLine("13-letter word that encodes to a palindrome: " + pair.Key + " " + pair.Value);
                    return;
                }
            }
        }

        /// <summary>
        /// Call a word perfectly balanced if its code has the same number of dots as dashes. 
        /// counterdemonstrations is one of two 21-letter words that's perfectly balanced. 
        /// Find the other one.
        /// </summary>
        /// <param name="dictWordAndCode"></param>
        private static void Bonus3(Dictionary<string, string> dictWordAndCode)
        {
            foreach (var pair in dictWordAndCode)
            {
                if (pair.Key.Length == 21 && pair.Key != "counterdemonstrations" && pair.Value.Count(p => p == '-') == pair.Value.Count(p => p == '.'))
                {
                    Console.WriteLine("21-letter word that's perfectly balanced: " + pair.Key + " " + pair.Value);
                    return;
                }
            }
        }

        /// <summary>
        /// Find the only word that has 15 dashes in a row
        /// </summary>
        private static void Bonus2(Dictionary<string, string> dictWordAndCode)
        {
            string FifteenDashes = new string('-', 15), strCode;

            strCode = dictWordAndCode.Values.First(p => p.Contains(FifteenDashes));

            Console.WriteLine("15 dashes in a row: " + dictWordAndCode.First(p => p.Value == strCode).Key + " " + strCode);
        }

        /// <summary>
        /// Find the only sequence that's the code for 13 different words.
        /// </summary>
        private static void Bonus1(Dictionary<string, string> dictWordAndCode)
        {
            List<string> lstCodes;
            string strCurrent;
            int count;

            lstCodes = dictWordAndCode.Values.ToList();

            lstCodes.Sort();

            strCurrent = lstCodes[0];
            count = 1;

            for (int i = 1; i < lstCodes.Count; i++)
            {
                if (strCurrent != lstCodes[i])
                {
                    strCurrent = lstCodes[i];
                    count = 1;
                }
                else
                    count++;

                if (count == 13)
                {
                    Console.WriteLine("Only code with 13 words: " + strCurrent);
                    return;
                }
            }
        }

        private static Dictionary<string, string> LoadWordsAndCodes()
        {
            Dictionary<string, string> dictWordAndCode;

            dictWordAndCode = new Dictionary<string, string>();

            foreach (string word in MorseCodeUtil.WordList)
                dictWordAndCode.Add(word, WordToCode(word));

            return dictWordAndCode;
        }

        private static void DisplayConversion(string strWord)
        {
            Console.WriteLine(strWord + ": " + WordToCode(strWord));
        }

        private static string WordToCode(string strWord)
        {
            string strCode = "";

            foreach (char ch in strWord)
                strCode += MorseCodeUtil.CharacterToCode(ch);

            return strCode;
        }
    }
}
