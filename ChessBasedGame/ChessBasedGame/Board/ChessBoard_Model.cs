using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class ChessBoard_Model
    {
        int numRows, numCols;
        BoardSpace_Model[,] tiles;
        //Turnset turnset;
        public ChessBoard_Model(int inNumRows, int inNumCols)//, Turnset inTurnSet)
        {
            //turnset = inTurnSet;
            numRows = inNumRows;
            numCols = inNumCols;
            tiles = new BoardSpace_Model[numRows, numCols];

            for (int r = 0; r < numRows; r++)
            {
                for (int c = 0; c < numCols; c++)
                {
                    bool isCorner = false;
                    bool isBorder = false;
                    if ((r == numRows - 1 || r == 0)
                        && (c == numCols - 1 || c == 0))
                    {
                        isCorner = true;
                        isBorder = true;
                    }
                    else if (r == numRows - 1
                        || c == numCols - 1
                        || r == 0
                        || c == 0)
                    {
                        isBorder = true;
                    }
                    tiles[r, c] = new BoardSpace_Model(r, c, isBorder, isCorner);
                }
            }
        }

        public int getNumRows()
        {
            return numRows;
        }
        public int getNumCols()
        {
            return numCols;
        }

        public BoardSpace_Model[][] getAllRows()
        {
            BoardSpace_Model[][] allRows = new BoardSpace_Model[numRows][];
            for (int i = 0; i < numRows; i++)
                allRows[i] = getRow(i);//ie get entire row!
            return allRows;
        }
        public BoardSpace_Model[] getRow(int rowNumber)
        {
            if (rowNumber < 0)
            {
                rowNumber = numRows + rowNumber;
            }
            return getRowFromColumn(rowNumber, 0);//ie get entire row!
        }
        public BoardSpace_Model[] getRowFromColumn(int rowNumber, int columnNumber, bool getRightOf = true)
        {
            BoardSpace_Model[] rowTiles;
            int index = 0;
            if (getRightOf)
            {
                rowTiles = new BoardSpace_Model[numCols - columnNumber];
                for (int c = columnNumber; c < numCols; c++)
                {
                    rowTiles[index++] = tiles[rowNumber, c];
                }
            }
            else
            {
                rowTiles = new BoardSpace_Model[columnNumber];
                for (int c = columnNumber; c >= 0; c--)
                {
                    rowTiles[index++] = tiles[rowNumber, c];
                }
            }
            return rowTiles;
        }


        public BoardSpace_Model[][] getAllColumns()
        {
            BoardSpace_Model[][] allColumns = new BoardSpace_Model[numCols][];
            for (int i = 0; i < numCols; i++)
                allColumns[i] = getColumn(i);//ie get entire row!
            return allColumns;
        }
        public BoardSpace_Model[] getColumn(int columnNumber)
        {
            if (columnNumber < 0)
            {
                columnNumber = numCols + columnNumber;
            }
            return getColumnFromRow(columnNumber, 0);//ie get entire column!
        }
        public BoardSpace_Model[] getColumnFromRow(int columnNumber, int rowNumber, bool getAboveOf = true)
        {
            BoardSpace_Model[] columnTiles;
            int index = 0;
            if (getAboveOf)
            {
                columnTiles = new BoardSpace_Model[numRows - rowNumber];
                for (int r = rowNumber; r < numRows; r++)
                {
                    columnTiles[index++] = tiles[r, columnNumber];
                }
            }
            else
            {
                columnTiles = new BoardSpace_Model[rowNumber];
                for (int r = rowNumber; r >= 0; r--)
                {
                    columnTiles[index++] = tiles[r, columnNumber];
                }
            }
            return columnTiles;
        }

        public BoardSpace_Model getTile(int rowNumber, int columnNumber)
        {
            return tiles[rowNumber, columnNumber];
        }
        public List<BoardSpace_Model> getTileStrip(int r1, int r2, int c1, int c2)
        {
            if (r1 > r2)
            {
                int tmp = r2;
                r2 = r1;
                r1 = tmp;
            }
            if (c1 > c2)
            {
                int tmp = c2;
                c2 = c1;
                c1 = tmp;
            }
            List<BoardSpace_Model> finalList = new List<BoardSpace_Model>();
            for (int r = r1; r <= r2; r++)
            {
                for (int c = c1; c <= c2; c++)
                {
                    finalList.Add(getTile(r, c));
                }
            }
            return finalList;
        }

        public List<BoardSpace_Model> getAllValidMoveTiles(ChessPiece cp)
        {
            List<BoardSpace_Model> finalList = new List<BoardSpace_Model>();
            List<BoardSpace_Model> prelimList = new List<BoardSpace_Model>();

            //get all possible valid spaces that the chess piece COULD move to!
            for (int r = 0; r < numRows; r++)
                for (int c = 0; c < numCols; c++)
                    if (cp.isValidMove(tiles[r, c]))
                        prelimList.Add(tiles[r, c]);


            //next, find any adjacent spaces to valid spaces that have units on them.  For instance, a rook cannot ram through two lines of enemy units, it can only take the one most out front.  Therefore, the spaces behind the enemy space are off limits
            //in short, remove any spaces that are "blocked" from the "list" object
            List<BoardSpace_Model> RightTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> LeftTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> TopTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> BottomTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> TopRightTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> TopLeftTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> BottomRightTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> BottomLeftTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> StandaloneTiles = new List<BoardSpace_Model>();
            foreach (BoardSpace_Model tile in prelimList)//look for obstructions
            {
                this.CategorizeTileIntoDirectionBasedList(
                    ref RightTiles,
                    ref LeftTiles,
                    ref TopTiles,
                    ref BottomTiles,
                    ref TopRightTiles,
                    ref TopLeftTiles,
                    ref BottomRightTiles,
                    ref BottomLeftTiles,
                    ref StandaloneTiles,
                    tile,
                    cp.getPosition());
            }

            for (int i = 0; i < 8; i++)
            {
                List<BoardSpace_Model> tileVectorList = RightTiles;
                switch (i)
                {
                    case 0:
                        tileVectorList = RightTiles;
                        break;
                    case 1:
                        tileVectorList = LeftTiles;
                        break;
                    case 2:
                        tileVectorList = TopTiles;
                        break;
                    case 3:
                        tileVectorList = BottomTiles;
                        break;
                    case 4:
                        tileVectorList = TopRightTiles;
                        break;
                    case 5:
                        tileVectorList = TopLeftTiles;
                        break;
                    case 6:
                        tileVectorList = BottomRightTiles;
                        break;
                    case 7:
                        tileVectorList = BottomLeftTiles;
                        break;
                    default:
                        break;
                }
                foreach (BoardSpace_Model tile in this.unobstructedTiles(tileVectorList, cp.getPosition()))
                    finalList.Add(tile);
            }
            
            foreach (BoardSpace_Model tile in StandaloneTiles)
                if (!tile.isOccupied() || tile.getUnit().getPlayer() != cp.getPlayer())//something like this will eventually have to be replaced with "getAllegiance()"
                    finalList.Add(tile);

            this.parseAllies(ref finalList, cp);

            //castling
            if (cp.getPieceClass() == pieceType.King)
            {
                foreach (ChessPiece piece in cp.getPlayer().getArmy())
                {
                    if (piece.getPieceClass() == pieceType.Rook)
                    {
                        if (KingMoveSet.canCastle(cp, piece) && this.tileStripIsClearBetween(cp.getPosition(), piece.getPosition()))
                        {
                            finalList.Add(piece.getPosition());
                        }
                    }
                }
            }

            return finalList;
        }

        public static bool isOnDiagonal(BoardSpace_Model bsm1, BoardSpace_Model bsm2)
        {
            return (Math.Abs(bsm1.getRow() - bsm2.getRow()) ==
                Math.Abs(bsm1.getColumn() - bsm2.getColumn()));
        }

        //Assumes that this is a vector of tiles to be checked!
        public List<BoardSpace_Model> unobstructedTiles(List<BoardSpace_Model> inList, BoardSpace_Model originTile)
        {
            List<BoardSpace_Model> finalList = new List<BoardSpace_Model>();

            BoardSpace_Model closestObstruction = null;
            foreach (BoardSpace_Model tile in inList)
            {
                if (tile.isOccupied())
                {
                    if (closestObstruction == null)
                    {
                        closestObstruction = tile;
                        finalList.Add(tile);
                    }
                    else if (tile.getDistanceFrom(originTile) < closestObstruction.getDistanceFrom(originTile))
                    {
                        finalList.Remove(closestObstruction);
                        closestObstruction = tile;
                        finalList.Add(tile);
                    }
                }
            }

            foreach (BoardSpace_Model tile in inList)
            {
                if (!tile.isOccupied())//if tile is empty
                {
                    if (closestObstruction != null)
                    {
                        if (tile.getDistanceFrom(originTile) < closestObstruction.getDistanceFrom(originTile))
                            finalList.Add(tile);
                    }
                    else
                    {
                        finalList.Add(tile);
                    }
                }
            }
            return finalList;
        }

        public void CategorizeTileIntoDirectionBasedList(
            ref List<BoardSpace_Model> RightTiles,
            ref List<BoardSpace_Model> LeftTiles,
            ref List<BoardSpace_Model> TopTiles,
            ref List<BoardSpace_Model> BottomTiles,
            ref List<BoardSpace_Model> TopRightTiles,
            ref List<BoardSpace_Model> TopLeftTiles,
            ref List<BoardSpace_Model> BottomRightTiles,
            ref List<BoardSpace_Model> BottomLeftTiles,
            ref List<BoardSpace_Model> StandaloneTiles,
            BoardSpace_Model tile, BoardSpace_Model originTile)
        {
            if (tile.getRow() == originTile.getRow())
            {
                //go same row to the right;
                if (tile.getColumn() > originTile.getColumn())
                {
                    RightTiles.Add(tile);
                    return;
                }
                //go same row to the left;
                else if (tile.getColumn() < originTile.getColumn())
                {
                    LeftTiles.Add(tile);
                    return;
                }
            }
            else if (tile.getColumn() == originTile.getColumn())
            {
                //go same column to the top;
                if (tile.getRow() > originTile.getRow())
                {
                    TopTiles.Add(tile);
                    return;
                }
                //go same column to the bottom;
                else if (tile.getRow() < originTile.getRow())
                {
                    BottomTiles.Add(tile);
                    return;
                }
            }
            else if (ChessBoard_Model.isOnDiagonal(originTile, tile))
            {
                //upwards diagonal
                if (tile.getRow() > originTile.getRow())
                {
                    //rightwards diagonal
                    if (tile.getColumn() > originTile.getColumn())
                    {
                        TopRightTiles.Add(tile);
                        return;
                    }
                    //leftwards diagonal
                    else if (tile.getColumn() < originTile.getColumn())
                    {
                        TopLeftTiles.Add(tile);
                        return;
                    }
                }
                //downwards diagonal
                else if (tile.getRow() < originTile.getRow())
                {
                    if (tile.getColumn() > originTile.getColumn())
                    {
                        BottomRightTiles.Add(tile);
                        return;
                    }
                    //leftwards diagonal
                    else if (tile.getColumn() < originTile.getColumn())
                    {
                        BottomLeftTiles.Add(tile);
                        return;
                    }
                }
            }
            else
            {
                StandaloneTiles.Add(tile);
                return;
            }
            return;
        }
        protected void parseAllies(ref List<BoardSpace_Model> list, ChessPiece cp)
        {
            List<BoardSpace_Model> tmpList = new List<BoardSpace_Model>();

            foreach (BoardSpace_Model elt in list)
                tmpList.Add(elt);

            //remove spaces that are no longer considered valid, for instance, a piece on the space is an allied piece
            foreach (BoardSpace_Model tile in tmpList)
                if (tile.isOccupied())
                    if (tile.getUnit().getPlayer() == cp.getPlayer())
                        list.Remove(tile);
        }
        public void moveUnitBy(Unit u, int diffR, int diffC)
        {
            int currentR = u.getPosition().getRow();
            int currentC = u.getPosition().getColumn();
            int finalR = currentR + diffR;
            int finalC = currentC + diffC;
            u.setPosition(tiles[finalR, finalC]);
        }
        public bool tileStripIsClearBetween(BoardSpace_Model tile1, BoardSpace_Model tile2)
        {
            int r1 = tile1.getRow();
            int r2 = tile2.getRow();
            int c1 = tile1.getColumn();
            int c2 = tile2.getColumn();
            if (tile1.getRow() == tile2.getRow())
            {
                if (c1 > c2)
                {
                    int tmp = c1;
                    c1 = c2;
                    c2 = c1;
                }
                c1++;
                c2--;
            }
            else if (tile1.getColumn() == tile2.getColumn())
            {
                if (r1 > r2)
                {
                    int tmp = r1;
                    r1 = r2;
                    r2 = r1;
                }
                r1++;
                r2--;
            }
            else
            {
                if (r1 > r2)
                {
                    int tmp = r1;
                    r1 = r2;
                    r2 = r1;
                }
                if (c1 > c2)
                {
                    int tmp = c1;
                    c1 = c2;
                    c2 = c1;
                }
                r1++;
                r2--;
                c1++;
                c2--;
            }

            foreach (BoardSpace_Model tile in getTileStrip(r1, r2, c1, c2))
            {
                if (tile.isOccupied())
                {
                    return false;
                }
            }

            return true;
        }
        /*public Player getCurrentPlayer()
        {
            return turnset.getCurrentPlayer();
        }
        public void endTurn()
        {
            turnset.endPlayerTurn();
        }*/
    }
}
