using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    public class GameManager
    {
        public Player Player1 { get; }
        public Player Player2 { get; }
        public Board Board { get; }

        public Player.ePlayerType CurrentTurn { get; set; }

        public GameManager(string i_NamePlayer1, int i_Size, string i_NamePlayer2 = null)
        {
            Player1 = new Player(i_Size, i_NamePlayer1, Player.ePlayerType.Player1);
            if (i_NamePlayer2 != null)
            {
                Player2 = new Player(i_Size, i_NamePlayer2, Player.ePlayerType.Player2);
            }
            else
            {
                Player2 = new Player(i_Size, i_NamePlayer2, Player.ePlayerType.Computer);
            }

            Board = new Board(i_Size);
            CurrentTurn = Player.ePlayerType.Player1;
        }

        public void InitializeGame()
        {
            initPiecesToPlayer1();
            initPiecesToPlayer2();
            putPiecesOnSquares();
        }

        public void MakeMove(Piece.Position i_FromPosition, Piece.Position i_ToPosition)
        {
            bool playerEatMove;

            if (CurrentTurn == Player.ePlayerType.Player1)
            {
                // $G$ DSN-004 (-0) Redundant code duplication.
                playerEatMove = Player1.CanEat;
                updateGameAfterMove(Player1, Player2, i_FromPosition, i_ToPosition);
                Player1.ClearPossibleMoves();
                if(playerEatMove == true)
                {
                    calcIfPieceCanStillEat(i_ToPosition);
                }
            }
            else
            {
                playerEatMove = Player2.CanEat;
                updateGameAfterMove(Player2, Player1, i_FromPosition, i_ToPosition);
                Player2.ClearPossibleMoves();
                if (playerEatMove == true)
                {
                    calcIfPieceCanStillEat(i_ToPosition);
                }
            }
        }

        public void MakeRandomMoveForCp(out Piece.Position i_FromPosition, out Piece.Position i_ToPosition)
        {
            List<Piece> possibleMovesArr = makePossibleMovesArr();
            Random random = new Random();
            int randomPieceIndex = random.Next(0, possibleMovesArr.Count);
            Piece selectedPiece = possibleMovesArr[randomPieceIndex];

            i_FromPosition = selectedPiece.PiecePosition;
            if (Player2.CanEat == true)
            {
                // $G$ DSN-004 (-0) Redundant code duplication.
                int randomPieceMove = random.Next(0, selectedPiece.EatenPossibleMoves.Count);
                i_ToPosition = selectedPiece.EatenPossibleMoves[randomPieceMove];
            }
            else
            {
                int randomPieceMove = random.Next(0, selectedPiece.RegularPossibleMoves.Count);
                i_ToPosition = selectedPiece.RegularPossibleMoves[randomPieceMove];
            }
        }

        public void UpdateTurns()
        {
            if (CurrentTurn == Player.ePlayerType.Player1)
            {
                // $G$ CSS-999 (-1) Bad practice. Why not 'if(![your condition])' ?
                if (Player1.CanEat == false)
                {
                    CurrentTurn = Player.ePlayerType.Player2;
                }
            }
            else
            {
                if (Player2.CanEat == false)
                {
                    CurrentTurn = Player.ePlayerType.Player1;
                }
            }
        }

        public bool IsMoveValid(Piece.Position i_From, Piece.Position i_To)
        {
            bool isValid = false;
            Piece piece = Board.Arr[i_From.Row, i_From.Col].PieceOnSquare;

            if (piece != null && CurrentTurn == Player.ePlayerType.Player1)
            {
                if (Player1.CanEat == true)
                {
                    foreach (Piece.Position position in piece.EatenPossibleMoves)
                    {
                        if (i_To.Row == position.Row && i_To.Col == position.Col)
                        {
                            isValid = true;
                        }
                    }
                }
                else
                {
                    foreach (Piece.Position position in piece.RegularPossibleMoves)
                    {
                        if (i_To.Row == position.Row && i_To.Col == position.Col)
                        {
                            isValid = true;
                        }
                    }
                }
            }
            else if (piece != null && CurrentTurn == Player.ePlayerType.Player2)
            {
                if (Player2.CanEat == true)
                {
                    foreach (Piece.Position position in piece.EatenPossibleMoves)
                    {
                        if (i_To.Row == position.Row && i_To.Col == position.Col)
                        {
                            isValid = true;
                        }
                    }
                }
                else
                {
                    foreach (Piece.Position position in piece.RegularPossibleMoves)
                    {
                        if (i_To.Row == position.Row && i_To.Col == position.Col)
                        {
                            isValid = true;
                        }
                    }
                }
            }

            return isValid;
        }

        public void CalcAllPossibleMoves()
        {
            if (CurrentTurn == Player.ePlayerType.Player1)
            {
                foreach(Piece piece in Player1.PieceArr)
                {
                    calcPiecePossibleMove(piece);
                }

                Player1.UpdatePieceListIfCanEat();
                
            }
            else
            {
                foreach(Piece piece in Player2.PieceArr)
                {
                    calcPiecePossibleMove(piece);
                }

                Player2.UpdatePieceListIfCanEat();
            }
        }

        public void UpdateScoreAfterGameEnd()
        {
            int ScoreFirstPlayer = 0;
            int ScoreSecondPlayer = 0;
            Player.ePlayerType playerWinner = CurrentTurn == Player.ePlayerType.Player1 ? Player.ePlayerType.Player2 : Player.ePlayerType.Player1;

            foreach (Piece piece in Player1.PieceArr)
            {
                ScoreFirstPlayer += piece.PieceType == Piece.ePieceType.Normal ? (int)Piece.ePieceValue.Normal : (int)Piece.ePieceValue.King;
            }

            foreach (Piece piece in Player2.PieceArr)
            {
                ScoreSecondPlayer += piece.PieceType == Piece.ePieceType.Normal ? (int)Piece.ePieceValue.Normal : (int)Piece.ePieceValue.King;
            }

            if (playerWinner == Player.ePlayerType.Player1)
            {
                Player1.Score += Math.Abs(ScoreFirstPlayer - ScoreSecondPlayer);
            }
            else
            {
                Player2.Score += Math.Abs(ScoreFirstPlayer - ScoreSecondPlayer);
            }
        }

        public bool IsWin()
        {
            bool isCurrentPlayerWon = true;
            Player.ePlayerType playerToCheck =
                CurrentTurn == Player.ePlayerType.Player1 ? Player.ePlayerType.Player2 : Player.ePlayerType.Player1;

            if (playerToCheck == Player.ePlayerType.Player1)
            {
                if (Player2.NumberOfPieces == 0)
                {
                    isCurrentPlayerWon = true;
                }
                else
                {
                    foreach (Piece piece in Player2.PieceArr)
                    {
                        calcPiecePossibleMove(piece);
                        if (piece.EatenPossibleMoves.Count > 0 || piece.RegularPossibleMoves.Count > 0)
                        {
                            isCurrentPlayerWon = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (Player1.NumberOfPieces == 0)
                {
                    isCurrentPlayerWon = true;
                }
                else
                {
                    foreach (Piece piece in Player1.PieceArr)
                    {
                        calcPiecePossibleMove(piece);
                        if (piece.EatenPossibleMoves.Count > 0 || piece.RegularPossibleMoves.Count > 0)
                        {
                            isCurrentPlayerWon = false;
                            break;
                        }
                    }
                }
            }
            Player1.ClearPossibleMoves();
            Player2.ClearPossibleMoves();
            
            return isCurrentPlayerWon;
        }

        public bool IsTie()
        {
            bool isPlayerOneHavePossibleMoves = false;
            bool isPlayerTwoHavePossibleMoves = false;
            bool isTie = false;

            foreach (Piece piece in Player1.PieceArr)
            {
                calcPiecePossibleMove(piece);

                if (piece.EatenPossibleMoves.Count > 0 || piece.RegularPossibleMoves.Count > 0)
                {
                    isPlayerOneHavePossibleMoves = true;
                    break;
                }
            }

            foreach (Piece piece in Player2.PieceArr)
            {
                calcPiecePossibleMove(piece);

                if (piece.EatenPossibleMoves.Count > 0 || piece.RegularPossibleMoves.Count > 0)
                {
                    isPlayerTwoHavePossibleMoves = true;
                    break;
                }
            }

            Player1.ClearPossibleMoves();
            Player2.ClearPossibleMoves();

            if (isPlayerOneHavePossibleMoves == false && isPlayerTwoHavePossibleMoves == false)
            {
                isTie = true;
            }

            return isTie;
        }

        public void ClearGame()
        {
            Player1.ClearPlayer(Board.Size);
            Player2.ClearPlayer(Board.Size);
            Board.ClearBoard();
            CurrentTurn = Player.ePlayerType.Player1;
        }

        private void calcPiecePossibleMove(Piece i_Piece)
        {
            if (i_Piece.PieceType == Piece.ePieceType.King)
            {
                insertIfMoveIsPossible(i_Piece, Piece.eMove.Up, Piece.eMove.Left);
                insertIfMoveIsPossible(i_Piece, Piece.eMove.Up, Piece.eMove.Right);
                insertIfMoveIsPossible(i_Piece, Piece.eMove.Down, Piece.eMove.Left);
                insertIfMoveIsPossible(i_Piece, Piece.eMove.Down, Piece.eMove.Right);
            }
            else
            {
                if (i_Piece.PlayerType == Player.ePlayerType.Player1)
                {
                    // $G$ DSN-004 (-0) Redundant code duplication.
                    insertIfMoveIsPossible(i_Piece, Piece.eMove.Up, Piece.eMove.Left);
                    insertIfMoveIsPossible(i_Piece, Piece.eMove.Up, Piece.eMove.Right);
                }
                else
                {
                    insertIfMoveIsPossible(i_Piece, Piece.eMove.Down, Piece.eMove.Left);
                    insertIfMoveIsPossible(i_Piece, Piece.eMove.Down, Piece.eMove.Right);
                }
            }

            if(i_Piece.EatenPossibleMoves.Count > 0)
            {
                if(i_Piece.PlayerType == Player.ePlayerType.Player1)
                {
                    Player1.CanEat = true;
                }
                else
                {
                    Player2.CanEat = true;
                }
            }
        }

        private Piece.Position calcEatenPosition(Piece.Position i_From, Piece.Position i_To)
        {
            Piece.Position newPosition = default(Piece.Position);

            if ((i_To.Row - i_From.Row) == -2)
            {
                if (i_To.Col - i_From.Col == 2)
                {
                    newPosition.Row = i_From.Row - 1;
                    newPosition.Col = i_From.Col + 1;
                }
                else
                {
                    newPosition.Row = i_From.Row - 1;
                    newPosition.Col = i_From.Col - 1;
                }
            }
            else
            {
                if (i_To.Col - i_From.Col == 2)
                {
                    newPosition.Row = i_From.Row + 1;
                    newPosition.Col = i_From.Col + 1;
                }
                else
                {
                    newPosition.Row = i_From.Row + 1;
                    newPosition.Col = i_From.Col - 1;
                }
            }

            return newPosition;
        }

        private void insertIfMoveIsPossible(Piece i_Piece, Piece.eMove i_RowDirection, Piece.eMove i_ColDirection)
        {
            Piece.Position nextPosition = new Piece.Position(
                    i_Piece.PiecePosition.Row + (int)i_RowDirection,
                    i_Piece.PiecePosition.Col + (int)i_ColDirection);

            if (Board.CheckIfPosInBoardRange(nextPosition) == true)
            {
                if (checkIfCanEat(i_Piece, nextPosition, i_RowDirection, i_ColDirection) == true)
                {
                    nextPosition.Row += (int)i_RowDirection;
                    nextPosition.Col += (int)i_ColDirection;
                    i_Piece.EatenPossibleMoves.Add(nextPosition);
                }
                else
                {
                    if(Board.Arr[nextPosition.Row, nextPosition.Col].PieceOnSquare == null)
                    {
                        i_Piece.RegularPossibleMoves.Add(nextPosition);
                    }
                }
            }
        }

        private void updateGameAfterMove(Player i_CurrentPlayer, Player i_EnemyPlayer, Piece.Position i_FromPosition, Piece.Position i_ToPosition)
        {
            Piece piece = Board.Arr[i_FromPosition.Row, i_FromPosition.Col].PieceOnSquare;
            Piece.Position eatenPosition;

            if (i_CurrentPlayer.CanEat == true)
            {
                eatenPosition = calcEatenPosition(i_FromPosition, i_ToPosition);
                Board.UpdateBoardAfterMove(i_FromPosition, i_ToPosition, eatenPosition);
                i_EnemyPlayer.UpdatePlayerAfterEatMove(eatenPosition);
            }
            else
            {
                Board.UpdateBoardAfterMove(i_FromPosition, i_ToPosition, null);
            }

            piece.UpdateIfKing(Board.Size);
        }

        private bool checkIfCanEat(Piece i_Piece, Piece.Position i_NextPosition, Piece.eMove i_RowDirection, Piece.eMove i_ColDirection)
        {
            bool canEat = false;
            Piece.Position nextNextPos = new Piece.Position(i_NextPosition.Row + (int)i_RowDirection, i_NextPosition.Col + (int)i_ColDirection);

            if (Board.CheckIfPosInBoardRange(nextNextPos) == true)
            {
                if (Board.Arr[i_NextPosition.Row, i_NextPosition.Col].PieceOnSquare != null && i_Piece.PlayerType != Board.Arr[i_NextPosition.Row, i_NextPosition.Col].PieceOnSquare.PlayerType)
                {
                    if (Board.Arr[nextNextPos.Row, nextNextPos.Col].PieceOnSquare == null)
                    {
                        canEat = true;
                    }
                }
            }

            return canEat;
        }

        private List<Piece> makePossibleMovesArr()
        {
            List<Piece> possibleMovesArr = new List<Piece>();

            if (Player2.CanEat == true)
            {
                // $G$ DSN-004 (-0) Redundant code duplication.
                foreach (Piece piece in Player2.PieceArr)
                {
                    if (piece.EatenPossibleMoves.Count > 0)
                    {
                        possibleMovesArr.Add(piece);
                    }
                }
            }
            else
            {
                foreach (Piece piece in Player2.PieceArr)
                {
                    if (piece.RegularPossibleMoves.Count > 0)
                    {
                        possibleMovesArr.Add(piece);
                    }
                }
            }

            return possibleMovesArr;
        }

        private void calcIfPieceCanStillEat(Piece.Position i_ToPosition)
        {
            Piece pieceToCheck = Board.Arr[i_ToPosition.Row, i_ToPosition.Col].PieceOnSquare;

            calcPiecePossibleMove(pieceToCheck);
            pieceToCheck.RegularPossibleMoves.Clear();
        }
        // $G$ DSN-004 (-0) Redundant code duplication - The method are very similar
        private void initPiecesToPlayer1()
        {
            int numRows = (Board.Size / 2) - 1;
            Player1.PieceArr = new List<Piece>(Player1.NumberOfPieces);

            for(int row = Board.Size - 1; row >= Board.Size - numRows; row--)
            {
                for(int col = 0; col < Board.Size; col++)
                {
                    if (Board.Arr[row, col].Color == Square.eSquareColor.Black)
                    {
                        Player1.PieceArr.Add(new Piece(Player1.PlayerType, new Piece.Position(row, col)));
                    }
                }
            }
        }

        private void initPiecesToPlayer2()
        {
            int numRows = (Board.Size / 2) - 1;
            Player2.PieceArr = new List<Piece>(Player2.NumberOfPieces);

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < Board.Size; col++)
                {
                    if (Board.Arr[row, col].Color == Square.eSquareColor.Black)
                    {
                        Player2.PieceArr.Add(new Piece(Player2.PlayerType, new Piece.Position(row, col)));
                    }
                }
            }
        }

        private void putPiecesOnSquares()
        {
            foreach(Piece piece in Player1.PieceArr)
            {
                Board.Arr[piece.PiecePosition.Row, piece.PiecePosition.Col].PieceOnSquare = piece;
            }

            foreach (Piece piece in Player2.PieceArr)
            {
                Board.Arr[piece.PiecePosition.Row, piece.PiecePosition.Col].PieceOnSquare = piece;
            }
        }
    }
}
