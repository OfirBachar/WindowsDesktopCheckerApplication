using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace CheckersLogic
{
    public class Square
    {
        public eSquareColor Color { get; set; }

        public Piece PieceOnSquare { get; set; }
        // $G$ CSS-999 (-0) Every Class/Enum which is not nested should be in a separate file.
        public enum eSquareColor
        {
            Black = 0,
            White = 1,
        }

        public Square(int i_Row, int i_Column)
        {
            Color = calcColorSquare(i_Row, i_Column);
            PieceOnSquare = null;
        }

        private eSquareColor calcColorSquare(int i_Row, int i_Column)
        {
            eSquareColor squareColor;

            if (i_Row % 2 == 0)
            {
                squareColor = i_Column % 2 == 1 ? eSquareColor.Black : eSquareColor.White;
            }
            else
            {
                squareColor = i_Column % 2 == 0 ? eSquareColor.Black : eSquareColor.White;
            }

            return squareColor;
        }
    }
}
