using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.MoveSets
{
    public class Pawn : MoveSet
    {
        const int MOVE_RANGE = 1;
        const int SPECIAL_MOVE_RANGE = 2;

        public override bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            cp = ((Units.ChessPieces.Pawn)cp);
            //can move one unoccupied space in the appropriate direction, except if it is the first move, then it may move two.
            int moveRange = MOVE_RANGE;
            if (cp.isUnmoved())
                moveRange = SPECIAL_MOVE_RANGE;

            if (cp.getPosition().getColumn() - newSpace.getColumn() == 0)//same column
            {
                if (Math.Abs(cp.getPosition().getRow() - newSpace.getRow()) <= Math.Abs(moveRange * ((Units.ChessPieces.Pawn)cp).getDirection())
                    && !newSpace.isOccupied())
                {
                    if (((Units.ChessPieces.Pawn)cp).getDirection() > 0)
                        return (cp.getPosition().getRow() - newSpace.getRow() > 0);
                    else if (((Units.ChessPieces.Pawn)cp).getDirection() < 0)
                        return (cp.getPosition().getRow() - newSpace.getRow() < 0);
                    return true;
                }
            }
            else if (Math.Abs(cp.getPosition().getColumn() - newSpace.getColumn()) == 1)//in the adjacent columns
            {
                if (cp.getPosition().getRow() - newSpace.getRow() == ((Units.ChessPieces.Pawn)cp).getDirection()
                    && newSpace.isOccupied())
                    return true;
            }
            return false;
        }
    }
}
