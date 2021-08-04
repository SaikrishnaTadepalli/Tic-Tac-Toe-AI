using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tic_Tac_Toe
{
    public class Player
    {
        public Image playerImage;
        string playerName;
        public int wins;
        public Player(Image img, string name)
        {
            playerImage = img;
            playerName = name;
            wins = 0;
        }

        public override string ToString()
        {
            return playerName.ToUpper();
        }
    }
}
