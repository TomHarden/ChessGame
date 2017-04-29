using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame//the "isValidMove()" functions shoould really be static!
{
    public class MoveSet//abstract
    {
        public static bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            return true;
        }
        public static bool canCastle(Unit k, Unit r/*, List<BoardSpace_Model> tilesBetweenKingAndRook*/)//does not check for obstructions!//assumes the tiles are between the King and Rook!
        {
            if (k.isUnmoved() && r.isUnmoved()
                && k.getPosition().getRow() == r.getPosition().getRow()
                && k.getPosition().getDistanceFrom(r.getPosition()) == 3)
                return true;
            return false;
        }
    }
    public class KingMoveSet : MoveSet
    {
        public static bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            if (Math.Abs(cp.getPosition().getRow() - newSpace.getRow()) <= 1
                && Math.Abs(cp.getPosition().getColumn() - newSpace.getColumn()) <= 1)
                return true;
            return false;
        }
        public static bool canCastle(King k, Rook r/*, List<BoardSpace_Model> tilesBetweenKingAndRook*/)//does not check for obstructions!//assumes the tiles are between the King and Rook!
        {
            if (k.isUnmoved() && r.isUnmoved()
                && k.getPosition().getRow() == r.getPosition().getRow()
                && k.getPosition().getDistanceFrom(r.getPosition()) == 3)
                return true;
            return false;
        }
    }
    public class QueenMoveSet : MoveSet
    {
        public static bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            return (RookMoveSet.isValidMove(cp, newSpace) || BishopMoveSet.isValidMove(cp, newSpace));
        }
    }
    public class BishopMoveSet : MoveSet
    {
        public static bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            if (Math.Abs(cp.getPosition().getRow() - newSpace.getRow()) ==
                Math.Abs(cp.getPosition().getColumn() - newSpace.getColumn()))
                return true;
            return false;
        }
    }
    public class KnightMoveSet : MoveSet
    {
        public static bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            //if the row is different by 2 and the column is different by 1
            if (Math.Abs(cp.getPosition().getRow() - newSpace.getRow()) == 2
                && Math.Abs(cp.getPosition().getColumn() - newSpace.getColumn()) == 1)
                return true;
            //else if the column is different by 2 and the row is different by 1
            else if (Math.Abs(cp.getPosition().getColumn() - newSpace.getColumn()) == 2
                && Math.Abs(cp.getPosition().getRow() - newSpace.getRow()) == 1)
                return true;
            return false;
        }
    }
    public class RookMoveSet : MoveSet
    {
        public static bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            if (cp.getPosition().getRow() == newSpace.getRow()
                || cp.getPosition().getColumn() == newSpace.getColumn())
                return true;
            return false;
        }
    }
    public class PawnMoveSet : MoveSet
    {
        const int MOVE_RANGE = 1;
        const int SPECIAL_MOVE_RANGE = 2;
        
        public static bool isValidMove(Pawn cp, BoardSpace_Model newSpace)
        {
            //can move one unoccupied space in the appropriate direction, except if it is the first move, then it may move two.
                int moveRange = MOVE_RANGE;
                if (cp.isUnmoved())
                    moveRange = SPECIAL_MOVE_RANGE;

                if (cp.getPosition().getColumn() - newSpace.getColumn() == 0)//same column
                {
                    if (Math.Abs(cp.getPosition().getRow() - newSpace.getRow()) <= Math.Abs(moveRange * cp.getDirection())
                        && !newSpace.isOccupied())
                    {
                        if (cp.getDirection() > 0)
                            return (cp.getPosition().getRow() - newSpace.getRow() > 0);
                        else if (cp.getDirection() < 0)
                            return (cp.getPosition().getRow() - newSpace.getRow() < 0);
                        return true;
                    }
                }
                else if (Math.Abs(cp.getPosition().getColumn() - newSpace.getColumn()) == 1)//in the adjacent columns
                {
                    if (cp.getPosition().getRow() - newSpace.getRow() == cp.getDirection()
                        && newSpace.isOccupied())
                        return true;
                }
            return false;
        }
    }
}
