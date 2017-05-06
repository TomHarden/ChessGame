using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.Abilities
{
    class TeleportToSpace : Ability//for now, it will be assumed that all teleports are to unoccupied tiles!
    {
        public TeleportToSpace(ChessPiece inCP)
            : base(inCP)
        {
        }
        public override bool actOutAbility(ChessPiece target = null)//for now, it will be assumed that all teleports are to unoccupied tiles!
        {
            //need to get a list of all available tiles and then have the user select one.
            return true;
        }
    }
    class TeleportToAdjacentSpace : TeleportToSpace//for now, it will be assumed that all teleports are to unoccupied tiles!
    {
        protected Units.Unit home;//this is the unit that will be teleported next to!
        public TeleportToAdjacentSpace(Units.Unit inHome, ChessPiece inCP)
            : base(inCP)
        {
            home = inHome;
        }
        public override bool actOutAbility(ChessPiece target = null)
        {
            //need to get a list of all available tiles around the home piece
            List<Board.BoardSpace_Model> tiles = new List<BoardSpace_Model>();
            foreach (BoardSpace_Model bsm in home.getPosition().getAdjacentTiles())
                if (!bsm.isOccupied())
                    tiles.Add(bsm);
            cp.getPlayer().extraTurn(cp, tiles);
            return true;
            //home.getPosition().getAdjacentTiles();
            //need to get a list of all available tiles and then have the user select one.
        }
    }
}
