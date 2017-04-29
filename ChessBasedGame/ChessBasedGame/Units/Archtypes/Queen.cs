using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class Queen : ChessPiece
    {
        public Queen(string inName, string inFlavorText, Skill inSkill, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inFlavorText, inSkill, inPlayer, inPosition)
        {
            piecetype = pieceType.Queen;
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            return QueenMoveSet.isValidMove(this, tile);
        }
    }
}
