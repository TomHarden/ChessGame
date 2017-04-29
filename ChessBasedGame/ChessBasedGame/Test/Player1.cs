using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ChessBasedGame.Test
{
    class Player1 : Player
    {
        public Player1(Game_Model inGameModel, List<BoardSpace_Model> inStartingTiles)
            : base(1, inStartingTiles, Color.AliceBlue, inGameModel)
        {
            List<ChessPiece> army = new List<ChessPiece>();
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, inStartingTiles[0]));
        }
    }
}
