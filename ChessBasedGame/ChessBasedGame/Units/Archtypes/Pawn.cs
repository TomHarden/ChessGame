using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class Pawn : ChessPiece
    {
        protected int direction;//+1 is upwards, -1 is downwards
        public Pawn(string inName, string inFlavorText, Skill inSkill, Player inPlayer, BoardSpace_Model inPosition, int startingDirection)
            : base(inName, inFlavorText, inSkill, inPlayer, inPosition)
        {
            direction = startingDirection;
            piecetype = pieceType.Pawn;
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            return PawnMoveSet.isValidMove(this, tile);
        }
        public int getDirection()
        {
            return direction;
        }
        public void KnightPiece(pieceType t)
        {
            KnightPiece(t, new Skill(skillType.None));
        }
        public void KnightPiece(pieceType t, Skill s)
        {
            ChessPiece c = this;
            if (t == pieceType.Queen)
                c = new Queen("Queen", "A Queen, knighted from a Pawn", s, player, position);
            else if (t == pieceType.Bishop)
                c = new Bishop("Bishop", "A Bishop, knighted from a Pawn", s, player, position);
            else if (t == pieceType.Knight)
                c = new Knight("Knight", "A Knight, knighted from a Pawn", s, player, position);
            else if (t == pieceType.Rook)
                c = new Rook("Rook", "A Rook, knighted from a Pawn", s, player, position);
            else if (t == pieceType.Pawn)
                c = new Pawn("Pawn", "A Pawn, knighted from a Pawn", s, player, position, direction);
            this.player.replaceUnit(this, c);//ie no changes...THIS IS A FORM OF ERROR CATCHING, BUT ITS NOT GREAT!
        }
    }
}
