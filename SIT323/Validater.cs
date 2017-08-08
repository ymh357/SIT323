using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SIT323
{
    class Validater
    {
        public bool ValidateWordlist(String path)
        {
            bool isValid = true;
            String RE_validMatch = "^[a-zA-Z]+$";
            // Separated by commas, from the start to the end of a word contains only more than 1 letter.

            List<String> wordList = new List<string>();

            // To show the position of invalid word.
            int timeInRow = 0;
            int timeInCol = 0;

            foreach (String line in File.ReadAllLines(path))
            {
                timeInRow++;

                foreach(String word in line.Split(new char[] { ',' }))
                {
                    timeInCol++;

                    // If the word is invalid.
                    if(! System.Text.RegularExpressions.Regex.IsMatch(word, RE_validMatch))
                    {
                        isValid = false;
                        if(String.IsNullOrWhiteSpace(word))
                        {
                            Console.WriteLine("Word in {0} row {1} column is empty or white space.", timeInRow, timeInCol);
                        }
                        else
                        {
                            Console.WriteLine("Word in {0} row {1} column contain non-letter character.", timeInRow, timeInCol);
                        }
                        continue;
                    }

                    // If the word is a real word.
                    else
                    {
                        // If dumplicate.
                        if (wordList.Contains(word.ToUpper()))
                        {
                            isValid = false;
                            Console.WriteLine("Word in {0} row {1} column is Duplicated.", timeInRow, timeInCol);
                            continue;
                        }
                        else
                        {
                            wordList.Add(word.ToUpper());
                        }
                    }
                }
            }
            return isValid;
        }
    }
}
