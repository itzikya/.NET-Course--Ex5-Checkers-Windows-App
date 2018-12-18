using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using B18_Ex02;

namespace Ex05.WindowsUI
{
     public class CheckersForm : Form
     {
          private readonly GameSettingsForm r_GameSettingsForm = new GameSettingsForm();
          private Label labelPlayer1 = new Label();
          private Label labelPlayer2 = new Label();
          private BoardButton[,] m_SquareButtons;
          private CheckersGame m_Game;
          private Square m_SourceSquare = null;

          public CheckersForm()
          {
               this.Text = "Checkers";
               this.StartPosition = FormStartPosition.Manual;
               this.Left = 20;
               this.Top = 20;
               this.AutoSize = true;
               this.BackColor = System.Drawing.Color.FloralWhite;
               this.FormBorderStyle = FormBorderStyle.FixedSingle;
               this.Load += checkersForm_Load;
               this.Shown += checkersForm_Shown;
          }

          private void checkersForm_Shown(object sender, EventArgs e)
          {
               m_Game = new CheckersGame(
                    r_GameSettingsForm.Player1Name,
                    r_GameSettingsForm.Player2Name,
                    r_GameSettingsForm.BoardSize,
                    r_GameSettingsForm.GameMode);
               initControls();
               assignMenToButtons();
               updateSourceButtonsAvailability();
          }

          private void checkersForm_Load(object sender, EventArgs e)
          {
               r_GameSettingsForm.ShowDialog();
          }

          private void initControls()
          {
               labelPlayer1.Font = new Font("Corbel", 11, FontStyle.Bold);
               labelPlayer1.Location = new System.Drawing.Point(30, 6);
               labelPlayer1.Text = m_Game.ActiveTeam.Name + ": " + m_Game.ActiveTeam.Score.ToString();
               labelPlayer1.AutoSize = true;
               this.Controls.Add(this.labelPlayer1);

               labelPlayer2.Font = new Font("Corbel", 11, FontStyle.Bold);
               labelPlayer2.Location = new System.Drawing.Point(260, 6);
               labelPlayer2.Text = m_Game.InactiveTeam.Name + ": " + m_Game.InactiveTeam.Score.ToString();
               labelPlayer2.AutoSize = true;
               this.Controls.Add(this.labelPlayer2);

               m_SquareButtons = new BoardButton[m_Game.Board.BoardSize, m_Game.Board.BoardSize];
               addSquareButtons(m_Game.Board.BoardSize);
          }

          private void updateScore()
          {
               labelPlayer1.Text = m_Game.ActiveTeam.Name + ": " + m_Game.ActiveTeam.Score.ToString();
               labelPlayer2.Text = m_Game.InactiveTeam.Name + ": " + m_Game.InactiveTeam.Score.ToString();
          }

          private void addSquareButtons(int i_BoardSize)
          {
               for (int i = 0; i < i_BoardSize; i++)
               {
                    for (int j = 0; j < i_BoardSize; j++)
                    {
                         addSquare(i, j);
                    }
               }
          }

          private void addSquare(int i_RowPosition, int i_ColPosition)
          {
               BoardButton button = new BoardButton(i_RowPosition, i_ColPosition);
               button.Left = 6 + (i_ColPosition * 62);
               button.Top = 32 + (i_RowPosition * 62);
               button.Size = new System.Drawing.Size(62, 62);
               button.Click += new System.EventHandler(boardButton_Click);
               button.Enabled = false;
               this.Controls.Add(button);
               m_SquareButtons[i_RowPosition, i_ColPosition] = button;
          }

          private void assignMenToButtons()
          {
               for (int i = 0; i < m_Game.Board.BoardSize; i++)
               {
                    for (int j = 0; j < m_Game.Board.BoardSize; j++)
                    {
                         if (m_Game.Board.GetSquare(i, j).CurrentMan == null)
                         {
                              m_SquareButtons[i, j].BackgroundImage = null;
                         }
                         else
                         {
                              m_SquareButtons[i, j].AddManToButton(m_Game.Board.GetSquare(i, j).CurrentMan);
                         }
                    }
               }
          }

          private void resetActiveButton(BoardButton i_BoardButton)
          {
               i_BoardButton.BackgroundImage = null;
               i_BoardButton.Enabled = false;
               i_BoardButton.BackColor = System.Drawing.Color.LightGoldenrodYellow;
          }

          private void updateSourceButtonsAvailability()
          {
               foreach (BoardButton button in m_SquareButtons)
               {
                    if (button.Active)
                    {
                         button.Enabled = false;
                    }
               }

               foreach (Man man in m_Game.ActiveTeam.ArmyOfMen)
               {
                    m_SquareButtons[man.CurrentPosition.Position.y, man.CurrentPosition.Position.x].Enabled = true;
               }
          }

          private void updateDestinationButtonsAvailability(BoardButton i_BoardButton)
          {
               foreach (BoardButton button in m_SquareButtons)
               {
                    if (button.Active)
                    {
                         button.Enabled = true;
                    }
               }
          }

          private void boardButton_Click(object sender, EventArgs e)
          {
               BoardButton button = sender as BoardButton;
               if (m_SourceSquare == null)
               {
                    chooseDestinationSquare(button);
               }
               else if (m_SourceSquare.Position.Equals(m_SquareButtons[button.Position.y, button.Position.x].Position))
               {
                    cancelChoise(button);
               }
               else
               {
                    handleMoveRequest(button);
               }
          }

