using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame
{
    class Player2 : Player
    {
        public Player2(List<Board.BoardSpace_Model> inStartingTiles, Game_Model inGameModel)
            : base(2, inStartingTiles, Color.Black, inGameModel)
        {
            /*int i = 0;
            for (; i < 15; i++)
                army.Add(new Knight("Knight", species.Human, locale.Furth, "Your Knight", this, inStartingTiles[i]));
            army.Add(new Kells__Warrior_of_Burning_Blood(this, inStartingTiles[i]));*/

            int direction = 1;//direction = otherPlayer's row - your row.  if neg, = direction!)
            if (playerNum % 2 == 1)
                direction = 1;
            else if (playerNum % 2 == 0)
                direction = -1;

            int spaceNum = 0;
            army.Add(new Rook("Rook", species.Human, locale.Furth, "Your Rook", this, startingTiles[spaceNum++]));
            army.Add(new Knight("Knight", species.Human, locale.Furth, "Your Knight", this, startingTiles[spaceNum++]));
            army.Add(new Bishop("Bishop", species.Human, locale.Furth, "Your Bishop", this, startingTiles[spaceNum++]));
            army.Add(new Queen("Queen", species.Human, locale.Furth, "Your Queen", this, startingTiles[spaceNum++]));
            army.Add(new King("King", species.Human, locale.Furth, "Your King", this, startingTiles[spaceNum++]));
            army.Add(new Queen("Queen", species.Human, locale.Furth, "Your Queen", this, startingTiles[spaceNum++]));
            army.Add(new Bishop("Bishop", species.Human, locale.Furth, "Your Bishop", this, startingTiles[spaceNum++]));
            army.Add(new Knight("Knight", species.Human, locale.Furth, "Your Knight", this, startingTiles[spaceNum++]));
            army.Add(new Rook("Rook", species.Human, locale.Furth, "Your Rook", this, startingTiles[spaceNum++]));
            //army.Add(new Rook("Rook", species.Human, locale.Furth, "Your Rook", this, startingTiles[spaceNum++]));

            for (int i = spaceNum; spaceNum < gameModel.getChessBoardModel().getNumCols() + i; spaceNum++)
                army.Add(new Pawn("Pawn", species.Human, locale.Furth, "One of your Pawns", this, startingTiles[spaceNum], direction));
        }
    }
}
