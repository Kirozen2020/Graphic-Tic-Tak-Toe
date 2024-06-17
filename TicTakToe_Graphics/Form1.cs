using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicTakToe_Graphics.Properties;

namespace TicTakToe_Graphics
{
    public partial class Form1 : Form
    {
        List<PictureBox> pictureBoxes;
        char[] board;
        bool turn;
        bool ai;
        public Form1()
        {
            InitializeComponent();
            InitListOfPictureBoxes();
            InitBoard();
            this.turn = true;
            this.ai = false;
        }

        private void InitBoard()
        {
            this.board = new char[9];

            for (int i = 0; i < this.board.Length; i++)
            {
                board[i] = ' ';
            }
        }

        private void InitListOfPictureBoxes()
        {
            this.pictureBoxes = new List<PictureBox>
            {
                pb1, pb2, pb3, pb4, pb5, pb6, pb7, pb8, pb9
            };
            foreach(PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Click += PictureBox_Click;
                pictureBox.Tag = this.pictureBoxes.IndexOf(pictureBox);
                pictureBox.Image = null;
                pictureBox.Enabled = true;
            }

        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (!this.ai)
            {
                PictureBox clicked = (PictureBox)sender;
                if (this.board[(int)clicked.Tag] != ' ')
                {
                    bool full = true;
                    foreach (var item in this.board)
                    {
                        if (item == ' ')
                        {
                            full = false;
                        }
                    }
                    if (full)
                    {
                        MessageBox.Show("You've run out of moves. Please restart the game.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }
                if (this.turn)
                {
                    clicked.Image = Resources.X;
                    this.turn = !this.turn;
                    this.board[(int)clicked.Tag] = 'X';
                }
                else
                {
                    clicked.Image = Resources.O;
                    this.turn = !this.turn;
                    this.board[(int)clicked.Tag] = 'O';
                }

                char win = CheckWinSituation();
                if (win != ' ')
                {
                    foreach (var item in this.pictureBoxes)
                    {
                        item.Enabled = false;
                    }
                    MessageBox.Show($"The player {win} won!", "We have a winner!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("To proceed, please press the restart button.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else//player first
            {
                PictureBox clicked = (PictureBox)sender;
                if (this.board[(int)clicked.Tag] != ' ')
                {
                    bool full = true;
                    foreach (var item in this.board)
                    {
                        if (item == ' ')
                        {
                            full = false;
                        }
                    }
                    if (full)
                    {
                        MessageBox.Show("You've run out of moves. Please restart the game.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }
                clicked.Image = Resources.X;
                this.board[(int)clicked.Tag] = 'X';

                TicTakToe_Bot bot = new TicTakToe_Bot();
                this.board = bot.GetBestNextMove(this.board, 'O');
                UpdateBoard();
                char win = CheckWinSituation();
                if (win != ' ')
                {
                    foreach (var item in this.pictureBoxes)
                    {
                        item.Enabled = false;
                    }
                    if(win == 'X')
                    {
                        MessageBox.Show("You Have won the game!", "We have a winner!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show("To proceed, please press the restart button.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The bot has Won the game", "We have a winner!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show("To proceed, please press the restart button.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.turn = true;
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Image = null;
                pictureBox.Enabled = true;
            }
            for (int i = 0; i < this.board.Length; i++)
            {
                this.board[i] = ' ';
            }
        }
        private char CheckWinSituation()
        {
            char winner = ' ';
            char[] brd = this.board;

            for (int i = 0; i < 3; i++)
            {
                if (brd[i * 3] == brd[i * 3 + 1] && brd[i * 3] == brd[i * 3 + 2] && brd[i * 3] != ' ')
                {
                    winner = brd[i * 3];
                    break;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (brd[i] == brd[i + 3] && brd[i] == brd[i + 6] && brd[i] != ' ')
                {
                    winner = brd[i];
                    break;
                }
            }
            if ((brd[0] == brd[4] && brd[0] == brd[8] && brd[0] != ' ') ||
                (brd[2] == brd[4] && brd[2] == brd[6] && brd[2] != ' '))
            {
                winner = brd[4];
            }

            return winner;
        }

        private void UpdateBoard()
        {
            for (int i = 0; i < this.board.Length; i++)
            {
                switch (this.board[i])
                {
                    case 'X':
                        pictureBoxes[i].Image = Resources.X;
                        break;
                    case 'O':
                        pictureBoxes[i].Image = Resources.O;
                        break;
                    case ' ':
                        pictureBoxes[i].Image = null;
                        break;
                }
            }
        }

        private void cbBot_CheckedChanged(object sender, EventArgs e)
        {
            this.ai = cbBot.Checked;
            //restart game for changing bot status
            
            this.turn = true;
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Image = null;
                pictureBox.Enabled = true;
            }
            for (int i = 0; i < this.board.Length; i++)
            {
                this.board[i] = ' ';
            }
        }
    }
}
