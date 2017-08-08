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
            /**
             * This method is to validate a wordlist.
             * Parameter is file path.
             * Return boolean to show if the wordlist is valid.
             */
             
            bool isValid = true;
            const String RE_validMatch = "^[a-zA-Z]+$";
            // Separated by commas, from the start to the end of a word contains only more than 1 letter.

            List<String> wordList = new List<string>();

            // To show the position of invalid word.
            int timeInRow = 0;
            int timeInCol = 0;

            String[] lines = File.ReadAllLines(path);
            foreach (String line in lines)
            {
                timeInRow++;

                String[] words = line.Split(new char[] { ',' });
                foreach (String word in words)
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

        public bool ValidateConfigText(String path)
        {
            /**
             * This method is to validate a configure text.
             * Parameter is file path.
             * Return boolean to show if the configure text is valid.
             */

            bool isValid = true;

            // Whole line is a comment.
            const String RE_commentLine = @"^//.*$";

            // Has comment in the end of the line.
            const String RE_commentBehind = "^\\S+.*//.*$";

            // LOGFILE_NAME="log.txt".
            const String RE_logFile = "^LOGFILE_NAME=\"\\w+.\\w+\"$";

            // MINIMUM_NUMBER_OF_UNIQUE_WORDS=10.
            const String RE_minWords = "^MINIMUM_NUMBER_OF_UNIQUE_WORDS=\\d+$";

            // MAXIMUM_NUMBER_OF_UNIQUE_WORDS=10.
            const String RE_maxWords = "^MAXIMUM_NUMBER_OF_UNIQUE_WORDS=\\d+$";

            // INVALID_CROZZLE_SCORE="INVALID CROZZLE"  *************Unkown expectation.
            const String RE_invalidCrozzleScore = "^INVALID_CROZZLE_SCORE=\".+\"$";

            // UPPERCASE=true/false.
            const String RE_upperCase = "^UPPERCASE=(TRUE|FALSE)$";

            // STYLE="<style> table, td { border: 1px solid black; border-collapse: collapse; } td { width:24px; height:18px; text-align: center; } </style>"
            const String RE_style = "^STYLE=\"<STYLE>.*</STYLE>\"$";

            // BGCOLOUR_EMPTY_TD=#77777.
            const String RE_bgcolourEmpty = "^BGCOLOUR_EMPTY_TD=#[0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F]$";

            // BGCOLOUR_NON_EMPTY_TD=#fffff.
            const String RE_bgcolourNonEmpty = "^BGCOLOUR_NON_EMPTY_TD=#[0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F]$";

            // MINIMUM_NUMBER_OF_ROWS=4.
            const String RE_minRows = "^MINIMUM_NUMBER_OF_ROWS=\\d+$";

            // MAXIMUM_NUMBER_OF_ROWS=4.
            const String RE_maxRows = "^MAXIMUM_NUMBER_OF_ROWS=\\d+$";

            // MAXIMUM_NUMBER_OF_COLUMNS=4.
            const String RE_minCols = "^MINIMUM_NUMBER_OF_COLUMNS=\\d+$";

            // MAXIMUM_NUMBER_OF_COLUMNS=4.
            const String RE_maxCols = "^MAXIMUM_NUMBER_OF_COLUMNS=\\d+$";

            // MINIMUM_HORIZONTAL_WORDS=4.
            const String RE_minHor = "^MINIMUM_HORIZONTAL_WORDS=\\d+$";

            // MaxIMUM_HORIZONTAL_WORDS=4.
            const String RE_maxHor = "^MAXIMUM_HORIZONTAL_WORDS=\\d+$";

            // MINIMUM_VERTICAL_WORDS=4.
            const String RE_minVer = "^MINIMUM_VERTICAL_WORDS=\\d+$";

            // MINIMUM_VERTICALL_WORDS=4.
            const String RE_maxVer = "^MAXIMUM_VERTICAL_WORDS=\\d+$";

            // MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = 1
            const String RE_minHIntersections = "^MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS=\\d+$";

            // MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = 100
            const String RE_maxHIntersections = "^MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS=\\d+$";

            // MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = 1
            const String RE_minVIntersections = "^MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS=\\d+$";

            // MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = 100
            const String RE_maxVIntersections = "^MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS=\\d+$";

            // MINIMUM_NUMBER_OF_THE_SAME_WORD=1.
            const String RE_minSameWord = "^MINIMUM_NUMBER_OF_THE_SAME_WORD=\\d+$";

            // MAXIMUM_NUMBER_OF_THE_SAME_WORD=1.
            const String RE_maxSameWord = "^MAXIMUM_NUMBER_OF_THE_SAME_WORD=\\d+$";

            // MINIMUM_NUMBER_OF_GROUPS=1.
            const String RE_minGroups = "^MINIMUM_NUMBER_OF_GROUPS=\\d+$";

            // MAXIMUM_NUMBER_OF_GROUPS=1.
            const String RE_maxGroups = "^MAXIMUM_NUMBER_OF_GROUPS=\\d+$";

            // POINTS_PER_WORD=10.
            const String RE_pointsPerWord = "^POINTS_PER_WORD=\\d+$";

            // INTERSECTING_POINTS_PER_LETTER="A=1,B=2,C=2,D=2,E=1,F=2,G=2,H=2,I=1,J=4,K=4,L=4,M=4,N=4,O=1,P=8,Q=8,R=8,S=8,T=8,U=1,V=16,W=16,X=32,Y=32,Z=64".
            const String RE_intersectingPoints = "^INTERSECTING_POINTS_PER_LETTER=\"([A-Z]=-?\\d+,){25,25}([A-Z]=-?\\d+)\"$";

            // NON_INTERSECTING_POINTS_PER_LETTER="A=1,B=2,C=2,D=2,E=1,F=2,G=2,H=2,I=1,J=4,K=4,L=4,M=4,N=4,O=1,P=8,Q=8,R=8,S=8,T=8,U=1,V=16,W=16,X=32,Y=32,Z=64".
            const String RE_nonIntersectingPoints = "^NON_INTERSECTING_POINTS_PER_LETTER=\"([A-Z]=-?\\d+,){25,25}([A-Z]=-?\\d+)\"$";

            // To show the invalid line.
            int timeInRow = 0;


            String[] lines = File.ReadAllLines(path);
            foreach (String line in lines)
            {   
                // Using matchedCount to judge whether the whole file is valid or not.
                int matchedCount = 0;

                timeInRow++;

                // Trim and Upper.
                String ruledLine = line.Trim().ToUpper();

                // If is a comment line then ignore.
                if(System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_commentLine))
                {
                    continue;
                }

                // If is a space line then ignore.
                if (String.IsNullOrEmpty(ruledLine.Trim()))
                {
                    continue;
                }

                // If has comment in the end of the line, remove it.
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_commentBehind))
                {
                    ruledLine = ruledLine.Remove(ruledLine.IndexOf("//")).Trim();
                }

                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_logFile))
                {
                    matchedCount++;
                }
              
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minWords))
                {
                    matchedCount++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxWords))
                {
                    matchedCount++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_invalidCrozzleScore))
                {
                    matchedCount++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_upperCase))
                {
                    matchedCount++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_style))
                {
                    matchedCount++;
                }
              
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_bgcolourEmpty))
                {
                    matchedCount++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_bgcolourNonEmpty))
                {
                    matchedCount++;
                }
              
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minRows))
                {
                    matchedCount++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxRows))
                {
                    matchedCount++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minCols))
                {
                    matchedCount++;
                }
              
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxCols))
                {
                    matchedCount++;
                }
             
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minHor))
                {
                    matchedCount++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxHor))
                {
                    matchedCount++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minVer))
                {
                    matchedCount++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxVer))
                {
                    matchedCount++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minSameWord))
                {
                    matchedCount++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxSameWord))
                {
                    matchedCount++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minGroups))
                {
                    matchedCount++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxGroups))
                {
                    matchedCount++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_pointsPerWord))
                {
                    matchedCount++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_intersectingPoints))
                {
                    matchedCount++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_nonIntersectingPoints))
                {
                    matchedCount++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minHIntersections))
                {
                    matchedCount++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxHIntersections))
                {
                    matchedCount++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minVIntersections))
                {
                    matchedCount++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxVIntersections))
                {
                    matchedCount++;
                }

                // If notMatchedTime < 0, not matched, invalid.
                if (matchedCount == 0)
                {
                    Console.WriteLine("{0} row is invalid", timeInRow);
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}
