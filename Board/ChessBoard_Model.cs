using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.MoveSets;
using ChessBasedGame.Board;

namespace ChessBasedGame.Board
{
    public class ChessBoard_Model
    {
        int numRows, numCols;
        BoardSpace_Model[,] tiles;
        Game_Model parent;//TMP!!  THIS SHOULD BE REPLACED BY EVENT RAISING IN THE FUTURE!!
        //Turnset turnset;
        public ChessBoard_Model(int inNumRows, int inNumCols, Game_Model inParent)//, Turnset inTurnSet)
        {
            //turnset = inTurnSet;
            parent = inParent;
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
                    tileType t = tileType.Black;
                    if ((r + c) % 2 == 0)
                        t = tileType.White;
                    tiles[r, c] = new BoardSpace_Model(r, c, t, isBorder, isCorner, this);
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
        public List<BoardSpace_Model> getTileStrip(BoardSpace_Model startTile, BoardSpace_Model stopTile, bool isInclusive = false)//inclusive!
        {
            return getTileStrip(startTile.getRow(), stopTile.getRow(), startTile.getColumn(), stopTile.getColumn(), isInclusive);
        }
        public List<BoardSpace_Model> getTileStrip(int r1, int r2, int c1, int c2, bool isInclusive = false)
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
                for (int c = c1; c <= c2; c++)
                    finalList.Add(this.getTile(r, c));
            return finalList;
        }

        /*public List<BoardSpace_Model> getAllValidMoveTiles(ChessPiece cp)
        {
            return cp.getAllValidMoveTiles(this);
        }*/
        /*public List<BoardSpace_Model> getAllValidMoveTiles(ChessPiece cp)
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
                if (!tile.isOccupied() || tile.getUnit().getPlayer() != cp.getPlayer())//ie "if tile not occupied or occupied by another player"...something like this will eventually have to be replaced with "getAllegiance()"
                    finalList.Add(tile);

            this.parseUntakeables(ref finalList, cp);

            //castling
            //if (cp.getPieceClass() == pieceType.King)
            if (cp.getMoveSet().canCastle())
                foreach (ChessPiece piece in cp.getPlayer().getArmy())
                    if (piece.getPieceClass() == pieceType.Rook)
                        if (cp.getMoveSet().validCastle(cp, piece) && this.tileStripIsClearBetween(cp.getPosition(), piece.getPosition()))
                            finalList.Add(piece.getPosition());
            return finalList;
        }*/

        public static bool isOnDiagonal(BoardSpace_Model bsm1, BoardSpace_Model bsm2)
        {
            return (Math.Abs(bsm1.getRow() - bsm2.getRow()) ==
                Math.Abs(bsm1.getColumn() - bsm2.getColumn()));
        }

        //Assumes that this is a vector of tiles to be checked!
        /// <summary>
        /// returns a list of tiles that form vectors from the current tile to the nearest obstruction.
        /// Ex. K a b c R e f g.  With K as the current tile, would return "a b c R"
        /// </summary>
        /// <param name="inList"></param>
        /// <param name="originTile"></param>
        /// <returns></returns>
        public static List<BoardSpace_Model> unobstructedTiles(List<BoardSpace_Model> inList, BoardSpace_Model originTile)
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
                if (!tile.isOccupied())//if tile is empty
                    if (closestObstruction != null)
                    {
                        if (tile.getDistanceFrom(originTile) < closestObstruction.getDistanceFrom(originTile))
                            finalList.Add(tile);
                    }
                    else
                    {
                        finalList.Add(tile);
                    }
            return finalList;
        }

        public static void parseUntakeables(ref List<BoardSpace_Model> list, ChessPiece cp)
        {
            parseAllies(ref list, cp);
            parseObstacles(ref list, cp);
        }
        public static void parseObstacles(ref List<BoardSpace_Model> list, ChessPiece cp)
        {
            List<BoardSpace_Model> tmpList = new List<BoardSpace_Model>();

            foreach (BoardSpace_Model elt in list)
                tmpList.Add(elt);

            foreach (BoardSpace_Model tile in tmpList)
                if (tile.isOccupied() && tile.isObstacle())
                    list.Remove(tile);
        }
        public static void parseAllies(ref List<BoardSpace_Model> list, ChessPiece cp)
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
        public void moveUnitBy(Units.Unit u, int diffR, int diffC)
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
                    c2 = tmp;
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
                if (tile.isOccupied())
                    return false;
            return true;
        }
        public void performCastle(ChessPiece k, ChessPiece r)
        {
            //if k is to the left of r...
            int k_move = 2;
            int r_move = -2;
            //else if k is to the right of r...
            if (k.getPosition().getRow() > r.getPosition().getRow())
            {
                k_move = -2;
                r_move = 2;
            }

            this.movePieceToTile(k,
                                getTile(k.getPosition().getRow(),
                                k.getPosition().getColumn() + k_move));
            this.movePieceToTile(r,
                                getTile(r.getPosition().getRow(),
                                r.getPosition().getColumn() + r_move));
        }
        public void movePieceToTile(ChessPiece cp, BoardSpace_View tile)
        {
            movePieceToTile(cp, tile.getModel());
        }
        public void movePieceToTile(ChessPiece cp, BoardSpace_Model tile)
        {
            cp.getPosition().removeUnit();//remove piece from tile
            cp.moveToPosition(tile);//tell piece its new location
            //tile.setUnit(cp);//set piece on the new tile;

            /*//Knighting a pawn
            if (cp.getPieceClass() == pieceType.Pawn)
            {
                if (cp.getPosition().onBorder())
                {
                    foreach (Player enemyPlayer in cp.getPlayer().getEnemies())
                    {
                        if (enemyPlayer.getStartingTiles().Contains(cp.getPosition()))
                        {
                            //NEED TO WORK OUT A WAY TO CHOOSE WHAT TO KNIGHT TO!!  CUSTOM MessageBoxButtons maybe?
                            Game_View.messageToPlayer("Knight " + cp.getName() + " (" + cp.getPieceClass() + ") to a Queen", System.Windows.Forms.MessageBoxButtons.OK);
                            ((Units.ChessPieces.Pawn)cp).KnightPiece(pieceType.Queen);
                            break;
                            //When knighting a pawn, look at the start tile and compare it to the stop tile:  if border tile, possibly a tile for knighting -> check the row / column of the start and stop tiles and come to a conclusion about the distance traversed!
                        }
                    }
                }
            }*/
            cp.getPlayer().endTurn();
            return;
        }
    }
}
