using System.Collections.Generic;


namespace CheckersLogic
{
    public class Player
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public ePlayerType PlayerType { get; set; }

        public int NumberOfPieces { get; set; }

        public List<Piece> PieceArr { get; set; }

        public bool CanEat { get; set; }

        // $G$ CSS-999 (-0) Every Class/Enum which is not nested should be in a separate file.
        public enum ePlayerType
        {
            Player1 = 0,
            Player2 = 1,
            Computer = 2,
        }

        public Player(int i_BoardSize, string i_Name, ePlayerType i_Type)
        {
            Score = 0;
            NumberOfPieces = ((i_BoardSize / 2) - 1) * (i_BoardSize / 2);
            Name = i_Name;
            PlayerType = i_Type;
            CanEat = false;
            PieceArr = null;
        }

        public void ClearPossibleMoves()
        {
            foreach(Piece piece in PieceArr)
            {
                piece.ClearLists();
            }

            CanEat = false;
        }

        public void UpdatePlayerAfterEatMove(Piece.Position i_PieceRemovePosition)
        {
            int indexInPieceArr = 0;

            foreach (Piece piece in PieceArr)
            {
                if (piece.PiecePosition.Row == i_PieceRemovePosition.Row && piece.PiecePosition.Col == i_PieceRemovePosition.Col)
                {
                    PieceArr.RemoveAt(indexInPieceArr);
                    NumberOfPieces--;
                    break;
                }

                indexInPieceArr += 1;
            }
        }

        public void UpdatePieceListIfCanEat()
        {
            if (CanEat == true)
            {
                foreach (Piece piece in PieceArr)
                {
                    piece.RegularPossibleMoves.Clear();
                }
            }
        }

        public void ClearPlayer(int i_BoardSize)
        {
            NumberOfPieces = ((i_BoardSize / 2) - 1) * (i_BoardSize / 2);
            PieceArr = null;
            CanEat = false;
        }
    }
}
