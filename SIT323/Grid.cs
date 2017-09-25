using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323
{
    class Grid
    {
        protected int row;
        protected int col;
        protected List<String> wordList;
        protected char[,] arrayGrid;

        protected int score = 0;
        protected int pointsPerWord=0;
        protected Dictionary<char, int> iwd = null; // Intersective letters points.
        protected Dictionary<char, int> niwd = null; // Non-Intersective letters points.

        public int PointsPerWord { get => pointsPerWord; set => pointsPerWord = value; }
        public Dictionary<char, int> Iwd { get => iwd; set => iwd = value; }
        public Dictionary<char, int> Niwd { get => niwd; set => niwd = value; }
        public int Score { get => score; set => score = value; }
        public char[,] ArrayGrid { get => arrayGrid; set => arrayGrid = value; }
        public List<string> WordList { get => wordList; set => wordList = value; }

        public enum Direction
        {
            HORIZENTAL,
            VERTICAL
        };

        public Grid(int row, int col, Dictionary<char, int> iwd, Dictionary<char, int> niwd, int pointsPerWord)
        {
            wordList = new List<string>();
            this.iwd = iwd;
            this.niwd = niwd;
            this.pointsPerWord = pointsPerWord;
            this.row = row;
            this.col = col;
            arrayGrid = new char[row,col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    arrayGrid[i, j] = ' ';
                }
            }
        }

        public void SetScore()
        {
            score = 0;
            score += (GetWordNumber() * pointsPerWord);
            for(int r = 0; r < row; r++)
            {
                for(int c = 0; c < col; c++)
                {
                    if(arrayGrid[r,c]!=' ')
                    {
                        if (IsIntersective(r, c))
                        {
                            score += iwd[arrayGrid[r, c]];
                        }
                        else
                        {
                            score += niwd[arrayGrid[r, c]];
                        }
                    }
                }
            }
        }

        protected int GetWordNumber()
        {
            /**
             * This method is to get words quantities of the grid.
             * */

            int count = 0;

            // Horizental:
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    int continuity = 0;
                    for (int i = 0; i + c < col; i++)
                    {
                        if(arrayGrid[r,c+i]!=' ')
                        {
                            continuity++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (continuity > 1)
                    {
                        count++;
                    }
                    c += continuity;
                }
            }

            // Vertical:
             for (int c = 0; c < col; c++)
             {
                for (int r = 0; r < row; r++)
                {
                    int continuity = 0;
                    for (int i = 0; i + r < row; i++)
                    {
                        if (arrayGrid[r+i, c] != ' ')
                        {
                            continuity++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (continuity > 1)
                    {
                        count++;
                    }
                    r += continuity;
                }
             }

            return count;
        }

        protected bool IsIntersective(int r,int c)
        {
            int hCounter = 0;
            int vCounter = 0;

            if (r-1>=0 && arrayGrid[r-1,c]!=' ')
            {
                vCounter++;
            }
            if (r + 1 < row && arrayGrid[r + 1, c] != ' ')
            {
                vCounter++;
            }
            if (c - 1 >= 0 && arrayGrid[r, c-1] != ' ')
            {
                hCounter++;
            }
            if (c+1 < col && arrayGrid[r, c+1] != ' ')
            {
                hCounter++;
            }
            
            if(hCounter >= 1 && vCounter >= 1)
            {
                return true;
            }
            return false;
        }

        public bool AddWord(int r,int c, String word, Direction direction)
        {
            /**
             *  This function is to add one word to the grid.
             *  Para: 
             *  r => start point's row. 
             *  c => start point's col.
             *  word: word to be added.
             *  direction: how word be put.
             *  Return whether or not successful.
             */

            // Check if the word can be added.
            int wordLength = word.Length;
            if (r < 0 || c < 0)
                return false;
            if (direction == Direction.HORIZENTAL && c + wordLength > col)
                return false;
            if (direction == Direction.VERTICAL && r + wordLength > row)
                return false;

            for(int i = 0; i < word.Length; i++)
            {
                if(direction == Direction.HORIZENTAL)
                {
                    arrayGrid[r, c+i] = word[i];
                }
                if (direction == Direction.VERTICAL)
                {
                    arrayGrid[r+i, c] = word[i];
                }
            }
            wordList.Add(word);
            SetScore();
            return true;
        }

        public List<Grid> Insert(SubSolution subSolution)
        {
            /**
             *  This method is to insert a subSolution to the gird.
             *  Return different grids in different ways of insertion.
             * */

            List<Grid> grids = null;

            Dictionary<char, int> temp = new Dictionary<char, int>();
            foreach(char key in iwd.Keys)
            {
                temp.Add(key, iwd[key]);
            }

            bool gridIsEmpty = true;
            bool inserted = false;      // When "word group" == 1, no need to consider insert into the space.

            // First, find the letter which has the maximize weight in the gird. Try to insert, if ok return. If not, then find the second maximize weight point. Try to insert...
            while (temp.Count > 0)
            {
                int max = 0;
                char letter = '\0';
                foreach (char key in temp.Keys)
                {
                    if (max < temp[key])
                    {
                        max = temp[key];
                        letter = key;
                    }
                }
                if (letter != '\0')
                {
                    temp.Remove(letter);
                }

                if (GetPointsByLetter(letter) == null)
                {
                    continue;
                }
                gridIsEmpty = false;
                foreach (Point gridP in GetPointsByLetter(letter))
                {
                    // Following code is similar as GetPointByLetter. 
                    foreach(Point p in subSolution.Points)
                    {
                        // Copyt grid to tempGrid.
                        Grid tempGrid = new Grid(row, col,iwd,niwd,pointsPerWord);
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                tempGrid.arrayGrid[i, j] = arrayGrid[i, j];
                            }
                        }
                        List<String> wl = new List<string>();
                        wl.AddRange(wordList);
                        tempGrid.Score = score;
                        tempGrid.PointsPerWord = pointsPerWord;
                        tempGrid.WordList = wl;

                        if (letter == p.Letter)
                        {
                            Point isp = new Point(p.Row,p.Column,letter);
                            if (CanBeInserted(gridP.Row, gridP.Column, isp, subSolution))
                            {
                                // Body of insertion.
                                // Have not concerned first time to insert.
                                foreach (Point solutionP in subSolution.Points)
                                {
                                    int row_in_grid = gridP.Row + (solutionP.Row - isp.Row);
                                    int col_in_grid = gridP.Column + (solutionP.Column - isp.Column);
                                    tempGrid.arrayGrid[row_in_grid, col_in_grid] = solutionP.Letter;
                                }

                                tempGrid.SetScore();
                                foreach(String word in subSolution.WordList)
                                {
                                    if (!tempGrid.WordList.Contains(word))
                                    {
                                        tempGrid.WordList.Add(word);
                                    }
                                }

                                if (grids == null)
                                {
                                    grids = new List<Grid>();
                                }
                                grids.Add(tempGrid);
                                inserted = true;
                            }
                        }
                    }
                }
                if (inserted)
                {
                    break;
                }
            }

            if (gridIsEmpty)
            {
                for(int r = 0; r < row; r++)
                {
                    for(int c = 0; c < col; c++)
                    {
                        ///////////////////
                        Point p = subSolution.Points[0];
                        Grid tempGrid = new Grid(row, col, iwd, niwd, pointsPerWord);
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                tempGrid.arrayGrid[i, j] = arrayGrid[i, j];
                            }
                        }

                        List<String> wl = new List<string>();
                        wl.AddRange(wordList);
                        tempGrid.Score = score;
                        tempGrid.PointsPerWord = pointsPerWord;
                        tempGrid.WordList = wl;

                        Point isp = new Point(p.Row, p.Column, p.Letter);
                        if (CanBeInserted(r, c, isp, subSolution))
                        {
                            // Body of insertion.
                            // First time to insert.
                            foreach (Point solutionP in subSolution.Points)
                            {
                                int row_in_grid = r + (solutionP.Row - isp.Row);
                                int col_in_grid = c + (solutionP.Column - isp.Column);
                                tempGrid.arrayGrid[row_in_grid, col_in_grid] = solutionP.Letter;
                            }

                            tempGrid.SetScore();
                            foreach (String word in subSolution.WordList)
                            {
                                if (!tempGrid.WordList.Contains(word))
                                {
                                    tempGrid.WordList.Add(word);
                                }
                            }

                            if (grids == null)
                            {
                                grids = new List<Grid>();
                            }
                            grids.Add(tempGrid);
                            inserted = true;
                        }
                    }
                }
            }

            return grids;
        }

        private List<Point> GetPointsByLetter(char letter)
        {
            /**
             * This method is to get points(in grid) by letter.
             * */
            List<Point> points = null;
            for(int r = 0; r < row; r++)
            {
                for(int c = 0; c < col; c++)
                {
                    if (arrayGrid[r, c] == letter)
                    {
                        if (points == null)
                        {
                            points = new List<Point>();
                        }
                        points.Add(new Point(r, c, letter));
                    }
                }
            }
            return points;
        }

        private bool CanBeInserted(int r, int c, Point isp,SubSolution subSolution)
        {
            /**
             * r and c are grid's row and col.
             * isp 's col and row are in terms of subSolution.
             * This method is to check if the solution can be inserted to the grid with intersective Point isp.
             * */

            // grid_isp 's col and row are in terms of grid.
            Point grid_isp = new Point(r, c, isp.Letter);

            foreach (Point p in subSolution.Points)
            {
                if (p.IsSamePointAs(isp))
                {
                    continue;
                }
                else
                {
                    int row_in_grid = r + (p.Row - isp.Row);
                    int col_in_grid = c + (p.Column - isp.Column);
                    
                    // If over the bound.
                    if(row_in_grid < 0 || col_in_grid < 0 || row_in_grid >= row || col_in_grid >= col)
                    {
                        return false;
                    }

                    // Should not overlap existing letters.
                    if( arrayGrid[row_in_grid,col_in_grid]!=' ')
                    {
                        return false;
                    }

                    Point p_inGrid = new Point(row_in_grid, col_in_grid, p.Letter);
                    
                    if (p.IsContigousPointAs(isp))
                    {                    
                        // Allow 1 because they are contigous as intersective Point.
                        if (CountSurround(p_inGrid) > 1){
                            return false;
                        }

                        // p should not be as the orginal direction. For example:
                        // 
                        // b        should not add 'act' like   b       because 'bact' is not valid word.
                        // ac                                   ac
                        //                                      c
                        //                                      t 

                        if (IsByDirection(p_inGrid, grid_isp))
                        {
                            return false;
                        }

                    }
                    else
                    {
                        // Should not affect other words.
                        if (CountSurround(p_inGrid) > 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool IsByDirection(Point contigousP, Point grid_isp)
        {
            /**
             * contigousP 's col and row is interms of grid.
             * This method is to check situation like:
             *  b        should not add 'act' like   b       because 'bact' is not valid word.
             *  ac                                   ac
             *                                       c
             *                                       t         
             */

            if (contigousP.Column==grid_isp.Column-1)
            {
                if (grid_isp.Column + 1 < col && arrayGrid[grid_isp.Row, grid_isp.Column + 1]!=' ')
                {
                    return true;
                }
            }
            else if (contigousP.Column == grid_isp.Column + 1)
            {
                if (grid_isp.Column - 1 >= 0 &&  arrayGrid[grid_isp.Row, grid_isp.Column - 1] != ' ')
                {
                    return true;
                }
            }
            else if (contigousP.Row == grid_isp.Row - 1)
            {
                if (grid_isp.Row + 1 < row && arrayGrid[grid_isp.Row+1, grid_isp.Column] != ' ')
                {
                    return true;
                }
            }
            else if (contigousP.Row == grid_isp.Row + 1)
            {
                if (grid_isp.Row - 1 >= 0 && arrayGrid[grid_isp.Row-1, grid_isp.Column] != ' ')
                {
                    return true;
                }
            }
            return false;
        }

        private int CountSurround(Point p)
        {
            /**
             * p is in terms of grid.
             * Count point p's surroud existing letters.
             * */
            int count = 0;
            if(p.Column+1<col && arrayGrid[p.Row,p.Column + 1]!=' ')
            {
                count++;
            }
            if (p.Column-1>=0 &&  arrayGrid[p.Row, p.Column - 1] != ' '){
                count++;
            }
            if (p.Row + 1 < row && arrayGrid[p.Row+1,p.Column] != ' '){
                count++;
            }
            if (p.Row - 1 >= 0 && arrayGrid[p.Row-1,p.Column] != ' '){
                count++;
            }
                

            return count;
        }

        public void ShowGrid()
        {
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    Console.Write(arrayGrid[i, j]);
                }
                Console.Write("\n");
            }
            Console.Write("--------Split line---------\n");
        }
    }
}
