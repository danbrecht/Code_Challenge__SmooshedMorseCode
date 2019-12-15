using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Challenge__SmooshedMorseCode
{
    public static class MorseCodeUtil
    {
        public static Dictionary<string, string> dictCodeToChar = InitMorseCode();
        private static Dictionary<char, string> dictCharToCode;

        private static Dictionary<string, string> InitMorseCode()
        {
            string strDotDash = ".- -... -.-. -.. . ..-. --. .... .. .--- -.- .-.. -- -. --- .--. --.- .-. ... - ..- ...- .-- -..- -.-- --..";
            string[] strSplit;
            int index = 0;

            strSplit = strDotDash.Split(' ');

            dictCharToCode = new Dictionary<char, string>();
            dictCodeToChar = new Dictionary<string, string>();

            for (char ch = 'a'; ch <= 'z'; ch++, index++)
            {
                dictCodeToChar[strSplit[index]] = ch.ToString();
                dictCharToCode[ch] = strSplit[index];
            }

            return dictCodeToChar;
        }

        public static string CharacterToCode(char ch)
        {
            return dictCharToCode[ch];
        }

        public static string CodeToCharacter(string strCode)
        {
            if (!dictCodeToChar.ContainsKey(strCode))
                return strCode;

            return dictCodeToChar[strCode];
        }

        static List<string> lstWords = null;

        public static List<string> WordList
        {
            get
            {
                if (lstWords == null)
                    lstWords = File.ReadAllLines(@"..\..\enable1.txt").ToList();

                return lstWords;
            }
        }
    }
}
