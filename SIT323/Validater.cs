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

            // Total numbers of data.
            const int Total = 27;

            // Connect number of data with its content.
            String[] dataContent = new String[Total];

            // Whole line is a comment.
            const String RE_commentLine = @"^//.*$";

            // Has comment in the end of the line.
            const String RE_commentBehind = "^\\S+.*//.*$";

            // LOGFILE_NAME="log.txt".
            const String RE_logFile = "^LOGFILE_NAME=\".+\"$";
            dataContent[0] = "LOGFILE_NAME";

            // MINIMUM_NUMBER_OF_UNIQUE_WORDS=10.
            const String RE_minWords = "^MINIMUM_NUMBER_OF_UNIQUE_WORDS=\\d+$";
            dataContent[1] = "MINIMUM_NUMBER_OF_UNIQUE_WORDS";

            // MAXIMUM_NUMBER_OF_UNIQUE_WORDS=10.
            const String RE_maxWords = "^MAXIMUM_NUMBER_OF_UNIQUE_WORDS=\\d+$";
            dataContent[2] = "MAXIMUM_NUMBER_OF_UNIQUE_WORDS";

            // INVALID_CROZZLE_SCORE="INVALID CROZZLE"  *************Unkown expectation.
            const String RE_invalidCrozzleScore = "^INVALID_CROZZLE_SCORE=\".+\"$";
            dataContent[3] = "INVALID_CROZZLE_SCORE";

            // UPPERCASE=true/false.
            const String RE_upperCase = "^UPPERCASE=(TRUE|FALSE)$";
            dataContent[4] = "UPPERCASE";

            // STYLE="<style> table, td { border: 1px solid black; border-collapse: collapse; } td { width:24px; height:18px; text-align: center; } </style>"
            const String RE_style = "^STYLE=\"<STYLE>.*</STYLE>\"$";
            dataContent[5] = "STYLE";

            // BGCOLOUR_EMPTY_TD=#77777.
            const String RE_bgcolourEmpty = "^BGCOLOUR_EMPTY_TD=#[0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F]$";
            dataContent[6] = "BGCOLOUR_EMPTY_TD";

            // BGCOLOUR_NON_EMPTY_TD=#fffff.
            const String RE_bgcolourNonEmpty = "^BGCOLOUR_NON_EMPTY_TD=#[0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F][0-9A-F]$";
            dataContent[7] = "BGCOLOUR_NON_EMPTY_TD";

            // MINIMUM_NUMBER_OF_ROWS=4.
            const String RE_minRows = "^MINIMUM_NUMBER_OF_ROWS=\\d+$";
            dataContent[8] = "MINIMUM_NUMBER_OF_ROWS";

            // MAXIMUM_NUMBER_OF_ROWS=4.
            const String RE_maxRows = "^MAXIMUM_NUMBER_OF_ROWS=\\d+$";
            dataContent[9] = "MAXIMUM_NUMBER_OF_ROWS";

            // MAXIMUM_NUMBER_OF_COLUMNS=4.
            const String RE_minCols = "^MINIMUM_NUMBER_OF_COLUMNS=\\d+$";
            dataContent[10] = "MINIMUM_NUMBER_OF_COLUMNS";

            // MAXIMUM_NUMBER_OF_COLUMNS=4.
            const String RE_maxCols = "^MAXIMUM_NUMBER_OF_COLUMNS=\\d+$";
            dataContent[11] = "MAXIMUM_NUMBER_OF_COLUMNS";

            // MINIMUM_HORIZONTAL_WORDS=4.
            const String RE_minHor = "^MINIMUM_HORIZONTAL_WORDS=\\d+$";
            dataContent[12] = "MINIMUM_HORIZONTAL_WORDS";

            // MaxIMUM_HORIZONTAL_WORDS=4.
            const String RE_maxHor = "^MAXIMUM_HORIZONTAL_WORDS=\\d+$";
            dataContent[13] = "MAXIMUM_HORIZONTAL_WORDS";

            // MINIMUM_VERTICAL_WORDS=4.
            const String RE_minVer = "^MINIMUM_VERTICAL_WORDS=\\d+$";
            dataContent[14] = "MINIMUM_VERTICAL_WORDS";

            // MINIMUM_VERTICALL_WORDS=4.
            const String RE_maxVer = "^MAXIMUM_VERTICAL_WORDS=\\d+$";
            dataContent[15] = "MAXIMUM_VERTICAL_WORDS";

            // MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = 1
            const String RE_minHIntersections = "^MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS=\\d+$";
            dataContent[16] = "MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS";

            // MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = 100
            const String RE_maxHIntersections = "^MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS=\\d+$";
            dataContent[17] = "MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS";

            // MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = 1
            const String RE_minVIntersections = "^MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS=\\d+$";
            dataContent[18] = "MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS";

            // MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = 100
            const String RE_maxVIntersections = "^MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS=\\d+$";
            dataContent[19] = "MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS";

            // MINIMUM_NUMBER_OF_THE_SAME_WORD=1.
            const String RE_minSameWord = "^MINIMUM_NUMBER_OF_THE_SAME_WORD=\\d+$";
            dataContent[20] = "MINIMUM_NUMBER_OF_THE_SAME_WORD";

            // MAXIMUM_NUMBER_OF_THE_SAME_WORD=1.
            const String RE_maxSameWord = "^MAXIMUM_NUMBER_OF_THE_SAME_WORD=\\d+$";
            dataContent[21] = "MAXIMUM_NUMBER_OF_THE_SAME_WORD";

            // MINIMUM_NUMBER_OF_GROUPS=1.
            const String RE_minGroups = "^MINIMUM_NUMBER_OF_GROUPS=\\d+$";
            dataContent[22] = "MINIMUM_NUMBER_OF_GROUPS";

            // MAXIMUM_NUMBER_OF_GROUPS=1.
            const String RE_maxGroups = "^MAXIMUM_NUMBER_OF_GROUPS=\\d+$";
            dataContent[23] = "MAXIMUM_NUMBER_OF_GROUPS";

            // POINTS_PER_WORD=10.
            const String RE_pointsPerWord = "^POINTS_PER_WORD=\\d+$";
            dataContent[24] = "POINTS_PER_WORD";

            // INTERSECTING_POINTS_PER_LETTER="A=1,B=2,C=2,D=2,E=1,F=2,G=2,H=2,I=1,J=4,K=4,L=4,M=4,N=4,O=1,P=8,Q=8,R=8,S=8,T=8,U=1,V=16,W=16,X=32,Y=32,Z=64".
            const String RE_intersectingPoints = "^INTERSECTING_POINTS_PER_LETTER=\"([A-Z]=-?\\d+,){25,25}([A-Z]=-?\\d+)\"$";
            dataContent[25] = "INTERSECTING_POINTS_PER_LETTER";

            // NON_INTERSECTING_POINTS_PER_LETTER="A=1,B=2,C=2,D=2,E=1,F=2,G=2,H=2,I=1,J=4,K=4,L=4,M=4,N=4,O=1,P=8,Q=8,R=8,S=8,T=8,U=1,V=16,W=16,X=32,Y=32,Z=64".
            const String RE_nonIntersectingPoints = "^NON_INTERSECTING_POINTS_PER_LETTER=\"([A-Z]=-?\\d+,){25,25}([A-Z]=-?\\d+)\"$";
            dataContent[26] = "NON_INTERSECTING_POINTS_PER_LETTER";

            // To show the invalid line.
            int timeInRow = 0;

            // Row of dataChecker is dataLine; Column of dataChecker is the time data appear;
            // To ensure no dumplicated data and every data exists.
            int[,] dataChecker = new int[Total,2];
            for(int i = 0; i < Total; i++)
            {
                dataChecker[i,0] = i;
                dataChecker[i,1] = 0;
            }

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

                    // If dumplicate.
                    if (dataChecker[0, 1]>0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[0,1]++;
                }
              
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minWords))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[1, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[1, 1]++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxWords))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[2, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[2, 1]++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_invalidCrozzleScore))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[3, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[3, 1]++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_upperCase))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[4, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[4, 1]++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_style))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[5, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[5, 1]++;
                }
              
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_bgcolourEmpty))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[6, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[6, 1]++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_bgcolourNonEmpty))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[7, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[7, 1]++;
                }
              
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minRows))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[8, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[8, 1]++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxRows))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[9, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[9, 1]++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minCols))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[10, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[10, 1]++;
                }
              
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxCols))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[11, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[11, 1]++;
                }
             
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minHor))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[12, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[12, 1]++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxHor))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[13, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[13, 1]++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minVer))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[14, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[14, 1]++;
                }
               
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxVer))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[15, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[15, 1]++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minSameWord))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[16, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[16, 1]++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxSameWord))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[17, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[17, 1]++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minGroups))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[18, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[18, 1]++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxGroups))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[19, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[19, 1]++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_pointsPerWord))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[20, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[20, 1]++;
                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_intersectingPoints))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[21, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[21, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_nonIntersectingPoints))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[22, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[22, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minHIntersections))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[23, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[23, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxHIntersections))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[24, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[24, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minVIntersections))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[25, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[25, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxVIntersections))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[26, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[26, 1]++;
                }

                // If notMatchedTime == 0, not matched, invalid.
                if (matchedCount == 0)
                {
                    Console.WriteLine("{0} row is invalid", timeInRow);
                    isValid = false;
                    continue;
                }

            }

            // Check if every data appears.
            for(int i = 0; i < Total; i++)
            {
                if(dataChecker[i,1] == 0)
                {
                    Console.WriteLine("data {0} is lack or invalid", dataContent[i]);
                    isValid = false;
                }
            }

            return isValid;
        }

        public bool ValidateCrozzleText(String path)
        {
            /**
             * This method is to validate a Crozzle.txt.
             * Parameter is file path.
             * Return boolean to show if the Crozzle.txt is valid.
             */

            bool isValid = true;

            // Total types of data.
            const int Total = 6;

            // Connect number of data with its content.
            String[] dataContent = new String[Total];

            // Whole line is a comment.
            const String RE_commentLine = @"^//.*$";

            // Has comment in the end of the line.
            const String RE_commentBehind = "^\\S+.*//.*$";

            // CONFIGURATION_FILE.
            const String RE_configFile = "^CONFIGURATION_FILE=\".+\"$";
            dataContent[0] = "CONFIGURATION_FILE";

            // WORDLIST_FILE.
            const String RE_wordlistFile = "^WORDLIST_FILE=\".+\"$";
            dataContent[1] = "WORDLIST_FILE";

            // The number of rows and columns.
            const String RE_rows = "^ROWS=\\d+$";
            const String RE_cols = "^COLUMNS=\\d+$";
            dataContent[2] = "ROWS";
            dataContent[3] = "COLUMNS";

            // The horizontal rows containing words.
            const String RE_horizentalWords = "^ROW=\\d+,[A-Z]+,\\d+$";
            dataContent[4] = "HorizentalWords";

            // The vertical rows containing words.
            const String RE_verticalWords = "^COLUMN=\\d+,[A-Z]+,\\d+$";
            dataContent[5] = "VerticalWords";

            // To show the invalid line.
            int timeInRow = 0;

            // Row of dataChecker is dataLine; Column of dataChecker is the time data appear;
            // To ensure no dumplicated data and every data exists.
            int[,] dataChecker = new int[Total, 2];
            for (int i = 0; i < Total; i++)
            {
                dataChecker[i, 0] = i;
                dataChecker[i, 1] = 0;
            }

            String[] lines = File.ReadAllLines(path);
            foreach (String line in lines)
            {
                // Using matchedCount to judge whether the whole file is valid or not.
                int matchedCount = 0;

                timeInRow++;

                // Trim and Upper.
                String ruledLine = line.Trim().ToUpper();

                // If is a comment line then ignore.
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_commentLine))
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

                if(System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_configFile))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[0, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[0, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_wordlistFile))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[1, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[1, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_rows))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[2, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[2, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_cols))
                {
                    matchedCount++;
                    // If dumplicate.
                    if (dataChecker[3, 1] > 0)
                    {
                        Console.WriteLine("{0} row is invalid", timeInRow);
                        isValid = false;
                        continue;
                    }
                    dataChecker[3, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_horizentalWords))
                {
                    matchedCount++;
                    
                    dataChecker[4, 1]++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_verticalWords))
                {
                    matchedCount++;
                   
                    dataChecker[5, 1]++;
                }

                // If notMatchedTime == 0, not matched, invalid.
                if (matchedCount == 0)
                {
                    Console.WriteLine("{0} row is invalid", timeInRow);
                    isValid = false;
                    continue;
                }
            }

            // Check if every data appears.
            for (int i = 0; i < Total; i++)   
            {
                if (dataChecker[i, 1] == 0)
                {
                    Console.WriteLine("data {0} is lack or invalid", dataContent[i]);
                    isValid = false;
                }
            }

            return isValid;
        }
    }

}
