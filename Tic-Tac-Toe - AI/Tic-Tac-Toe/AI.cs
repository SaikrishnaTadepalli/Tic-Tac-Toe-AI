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
    public class AI: Player
    {
        private frmHome home;
        public AI(Image img, string name, frmHome formHome) : base(img, name)
        {
            home = formHome;
            playerImage = img;
            wins = 0;
        }

        public void MakeMove() 
        {
            int bestScore = -2147483648;
            int bestMoveR = -1;
            int bestMoveC = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (frmHome.board[i,j] == "") {
                        frmHome.board[i, j] = this.ToString();
                        int score = minimax(0, false);
                        frmHome.board[i, j] = "";
                        if (score > bestScore) {
                            bestScore = score;
                            bestMoveR = i;
                            bestMoveC = j;
                        }
                    }
                }
            }

            if (bestMoveR != -1 && bestMoveC != -1)
                home.Move(this, bestMoveR, bestMoveC);
        }

        private int minimax(int depth, bool IsMaximizing) 
        {
            Player winner = home.Winner();

            if (winner != null)
                if (winner == home.player)
                    return -1;
                else
                    return 1;
            else if (frmHome.IsTie()) 
                return 0;

            int bestScore = 0;

            if (IsMaximizing)
            {
                bestScore = -2147483648;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (frmHome.board[i, j] == "")
                        {
                            frmHome.board[i, j] = this.ToString();
                            int score = minimax(depth + 1, false);
                            frmHome.board[i, j] = "";
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
            }
            else 
            {
                bestScore = 2147483647;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (frmHome.board[i, j] == "")
                        {
                            frmHome.board[i, j] = home.player.ToString();
                            int score = minimax(depth + 1, true);
                            frmHome.board[i, j] = "";
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
            }

            return bestScore;
        }


    }
}
