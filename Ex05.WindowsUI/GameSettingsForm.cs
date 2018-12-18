using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using B18_Ex02;

namespace Ex05.WindowsUI
{
     public class GameSettingsForm : Form
     {
          private const int k_SmallBoardSize = 6;
          private const int k_MediumBoardSize = 8;
          private const int k_BigBoardSize = 10;
          private Label labelBoardSize = new Label();
          private RadioButton button6x6 = new RadioButton();
          private RadioButton button8x8 = new RadioButton();
          private RadioButton button10x10 = new RadioButton();
          private Label labelPlayers = new Label();
          private Label labelPlayer1 = new Label();
          private Label labelPlayer2 = new Label();
          private TextBox textBoxPlayer1 = new TextBox();
          private TextBox textBoxPlayer2 = new TextBox();
          private CheckBox checkBoxAgainstUserMode = new CheckBox();
          private Button buttonDone = new Button();

          public int BoardSize
          {
               get
               {
                    int boardSize = 0;
                    if (button6x6.Checked)
                    {
                         boardSize = k_SmallBoardSize;
                    }
                    else if (button8x8.Checked)
                    {
                         boardSize = k_MediumBoardSize;
                    }
                    else if (button10x10.Checked)
                    {
                         boardSize = k_BigBoardSize;
                    }

                    return boardSize;
               }
          }

          public string Player1Name
          {
               get { return textBoxPlayer1.Text; }
          }

          public string Player2Name
          {
               get { return textBoxPlayer2.Text; }
          }

          public CheckersGame.eGameMode GameMode
          {
               get { return checkBoxAgainstUserMode.Checked ? CheckersGame.eGameMode.VersusAnotherPlayer : CheckersGame.eGameMode.VersusComputer; }
          }

          public GameSettingsForm()
          {
               this.Name = "GameSettings";
               this.Text = "Game Settings";
               this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
               this.StartPosition = FormStartPosition.CenterScreen;
               initControls();
          }

          private void initControls()
          {
               labelBoardSize.Location = new System.Drawing.Point(12, 10);
               labelBoardSize.Name = "labelBoardSize";
               labelBoardSize.Size = new System.Drawing.Size(80, 14);
               labelBoardSize.Text = "Board Size: ";

               button6x6.Location = new System.Drawing.Point(25, 25);
               button6x6.Name = "button6x6";
               button6x6.Size = new System.Drawing.Size(48, 17);
               button6x6.TabStop = true;
               button6x6.Text = "6 x 6";
               button6x6.UseVisualStyleBackColor = true;
               button6x6.Checked = true;

               button8x8.Location = new System.Drawing.Point(80, 25);
               button8x8.Name = "button8x8";
               button8x8.Size = new System.Drawing.Size(48, 17);
               button8x8.TabStop = true;
               button8x8.Text = "8 x 8";
               button8x8.UseVisualStyleBackColor = true;

               button10x10.Location = new System.Drawing.Point(135, 25);
               button10x10.Name = "button10x10";
               button10x10.Size = new System.Drawing.Size(60, 17);
               button10x10.TabStop = true;
               button10x10.Text = "10 x 10";
               button10x10.UseVisualStyleBackColor = true;

               labelPlayers.Location = new System.Drawing.Point(12, 45);
               labelPlayers.Name = "labelPlayers";
               labelPlayers.Size = new System.Drawing.Size(50, 14);
               labelPlayers.Text = "Players: ";

               labelPlayer1.Location = new System.Drawing.Point(22, 68);
               labelPlayer1.Name = "labelPlayer1";
               labelPlayer1.Size = new System.Drawing.Size(50, 14);
               labelPlayer1.Text = "Player 1: ";

               textBoxPlayer1.Location = new System.Drawing.Point(109, 65);
               textBoxPlayer1.Name = "m_Player1TextBox";
               textBoxPlayer1.Size = new System.Drawing.Size(117, 20);

               checkBoxAgainstUserMode.Location = new System.Drawing.Point(22, 97);
               checkBoxAgainstUserMode.Name = "m_AgainstUserModeCheckBox";
               checkBoxAgainstUserMode.Size = new System.Drawing.Size(15, 14);
               checkBoxAgainstUserMode.UseVisualStyleBackColor = true;
               checkBoxAgainstUserMode.Click += new EventHandler(AgainstUserModeCheckBox_CheckedChanged);

               labelPlayer2.Location = new System.Drawing.Point(42, 97);
               labelPlayer2.Name = "labelPlayer2";
               labelPlayer2.Size = new System.Drawing.Size(50, 14);
               labelPlayer2.Text = "Player 2: ";

               textBoxPlayer2.Enabled = false;
               textBoxPlayer2.Location = new System.Drawing.Point(109, 94);
               textBoxPlayer2.Name = "textBoxPlayer2";
               textBoxPlayer2.Size = new System.Drawing.Size(117, 20);
               textBoxPlayer2.Text = "[Computer]";
               buttonDone.Location = new System.Drawing.Point(180, 130);
               buttonDone.Name = "buttonDone";
               buttonDone.Size = new System.Drawing.Size(75, 23);
               buttonDone.TabIndex = 10;
               buttonDone.Text = "Done";
               buttonDone.UseVisualStyleBackColor = true;
               buttonDone.Click += new System.EventHandler(doneButton_Click);

               this.ClientSize = new System.Drawing.Size(284, 176);
               this.Controls.Add(this.buttonDone);
               this.Controls.Add(this.checkBoxAgainstUserMode);
               this.Controls.Add(this.textBoxPlayer2);
               this.Controls.Add(this.textBoxPlayer1);
               this.Controls.Add(this.labelPlayer2);
               this.Controls.Add(this.labelPlayer1);
               this.Controls.Add(this.labelPlayers);
               this.Controls.Add(this.button10x10);
               this.Controls.Add(this.button8x8);
               this.Controls.Add(this.button6x6);
               this.Controls.Add(this.labelBoardSize);
          }

          public void AgainstUserModeCheckBox_CheckedChanged(object sender, EventArgs e)
          {
               textBoxPlayer2.Enabled = !textBoxPlayer2.Enabled;
          }

          protected override void OnClosed(EventArgs e)
          {
               if (Player1Name == string.Empty)
               {
                    textBoxPlayer1.Text = "Player 1";
               }

               if (Player2Name == string.Empty)
               {
                    textBoxPlayer1.Text = "Player 2";
               }
          }

          public void doneButton_Click(object sender, EventArgs e)
          {
               if (isFormFulfilled())
               {
                    this.Close();
               }
               else
               {
                    MessageBox.Show("Invalid input.");
               }
          }

          private bool isFormFulfilled()
          {
               return (textBoxPlayer1.Text != string.Empty && textBoxPlayer2.Text != string.Empty) ? true : false;
          }
     }
}
