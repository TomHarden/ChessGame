using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.MoveSets
{
    public class King : MoveSet
    {
        public override bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            if (Math.Abs(cp.getPosition().getRow() - newSpace.getRow()) <= 1
                && Math.Abs(cp.getPosition().getColumn() - newSpace.getColumn()) <= 1)
                return true;
            return false;
        }
        public override bool validCastle(ChessPiece k, ChessPiece r/*, List<BoardSpace_Model> tilesBetweenKingAndRook*/)//does not check for obstructions!//assumes the tiles are between the King and Rook!
        {
            if (k.isUnmoved() && r.isUnmoved()
                && k.getPosition().getRow() == r.getPosition().getRow()
                && k.getPosition().getDistanceFrom(r.getPosition()) == 3)
                return true;
            return false;
        }
        public override bool canCastle()// returns whether the unit is allowed to castle at all.
        {
            return true;
        }
    }
}
