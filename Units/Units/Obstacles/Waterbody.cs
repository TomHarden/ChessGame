using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame.Units.Obstacles
{
    class Waterbody : Obstacle
    {
        public Waterbody(Player inPlayer, Board.BoardSpace_Model inPosition)
            : base(inPlayer, inPosition)
        {
            type = obstacleType.Water;
            name = "Waterbody";//tmp
        }
    }
}
