using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class BoardSpace_Model
    {
        int space_ID;
        private Unit unit = null;
        int row, column;
        bool isBorderTile;
        bool isCornerTile;
        /*public BoardSpace_Model(int inRow, int inColumn)
        {
            row = inRow;
            column = inColumn;
            unit = null;
        }*/
        public BoardSpace_Model(int inRow, int inColumn, bool inIsBorderTile, bool inIsCornerTile, Unit inUnit = null)
        {
            row = inRow;
            column = inColumn;
            unit = inUnit;
            isBorderTile = inIsBorderTile;
            isCornerTile = inIsCornerTile;
        }
        public bool isOccupied()
        {
            return unit != null;
        }
        public bool isObstacle()
        {
            return unit.isObstacle();
        }
        public Unit getUnit()
        {
            return unit;
        }
        public void removeUnit()
        {
            unit = null;
        }
        public void setUnit(Unit newUnit)
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
    }
}
