using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Executions;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    class Grace__Knight_of_Seven_Swords : Knight
    {
        private const string FLAVOR_TEXT = "\"Where do I keep them all?\" She smiled. \"That's for me to know and you to find out.\"";
        private const string DESCPRIPTION =
            "A high-ranking noble from Furth, Grace is hot-tempered and fiercely"
            + " independent. She has no faith in higher powers or divine intervention.";
        public Grace__Knight_of_Seven_Swords(Player inPlayer, BoardSpace_Model inPosition)
            : base("Grace, Knight of Seven Swords",
            species.Human,
            locale.Furth,
            FLAVOR_TEXT,
            inPlayer,
            inPosition)
        {
            skill = new Skill(new Abilities.MultipleMoves(2, this), new Executions.LimitedCharge(2, 2, this), this);
        }
        public override void standardTakeOf(ChessPiece enemyPiece)
        {
            base.standardTakeOf(enemyPiece);
            ((LimitedCharge)skill.getExecution()).chargeUp();
            skill.useSkill();//attempts to use skill
        }
        /*public override void moveToPosition(BoardSpace_Model newPosition)
        {
            base.moveToPosition(newPosition);
            skill.useSkill();
        }*/
    }
}
