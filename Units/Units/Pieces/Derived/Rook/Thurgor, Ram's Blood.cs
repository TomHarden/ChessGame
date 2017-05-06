using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Executions;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    class Thurgor__Ram_s_Blood : Rook
    {
        private const string FLAVOR_TEXT = "On the day of the blood-letting, his cry can be heard 10 miles away, \"THURGOR SMASH!!\"";
        private const string DESCPRIPTION =
            "Barbarian human from the Wehnor Tribe to the far west."
            + " Enjoys animal sacrifice to appease his many grotesque gods."
            + " Never bathes. Thinks vegeatables are for sissies.";
        public Thurgor__Ram_s_Blood(Player inPlayer, BoardSpace_Model inPosition)
            : base("Thurgor, Ram's Blood",
            species.Human,
            locale.Western_Mountains,
            FLAVOR_TEXT,
            inPlayer,
            inPosition)
        {
            skill = new Skill(new Abilities.TakePiecesOnVector(this), new Executions.LimitedCharge(3, 3, this), this);
        }
        public override void moveToPosition(BoardSpace_Model newPosition)
        {
            if (skill.getExecution().canExecute())
            {
                foreach(BoardSpace_Model tile in boardModel.getTileStrip(this.position, newPosition))
                {
                    if (tile.isOccupied() && !tile.isObstacle())
                        try
                        {
                            base.standardTakeOf((ChessPiece)tile.getUnit());
                        }
                        catch
                        {
                            continue;
                        }
                }
            }
            base.moveToPosition(newPosition);
        }
        public override void standardTakeOf(ChessPiece enemyPiece)
        {
            if (skill.getExecution().canExecute())
                skill.useSkill(enemyPiece);//attempts to use skill
            else
                base.standardTakeOf(enemyPiece);
            ((LimitedCharge)skill.getExecution()).chargeUp();
        }
        protected override void removeInvalidMoveTiles(List<List<BoardSpace_Model>> vectorList, ref List<BoardSpace_Model> finalList)
        {
            if (skill.getExecution().canExecute())
                foreach (List<BoardSpace_Model> tileVectorList in vectorList)
                    foreach (BoardSpace_Model tile in tileVectorList)
                        if (tile.getUnit() != null && tile.getUnit().isObstacle())
                            break;
                        else
                            finalList.Add(tile);
            else
                base.removeInvalidMoveTiles(vectorList, ref finalList);
        }
    }
}
