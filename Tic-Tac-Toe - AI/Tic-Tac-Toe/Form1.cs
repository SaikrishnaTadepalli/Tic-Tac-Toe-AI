using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class frmHome : Form
    {
        public static PictureBox[,] picCol = new PictureBox[3, 3];
        public static string[,] board = new string[3, 3] { { "", "", "" }, { "", "", "" }, { "", "", "" } };

        public  Player player = new Player(Properties.Resources.X, "X");
        public AI ai;

        public static bool canPlay = false;
        int ties = 0;

        public frmHome()
        {
            InitializeComponent();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            for (int r = 0; r < 3; r++)
                for (int s = 0; s < 3; s++)
                    picCol[r, s] = (PictureBox)(Controls["picBox" + r.ToString() + s.ToString()]);

            for (int r = 0; r < 3; r++)
            {
                for (int s = 0; s < 3; s++)
                {
                    picCol[r, s].Image = Properties.Resources.blank;
                    picCol[r, s].Click += new EventHandler(PicClicked);
                    picCol[r, s].Enabled = false;
                }
            }

            lblScoreBoard.Text = "Scoreboard";
            ai = new AI(Properties.Resources.O, "O", this);
        }

        public void Move(Player player, int row, int column)
        {
            picCol[row, column].Image = player.playerImage;
            picCol[row, column].Enabled = false;

            board[row, column] = player.ToString();

            StateCheck(player);
        }

        private void PicClicked(object sender, EventArgs e)
        {
            PictureBox picClicked = (PictureBox)sender;

            int clickedR = int.Parse(picClicked.Name[6].ToString());
            int clickedS = int.Parse(picClicked.Name[7].ToString());

            Move(player, clickedR, clickedS);
        }

        private void StateCheck(Player caller)
        {
            Player winner = Winner();
            /*
            MessageBox.Show(board[0, 0] + "|\t|" + board[0, 1] + "|\t|" + board[0, 2] +
                "\n" +
                board[1, 0] + "|\t|" + board[1, 1] + "|\t|" + board[1, 2] +
                "\n" +
                board[2, 0] + "|\t|" + board[2, 1] + "|\t|" + board[2, 2], "State");
            */
            if (winner == null)
            {
                if (IsTie())
                    GameOver(winner);

                canPlay = (caller == ai);

                if (!canPlay)
                    ai.MakeMove();
            }
            else 
            {
                GameOver(winner);
            }
        }

        private void GameOver(Player winner)
        {
            for (int r = 0; r < 3; r++)
                for (int s = 0; s < 3; s++)
                    picCol[r, s].Enabled = false;

            if (winner == null)
            {
                lblStatusOut.Text = "TIE";
                ties += 1;
            }
            else if (winner == player)
            {
                lblStatusOut.Text = "Player Won!";
                player.wins += 1;
            }
            else if (winner == ai)
            {
                lblStatusOut.Text = "AI Won!";
                ai.wins += 1;
            }
            else
                lblStatusOut.Text = "SumTingWong";

            btnPlay.Enabled = true;
            lblScoreBoard.Text = ("Player: " + player.wins + "\n" + "AI: " + ai.wins + "\n" + "Ties: " + ties);
        }
        
        public Player Winner() 
        {
            if (IsWinner(player) == true)
                return player;
            else if (IsWinner(ai) == true)
                return ai;
            else
                return null;
        }

        private bool IsWinner(Player player) 
        {
            for (int i = 0; i < 3; i++)
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 2] == player.ToString()) 
                    return true;

            for (int i = 0; i < 3; i++)
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[2, i] == player.ToString())
                    return true;

            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[2, 2] == player.ToString())
                return true;

            if (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2] && board[0, 2] == player.ToString())
                return true;

            return false;
        }

        public static bool IsTie()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == "")
                        return false;

            return true;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < 3; r++)
            {
                for (int s = 0; s < 3; s++)
                {
                    picCol[r, s].Image = Properties.Resources.blank;
                    picCol[r, s].Enabled = true;
                    board[r, s] = "";
                }
            }
            canPlay = true;
            btnPlay.Enabled = false;
            lblStatusOut.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
