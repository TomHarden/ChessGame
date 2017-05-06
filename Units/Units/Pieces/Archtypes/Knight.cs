using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    public class Knight : ChessPiece
    {
        public Knight(string inName, species inRace, locale inRegion, string inFlavorText, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inRace, inRegion, inFlavorText, inPlayer, inPosition)
        {
            piecetype = pieceType.Knight;
            moveset = new MoveSets.Knight();
        }
        /*public override bool isValidMove(BoardSpace_Model tile)
        {
            return MoveSets.King.isValidMove(this, tile);
        }*/
    }
}
