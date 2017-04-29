using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class King : ChessPiece
    {
        public King(string inName, string inFlavorText, Skill inSkill, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inFlavorText, inSkill, inPlayer, inPosition)
        {
            piecetype = pieceType.King;
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            return KingMoveSet.isValidMove(this, tile);
        }
    }
}
