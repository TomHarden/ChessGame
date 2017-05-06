using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Executions;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    class Blustag__Luckiest_Cannoneer : Bishop
    {
        private const string FLAVOR_TEXT = "His favorite pastime is lying in a field of clover, looking for one with four leaves...seriously.";
        private const string DESCPRIPTION =
            "A soldier of the Furth army, part of the newly formed gunnery division."
            + " He is one of two artillery men."
            + " He is fascinated with good luck charms and somewhere along the way,"
            + " he finally found a charm that works."
            + " Unfortunately, he has collected so many, he isn't sure which charm that is."
            + " He has a sunny outlook on life, and only ever complains about lugging around his cannon and shot.";
        public Blustag__Luckiest_Cannoneer(Player inPlayer, BoardSpace_Model inPosition)
            : base("Blustag, Luckiest Cannoneer",
            species.Human,
            locale.Furth,
            FLAVOR_TEXT,
            inPlayer,
            inPosition)
        {
            skill = new Skill(
                        new Abilities.TeleportToAdjacentSpace(this, this),
                        new Executions.Charge(1, this),
                        this);
            //skill = new Skill(new Abilities.LoseMoveSet(new MoveSets.Queen(), this), new Executions.Charge(3, this), this);
        }
        public override void standardTakeOf(ChessPiece enemyPiece)
        {

            base.standardTakeOf(enemyPiece);
            ((Charge)skill.getExecution()).chargeUp();
            skill.useSkill();//attempts to use skill
        }
    }
}
