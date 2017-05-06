using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame.MoveSets
{
    public class Queen : DualMoveSets
    {
        public Queen()
            : base(new MoveSets.Rook(), new MoveSets.Bishop())
        {

        }
        /*public override bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            MoveSets.Rook tmpRook = new MoveSets.Rook();
            MoveSets.Bishop tmpBishop = new MoveSets.Bishop();
            return (tmpRook.isValidMove(cp, newSpace)
                || tmpBishop.isValidMove(cp, newSpace));
            //return (Rook.isValidMove(cp, newSpace) || Bishop.isValidMove(cp, newSpace));
            //getting valid moves through static 
        }*/
    }
}
