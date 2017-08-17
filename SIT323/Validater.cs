﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SIT323
{
    class Validater
    {
        private Dictionary<String,String> configDic=null;
        private int score=0;
        char[,] crozzleGrid = null;
        String logPath = null;

        public char[,] CrozzleGrid { get => crozzleGrid;}
        public string LogPath { get => logPath;}
        public int Score { get => score; }

        public Validater()
        {
            configDic = new Dictionary<string, string>();

            configDic.Add("LOGFILE_NAME",null);

            configDic.Add("MINIMUM_NUMBER_OF_UNIQUE_WORDS",null);

            configDic.Add("MAXIMUM_NUMBER_OF_UNIQUE_WORDS",null);

            configDic.Add("INVALID_CROZZLE_SCORE",null);

            configDic.Add("UPPERCASE",null);

            configDic.Add("STYLE",null);
            ;
            configDic.Add("BGCOLOUR_EMPTY_TD",null);

            configDic.Add("BGCOLOUR_NON_EMPTY_TD",null);

            configDic.Add("MINIMUM_NUMBER_OF_ROWS",null);

            configDic.Add("MAXIMUM_NUMBER_OF_ROWS",null);

            configDic.Add("MINIMUM_NUMBER_OF_COLUMNS",null);

            configDic.Add("MAXIMUM_NUMBER_OF_COLUMNS",null);

            configDic.Add("MINIMUM_HORIZONTAL_WORDS",null);

            configDic.Add("MAXIMUM_HORIZONTAL_WORDS",null);

            configDic.Add("MINIMUM_VERTICAL_WORDS",null);

            configDic.Add("MAXIMUM_VERTICAL_WORDS",null);

            configDic.Add("MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS",null);

            configDic.Add("MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS",null);

            configDic.Add("MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS",null);

            configDic.Add("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS",null);

            configDic.Add("MINIMUM_NUMBER_OF_THE_SAME_WORD",null);

            configDic.Add("MAXIMUM_NUMBER_OF_THE_SAME_WORD",null);

            configDic.Add("MINIMUM_NUMBER_OF_GROUPS",null);

            configDic.Add("MAXIMUM_NUMBER_OF_GROUPS",null);

            configDic.Add("POINTS_PER_WORD",null);

            configDic.Add("INTERSECTING_POINTS_PER_LETTER",null);

            configDic.Add("NON_INTERSECTING_POINTS_PER_LETTER",null);

        }

        public bool ValidateWordlist(String path)
        {
            /**
             * This method is to validate a wordlist.
             * Parameter is file path.
             * Return boolean to show if the wordlist is valid.
             */

            List<String> wordlistError = new List<string>();
            wordlistError.Add("-------------------------Wordlist error.\r\n");

            bool isValid = true;
            const String RE_validMatch = "^[a-zA-Z]+$";
            // Separated by commas, from the start to the end of a word contains only more than 1 letter.

            List<String> wordList = new List<string>();

            // To show the position of invalid word.
            int timeInRow = 0;
            int timeInCol = 0;

            try
            {
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
                                wordlistError.Add("Word in "+timeInRow+" row "+ timeInCol + " column is empty or white space.\r\n");
                            }
                            else
                            {
                                wordlistError.Add("Word in " + timeInRow + " row "+ timeInCol +"  column contain non-letter character.\r\n");
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
                                wordlistError.Add("Word in " + timeInRow + " row " + timeInCol + " column is Duplicated.\r\n");
                                continue;
                            }
                            else
                            {
                                wordList.Add(word.ToUpper());
                            }
                        }
                    }
                }
            }catch(Exception)
            {
                wordlistError.Add("Please check Wordlist file path.");
            }
            // If not meet the demand in configuration.txt.
            try
            {
                if (wordList.Count < Int32.Parse(configDic["MINIMUM_NUMBER_OF_UNIQUE_WORDS"]) || wordList.Count > Int32.Parse(configDic["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"]))
                {
                    isValid = false;
                    wordlistError.Add("Words' number does not meet the demand in configuration.txt.\r\n");
                }
            }
            catch (Exception)
            {
                int i;
                if(!Int32.TryParse(configDic["MINIMUM_NUMBER_OF_UNIQUE_WORDS"], out i) || !Int32.TryParse(configDic["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"],out i))
                {
                    //////////////////////////
                }
            }



            if (isValid)
            {
                wordlistError.Add("No error.\r\n");
            }
            try
            {
                File.AppendAllLines(logPath, wordlistError);
            }
            catch (Exception)
            {
                /////////////////
            }

            return isValid;
        }

        public bool ValidateConfigText(String path)
        {
            /**
             * This method is to 
             * 1. Validate a configure text; 
             * 2. Save configurations.
             * Parameter is file path.
             * Return boolean to show if the configure text is valid.
             */

            List<String> configTxtError = new List<string>();
            configTxtError.Add("-------------------------Configuration error.\r\n");

            bool isValid = true;

            // Whole line is a comment.
            const String RE_commentLine = @"^//.*$";

            // Has comment in the end of the line.
            const String RE_commentBehind = "^\\S+.*//.*$";

            // LOGFILE_NAME="log.txt".
            const String RE_logFile = "^LOGFILE_NAME=\".+\"$";

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
            const String RE_intersectingPoints = "^INTERSECTING_POINTS_PER_LETTER=\"A=-?\\d+,B=-?\\d+,C=-?\\d+,D=-?\\d+,E=-?\\d+,F=-?\\d+,G=-?\\d+,H=-?\\d+,I=-?\\d+,J=-?\\d+,K=-?\\d+,L=-?\\d+,M=-?\\d+,N=-?\\d+,O=-?\\d+,P=-?\\d+,Q=-?\\d+,R=-?\\d+,S=-?\\d+,T=-?\\d+,U=-?\\d+,V=-?\\d+,W=-?\\d+,X=-?\\d+,Y=-?\\d+,Z=-?\\d+\"$";

            // NON_INTERSECTING_POINTS_PER_LETTER="A=1,B=2,C=2,D=2,E=1,F=2,G=2,H=2,I=1,J=4,K=4,L=4,M=4,N=4,O=1,P=8,Q=8,R=8,S=8,T=8,U=1,V=16,W=16,X=32,Y=32,Z=64".
            const String RE_nonIntersectingPoints = "^NON_INTERSECTING_POINTS_PER_LETTER=\"A=-?\\d+,B=-?\\d+,C=-?\\d+,D=-?\\d+,E=-?\\d+,F=-?\\d+,G=-?\\d+,H=-?\\d+,I=-?\\d+,J=-?\\d+,K=-?\\d+,L=-?\\d+,M=-?\\d+,N=-?\\d+,O=-?\\d+,P=-?\\d+,Q=-?\\d+,R=-?\\d+,S=-?\\d+,T=-?\\d+,U=-?\\d+,V=-?\\d+,W=-?\\d+,X=-?\\d+,Y=-?\\d+,Z=-?\\d+\"$";

            // To show the invalid line.
            int timeInRow = 0;

            try
            {

                String[] lines = File.ReadAllLines(path);
                foreach (String line in lines)
                {
                    // To see current row is valid or not.
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

                        // If dumplicate.
                        if (configDic["LOGFILE_NAME"]!=null)
                        {
                            configTxtError.Add(timeInRow+" row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["LOGFILE_NAME"] = rightArr;
                        matchedCount++;
                    }
              
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minWords))
                    {
                        // If dumplicate.
                        if (configDic["MINIMUM_NUMBER_OF_UNIQUE_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow+ " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MINIMUM_NUMBER_OF_UNIQUE_WORDS"] = rightArr;
                        matchedCount++;
                    }
               
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxWords))
                    {
                        // If dumplicate.
                        if (configDic["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow+ " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"] = rightArr;
                        matchedCount++;
                    }
                
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_invalidCrozzleScore))
                    {
                        // If dumplicate.
                        if (configDic["INVALID_CROZZLE_SCORE"] != null)
                        {
                            configTxtError.Add(timeInRow+ " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["INVALID_CROZZLE_SCORE"] = rightArr;
                        matchedCount++;
                    }
               
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_upperCase))
                    {
                        // If dumplicate.
                        if (configDic["UPPERCASE"] != null)
                        {
                            configTxtError.Add(timeInRow+ " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["UPPERCASE"] = rightArr;
                        matchedCount++;
                    }
               
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_style))
                    {
                        // If dumplicate.
                        if (configDic["STYLE"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["STYLE"] = rightArr;
                        matchedCount++;
                    }
              
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_bgcolourEmpty))
                    {
                        // If dumplicate.
                        if (configDic["BGCOLOUR_EMPTY_TD"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["BGCOLOUR_EMPTY_TD"] = rightArr;
                        matchedCount++;
                    }
                
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_bgcolourNonEmpty))
                    {
                        // If dumplicate.
                        if (configDic["BGCOLOUR_NON_EMPTY_TD"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["BGCOLOUR_NON_EMPTY_TD"] = rightArr;
                        matchedCount++;
                    }
              
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minRows))
                    {
                        // If dumplicate.
                        if (configDic["MINIMUM_NUMBER_OF_ROWS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MINIMUM_NUMBER_OF_ROWS"] = rightArr;
                        matchedCount++;
                    }
               
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxRows))
                    {
                        // If dumplicate.
                        if (configDic["MAXIMUM_NUMBER_OF_ROWS"] != null)
                        {
                            Console.WriteLine(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MAXIMUM_NUMBER_OF_ROWS"] = rightArr;
                        matchedCount++;
                    }
               
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minCols))
                    {
                        // If dumplicate.
                        if (configDic["MINIMUM_NUMBER_OF_COLUMNS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MINIMUM_NUMBER_OF_COLUMNS"] = rightArr;
                        matchedCount++;
                    }
              
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxCols))
                    {
                        // If dumplicate.
                        if (configDic["MAXIMUM_NUMBER_OF_COLUMNS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MAXIMUM_NUMBER_OF_COLUMNS"] = rightArr;
                        matchedCount++;
                    }
             
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minHor))
                    {
                        // If dumplicate.
                        if (configDic["MINIMUM_HORIZONTAL_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MINIMUM_HORIZONTAL_WORDS"] = rightArr;
                        matchedCount++;
                    }
               
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxHor))
                    {
                        // If dumplicate.
                        if (configDic["MAXIMUM_HORIZONTAL_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MAXIMUM_HORIZONTAL_WORDS"] = rightArr;
                        matchedCount++;
                    }
               
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minVer))
                    {
                        // If dumplicate.
                        if (configDic["MINIMUM_VERTICAL_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MINIMUM_VERTICAL_WORDS"] = rightArr;
                        matchedCount++;
                    }
               
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxVer))
                    {
                        // If dumplicate.
                        if (configDic["MAXIMUM_VERTICAL_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MAXIMUM_VERTICAL_WORDS"] = rightArr;
                        matchedCount++;
                    }
                
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minSameWord))
                    {
                        // If dumplicate.
                        if (configDic["MINIMUM_NUMBER_OF_THE_SAME_WORD"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MINIMUM_NUMBER_OF_THE_SAME_WORD"] = rightArr;
                        matchedCount++;
                    }
                
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxSameWord))
                    {
                        // If dumplicate.
                        if (configDic["MAXIMUM_NUMBER_OF_THE_SAME_WORD"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MAXIMUM_NUMBER_OF_THE_SAME_WORD"] = rightArr;
                        matchedCount++;
                    }
                
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minGroups))
                    {
                        // If dumplicate.
                        if (configDic["MINIMUM_NUMBER_OF_GROUPS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MINIMUM_NUMBER_OF_GROUPS"] = rightArr;
                        matchedCount++;
                    }
                
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxGroups))
                    {
                        // If dumplicate.
                        if (configDic["MAXIMUM_NUMBER_OF_GROUPS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MAXIMUM_NUMBER_OF_GROUPS"] = rightArr;
                        matchedCount++;
                    }
                
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_pointsPerWord))
                    {
                        // If dumplicate.
                        if (configDic["POINTS_PER_WORD"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["POINTS_PER_WORD"] = rightArr;
                        matchedCount++;
                    }
                
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_intersectingPoints))
                    {
                        // If dumplicate.
                        if (configDic["INTERSECTING_POINTS_PER_LETTER"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["INTERSECTING_POINTS_PER_LETTER"] = rightArr;
                        matchedCount++;
                    }
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_nonIntersectingPoints))
                    {
                        // If dumplicate.
                        if (configDic["NON_INTERSECTING_POINTS_PER_LETTER"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["NON_INTERSECTING_POINTS_PER_LETTER"] = rightArr;
                        matchedCount++;
                    }
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minHIntersections))
                    {
                        // If dumplicate.
                        if (configDic["MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] = rightArr;
                        matchedCount++;
                    }
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxHIntersections))
                    {
                        // If dumplicate.
                        if (configDic["MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] = rightArr;
                        matchedCount++;
                    }
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_minVIntersections))
                    {
                        // If dumplicate.
                        if (configDic["MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }
                        String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                        configDic["MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] = rightArr;
                        matchedCount++;
                    }
                    if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_maxVIntersections))
                    {
                        // If dumplicate.
                        if (configDic["MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] != null)
                        {
                            configTxtError.Add(timeInRow + " row is invalid(Dumplicated)\r\n");
                            isValid = false;
                            continue;
                        }

                        String rightArr=ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                    
                        configDic["MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] = rightArr;
                        matchedCount++;
                    }

                    // If notMatchedTime == 0, not matched, invalid.
                    if (matchedCount == 0)
                    {
                        configTxtError.Add(timeInRow + " row is invalid\r\n");
                        isValid = false;
                        continue;
                    }

                }
            }catch(Exception)
            {
                configTxtError.Add("Please check Configuration file path.");
            }

            // Check if every data appears.
            foreach(String key in configDic.Keys)
            {
                if(configDic[key] == null)
                {
                    configTxtError.Add("data "+ key + " is lack or invalid\r\n");
                    isValid = false;
                }
            }


            if (isValid)
            {
                configTxtError.Add("No error.\r\n");
            }
            try
            {
                String directory = Path.GetDirectoryName(Path.GetDirectoryName(path));
                logPath = directory+"/Log Files/"+configDic["LOGFILE_NAME"].Split('\"')[1];
                File.Delete(logPath);
                File.AppendAllLines(logPath, configTxtError);
            }
            catch (Exception)
            {
                ////////////////////////
            }

            return isValid;
        }

        public bool[] ValidateCrozzleText(String path)
        {
            /**
             * This method is to 
             * 1.Validate a Crozzle.txt.
             * 2.Build crozzle array.
             * 3.Calculate the score.
             * Parameter is file path.
             * Return boolean array to show if the Crozzle.txt and the crozzle is valid.
             */

            List<String> crozzleTxtError = new List<string>();
            crozzleTxtError.Add("-------------------------Crozzle.txt error.\r\n");
            List<String> crozzleError = new List<string>();
            crozzleError.Add("-------------------------Crozzle error.\r\n");

            bool txtIsValid = true;
            bool crozzleIsValid = true;

            // Total types of data.
            const int Total = 6;
            int rows = 0;
            int cols = 0;
            String configPath = null;
            String wordlistPath = null;
            List<String> wordList = new List<string>();
            List<List<Point>> horizentalWords = new List<List<Point>>();
            List<List<Point>> verticalWords = new List<List<Point>>();

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

            String[] temp = File.ReadAllLines(path);
            List<String> lines = new List<string>(temp);

            // Find configuration.txt and wordlist.txt first.
            foreach(String line in lines)
            {
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

                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_configFile))
                {

                    // If dumplicate.
                    if (dataChecker[0, 1] > 0)
                    {
                        crozzleTxtError.Add(timeInRow+ " row is invalid\r\n");
                        txtIsValid = false;
                        dataChecker[0, 1]++;
                        continue;
                    }
                    dataChecker[0, 1]++;
                    String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                    String directory=Path.GetDirectoryName(path);
                    configPath = directory+rightArr.TrimStart('\"').TrimEnd('\"');

                    continue;
                }

                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_wordlistFile))
                {
                    

                    // If dumplicate.
                    if (dataChecker[1, 1] > 0)
                    {
                        crozzleTxtError.Add(timeInRow + " row is invalid\r\n");
                        txtIsValid = false;
                        dataChecker[1, 1]++;
                        continue;
                    }
                    dataChecker[1, 1]++;
                    String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                    String directory = Path.GetDirectoryName(path);
                    wordlistPath = directory+rightArr.TrimStart('\"').TrimEnd('\"');
                    continue;
                }
            }
            timeInRow = 0;
            if (configPath == null)
            {
                crozzleTxtError.Add("Lack or invalid configuration path.\r\n");
                txtIsValid = false;
            }
            if (wordlistPath == null)
            {
                crozzleTxtError.Add("Lack or invalid wordlist path.\r\n");
                txtIsValid = false;
            }
           
            ValidateConfigText(configPath);
            ValidateWordlist(wordlistPath);

            // Second, find rows and columns.

            foreach (String line in lines)
            {
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

                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_rows))
                {
                    

                    // If dumplicate.
                    if (dataChecker[2, 1] > 0)
                    {
                        crozzleTxtError.Add(timeInRow + " row is invalid\r\n");
                        txtIsValid = false;
                        dataChecker[2, 1]++;
                        continue;
                    }
                    dataChecker[2, 1]++;
                    String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                    rows = Int32.Parse(rightArr);
                    try
                    {
                        if (rows < Int32.Parse(configDic["MINIMUM_NUMBER_OF_ROWS"]) || rows > Int32.Parse(configDic["MAXIMUM_NUMBER_OF_ROWS"]))
                        {
                            crozzleError.Add(timeInRow + " row is invalid\r\n");
                            crozzleError.Add("Rows number does not meet the demand of configuration.txt\r\n");
                            crozzleIsValid = false;
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        //////////////////
                    }
                    continue;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_cols))
                {
                    

                    // If dumplicate.
                    if (dataChecker[3, 1] > 0)
                    {
                        crozzleTxtError.Add(timeInRow+ " row is invalid\r\n");
                        txtIsValid = false;
                        dataChecker[3, 1]++;
                        continue;
                    }
                    dataChecker[3, 1]++;
                    String rightArr = ruledLine.Remove(0, ruledLine.IndexOf('=') + 1);
                    cols = Int32.Parse(rightArr);
                    try
                    {
                        if (cols < Int32.Parse(configDic["MINIMUM_NUMBER_OF_COLUMNS"]) || cols > Int32.Parse(configDic["MAXIMUM_NUMBER_OF_COLUMNS"]))
                        {
                            crozzleError.Add(timeInRow + " row is invalid\r\n");
                            crozzleError.Add("Columns number does not meet the demand of configuration.txt\r\n");
                            crozzleIsValid = false;
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        ////////////////////////
                    }
                    continue;
                }
            }
            if (rows == 0)
            {
                crozzleTxtError.Add("Lack or invalid rows.\r\n");
                txtIsValid = false;
            }
            if (cols == 0)
            {
                crozzleTxtError.Add("Lack or invalid columns.\r\n");
                txtIsValid = false;
            }
            timeInRow = 0;

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

                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_configFile))
                {
                    matchedCount++;
                }

                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_wordlistFile))
                {
                    matchedCount++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_cols))
                {
                    matchedCount++;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_rows))
                {
                    matchedCount++;
                }


                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_horizentalWords))
                {
                    matchedCount++;
                    

                    String[] str=ruledLine.Split(new char[]{ '=',','});

                    wordList.Add(str[2]);

                    List<Point> word = new List<Point>();
                    for(int i=0; i<str[2].Length;i++){
                        Point p = new Point(Int32.Parse(str[1]), Int32.Parse(str[3])+i, str[2][i]);
                        word.Add(p);
                    }
                    horizentalWords.Add(word);

                    int rightPos=str[2].Length + Int32.Parse(str[3]) - 1;
                    if (rightPos > cols)
                    {
                        crozzleError.Add(timeInRow+"row is invalid\r\n");
                        crozzleError.Add("Horizental word overstep the boundary.\r\n");
                        crozzleIsValid = false;
                        dataChecker[4, 1]++;
                        continue;
                    }
                    dataChecker[4, 1]++;
                    continue;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(ruledLine, RE_verticalWords))
                {
                    matchedCount++;

                    String[] str = ruledLine.Split(new char[] { '=', ',' });

                    wordList.Add(str[2]);

                    List<Point> word = new List<Point>();
                    for (int i = 0; i < str[2].Length; i++)
                    {
                        Point p = new Point(Int32.Parse(str[3])+i, Int32.Parse(str[1]), str[2][i]);
                        word.Add(p);
                    }
                    verticalWords.Add(word);

                    int bottomPos = str[2].Length + Int32.Parse(str[3]) - 1;
                    if (bottomPos > rows)
                    {
                        crozzleError.Add(timeInRow+" row is invalid\r\n");
                        crozzleError.Add("Vertical word overstep the boundary.\r\n");
                        crozzleIsValid = false;
                        dataChecker[5, 1]++;
                        continue;
                    }

                    dataChecker[5, 1]++;
                    continue;
                }

                // If notMatchedTime == 0, not matched, invalid.
                if (matchedCount == 0)
                {
                    crozzleTxtError.Add(timeInRow+" row is invalid\r\n");
                    txtIsValid = false;
                    continue;
                }
            }

            // Check if every data appears.
            for (int i = 2; i < Total; i++)   
            {
                if (dataChecker[i, 1] == 0)
                {
                    crozzleTxtError.Add("data "+ dataContent[i] + " is lack or invalid\r\n" );
                    txtIsValid = false;
                }
            }

            // Check horrizental words.
            try
            {
                if (dataChecker[4, 1] < Int32.Parse(configDic["MINIMUM_HORIZONTAL_WORDS"]) || dataChecker[4, 1] > Int32.Parse(configDic["MAXIMUM_HORIZONTAL_WORDS"]))
                {
                    crozzleError.Add("Horizental words number does not meet the demand of configuration.txt\r\n");
                    crozzleIsValid = false;
                }
            }
            catch (Exception)
            {
                //////////////////
            }
            // Check vertical words.
            try
            {
                if (dataChecker[4, 1] < Int32.Parse(configDic["MINIMUM_VERTICAL_WORDS"]) || dataChecker[4, 1] > Int32.Parse(configDic["MAXIMUM_VERTICAL_WORDS"]))
                {
                    crozzleError.Add("Vertical words number does not meet the demand of configuration.txt\r\n");
                    crozzleIsValid = false;
                }
            }
            catch (Exception)
            {
                ////////////////////
            }

            // Check dumplicated words meet the demand of configuration.
            try
            {
                if (SameElement(wordList)[0] < Int32.Parse(configDic["MINIMUM_NUMBER_OF_THE_SAME_WORD"]) || SameElement(wordList)[1] > Int32.Parse(configDic["MAXIMUM_NUMBER_OF_THE_SAME_WORD"]))
                {
                    crozzleError.Add("Same word number does not meet the demand of configuration.\r\n");
                    crozzleIsValid = false;
                }
            }
            catch (Exception)
            {
                ///////////////////////////
            }

            // Check if there is inside confliction.
            if (hasConflictionInside(horizentalWords))
            {
                crozzleError.Add("Has confliction in horizental words.\r\n");
                crozzleIsValid = false;
            }
            if (hasConflictionInside(verticalWords))
            {
                crozzleError.Add("Has confliction in vertical words.\r\n");
                crozzleIsValid = false;
            }

            // Check if there is outer confliction.
            if (IntersectingNum(horizentalWords, verticalWords)[2] == 1)
            {
                crozzleError.Add("Has confliction between vertical and horizental words.\r\n");
                crozzleIsValid = false;
            }

            // Check intersecting words meet the demand of configuration.
            try
            {
                if (IntersectingNum(horizentalWords, verticalWords)[0] < Int32.Parse(configDic["MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"]) || IntersectingNum(horizentalWords, verticalWords)[1] > Int32.Parse(configDic["MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"]))
                {
                    crozzleError.Add("INTERSECTIONS_IN_HORIZONTAL_WORDS does not meet the demand of configuration\r\n");
                    crozzleIsValid = false;
                }
            }
            catch (Exception) 
            {
                ///////////////
            }

            try
            {
                if (IntersectingNum(verticalWords, horizentalWords)[0] < Int32.Parse(configDic["MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"]) || IntersectingNum(verticalWords, horizentalWords)[1] > Int32.Parse(configDic["MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"]))
                {
                    crozzleError.Add("INTERSECTIONS_IN_VERTICAL_WORDS does not meet the demand of configuration\r\n");
                    crozzleIsValid = false;
                }
            }
            catch (Exception)
            {
                /////////////////////
            }

            // Check word group.
            List<Point> points = new List<Point>();
            foreach(List<Point> word in horizentalWords)
            {
                points.AddRange(word);
            }
            foreach(List<Point> word in verticalWords)
            {
                points.AddRange(word);
            }
            List<List<Point>> groups = devideIntoGroups(points);
            try
            {
                if (groups.Count < Int32.Parse(configDic["MINIMUM_NUMBER_OF_GROUPS"]) || groups.Count > Int32.Parse(configDic["MAXIMUM_NUMBER_OF_GROUPS"]))
                {
                    crozzleError.Add("Word groups number does not meet the demand of configuration\r\n");
                    crozzleIsValid = false;
                }
            }
            catch (Exception)
            {
                //////////////////////
            }

            // Calculate the score.
            // Points per word.
            try
            {
                int ppw = Int32.Parse(configDic["POINTS_PER_WORD"]);
                score += (wordList.Count * ppw);
            }
            catch (Exception)
            {
                /////////////////////
            }

            List<Point>[] gs = devideByCross(points);
            List<Point> niGroup = gs[0];
            List<Point> iGroup = gs[1];

            // Intersecting letter points dictionary
            Dictionary<char, int> ipDic = new Dictionary<char, int>();
            try
            {
                String tempStr = configDic["INTERSECTING_POINTS_PER_LETTER"];
                String[] ss = tempStr.Split(new char[] { '\"', ',' });
                for (int i = 1; i < 27; i++)
                {
                    String[] tempss = ss[i].Split('=');
                    ipDic.Add(Char.Parse(tempss[0]), Int32.Parse(tempss[1]));
                }
                foreach (Point p in iGroup)
                {
                    score += ipDic[p.Letter];
                }
            }
            catch (Exception)
            {
                /////////////////////////////////////
            }

            // Non intersecting letter points dictionary
            Dictionary<char, int> nipDic = new Dictionary<char, int>();
            try
            {
                String tempStr2 = configDic["NON_INTERSECTING_POINTS_PER_LETTER"];
                String[] ss2 = tempStr2.Split(new char[] { '\"', ',' });
                for (int i = 1; i < 27; i++)
                {
                    String[] tempss = ss2[i].Split('=');
                    nipDic.Add(Char.Parse(tempss[0]), Int32.Parse(tempss[1]));
                }
                foreach (Point p in niGroup)
                {
                    score += nipDic[p.Letter];
                }
            }
            catch (Exception)
            {
                //////////////////
            }

            
            

            // Show crozzle.
            List<Point> crozzlePoints = new List<Point>();
            crozzlePoints.AddRange(iGroup);
            crozzlePoints.AddRange(niGroup);
            crozzleGrid = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    crozzleGrid[i, j] = '\0';
                }
            }
            foreach (Point p in crozzlePoints)
            {
                // Do not display wrong points.
                if(p.Row>rows || p.Column > cols)
                {
                    continue;
                }
                else
                {
                    crozzleGrid[p.Row - 1, p.Column - 1] = p.Letter;
                }
            }            
            if (txtIsValid)
            {
                crozzleTxtError.Add("No error.\r\n");
            }
            try
            {
                File.AppendAllLines(logPath, crozzleTxtError);
            }
            catch (Exception)
            {
                ////////////////////////
            }

            if (crozzleIsValid)
            {
                crozzleError.Add("No error.\r\n");
            }
            try
            {
                File.AppendAllLines(logPath, crozzleError);
            }
            catch (Exception)
            {
                ////////////////////////////////
            }

            return new bool[] { txtIsValid, crozzleIsValid };
        }

        private static int[] SameElement(List<String> wordList)
        {
            /**
             * This method is to get the MINIMUM_NUMBER_OF_THE_SAME_WORD and MAXIMUM_NUMBER_OF_THE_SAME_WORD of wordlist.
             * Return an array containing two element. 1st is min, 2nd is max. 
             */

            if (wordList == null)
            {
                return null;
            }
            int[] nums = new int[2] { -1,-1};
            int max = 1;
            int min = wordList.Count;
            for(int i=0;i<wordList.Count;i++)
            {
                int count = 0;
                for(int j = 0; j < wordList.Count; j++)
                {
                    if (wordList[i] == wordList[j])
                    {
                        count++;
                    }
                }
                if (count > max)
                {
                    max = count;
                }
                if (count < min)
                {
                    min = count;
                }
            }
            nums[0] = min;
            nums[1] = max;

            return nums;
        }

        private static bool hasConflictionInside(List<List<Point>> words)
        {
            /**
             * To see whether or not wordsList have confliction. 
             * Confliction means different letter in the same position.
             */
             foreach(List<Point> word in words)
            {
                foreach (Point p in word)
                {
                    foreach (List<Point> otherWord in words)
                    {
                        foreach(Point otherP in otherWord)
                        {
                            if (! IsSamePoint(p, otherP))
                            {
                                if(p.Row==otherP.Row && p.Column == otherP.Column)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private static int[] IntersectingNum(List<List<Point>> wordsA, List<List<Point>> wordsB)
        {
            /**
             * This method is to get the 
             * Intersecting vertical words for each horizontal word.
             * Intersecting horizontal words for each vertical word. 
             * Return an array containing 3 element. 1st is min, 2nd is max, 3rd is hasOuterConfliction.
             */

            int max = 0;
            int min = wordsB.Count;
            int[] nums = new int[3] { -1, -1, -1 };

            foreach (List<Point> wordA in wordsA)
            {
                int count = 0;
                foreach (Point pA in wordA)
                {
                    foreach (List<Point> wordB in wordsB)
                    {
                         foreach (Point pB in wordB)
                        {
                            if (IsSamePoint(pA, pB))
                            {
                                count++;
                            }
                            // Outer confliction.
                            else if(pA.Column==pB.Column && pA.Row==pB.Row && pA.Letter != pB.Letter)
                            {
                                nums[2] = 1;
                            }
                        }
                    }
                }
                if (count > max)
                {
                    max = count;
                }
                if (count < min)
                {
                    min = count;
                }
            }
            nums[0] = min;
            nums[1] = max;
            return nums;
        }

        private static bool IsSamePoint(Point pointA, Point pointB)
        {
            /**
             * Judge if two Point object represent the same point.
             */

            if (pointA.Row == pointB.Row && pointA.Column == pointB.Column && pointA.Letter == pointB.Letter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsContigousPoint(Point pointA, Point pointB)
        {
            /**
             * Judge if two points are contigous.
             */

            if ((pointA.Row == pointB.Row && pointA.Column == pointB.Column+1)
                || (pointA.Row == pointB.Row && pointA.Column == pointB.Column - 1)
                || (pointA.Column == pointB.Column && pointA.Row == pointB.Row + 1)
                || (pointA.Column == pointB.Column && pointA.Row == pointB.Row - 1)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<List<Point>> devideIntoGroups(List<Point> points)
        {
            /**
             * This method is to devide all the points into non contigous groups.
             * 
             */
            List<List<Point>> groups = new List<List<Point>>();
            if (points==null || points.Count==0)
            {
                return groups;
            }
            List<Point> temp = new List<Point>();
            temp.AddRange(points);
            while (temp.Count != 0)
            {
                Queue<Point> group = new Queue<Point>();
                group.Enqueue(temp[0]);
                for(int i = 0; i < group.Count; i++)
                {
                    List<Point> neighbours=GrabNeighbours(group.ElementAt(i), temp);
                    foreach(Point p in neighbours)
                    {
                        group.Enqueue(p);
                    }
                }
                groups.Add(new List<Point>(group.ToArray()));
            }
            return groups;
        }

        private static List<Point> GrabNeighbours(Point p,List<Point> points)
        {
            /**
             * This method is to find and return p's neighbour and remove them from points.
             */ 
            List<Point> neighbours = new List<Point>();
            for(int i = 0; i < points.Count; i++)
            {
                if (IsSamePoint(p,points[i]))
                {
                    points.RemoveAt(i);
                    i--;
                    continue;
                }
                if (IsContigousPoint(p, points[i]))
                {
                    neighbours.Add(points[i]);
                    points.RemoveAt(i);
                    i--;
                    continue;
                }
            }
            return neighbours;
        }

        private static List<Point>[] devideByCross(List<Point> points)
        {
            /**
             * This method is to devide points into two groups:
             * 1. Intersecting points.
             * 2. NonIntersecting points.
             * Return List<Point> array. 1st Element is nonIntersecting points. 2nd is intersecting points. Empty if no intersecting points. 
             */

            List<Point> iGroup = new List<Point>();
            List<Point> niGroup;
            List<Point> temp = new List<Point>();
            temp.AddRange(points);

            for (int i = 0; i < temp.Count; i++)
            {
                for (int j = i+1; j < temp.Count; j++)
                {
                    if (IsSamePoint(temp[i], temp[j]))
                    {

                        iGroup.Add(temp[i]);
                        temp.RemoveAt(j);
                        temp.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }
            niGroup = temp;
            return new List<Point>[] { niGroup, iGroup };
        }

        private class Point
        {
            /**
                * This inner class represents point in crozzle grid.
                * 
                * Contain 3 properties.
                */
            private int row;
            private int column;
            private char letter;

            public Point(int row, int column, char letter)
            {
                this.row = row;
                this.column = column;
                this.letter = letter;
            }

            public int Row { get => row; set => row = value; }
            public int Column { get => column; set => column = value; }
            public char Letter { get => letter; set => letter = value; }
        }
    }
}

