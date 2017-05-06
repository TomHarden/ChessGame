using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    public class Pawn : ChessPiece
    {
        protected int direction;//+1 is upwards, -1 is downwards
        public Pawn(string inName, species inRace, locale inRegion, string inFlavorText, Player inPlayer, BoardSpace_Model inPosition, int startingDirection)
            : base(inName, inRace, inRegion, inFlavorText, inPlayer, inPosition)
        {
            startingPosition = inPosition;
            direction = startingDirection;
            piecetype = pieceType.Pawn;
            moveset = new MoveSets.Pawn();
        }
        /*public override bool isValidMove(BoardSpace_Model tile)
        {
            return MoveSets.Pawn.isValidMove(this, tile);
        }*/
        public int getDirection()
        {
            return direction;
        }
        public void KnightPiece(pieceType t)
        {
            KnightPiece(t, new Skill(this));
        }
        public void KnightPiece(pieceType t, Skill s)
        {
            ChessPiece c = this;
            if (t == pieceType.Queen)
                c = new Queen("Queen", species.Human, locale.Furth, "A Queen, knighted from a Pawn", player, position);
            else if (t == pieceType.Bishop)
                c = new Bishop("Bishop", species.Human, locale.Furth, "A Bishop, knighted from a Pawn", player, position);
            else if (t == pieceType.Knight)
                c = new Knight("Knight", species.Human, locale.Furth, "A Knight, knighted from a Pawn", player, position);
            else if (t == pieceType.Rook)
                c = new Rook("Rook", species.Human, locale.Furth, "A Rook, knighted from a Pawn", player, position);
            else if (t == pieceType.Pawn)
                c = new Pawn("Pawn", species.Human, locale.Furth, "A Pawn, knighted from a Pawn", player, position, direction);
            
            Game_View.messageToPlayer("Knight " + this.getName() + " (" + this.getPieceClass() + ") to a " + c.getPieceClass(), System.Windows.Forms.MessageBoxButtons.OK);
            this.player.replacePiece(this, c);//ie no changes...THIS IS A FORM OF ERROR CATCHING, BUT ITS NOT GREAT!
        }
        public override void moveToPosition(BoardSpace_Model newPosition)//THIS FAILS FRINGE CASES!!! TMP!!!
        {
            BoardSpace_Model previousPosition = this.position;
            base.moveToPosition(newPosition);
            int dummy1 = Math.Abs(this.startingPosition.getRow() - newPosition.getRow());
            int dummy2 = boardModel.getNumRows() - Math.Abs(this.startingPosition.getRow() - boardModel.getNumRows());
            if (!previousPosition.onBorder())
            {
                if (Math.Abs(this.startingPosition.getRow() - newPosition.getRow())
                    >= boardModel.getNumRows() - Math.Abs(this.startingPosition.getRow() - boardModel.getNumRows())
                    && newPosition.onBorder())
                {
                    KnightPiece(pieceType.Queen);
                }
                else if (Math.Abs(this.startingPosition.getColumn() - newPosition.getColumn())
                    >= boardModel.getNumRows() - Math.Abs(this.startingPosition.getRow() - boardModel.getNumRows())
                    && newPosition.onBorder())
                {
                    KnightPiece(pieceType.Queen);
                }
            }
            else if (previousPosition.onBorder() && newPosition.inCorner())
            {
                if (Math.Abs(this.startingPosition.getRow() - newPosition.getRow())
                    >= boardModel.getNumRows() - Math.Abs(this.startingPosition.getRow() - boardModel.getNumRows())
                    && newPosition.onBorder())
                {
                    KnightPiece(pieceType.Queen);
                }
                else if (Math.Abs(this.startingPosition.getColumn() - newPosition.getColumn())
                    >= boardModel.getNumRows() - Math.Abs(this.startingPosition.getRow() - boardModel.getNumRows())
                    && newPosition.onBorder())
                {
                    KnightPiece(pieceType.Queen);
                }
            }
            return;
        }
    }
}
