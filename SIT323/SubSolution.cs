using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323
{
    class SubSolution : Grid
    {
        private List<Point> points;

        public SubSolution(int row, int col,Dictionary<char,int> iwd, Dictionary<char, int> niwd,int ppw) : base(row,col, iwd, niwd, ppw)
        {
            points = new List<Point>();
            
        }

        public List<Point> Points { get => points;}

        public void SetPoints()
        {
            for(int r = 0; r < row; r++)
            {
                for(int c = 0; c < col; c++)
                {
                    if(arrayGrid[r,c]!=' ')
                    {
                        points.Add(new Point(r,c, arrayGrid[r, c]));
                    }
                }
            }
        }
    }
}
