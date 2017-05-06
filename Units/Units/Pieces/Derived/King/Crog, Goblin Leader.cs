using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    class Crog__Goblin_Leader : King
    {
        private const string FLAVOR_TEXT = "\"...I'm surrounded by IDIOTS!!!\"";
        private const string DESCPRIPTION =
            "A Goblin from an unknown den.  Given his love of picture books,"
            + " he is arguably among the most scholarly of goblins, but his"
            + " father's wishes to have a warrior son led to his current career.";
        public Crog__Goblin_Leader(Player inPlayer, BoardSpace_Model inPosition)
            : base("Crog, Goblin Leader",
            species.Goblin,
            locale.Furth,
            FLAVOR_TEXT,
            inPlayer, inPosition)
        {
            skill = new Skill(this);
        }
    }
}
