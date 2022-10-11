using System.Collections.Generic;

namespace CheckersLogic
{
    public class Piece
    {
        public ePieceType PieceType { get; set; }

        public Player.ePlayerType PlayerType { get; set; }

        public Position PiecePosition { get; set; }

        public List<Position> RegularPossibleMoves { get; set; }

        public List<Position> EatenPossibleMoves { get; set; }
        // $G$ CSS-999 (-0) Every Class/Enum which is not nested should be in a separate file.
        public enum ePieceType
        {
            Normal = 0,
            King = 1,
        }

        public enum eMove
        {
            Up = -1,
            Down = 1,
            Left = -1,
            Right = 1,
        }

        public enum ePieceValue
        {
            Normal = 1,
            King = 4,
        }

        public struct Position
        {
            public int Row { get; set; }

            public int Col { get; set; }

            public Position(int i_Row, int i_Col)
            {
                Row = i_Row;
                Col = i_Col;
            }
        }
        public Piece(Player.ePlayerType i_PlayerType, Position i_Position)
        {
            PieceType = ePieceType.Normal;
            PlayerType = i_PlayerType;
            PiecePosition = i_Position;
            EatenPossibleMoves = new List<Position>();
            RegularPossibleMoves = new List<Position>();
        }

        public void UpdateIfKing(int i_BoardSize)
        {
            if((PiecePosition.Row == 0 && PlayerType == Player.ePlayerType.Player1) ||
               (PiecePosition.Row == (i_BoardSize - 1) && PlayerType == Player.ePlayerType.Player2) ||
               (PiecePosition.Row == (i_BoardSize - 1) && PlayerType == Player.ePlayerType.Computer))
            {
                PieceType = ePieceType.King;
            }
        }

        public void ClearLists()
        {
            EatenPossibleMoves.Clear();
            RegularPossibleMoves.Clear();
        }
    }
}
