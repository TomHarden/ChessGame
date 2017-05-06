using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    class Lafok__Storm_of_Rust : Knight
    {
        private const string FLAVOR_TEXT = "She turned to the children."
            + "\"Stay inside no matter what!\" Grabbing the great sword"
            + " from the mantle, she stepped out to face the poachers.";
        private const string DESCPRIPTION =
            "A troll mother who wishes to be a renound swordswoman like her father was."
            + " She has no fear of the sun, but must still wear protective clothing" 
            + " during the day. She carries her father's rusty greatsword with her: it is "
            + " her most prized possession, and she will not let anyone touch it" 
            + " - not even to polish it.";
        public Lafok__Storm_of_Rust(Player inPlayer, BoardSpace_Model inPosition)
            : base("Lafok, Storm of Rust",
            species.Troll,
            locale.Furth,
            FLAVOR_TEXT,
            inPlayer,
            inPosition)
        {
            skill = new DualConditionSkill(
                new Abilities.GainMoveSet_Temporary(new MoveSets.Rook(), this),
                new Executions.SoleTypeSurvivor(this),
                new Executions.TileColor(this, tileType.White),
                this);
        }
        /*public override void moveToPosition(BoardSpace_Model newPosition)
        {
            base.moveToPosition(newPosition);
            skill.useSkill();
        }*///no longer using this!
        public override bool isValidMove(BoardSpace_Model tile)
        {
            skill.useSkill();
            return base.isValidMove(tile);
        }
    }
}
