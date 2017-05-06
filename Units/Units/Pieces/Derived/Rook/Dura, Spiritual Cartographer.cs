using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Executions;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    class Dura__Spiritual_Cartographer : Rook
    {
        private const string FLAVOR_TEXT = "The founder of Dura Maps,"
            + " he made quite a fortune on travel maps with"
            + " enchanted, interactive interfaces.";
        private const string DESCPRIPTION =
            "A nobleman who became a mage of the Royal House of Furth. "
            + " He has an immense interest in cartography."
            + " He has decided to combine his two passions by creating maps of the spiritual laylines"
            + " that crisscross the continent,"
            + " a task which has so far only been accomplished on a local level by accomplished mages."
            + " He is the founder of Dura Maps, which has made a small fortune from"
            + " their encahnted, interactive travel maps."
            + " He always smokes from his pipe after dinner before bed,"
            + " and woe beholds those who try to stop him.";
        public Dura__Spiritual_Cartographer(Player inPlayer, BoardSpace_Model inPosition)
            : base("Dura, Spiritual Cartographer",
            species.Human,
            locale.Furth,
            FLAVOR_TEXT,
            inPlayer,
            inPosition)
        {
            skill = new Skill(
                new Abilities.GainMoveSet_Temporary(
                    new MoveSets.Queen(), this),
                    new Executions.TotalDeadCount(20, this),
                this);
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            skill.useSkill();
            return base.isValidMove(tile);
        }
    }
}
