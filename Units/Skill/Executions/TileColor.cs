using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame.Executions
{
    public class TileColor : Execution
    {
        tileType tileColor;
        public TileColor(ChessPiece inCP, tileType inTileColor)//TMP!!
            : base(inCP)//TMP!!
        {
            tileColor = inTileColor;
        }
        /*public TileColor(List<BoardSpace_Model> inValidTiles, ChessPiece inCP)
            : base(inCP)
        {
            validTiles = inValidTiles;
        }*/
        public override bool canExecute()
        {
            //return validTiles.contains(currentPosition);
            if (tileColor == cp.getPosition().getTileType())
                return true;
            return false;
        }
    }
}
