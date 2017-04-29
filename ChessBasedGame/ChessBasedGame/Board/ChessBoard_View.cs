using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChessBasedGame
{
    public class ChessBoard_View : Panel
    {
        protected Game_View parent;

        protected ChessBoard_Model model;
        protected BoardSpace_View[,] tiles;

        //variables that relate to piece moving
        protected ChessPiece selectedPiece = null;
        protected List<BoardSpace_Model> validMoveTiles = new List<BoardSpace_Model>();

        //
        protected Turnset turnset;

        public ChessBoard_View(ChessBoard_Model inModel, Turnset inTurnset, Game_View inParent)
        {
            model = inModel;
            parent = inParent;
            turnset = inTurnset;
            createBoard();
            drawBoard();
        }
        private void createBoard()
        {
            Size TILE_SIZE = new Size(75, 75);
            tiles = new BoardSpace_View[model.getNumRows(), model.getNumCols()];
            this.Size = new System.Drawing.Size(model.getNumRows() * TILE_SIZE.Height, model.getNumCols() * TILE_SIZE.Width);
            this.SuspendLayout();
            for (int r = 0; r < model.getNumRows(); r++)
            {
                for (int c = 0; c < model.getNumCols(); c++)
                {
                    tiles[r, c] = new BoardSpace_View(model.getTile(r, c));
                    tiles[r, c].Location = new System.Drawing.Point(c * TILE_SIZE.Width, r * TILE_SIZE.Height);
                    tiles[r, c].Size = TILE_SIZE;
                    tiles[r, c].BackColor = getDefaultTileColor(tiles[r, c]);
                    this.tiles[r, c].Click += new System.EventHandler(this.ChessBoard_View_Click);
                    this.Controls.Add(tiles[r, c]);//+_+ see comment header below!
                }
            }
            this.ResumeLayout(false);
        }

        public void drawBoard()//draws board and pieces
        {
            Color fontColor, backgroundColor;
            Bitmap pieceImage = null;
            string tileText;
            for (int r = 0; r < model.getNumRows(); r++)
            {
                for (int c = 0; c < model.getNumCols(); c++)
                {
                    if (model.getTile(r, c).isOccupied())//has a unit on it
                    {
                        tileText = model.getTile(r, c).getUnit().getName() + "\n" + getTileName(r,c);
                        if (tiles[r,c].getModel().getUnit() == selectedPiece)
                            fontColor = Color.Green;
                        else
                            fontColor = Color.Red;
                        pieceImage = model.getTile(r, c).getUnit().getTileImage();
                        //pieceImage = new Bitmap("c:\\Documents and Settings\\Teake.TEAKEPC\\My Documents\\My Pictures\\Chess Pieces\\" + model.getTile(r, c).getUnit().getImage() + ".bmp");
                        pieceImage.MakeTransparent(Color.White);
                        colorPieceImage(ref pieceImage, model.getTile(r, c).getUnit().getPlayer().getColor());
                        
                    }
                    else//is empty tile
                    {
                        pieceImage = null;
                        fontColor = Color.Black;
                        tileText = getTileName(r, c);
                        
                    }

                    backgroundColor = getDefaultTileColor(tiles[r, c]);
                    if (validMoveTiles.Contains(tiles[r, c].getModel()))
                        backgroundColor = Color.LightGreen;

                    tiles[r, c].BackgroundImage = pieceImage;
                    tiles[r, c].Text = tileText;
                    tiles[r, c].ForeColor = fontColor;
                    tiles[r, c].BackColor = backgroundColor;
                }
            }
            return;//+_+tmp
        }
        public void getTiles(ref Button[,] allTiles)//this should really be pass by ref!!
        {
            allTiles = tiles;
        }
        public void colorPieceImage(ref Bitmap pieceImage, Color playerColor)
        {
            for (int h = 0; h < pieceImage.Height; h++)
                for (int w = 0; w < pieceImage.Width; w++)
                    if (pieceImage.GetPixel(w, h).ToArgb() == Color.Black.ToArgb())
                        pieceImage.SetPixel(w, h, playerColor);
        }
        /*public void invertColor(ref Color inColor)
        {
            Color invertedColor = new Color();
        }*/
        public string getTileName(int r, int c)
        {
            char tmp_c = 'A';
            for (int i = 0; i < r; i++)
                tmp_c++;
            return "" + tmp_c + (c + 1);
        }



        private void ChessBoard_View_Click(object sender, System.EventArgs e)//BoardSpace_Model eventTile)
        {
            BoardSpace_Model eventTile = ((BoardSpace_View)sender).getModel();
            if (selectedPiece == (ChessPiece)eventTile.getUnit())//if clicked on selected self, undo selection
            {
                deselectPiece();
            }
            else if (eventTile.isOccupied()
                && selectedPiece == null
                && eventTile.getUnit().getPlayer() == turnset.getCurrentPlayer())//selecting a piece
            {
                selectedPiece = (ChessPiece) eventTile.getUnit();
                validMoveTiles = model.getAllValidMoveTiles(selectedPiece);//validMoveTiles will hold all tiles which the selectedPiece may move to.  All info needed to know that is contained in the unit itself!
            }
            else if (validMoveTiles.Contains(eventTile) && selectedPiece != null)
            {
                if (!eventTile.isOccupied())//moving selectedPiece
                {
                    movePieceToTile(selectedPiece, eventTile);
                    deselectPiece();//the piece is no longer selected (be careful this does not obliterate the piece to be moved!)
                }
                else if (eventTile.isOccupied())//taking another player's piece!
                {
                    if (eventTile.getUnit().getPlayer()/*.getAllegiance()*/ == selectedPiece.getPlayer()/*.getAllegiance()*/)
                    {
                        //eventually will check if the piece is a king or rook, and is trying to take a rook or king, check if castling is possible!)
                        //selectedPiece = null;//remove??
                        if (((ChessPiece)eventTile.getUnit()).getPieceClass() == pieceType.Rook)
                        {
                            performCastle((King)selectedPiece, (Rook)eventTile.getUnit());
                            deselectPiece();
                        }
                    }
                    else//piece is enemy, and will be taken!
                    {
                        removePieceFromGame((ChessPiece)eventTile.getUnit());//remove the piece occupying the eventTile from the game;
                        movePieceToTile(selectedPiece, eventTile);//move the selectedPiece onto that tile
                        deselectPiece();//will eventually involve taking that player's piece!
                    }
                }
            }
            this.drawBoard();//this function now draws both tiles and pieces at the same time, which will presumably be faster to compute!!
            parent.updateView(this);
            return;
        }
        public void movePieceToTile(ChessPiece cp, BoardSpace_View tile)
        {
            movePieceToTile(cp, tile.getModel());
        }
        public void movePieceToTile(ChessPiece cp, BoardSpace_Model tile)
        {
            cp.getPosition().removeUnit();//remove piece from tile
            cp.moveToPosition(tile);//tell piece its new location
            tile.setUnit(cp);//set piece on the new tile;
            
            //Knighting a pawn
            if (cp.getPieceClass() == pieceType.Pawn)
            {
                if (cp.getPosition().onBorder())
                {
                    foreach (Player enemy in cp.getPlayer().getEnemies())
                    {
                        if (enemy.getStartingTiles().Contains(cp.getPosition()))
                        {
                            ((Pawn)cp).KnightPiece(pieceType.Queen);
                            break;
                        }
                    }
                }
            }
            turnset.endPlayerTurn();
            return;
        }
        public void removePieceFromGame(ChessPiece cp)
        {
            cp.getPosition().removeUnit();//remove piece from chessTile (and board)
            cp.getPlayer().removeUnit(cp);//remove piece from player
            //put piece into graveyard
        }
        public void deselectPiece()
        {
            this.selectedPiece = null;
            this.validMoveTiles.Clear();
        }
        public void selectPiece(ChessPiece cp)
        {
            //deselectPiece();
            this.selectedPiece = cp;
            this.validMoveTiles = this.model.getAllValidMoveTiles(selectedPiece);
        }
        public ChessBoard_Model getModel()
        {
            return model;
        }
        public void setModel(ChessBoard_Model newModel)
        {
            model = newModel;
        }
        private Color getDefaultTileColor(BoardSpace_View tile)
        {
            int r = tile.getModel().getRow();
            int c = tile.getModel().getColumn();
            if ((r + c) % 2 == 0)
                return Color.Peru;
            return Color.Beige;
        }
        protected void performCastle(King k, Rook r)
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
                                model.getTile(k.getPosition().getRow(),
                                k.getPosition().getColumn() + k_move));
            this.movePieceToTile(r,
                                model.getTile(r.getPosition().getRow(),
                                r.getPosition().getColumn() + r_move));
            turnset.endPlayerTurn();
        }
        public ChessPiece getSelectedPiece()
        {
            return selectedPiece;
        }
        
    }
}
