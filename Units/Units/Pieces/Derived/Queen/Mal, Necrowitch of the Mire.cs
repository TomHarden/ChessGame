using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Executions;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    class Mal__Necrowitch_of_the_Mire : Queen
    {
        private const string FLAVOR_TEXT = "\"Double, double toil and trouble;"
            + " \\ Fire burn, and cauldron bubble!\""
            + " The Three Witches, <i>Macbeth</i>";
        private const string DESCPRIPTION =
            "A witch who lived in seclusion deep in the mires of Furth to escape prosecution."
            + " She has honed her dark magic so finely she can raise the dead...for a price."
            + " She has declared herself an eternal enemy of the Eastern Pantheon,"
            + " and claims to be immortal as well.";
        public Mal__Necrowitch_of_the_Mire(Player inPlayer, BoardSpace_Model inPosition)
            : base("Mal, Necrowitch of the Mire", species.Human, locale.Furth, FLAVOR_TEXT, inPlayer, inPosition)
        {
            skill = new Skill(new Abilities.Revive(this), new Executions.TotalDeadCount(10, this), this);
        }
        public override bool isValidMove(BoardSpace_Model tile)//IS THIS NECC?
        {
            return moveset.isValidMove(this, tile);
        }
        public override void standardTakeOf(ChessPiece enemyPiece)
        {
            base.standardTakeOf(enemyPiece);
            skill.useSkill();
        }
    }
}
