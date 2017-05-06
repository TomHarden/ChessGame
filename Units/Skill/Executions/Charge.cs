using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame.Executions
{
    public class Charge : Execution
    {
        protected int currentCharge = 0;
        protected int costCharge = 1;
        public Charge(int inCostCharge, ChessPiece inCP)
            : base(inCP)
        {
            costCharge = inCostCharge;
        }
        public virtual void chargeUp(int i = 1)
        {
            currentCharge += i;
        }
        public override void abilityUsed()
        {
            currentCharge -= costCharge;
        }
        public override bool canExecute()
        {
            return currentCharge >= costCharge;
        }
    }
    public class LimitedCharge : Charge
    {
        protected int maxCharge = 1;
        public LimitedCharge(int inMaxCharge, int inCostCharge, ChessPiece inCP) :
            base(inCostCharge, inCP)
        {
            maxCharge = inMaxCharge;
        }
        public override void chargeUp( int i = 1)
        {
            if (currentCharge < maxCharge)
                base.chargeUp(i);
        }
    }
}
