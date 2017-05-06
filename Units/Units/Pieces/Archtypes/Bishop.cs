using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    public class Bishop : ChessPiece
    {
        public Bishop(string inName, species inRace, locale inRegion, string inFlavorText, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inRace, inRegion, inFlavorText, inPlayer, inPosition)
        {
            piecetype = pieceType.Bishop;
            moveset = new MoveSets.Bishop();
        }
        /*public override bool isValidMove(BoardSpace_Model tile)
        {
            return MoveSets.Bishop.isValidMove(this, tile);
        }*/
    }
}
