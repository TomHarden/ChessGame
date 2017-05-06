﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.MoveSets
{
    public class Rook : MoveSet
    {
        public override bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            if (cp.getPosition().getRow() == newSpace.getRow()
                || cp.getPosition().getColumn() == newSpace.getColumn())
                return true;
            return false;
        }
        public override bool validCastle(ChessPiece r, ChessPiece k/*, List<BoardSpace_Model> tilesBetweenKingAndRook*/)//does not check for obstructions!//assumes the tiles are between the King and Rook!
        {
            if (k.isUnmoved() && r.isUnmoved()
                && k.getPosition().getRow() == r.getPosition().getRow()
                && k.getPosition().getDistanceFrom(r.getPosition()) == 3)
                return true;
            return false;
        }
        public override bool canCastle()// returns whether the unit is allowed to castle at all.
        {
            return true;
        }
    }
}
