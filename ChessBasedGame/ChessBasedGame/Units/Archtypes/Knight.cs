using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class Knight : ChessPiece
    {
        public Knight(string inName, string inFlavorText, Skill inSkill, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inFlavorText, inSkill, inPlayer, inPosition)
        {
            piecetype = pieceType.Knight;
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            return KnightMoveSet.isValidMove(this, tile);
        }
    }
}
