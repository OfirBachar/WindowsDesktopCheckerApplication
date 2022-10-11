using CheckersLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace Checkers
{
    public class WindowGame : Form
    {
        private PictureBoxIndex m_SelectedPiece;
        private readonly PictureBoxIndex[] r_PreviousMovePictureBox;
        private PictureBoxIndex[,] m_Board;
        private GameManager m_GameManager;
        private Label m_LabelTurn;
        private readonly Timer r_Timer;
        private Label labelScorePlayer2;
        private Label labelScorePlayer1;
        private readonly List<Piece.Position> r_LastMarkedPositions;

        public WindowGame(string i_PlayerOneName, string i_PlayerTwoName, int i_Size)
        {
            m_SelectedPiece = new PictureBoxIndex();
            r_LastMarkedPositions = new List<Piece.Position>();
            r_PreviousMovePictureBox = new PictureBoxIndex[2];
            r_Timer = new Timer();

            makeGameVsPlayerOrPc(i_PlayerOneName, i_PlayerTwoName, i_Size);
            m_GameManager.InitializeGame();
            InitializeComponent();
            initBoardUi();
            drawBoard();
        }

        private void makeGameVsPlayerOrPc(string i_PlayerOneName, string i_PlayerTwoName, int i_Size)
        {
            if (i_PlayerTwoName == null)
            {
                m_GameManager = new GameManager(i_PlayerOneName, i_Size);
                m_GameManager.Player2.Name = "Computer";
                r_Timer.Interval = 1000;
                r_Timer.Tick += M_Timer_Tick;
            }
            else
            {
                m_GameManager = new GameManager(i_PlayerOneName, i_Size, i_PlayerTwoName);
            }
        }

        private void M_Timer_Tick(object sender, EventArgs e)
        {
            makePcMove();
            drawBoard();

            if (m_GameManager.CurrentTurn == Player.ePlayerType.Player1)
            {
                foreach(PictureBoxIndex pictureBoxIndex in m_Board)
                {
                    pictureBoxIndex.Enabled = true;
                }
                r_Timer.Stop();
                StartTurnLoop();
            }
        }

        private void moveOccurred(Piece.Position i_FromPosition, Piece.Position i_ToPosition)
        {
            m_GameManager.MakeMove(i_FromPosition, i_ToPosition);
            m_GameManager.UpdateTurns();
            addLastMove(m_Board[i_ToPosition.Row, i_ToPosition.Col]);
            m_SelectedPiece = null;
        }

        private void makePcMove()
        {
            Piece.Position fromPictureBox;
            Piece.Position toPictureBox;
            m_GameManager.MakeRandomMoveForCp(out fromPictureBox, out toPictureBox);
            m_SelectedPiece = m_Board[fromPictureBox.Row, fromPictureBox.Col];
            moveOccurred(fromPictureBox, toPictureBox);
        }

        public void StartTurnLoop()
        {
            if (m_GameManager.IsTie())
            {
                showWhenTie();
            }

            if (m_GameManager.IsWin())
            {
                showWhenWin();
            }

            m_GameManager.CalcAllPossibleMoves();

            if (m_GameManager.CurrentTurn == Player.ePlayerType.Player2
                && m_GameManager.Player2.PlayerType == Player.ePlayerType.Computer)
            {
                foreach(PictureBoxIndex pictureBoxIndex in m_Board)
                {
                    pictureBoxIndex.Enabled = false;
                }
                r_Timer.Start();
            }

            drawBoard();
        }

        private void showWhenWin()
        {
            drawBoard();

            string PlayerNameWon = m_GameManager.CurrentTurn == Player.ePlayerType.Player1 ? m_GameManager.Player2.Name : m_GameManager.Player1.Name;

            DialogResult resultOfRetry = MessageBox.Show(
                $"{PlayerNameWon} Won! {System.Environment.NewLine}Another Round?",
                "Damka",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultOfRetry == DialogResult.Yes)
            {
                startNewGame();
            }
            else
            {
                this.Close();
            }
        }

        private void startNewGame()
        {
            m_GameManager.UpdateScoreAfterGameEnd();
            m_GameManager.ClearGame();
            m_GameManager.InitializeGame();
            showScore();
            m_SelectedPiece = null;

            for(int i = 0; i < r_PreviousMovePictureBox.Length; i++)
            {
                r_PreviousMovePictureBox[i] = null;
            }
        }

        private void showWhenTie()
        {
            drawBoard();
            DialogResult resultOfRetry = MessageBox.Show(
                $"Tie! {System.Environment.NewLine}Another Round?",
                "Damka",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultOfRetry == DialogResult.Yes)
            {
                m_GameManager.UpdateScoreAfterGameEnd();
                m_GameManager.ClearGame();
                m_GameManager.InitializeGame();
                showScore();
            }
            else
            {
                this.Close();
            }
        }

        private void InitializeComponent()
        {
            this.m_LabelTurn = new System.Windows.Forms.Label();
            this.labelScorePlayer2 = new System.Windows.Forms.Label();
            this.labelScorePlayer1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_LabelTurn
            // 
            this.m_LabelTurn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_LabelTurn.AutoSize = true;
            this.m_LabelTurn.Font = new System.Drawing.Font("Rockwell Nova", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_LabelTurn.Location = new System.Drawing.Point(12, 497);
            this.m_LabelTurn.Name = "m_LabelTurn";
            this.m_LabelTurn.Size = new System.Drawing.Size(191, 38);
            this.m_LabelTurn.TabIndex = 0;
            this.m_LabelTurn.Text = "Player Turn:";
            // 
            // labelScorePlayer2
            // 
            this.labelScorePlayer2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScorePlayer2.AutoSize = true;
            this.labelScorePlayer2.Font = new System.Drawing.Font("Rockwell Nova", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScorePlayer2.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelScorePlayer2.Location = new System.Drawing.Point(305, 9);
            this.labelScorePlayer2.Name = "labelScorePlayer2";
            this.labelScorePlayer2.Size = new System.Drawing.Size(191, 38);
            this.labelScorePlayer2.TabIndex = 1;
            this.labelScorePlayer2.Text = "Player Turn:";
            // 
            // labelScorePlayer1
            // 
            this.labelScorePlayer1.AutoSize = true;
            this.labelScorePlayer1.Font = new System.Drawing.Font("Rockwell Nova", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScorePlayer1.Location = new System.Drawing.Point(12, 9);
            this.labelScorePlayer1.Name = "labelScorePlayer1";
            this.labelScorePlayer1.Size = new System.Drawing.Size(191, 38);
            this.labelScorePlayer1.TabIndex = 2;
            this.labelScorePlayer1.Text = "Player Turn:";
            // 
            // WindowGame
            // 
            this.ClientSize = new System.Drawing.Size(508, 544);
            this.Controls.Add(this.labelScorePlayer1);
            this.Controls.Add(this.labelScorePlayer2);
            this.Controls.Add(this.m_LabelTurn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WindowGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkers Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void initBoardUi()
        {
            showScore();
            m_GameManager.CalcAllPossibleMoves();

            m_Board = new PictureBoxIndex[m_GameManager.Board.Size, m_GameManager.Board.Size];
            int i, j;
            int left = 0;
            int top = 40;
            
            for (i = 0; i < m_GameManager.Board.Size; i++)
            {
                left = 10;

                for (j = 0; j < m_GameManager.Board.Size; j++)
                {
                    m_Board[i, j] = new PictureBoxIndex();
                    m_Board[i, j].BackColor = m_GameManager.Board.Arr[i, j].Color == Square.eSquareColor.Black ? Color.Black : Color.White;
                    m_Board[i, j].Row = i;
                    m_Board[i, j].Col = j;
                    m_Board[i, j].Size = new Size(60, 60);
                    m_Board[i, j].Location = new Point(left, top);
                    m_Board[i, j].SizeMode = PictureBoxSizeMode.StretchImage;

                    if(m_Board[i, j].BackColor == Color.Black)
                    {
                        m_Board[i, j].MouseClick += WindowGame_MouseClick;
                        m_Board[i, j].MouseEnter += WindowGame_MouseEnter;
                        m_Board[i, j].MouseLeave += WindowGame_MouseLeave;
                    }
                    else
                    {
                        m_Board[i, j].Enabled = false;
                    }
                    
                    Controls.Add(m_Board[i, j]);
                    left += 60;
                }

                top += 60;
            }

            this.ClientSize = new Size(left + 10, top + 50);
        }

        private void showScore()
        {
            labelScorePlayer1.Text = string.Format($"{m_GameManager.Player1.Name}: {m_GameManager.Player1.Score}");
            labelScorePlayer2.Text = string.Format($"{m_GameManager.Player2.Name}: {m_GameManager.Player2.Score}");
        }

        private void drawBoard()
        {
            showNameOfCurrentPlayerTurn();
            setImagesInBoard();

            if(m_SelectedPiece != null)
            {
                m_SelectedPiece.BackColor = Color.Blue;
            }

            foreach (Piece.Position lastMarkedPosition in r_LastMarkedPositions)
            {
                m_Board[lastMarkedPosition.Row, lastMarkedPosition.Col].Image =
                    Checkers.Properties.Resources.Marked;
            }

            if(r_PreviousMovePictureBox[0] != null)
            {
                r_PreviousMovePictureBox[0].BackColor = Color.FromArgb(90, Color.Yellow);
                r_PreviousMovePictureBox[1].BackColor = Color.FromArgb(200, Color.Yellow);
            }
        }

        private void setImagesInBoard()
        {
            for(int rowIndex = 0; rowIndex < m_GameManager.Board.Size; rowIndex++)
            {
                for(int colIndex = 0; colIndex < m_GameManager.Board.Size; colIndex++)
                {
                    m_Board[rowIndex, colIndex].BackColor = m_GameManager.Board.Arr[rowIndex, colIndex].Color == Square.eSquareColor.Black ? Color.Black : Color.White;

                    if (m_GameManager.Board.Arr[rowIndex, colIndex].PieceOnSquare != null)
                    {
                        Piece piece = m_GameManager.Board.Arr[rowIndex, colIndex].PieceOnSquare;
                        if (piece.PlayerType == Player.ePlayerType.Player1)
                        {
                            m_Board[rowIndex, colIndex].Image = piece.PieceType == Piece.ePieceType.Normal ? Checkers.Properties.Resources.b : Checkers.Properties.Resources.bb;
                        }
                        else
                        {
                            m_Board[rowIndex, colIndex].Image = piece.PieceType == Piece.ePieceType.Normal ? Checkers.Properties.Resources.r : Checkers.Properties.Resources.rb;
                        }
                    }
                    else
                    {
                        m_Board[rowIndex, colIndex].Image = null;
                    }
                }
            }
        }

        private void showNameOfCurrentPlayerTurn()
        {
            m_LabelTurn.Text = m_GameManager.CurrentTurn == Player.ePlayerType.Player1
                                   ? $"Player Turn: {m_GameManager.Player1.Name}"
                                   : $"Player Turn: {m_GameManager.Player2.Name}";
        }

        private void WindowGame_MouseLeave(object sender, EventArgs e)
        {
            PictureBoxIndex currentBox = sender as PictureBoxIndex;
            if (m_GameManager.Board.Arr[currentBox.Row, currentBox.Col].PieceOnSquare != null && sender != r_PreviousMovePictureBox[0] && sender != r_PreviousMovePictureBox[1] && sender != m_SelectedPiece)
            {
                currentBox.BackColor = Color.Black;
            }
        }

        private void WindowGame_MouseEnter(object sender, EventArgs e)
        {
            PictureBoxIndex currentBox = sender as PictureBoxIndex;
            if (m_GameManager.Board.Arr[currentBox.Row, currentBox.Col].PieceOnSquare != null && sender != r_PreviousMovePictureBox[0] && sender != r_PreviousMovePictureBox[1] && sender != m_SelectedPiece)
            {
                currentBox.BackColor = Color.FromArgb(255, 64, 64, 64);
            }
        }

        private void WindowGame_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBoxIndex currentBoxIndex = sender as PictureBoxIndex;

            if(m_GameManager.Board.Arr[currentBoxIndex.Row, currentBoxIndex.Col].PieceOnSquare?.PlayerType == m_GameManager.CurrentTurn)
            {
                if (currentBoxIndex == m_SelectedPiece)
                {
                    m_SelectedPiece = null;
                    clearMarkedPositions();
                }
                else
                {
                    m_SelectedPiece = currentBoxIndex;
                    addMarkedPosition();
                }
            }
            else
            {
                if (m_SelectedPiece != null && m_GameManager.IsMoveValid(new Piece.Position(m_SelectedPiece.Row, m_SelectedPiece.Col), new Piece.Position(currentBoxIndex.Row, currentBoxIndex.Col)))
                {
                    clearMarkedPositions();
                    moveOccurred(new Piece.Position(m_SelectedPiece.Row, m_SelectedPiece.Col),
                        new Piece.Position(currentBoxIndex.Row, currentBoxIndex.Col));
                }
                else
                { 
                    selectNotValidBox();
                }
            }
            
            StartTurnLoop();
        }
        
        private void selectNotValidBox()
        {
            if(m_SelectedPiece == null)
            {
                string currentTurnName = m_GameManager.CurrentTurn == Player.ePlayerType.Player1
                                             ? m_GameManager.Player1.Name
                                             : m_GameManager.Player2.Name;
                MessageBox.Show($"Not your Turn {System.Environment.NewLine}Player {currentTurnName} is Playing.","Turn Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(
                    $"Not Valid Move {System.Environment.NewLine}Please select from valid options only.",
                    "Move Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void addLastMove(PictureBoxIndex i_CurrentMove)
        {
            r_PreviousMovePictureBox[0] = m_SelectedPiece;
            r_PreviousMovePictureBox[1] = i_CurrentMove;
        }

        private void addMarkedPosition()
        {
            clearMarkedPositions();
            Piece pieceSelected = m_GameManager.Board.Arr[m_SelectedPiece.Row, m_SelectedPiece.Col].PieceOnSquare;

            if (pieceSelected != null)
            {
                if(pieceSelected.PlayerType == Player.ePlayerType.Player1 && m_GameManager.CurrentTurn == Player.ePlayerType.Player1 ||
                   pieceSelected.PlayerType == Player.ePlayerType.Player2 && m_GameManager.CurrentTurn == Player.ePlayerType.Player2)
                {
                    if(m_GameManager.Player1.CanEat || m_GameManager.Player2.CanEat)
                    {
                        foreach(Piece.Position pieceSelectedEatenPossibleMove in pieceSelected.EatenPossibleMoves)
                        {
                            r_LastMarkedPositions.Add(pieceSelectedEatenPossibleMove);
                        }
                    }
                    else
                    {
                        foreach (Piece.Position pieceSelectedRegularPossibleMove in pieceSelected.RegularPossibleMoves)
                        {
                            r_LastMarkedPositions.Add(pieceSelectedRegularPossibleMove);
                        }
                    }
                }
            }
        }

        private void clearMarkedPositions()
        {
            r_LastMarkedPositions.Clear();
        }

        public void RunGame()
        {
            ShowDialog();
        }
    }
}