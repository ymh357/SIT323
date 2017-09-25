using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323
{
    class SubSolutions
    {
        private List<SubSolution> solutions;
        private int groupSize;

        protected int pointsPerWord = 0;
        protected Dictionary<char, int> iwd = null; // Intersective letters points.
        protected Dictionary<char, int> niwd = null; // Non-Intersective letters points.

        public int PointsPerWord { get => pointsPerWord; set => pointsPerWord = value; }
        public Dictionary<char, int> Iwd { get => iwd; set => iwd = value; }
        public Dictionary<char, int> Niwd { get => niwd; set => niwd = value; }

        public List<SubSolution> Solutions { get => solutions; set => solutions = value; }

        public SubSolutions(List<String> wordlist, int groupSize, Dictionary<char,int> iwd, Dictionary<char, int> niwd, int pointsPerWord)
        {
            this.iwd = iwd;
            this.niwd = niwd;
            this.pointsPerWord = pointsPerWord;
            this.groupSize = groupSize;
            solutions = new List<SubSolution>();
            // Consider groupSize==2 first.
            foreach(String hWord in wordlist)
            {
                foreach(String vWord in wordlist)
                {
                    if (vWord == hWord)
                        continue;

                    int longer = hWord.Length;
                    if (vWord.Length > longer)
                        longer = vWord.Length;

                    // row: row that hWord is currently in.
                    for(int row=0; row<longer; row++)
                    {
                        // col: hWord's start point's col.
                        for(int col=0; col + hWord.Length <= longer; col++)
                        {
                            //Grid subSolution = new Grid(longer, longer);
                            //subSolution.addWord(row, col, hWord, Grid.Direction.HORIZENTAL);
                            
                            // hWord[i]: current letter in hWord.
                            for (int i = 0; i < hWord.Length; i++)
                            {
                                // vWord[j]: current letter in vWord.
                                for (int j=0; j < vWord.Length; j++)
                                {
                                    if (vWord[j] == hWord[i])
                                    {
                                        // If over the bound.
                                        if(row-j<0 || row + vWord.Length - j > longer)
                                        {
                                            continue;
                                        }
                                        SubSolution subSolution = new SubSolution(longer, longer, iwd, niwd, pointsPerWord);
                                        subSolution.AddWord(row, col, hWord, Grid.Direction.HORIZENTAL);
                                        subSolution.AddWord(row - j, i + col, vWord, Grid.Direction.VERTICAL);

                                        subSolution.SetPoints();
                                        subSolution.SetScore();
                                        solutions.Add(subSolution);
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Remove(SubSolution subSolution)
        {
            /**
             * This method is to remove all the subSolutions contain certain words.
             * */
            List<String> words = subSolution.WordList;
            List<SubSolution> temp = new List<SubSolution>();
            foreach(SubSolution sub in solutions)
            {
                foreach(String word in words)
                {
                    if (sub.WordList.Contains(word))
                    {
                        temp.Add(sub);
                        break;
                    }
                }
            }
            foreach(SubSolution sub in temp)
            {
                solutions.Remove(sub);
            }
        }

        public SubSolution GetBestSubSolution()
        {
            SubSolution bestSolution = null;
            int max = 0;
            for(int i=0;i<solutions.Count;i++)
            {
                if (max < solutions[i].Score)
                {
                    max = solutions[i].Score;
                    bestSolution = solutions[i];
                }
            }
            return bestSolution;
        }

        public void TestSolutions()
        {
            foreach(Grid subSolution in solutions)
            {
                subSolution.ShowGrid();
            }
        }
    }
}
