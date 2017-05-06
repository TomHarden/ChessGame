using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units;

namespace ChessBasedGame.Board
{
    public enum tileType { White, Black };//may be others in the future...maybe...
    public class BoardSpace_Model
    {
        private Units.Unit unit = null;
        int row, column;
        bool isBorderTile;
        bool isCornerTile;
        tileType tileType;
        ChessBoard_Model composingBoard;
        /*public BoardSpace_Model(int inRow, int inColumn)
        {
            row = inRow;
            column = inColumn;
            unit = null;
        }*/
        public BoardSpace_Model(int inRow, int inColumn, tileType inTileType, bool inIsBorderTile, bool inIsCornerTile, ChessBoard_Model inComposingBoard, Units.Unit inUnit = null)
        {
            row = inRow;
            column = inColumn;
            tileType = inTileType;
            unit = inUnit;
            isBorderTile = inIsBorderTile;
            isCornerTile = inIsCornerTile;
            composingBoard = inComposingBoard;
        }
        public bool isOccupied()
        {
            return unit != null;
        }
        public bool isObstacle()
        {
            return unit.isObstacle();
        }
        public Units.Unit getUnit()
        {
            return unit;
        }
        public void removeUnit()
        {
            unit = null;
        }
        public void setUnit(Units.Unit newUnit)
        {
            unit = newUnit;
        }
        public void clearSpace()
        {
            unit = null;
        }
        public int getRow()
        {
            return row;
        }
        public int getColumn()
        {
            return column;
        }
        public double getDistanceFrom(BoardSpace_Model otherTile)
        {
            //a2 + b2 = c2
            return Math.Sqrt(Math.Pow(this.getColumn() - otherTile.getColumn(), 2) + Math.Pow(this.getRow() - otherTile.getRow(),2));
        }
        public bool onBorder()
        {
            return isBorderTile;
        }
        public bool inCorner()
        {
            return isCornerTile;
        }
        public tileType getTileType()
        {
            return this.tileType;
        }
        public List<BoardSpace_Model> getAdjacentTiles()
        {
            List<BoardSpace_Model> adjTilesList = new List<BoardSpace_Model>();
            for (int rMod = -1; rMod <= 1; rMod++)
                for (int cMod = -1; cMod <= 1; cMod++)
                    try
                    {
                        BoardSpace_Model tile = composingBoard.getTile(this.row + rMod, this.column + cMod);
                        if (tile != this)
                            adjTilesList.Add(tile);
                    }
                    catch
                    {
                        continue;
                    }
            return adjTilesList;
        }
        /*public List<BoardSpace_Model> getTileStrip(BoardSpace_Model stopTile)
        {
            return composingBoard.getTileStrip(this, stopTile);
        }*/
        public ChessBoard_Model getComposingBoard()
        {
            return composingBoard;
        }
    }
}
