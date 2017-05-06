using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.MoveSets
{
    public class Bishop : MoveSet
    {
        public override bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            if (Math.Abs(cp.getPosition().getRow() - newSpace.getRow()) ==
                Math.Abs(cp.getPosition().getColumn() - newSpace.getColumn()))
                return true;
            return false;
        }
    }
}
