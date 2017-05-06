using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    public class Queen : ChessPiece
    {
        public Queen(string inName, species inRace, locale inRegion, string inFlavorText, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inRace, inRegion, inFlavorText, inPlayer, inPosition)
        {
            piecetype = pieceType.Queen;
            moveset = new MoveSets.Queen();
        }
        /*public override bool isValidMove(BoardSpace_Model tile)
        {
            return MoveSets.Queen.isValidMove(this, tile);
        }*/
    }
}
