using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.Abilities
{
    class TakePiecesOnVector: Ability
    {
        public TakePiecesOnVector(ChessPiece inCP)
            : base(inCP)
        {
        }
        public override bool actOutAbility(ChessPiece target = null)//for now, it will be assumed that all teleports are to unoccupied tiles!
        {
            foreach (BoardSpace_Model tile in cp.getBoard().getTileStrip(cp.getPosition(), target.getPosition()))//cp.getPosition().getTileStrip(target.getPosition()))
            {
                if (tile.isOccupied())
                {
                    try
                    {
                        ChessPiece targetPiece = (ChessPiece) tile.getUnit();
                        if (targetPiece != cp)
                        {
                            tile.removeUnit();//remove piece from chessTile (and board)
                            targetPiece.getPlayer().removeUnit(targetPiece);
                            cp.moveToPosition(tile);
                        }
                    }
                    catch
                    {
                        continue;
                        //what to do here?
                    }
                }
            }
            return true;
            
        }
    }
}
