using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.MoveSets;

namespace ChessBasedGame.Abilities
{
    public class GainMoveSet : Ability //THIS CLASS PERMANENTLY GAINS A MOVESET!
    {
        protected MoveSets.MoveSet ms;
        public GainMoveSet(MoveSets.MoveSet inMS, ChessPiece inCP)
            : base(inCP)
        {
            ms = inMS;
        }
        public override bool actOutAbility(ChessPiece target = null)
        {
            cp.setMoveSet(new MoveSets.DualMoveSets(cp.getMoveSet(), ms));
            return true;
            //when valid moves are being determined, need to interface with this moveset as well!!
        }
    }

    public class GainMoveSet_Temporary : GainMoveSet
    {
        protected MoveSets.MoveSet originalMS;
        public GainMoveSet_Temporary(MoveSets.MoveSet inAdditionalMS, ChessPiece inCP)
            : base(inAdditionalMS, inCP)
        {
            originalMS = cp.getMoveSet();
        }
        public void updateOriginalMoveSet(MoveSets.MoveSet newOriginalMS)
        {
            originalMS = newOriginalMS;
        }
        public override bool actOutAbility(ChessPiece target = null)
        {
            cp.setMoveSet(new MoveSets.DualMoveSets(cp.getMoveSet(), ms));
            return true;
        }
        public override void retractAbility()
        {
            cp.setMoveSet(originalMS);
        }
    }

    public class LoseMoveSet : GainMoveSet_Temporary
    {
        public LoseMoveSet(MoveSets.MoveSet inAdditionalMS, ChessPiece inCP)
            : base(inAdditionalMS, inCP)
        {
            cp.setMoveSet(new MoveSets.DualMoveSets(cp.getMoveSet(), ms));
        }
        public override bool actOutAbility(ChessPiece target = null)
        {
            cp.setMoveSet(originalMS);
            return true;
        }
        public override void retractAbility()//ie restore DualMovesets!
        {
            cp.setMoveSet(new MoveSets.DualMoveSets(cp.getMoveSet(), ms));
        }
    }
    /*public class GainMoveSet_Permanent : GainMoveSet
    {
        public override void actOutAbility()
        {
            cp.setMoveSet(new MoveSets.DualMoveSets(cp.getMoveSet(), new MoveSets.Rook()));
            //when valid moves are being determined, need to interface with this moveset as well!!
        }
    }*/
}
