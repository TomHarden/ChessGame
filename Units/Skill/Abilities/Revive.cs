using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.Abilities
{
    class Revive : Ability
    {
        public Revive(ChessPiece inCP)
            : base(inCP)
        {
        }
        public override bool actOutAbility(ChessPiece target = null)
        {
            //need to ask the player which piece should be revived!
            ChessPiece revivalPiece;
            try
            {
                revivalPiece = cp.getPlayer().getGraveyard().ElementAt(0);//tmp!
            }
            catch
            {
                return false;
            }
            BoardSpace_Model revivalSpace = null;
            foreach (BoardSpace_Model bsm in cp.getPosition().getAdjacentTiles())
            {
                if (!bsm.isOccupied())
                {
                    revivalSpace = bsm;
                    break;
                }
            }
            //need to remove the piece from the graveyard and place it on a tile adjacent to this piece!
            if (revivalSpace == null)
                return false;
            cp.getPlayer().revivePiece(revivalPiece, revivalSpace);
            Game_View.notifyPlayer(cp.getPlayer(), "Your piece, " + revivalPiece.getName() + ", was revived by " + cp.getName() + "!");
            return true;
        }
    }
}
