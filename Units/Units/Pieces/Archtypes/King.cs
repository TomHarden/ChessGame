﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    public class King : ChessPiece
    {
        public King(string inName, species inRace, locale inRegion, string inFlavorText, Player inPlayer, BoardSpace_Model inPosition)
            : base(inName, inRace, inRegion, inFlavorText, inPlayer, inPosition)
        {
            piecetype = pieceType.King;
            moveset = new MoveSets.King();
        }
        /*public override bool isValidMove(BoardSpace_Model tile)
        {
            return MoveSets.King.isValidMove(this, tile);
        }*/
    }
}
