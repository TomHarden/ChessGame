using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame.Executions
{
    class PlayerDeadCount : Execution
    {
        protected int threshold = 1;//must be a minimum of 1!
        public PlayerDeadCount(int inThreshold, ChessPiece inCP)
            : base(inCP)
        {
            threshold = inThreshold;
        }
        public override bool canExecute()
        {
            return cp.getPlayer().getGraveyard().Count >= threshold;
        }
    }
    class TotalDeadCount : PlayerDeadCount
    {
        public TotalDeadCount(int inThreshold, ChessPiece inCP)
            : base(inThreshold, inCP)
        {
        }
        public override bool canExecute()
        {
            if (base.canExecute())
                return true;
            else
            {
                int totalDead = cp.getPlayer().getGraveyard().Count;
                foreach (Player p in cp.getPlayer().getEnemies())
                    totalDead += p.getGraveyard().Count();
                foreach (Player p in cp.getPlayer().getAllies())
                    totalDead += p.getGraveyard().Count();
                return totalDead >= threshold;
            }
        }
    }
}
