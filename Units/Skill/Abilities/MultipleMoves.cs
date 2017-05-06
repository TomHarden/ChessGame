using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame.Abilities
{
    class MultipleMoves : Ability
    {
        protected int totalMoves = 2;//minimum of two moves!//ie one standard and one extra!
        protected int currentMoves = 1;//the ability would be invoked after moving once
        //protected Turnset turnset = null;
        public MultipleMoves(int inTotalMoves, ChessPiece inCP)
            : base(inCP)
        {
            totalMoves = inTotalMoves;
        }
        public override bool actOutAbility(ChessPiece target = null)
        {
            //need to access the turnset and give the player another turn
            //lock the player focus to this piece (so that only this piece can be moved.
            if (++currentMoves <= totalMoves)
            {
                cp.getPlayer().extraTurn(cp);//ie player gets an extra turn with this piece!
            }
            else
            {
                currentMoves = 1;
                return true;
                //cp.getPlayer().endTurn();//this may be unsafe...
            }
            return false;
        }
    }
}
