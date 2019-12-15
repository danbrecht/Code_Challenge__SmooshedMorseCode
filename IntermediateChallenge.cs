using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// https://www.reddit.com/r/dailyprogrammer/comments/cn6gz5/20190807_challenge_380_intermediate_smooshed/
/// </summary>
namespace Code_Challenge__SmooshedMorseCode
{
    static class IntermediateChallenge
    {
        internal static void Challenge()
        {
            int numFound = 0;

            numFound = 0;
            FindDecode(".--...-.-.-.....-.--........----.-.-..---.---.--.--.-.-....-..-...-.---..--.----..", 0, 1);
            numFound = 0;
            FindDecode(".----...---.-....--.-........-----....--.-..-.-..--.--...--..-.---.--..-.-...--..-", 0, 1);
            numFound = 0;
            FindDecode("..-...-..-....--.---.---.---..-..--....-.....-..-.--.-.-.--.-..--.--..--.----..-..", 0, 1);
        }

        internal static void Bonus()
        {
            Bonus1();
           // Bonus2();
        }

        private static void Bonus2()
        {
            
        }

        class LetterCode
        {
            internal string strLetter, strCode;
            internal int index = -1;

            public LetterCode(string strLetter, string strCode)
            {
                this.strLetter = strLetter;
                this.strCode = strCode;
            }
        }

        /// <summary>
        /// Here's a list of 1000 inputs. How fast can you find the output for all of them? 
        /// A good time depends on your language of choice and setup, so there's no specific time to aim for.
        /// </summary>
        private static void Bonus1()
        {
            List<string> lstLines;
            Stopwatch stopwatch = new Stopwatch();
            
            lstLines = File.ReadLines("..\\..\\IntermediateInputs.txt").ToList();

            lstCodes = new List<LetterCode>();

            foreach (var pair in MorseCodeUtil.dictCodeToChar)
                lstCodes.Add(new LetterCode(pair.Value, pair.Key));

            lstCodes.Sort((p1, p2) => p2.strCode.Length.CompareTo(p1.strCode.Length));

            stopwatch.Start();

            foreach (string line in lstLines)
            {
                numFound = 0;
                lstCodes.ForEach(p => p.index = -1);
                FindDecode(line, 0, 1);

                //if (++count == 30)
                //    break;
            }

            Console.WriteLine(stopwatch.Elapsed.ToString());
        }

        static List<LetterCode> lstCodes;
        static int numFound;

        private static void FindDecode(string strCode, int index, int maxToFind)
        {
            string strTemp;

            foreach (var code in lstCodes)
            {
                if (code.index != -1)
                    continue;

                if (strCode.StartsWith(code.strCode))
                {
                    strTemp = strCode.Substring(code.strCode.Length);

                    code.index = index;

                    if (strTemp.Length == 0)
                    {
                        string strLetters = "";

                        for (int i = 0; i < 26; i++)
                            strLetters += lstCodes.Find(p => p.index == i).strLetter;
                        Console.WriteLine(strLetters);
                        numFound++;
                        return;
                    }

                    FindDecode(strTemp, index + 1, maxToFind);

                    if (numFound == maxToFind)
                        return;

                    code.index = -1;
                }
            }
        }
    }
}