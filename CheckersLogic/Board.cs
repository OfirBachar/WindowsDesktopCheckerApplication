namespace CheckersLogic
{
    public class Board
    {
        // $G$ CSS-000 (-1) The variable names are not meaningful and understandable.
        public Square[,] Arr { get; set; }

        public int Size { get; }

        public Board(int i_Size)
        {
            Size = i_Size;
            Arr = new Square[i_Size, i_Size];
            for (int i = 0; i < i_Size; i++)
            {
                for (int j = 0; j < i_Size; j++)
                {
                    Arr[i, j] = new Square(i, j);
                }
            }
        }

        public void UpdateBoardAfterMove(Piece.Position i_From, Piece.Position i_To, Piece.Position? i_EatenPosition)
        {
            if (i_EatenPosition.HasValue)
            {
                Arr[i_EatenPosition.Value.Row, i_EatenPosition.Value.Col].PieceOnSquare = null;
            }

            Arr[i_From.Row, i_From.Col].PieceOnSquare.PiecePosition = i_To;
            Arr[i_To.Row, i_To.Col].PieceOnSquare = Arr[i_From.Row, i_From.Col].PieceOnSquare;
            Arr[i_From.Row, i_From.Col].PieceOnSquare = null;
        }

        public bool CheckIfPosInBoardRange(Piece.Position i_Position)
        {
            bool isInRange = false;

            if (i_Position.Row < Size && i_Position.Row >= 0)
            {
                if (i_Position.Col < Size && i_Position.Col >= 0)
                {
                    isInRange = true;
                }
            }

            return isInRange;
        }


        public void ClearBoard()
        {
            Arr = new Square[Size, Size]; 
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Arr[i, j] = new Square(i, j);
                }
            }
        }
    }
}
