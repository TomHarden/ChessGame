using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame.Executions
{
    public abstract class Execution
    {
        //protected bool canExecuteSkill = false;
        protected ChessPiece cp;
        public Execution(ChessPiece inCP)
        {
            cp = inCP;
        }
        public virtual bool canExecute()
        {
            return false;
        }
        public virtual void abilityUsed()
        {
            return;
        }
    }
}