          private void handleMoveRequest(BoardButton i_BoardButton)
          {
               if (m_Game.IsLegalMove(m_SourceSquare, m_Game.Board.GetSquare(i_BoardButton.Position.y, i_BoardButton.Position.x)))
               {
                    makeAMoveProcess(i_BoardButton);
               }
               else
               {
                    MessageBox.Show("Illegal Move.");
               }
          }

          private void makeAMoveProcess(BoardButton i_BoardButton)
          {
               Move requestedMove = moveCreation(m_SourceSquare, m_Game.Board.GetSquare(i_BoardButton.Position.y, i_BoardButton.Position.x));
               m_Game.MakeAMoveProcess(requestedMove);
               if (m_Game.IsProgressiveMoveAvailable(requestedMove))
               {
                    handleProgressiveMove(i_BoardButton);
               }
               else
               {
                    if (m_Game.IsEndOfRound())
                    {
                         handleEndOfRound();
                    }
                    else
                    {
                         m_Game.SwapActiveTeam();
                         endUserChoise(i_BoardButton);
                    }
               }
          }

          private void handleProgressiveMove(BoardButton i_BoardButton)
          {
               i_BoardButton.BackColor = System.Drawing.Color.LightGoldenrodYellow;
               m_SquareButtons[m_SourceSquare.Position.y, m_SourceSquare.Position.x].BackColor = System.Drawing.Color.LightGoldenrodYellow;
               m_SourceSquare = null;
               assignMenToButtons();
               updateSourceButtonsAvailability();
               chooseDestinationSquare(i_BoardButton);
               i_BoardButton.BackColor = System.Drawing.Color.Firebrick;
               i_BoardButton.Enabled = false;
          }

          private void chooseDestinationSquare(BoardButton i_BoardButton)
          {
               m_SourceSquare = m_Game.Board.GetSquare(i_BoardButton.Position.y, i_BoardButton.Position.x);
               i_BoardButton.BackColor = System.Drawing.Color.PaleTurquoise;
               updateDestinationButtonsAvailability(i_BoardButton);
          }

          private void cancelChoise(BoardButton i_BoardButton)
          {
               i_BoardButton.BackColor = System.Drawing.Color.LightGoldenrodYellow;
               m_SourceSquare = null;
               updateSourceButtonsAvailability();
          }

          private void endUserChoise(BoardButton i_BoardButton)
          {
               i_BoardButton.BackColor = System.Drawing.Color.LightGoldenrodYellow;
               m_SquareButtons[m_SourceSquare.Position.y, m_SourceSquare.Position.x].BackColor = System.Drawing.Color.LightGoldenrodYellow;
               m_SourceSquare = null;
               assignMenToButtons();
               updateSourceButtonsAvailability();
               if (m_Game.ActiveTeam.Type == Team.eTeamType.Computer)
               {
                    makeComputerMove();
               }
          }

          private void endComputerTurn()
          {
               m_Game.SwapActiveTeam();
               assignMenToButtons();
               updateSourceButtonsAvailability();
          }

          private void makeComputerMove()
          {
               Move requestedMove = m_Game.GenerateMoveRequest();
               m_Game.MakeAMoveProcess(requestedMove);
               assignMenToButtons();
              updateSourceButtonsAvailability();
               while (m_Game.IsProgressiveMoveAvailable(requestedMove))
               {
                    m_Game.GenerateProgressiveAttack(ref requestedMove);
                    m_Game.MakeAMoveProcess(requestedMove);
                    assignMenToButtons();
                    updateSourceButtonsAvailability();
               }

               if (m_Game.IsEndOfRound())
               {
                    handleEndOfRound();
               }
               else
               {
                    endComputerTurn();
               }
          }

          private void handleEndOfRound()
          {
               string endOfRoundMessage;
               if (m_Game.Status == CheckersGame.eGameStatus.RoundEndWithDraw)
               {
                    endOfRoundMessage = string.Format(@"Tie!{1}Another Round?", m_Game.Status.ToString(), Environment.NewLine);
               }
               else
               {
                    endOfRoundMessage = string.Format(@"{0} Won!{1}Another Round?", m_Game.ActiveTeam.Name, Environment.NewLine);
               }

               DialogResult dialogResult = MessageBox.Show(endOfRoundMessage, "Checkers", MessageBoxButtons.YesNo);
               if (dialogResult == DialogResult.Yes)
               {
                    handleNewRoundRequest();
               }
               else
               {
                    this.Close();
               }
          }

          private void cleanAllButtons()
          {
               foreach (BoardButton button in m_SquareButtons)
               {
                    if (button.Enabled)
                    {
                         resetActiveButton(button);
                    }
               }
          }

          private void handleNewRoundRequest()
          {
               m_Game.CreateNewRound();
               cleanAllButtons();
               assignMenToButtons();
               updateSourceButtonsAvailability();
               m_SourceSquare = null;
               updateScore();
          }

          private Move moveCreation(Square i_SourceSquare, Square i_DestinationSquare)
          {
               Move requestedMove = new Move();
               foreach (Move attackMove in m_Game.ActiveTeam.AttackMoves)
               {
                    if (i_SourceSquare.Position.Equals(attackMove.SourceSquare.Position) &&
                         i_DestinationSquare.Position.Equals(attackMove.DestinationSquare.Position))
                    {
                         requestedMove = attackMove;
                         break;
                    }
               }

               foreach (Move regularMove in m_Game.ActiveTeam.RegularMoves)
               {
                    if (i_SourceSquare.Position.Equals(regularMove.SourceSquare.Position) &&
                         i_DestinationSquare.Position.Equals(regularMove.DestinationSquare.Position))
                    {
                         requestedMove = regularMove;
                         break;
                    }
               }

               return requestedMove;
          }
     }
}