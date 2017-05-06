using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.MoveSets
{
    public class Knight : MoveSet
    {
        public override bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
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
}
