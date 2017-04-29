using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class Bishop : ChessPiece
    {
        public Bishop(string inName, string inFlavorText, Skill inSkill, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inFlavorText, inSkill, inPlayer, inPosition)
        {
            piecetype = pieceType.Bishop;
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            return BishopMoveSet.isValidMove(this, tile);
        }
    }
}
