using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame.Board
{
    public class ChessBoard_View : Panel
    {
        protected Game_View parent;

        protected ChessBoard_Model model;
        protected BoardSpace_View[,] tiles;

        //variables that relate to piece moving
        protected ChessPiece selectedPiece = null;
        protected List<BoardSpace_Model> validMoveTiles = new List<BoardSpace_Model>();

        public ChessBoard_View(ChessBoard_Model inModel, /*Turnset inTurnset,*/ Game_View inParent)
        {
            model = inModel;
            parent = inParent;
            //turnset = inTurnset;
            createBoard();
            drawBoard();
        }
        private void createBoard()
        {
            Size TILE_SIZE = new Size(75, 75);
            tiles = new BoardSpace_View[model.getNumRows(), model.getNumCols()];
            //this.Size = new System.Drawing.Size(model.getNumRows() * TILE_SIZE.Height, model.getNumCols() * TILE_SIZE.Width);
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
            this.Size = new Size(TILE_SIZE.Width * model.getNumCols(), TILE_SIZE.Height * model.getNumRows());
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
                        BoardSpace_Model tmp = model.getTile(r, c);
                        Units.Unit tmp1 = model.getTile(r, c).getUnit();
                        Player tmp2 = model.getTile(r, c).getUnit().getPlayer();
                        Color tmp3 = model.getTile(r, c).getUnit().getPlayer().getColor();
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
            if (selectedPiece == (ChessPiece)eventTile.getUnit()
                && eventTile.isOccupied())//if clicked on selected self, undo selection
            {
                deselectPiece();
            }
            else if (eventTile.isOccupied()
                    && selectedPiece == null
                    && eventTile.getUnit().getPlayer() == parent.getCurrentPlayer())//selecting a piece
            {
                selectPiece((ChessPiece)eventTile.getUnit());
                //validMoveTiles = selectedPiece.getAllValidMoveTiles();//validMoveTiles will hold all tiles which the selectedPiece may move to.  All info needed to know that is contained in the unit itself!
            }
            else if (validMoveTiles.Contains(eventTile) && selectedPiece != null)
            {
                if (!eventTile.isOccupied())//moving selectedPiece
                    model.movePieceToTile(selectedPiece, eventTile);
                else if (eventTile.isOccupied())//taking another player's piece!
                    selectedPiece.pieceInteraction(eventTile.getUnit(), this.model);
                deselectPiece();
            }

            if (this.parent.getCurrentPlayer().hasFocusPiece())
            {
                if (this.selectedPiece != this.parent.getCurrentPlayer().getFocusPiece())
                {
                    selectPiece(this.parent.getCurrentPlayer().getFocusPiece());
                    if (this.parent.getCurrentPlayer().hasFocusTiles())
                        validMoveTiles = this.parent.getCurrentPlayer().getFocusTiles();
                }
            }

            this.drawBoard();//this function now draws both tiles and pieces at the same time, which will presumably be faster to compute!!
            parent.updateView(this);
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
            selectedPiece.getPlayer().setFocusPiece(null);
            this.selectedPiece = null;
            this.validMoveTiles.Clear();
        }
        public void selectPiece(ChessPiece cp)
        {
            cp.getPlayer().setFocusPiece(cp);
            this.selectedPiece = cp;
            this.validMoveTiles = selectedPiece.getAllValidMoveTiles();
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
            /*int r = tile.getModel().getRow();
            int c = tile.getModel().getColumn();
            if ((r + c) % 2 == 0)
                return Color.Peru;
            return Color.Beige;*/
            if (tile.getModel().getTileType() == tileType.White)
                return Color.Beige;
            return Color.Peru;
        }
        
        public ChessPiece getSelectedPiece()
        {
            return selectedPiece;
        }
        
        /*public Player getMovingPlayer()
        {
            return this.model.getParent().getTurnSet().getCurrentPlayer();
        }*///this is a bad idea.  Should probably erase soon.
        
    }
}
