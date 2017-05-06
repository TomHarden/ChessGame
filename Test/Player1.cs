using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame
{
    class Player1 : Player
    {
        public Player1(List<BoardSpace_Model> inStartingTiles, Game_Model inGameModel)
            : base(1, inStartingTiles, Color.WhiteSmoke, inGameModel)
        {
            
            int direction = 1;//direction = otherPlayer's row - your row.  if neg, = direction!)
            if (playerNum % 2 == 1)
                direction = 1;
            else if (playerNum % 2 == 0)
                direction = -1;

            int spaceNum = 0;
            //army.Add(new Rook("Rook", species.Human, locale.Furth, "Your Rook", this, startingTiles[spaceNum++]));
            //army.Add(new Mura__Goddess_of_Life_and_Death(this, startingTiles[spaceNum++]));

            //army.Add(new Rook("Rook", species.Human, locale.Furth, "Your Rook", this, startingTiles[spaceNum++]));
            army.Add(new Thurgor__Ram_s_Blood(this, startingTiles[spaceNum++]));

            //army.Add(new Grace__Knight_of_Seven_Swords(this, startingTiles[spaceNum++]));
            army.Add(new Lafok__Storm_of_Rust(this, startingTiles[spaceNum++]));

            //army.Add(new Bishop("Bishop", species.Human, locale.Furth, "Your Bishop", this, startingTiles[spaceNum++]));
            army.Add(new Blustag__Luckiest_Cannoneer(this, startingTiles[spaceNum++]));

            army.Add(new Mal__Necrowitch_of_the_Mire(this, startingTiles[spaceNum++]));

            army.Add(new Kells__Warrior_of_Burning_Blood(this, startingTiles[spaceNum++]));

            //army.Add(new Queen("Queen", species.Human, locale.Furth, "Your Queen", this, startingTiles[spaceNum++]));
            army.Add(new Frazz__the_Neon_Flash(this, startingTiles[spaceNum++]));

            //army.Add(new Bishop("Bishop", species.Human, locale.Furth, "Your Bishop", this, startingTiles[spaceNum++]));
            army.Add(new Bron__Dashing_Archer(this, startingTiles[spaceNum++]));

            //army.Add(new Lafok__Storm_of_Rust(this, startingTiles[spaceNum++]));
            army.Add(new Grace__Knight_of_Seven_Swords(this, startingTiles[spaceNum++]));

            //army.Add(new Rook("Rook", species.Human, locale.Furth, "Your Rook", this, startingTiles[spaceNum++]));
            army.Add(new Dura__Spiritual_Cartographer(this, startingTiles[spaceNum++]));

            for (int i = spaceNum; spaceNum < gameModel.getChessBoardModel().getNumCols() + i; spaceNum++)
                army.Add(new Pawn("Pawn", species.Human, locale.Furth, "One of your Pawns", this, startingTiles[spaceNum], direction));

            /*int i = 0;
            for (; i < 12; i++)
            {
                Rook newRook = new Rook("Rook", species.Human, locale.Furth, "Your Rook", this, inStartingTiles[i]);
                army.Add(newRook);
            }
            army.Add(new Crog__Goblin_Leader(this, inStartingTiles[i++]));
            army.Add(new Grace__Knight_of_Seven_Swords(this, inStartingTiles[i++]));
            army.Add(new Lafok__Storm_of_Rust(this, inStartingTiles[i++]));*/
        }
    }
}
