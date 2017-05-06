using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.MoveSets//the "isValidMove()" functions shoould really be static!
{
    public abstract class MoveSet//abstract
    {
        public virtual bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            return true;
        }
        public virtual bool canCastle()// returns whether the unit is allowed to castle at all.
        {
            return false;
        }
        //public bool validCastle()
        public virtual bool validCastle(ChessPiece k, ChessPiece r/*, List<BoardSpace_Model> tilesBetweenKingAndRook*/)//does not check for obstructions!//assumes the tiles are between the King and Rook!
        {
            if (k.isUnmoved() && r.isUnmoved()
                && k.getPosition().getRow() == r.getPosition().getRow()
                && k.getPosition().getDistanceFrom(r.getPosition()) == 3)
                return true;
            return false;
        }
    }
    public class DualMoveSets : MoveSet
    {
        MoveSet ms1, ms2;
        public DualMoveSets(MoveSet inMS1, MoveSet inMS2)
            : base()
        {
            ms1 = inMS1;
            ms2 = inMS2;
        }
        public override bool isValidMove(ChessPiece cp, BoardSpace_Model newSpace)
        {
            return (ms1.isValidMove(cp, newSpace) || ms2.isValidMove(cp, newSpace));
        }
        public virtual bool canCastle()// returns whether the unit is allowed to castle at all.
        {
            return false;
        }
        //public bool validCastle()
        public virtual bool validCastle(ChessPiece k, ChessPiece r/*, List<BoardSpace_Model> tilesBetweenKingAndRook*/)//does not check for obstructions!//assumes the tiles are between the King and Rook!
        {
            if (k.isUnmoved() && r.isUnmoved()
                && k.getPosition().getRow() == r.getPosition().getRow()
                && k.getPosition().getDistanceFrom(r.getPosition()) == 3)
                return true;
            return false;
        }
    }
}
