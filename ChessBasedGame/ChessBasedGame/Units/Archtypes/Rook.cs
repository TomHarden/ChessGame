using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class Rook : ChessPiece
    {
        public Rook(string inName, string inFlavorText, Skill inSkill, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inFlavorText, inSkill, inPlayer, inPosition)
        {
            piecetype = pieceType.Rook;
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            return RookMoveSet.isValidMove(this, tile);
        }
    }
}
