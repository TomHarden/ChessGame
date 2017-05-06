using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame.Units.Obstacles
{
    public enum obstacleType { Water, Mountain, Canyon, Magic, Fire, Forest }//more to come!
    public abstract class Obstacle : Unit
    {
        protected obstacleType type;
        public Obstacle(Player inPlayer, Board.BoardSpace_Model inPosition)
            : base(inPlayer, inPosition)
        {
            isObstacle_val = true;
            //player = null;
        }
        //isObstacle_val = true;will need to be done in constructor!
    }
}
