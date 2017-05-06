using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame.Executions
{
    public class None : Execution
    {
        public None(ChessPiece inCP)
            : base(inCP)
        {

        }
        public override bool canExecute()
        {
            return true;//should this be false?
        }
    }
}
