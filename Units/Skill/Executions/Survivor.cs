using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame.Executions
{
    public abstract class Survivor : Execution
    {
        //only remaining __piece__
        public Survivor(ChessPiece inCP)
            : base(inCP)
        {

        }
    }
    public class SoleTypeSurvivor : Survivor
    {
        //only remaining __piece__
        public SoleTypeSurvivor(ChessPiece inCP)
            : base(inCP)
        {

        }
        public override bool canExecute()
        {
            foreach (ChessPiece piece in cp.getPlayer().getArmy())
                if (cp.getPieceClass() == piece.getPieceClass() && piece != cp)
                    return false;
            return true;
        }
    }
}
