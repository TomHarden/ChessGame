using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Executions;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    class Bron__Dashing_Archer : Bishop
    {
        private const string FLAVOR_TEXT = "He blazed through in a flash, leaving the enemy with bolts in their necks. He reloaded and proceeded.";
        private const string DESCPRIPTION =
            "His parents raised him outside of Furth as colonists of the Interior."
            + " His father taught him to move through the forest and hunt with a bow."
            + " He became a mercenary for pay and developed a unique method of"
            + " using multiple crossbows. He tends to be popular with the ladies.";
        public Bron__Dashing_Archer(Player inPlayer, BoardSpace_Model inPosition)
            : base("Bron, Dashing Archer",
            species.Human,
            locale.Furth,
            FLAVOR_TEXT,
            inPlayer,
            inPosition)
        {
            skill = new Skill(new Abilities.LoseMoveSet(new MoveSets.Queen(), this), new Executions.Charge(3, this), this);
        }
        public override void standardTakeOf(ChessPiece enemyPiece)
        {
            base.standardTakeOf(enemyPiece);
            ((Charge)skill.getExecution()).chargeUp();
            skill.useSkill();//attempts to use skill
        }
    }
}
