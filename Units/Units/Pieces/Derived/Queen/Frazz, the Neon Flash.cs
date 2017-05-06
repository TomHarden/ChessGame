using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Executions;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    class Frazz__the_Neon_Flash : Queen
    {
        private const string FLAVOR_TEXT = "\"Can't catch me!\"";
        private const string DESCPRIPTION =
            "A mysterious woman in a skintight glowing suit."
            + " She frequently claims about being from the future,"
            + " but is conveniently stuck in the present time."
            + " She is extremely progressive and insists on equality and fairness."
            + " She is an adrenaline junkie, and frequently gripes about the food";
        public Frazz__the_Neon_Flash(Player inPlayer, BoardSpace_Model inPosition)
            : base("Frazz, the Neon Flash", species.Human, locale.NA, FLAVOR_TEXT, inPlayer, inPosition)
        {
            skill = new Skill(new Abilities.MultipleMoves(3, this), new Executions.Charge(5, this), this);
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            return moveset.isValidMove(this, tile);
        }
        public override void standardTakeOf(ChessPiece enemyPiece)
        {
            base.standardTakeOf(enemyPiece);
            ((Charge)skill.getExecution()).chargeUp();
            skill.useSkill();//attempts to use skill
        }
    }
}
