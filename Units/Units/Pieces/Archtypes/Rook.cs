using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    public class Rook : ChessPiece
    {
        public Rook(string inName, species inRace, locale inRegion, string inFlavorText, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inRace, inRegion, inFlavorText, inPlayer, inPosition)
        {
            piecetype = pieceType.Rook;
            moveset = new MoveSets.Rook();
        }
        /*public override bool isValidMove(BoardSpace_Model tile)
        {
            return MoveSets.Rook.isValidMove(this, tile);
        }*/
    }
}
