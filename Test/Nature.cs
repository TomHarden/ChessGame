using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Board;
using System.Drawing;

namespace ChessBasedGame.Test
{
    class Nature : Player
    {
        protected List<Units.Obstacles.Obstacle> allObstacles = new List<Units.Obstacles.Obstacle>();
        public Nature(List<BoardSpace_Model> inStartingTiles, Game_Model inGameModel)
            : base(0, inStartingTiles, Color.YellowGreen, inGameModel)
        {
            foreach (BoardSpace_Model tile in inStartingTiles)
                allObstacles.Add(new Units.Obstacles.Waterbody(this, tile));
        }
    }
}
