using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323
{
    class Point
    {
        /**
                * 
                * Contain 3 properties.
                */
        private int row;
        private int column;
        private char letter;

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }
        public char Letter { get => letter; set => letter = value; }

        public Point(int row, int column, char letter)
        {
            this.row = row;
            this.column = column;
            this.letter = letter;
        }

        public bool IsSamePointAs(Point pointB)
        { 
            /**
             * Judge if two Point object represent the same point.
             */

            if (row == pointB.Row && column == pointB.Column && letter == pointB.Letter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsContigousPointAs(Point pointB)
        {
            /**
             * Judge if two points are contigous.
             */

            if ((row == pointB.Row && column == pointB.Column + 1)
                || (row == pointB.Row && column == pointB.Column - 1)
                || (column == pointB.Column && row == pointB.Row + 1)
                || (column == pointB.Column && row == pointB.Row - 1)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
