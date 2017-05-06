using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units
{
    public abstract class Unit
    {
        protected ChessBoard_Model boardModel;
        protected string name = "Some unit";
        protected string flavorText = "Description here";
        protected bool isObstacle_val = false;
        protected BoardSpace_Model position;
        protected Player player;
        protected bool isUnMoved = true;

        public Unit(Player inPlayer, BoardSpace_Model inPosition)
        {
            player = inPlayer;
            setPosition(inPosition);
            boardModel = position.getComposingBoard();
        }

        public Bitmap getTileImage()
        {
            return (Bitmap)Bitmap.FromFile("Images\\" + this.getFileName() + ".bmp");
            //IMAGES ARE KEPT IN THE WORKING DIRECTORY (DEBUG FOLDER) FOR NOW!!
        }
        public ChessBoard_Model getBoard()
        {
            return this.boardModel;
        }
        public bool isObstacle()
        {
            return isObstacle_val;
        }
        public string getFileName()
        {
            string fileName = name;
            fileName = fileName.Replace(",", "_");
            fileName = fileName.Replace(" ", "_");
            fileName = fileName.Replace("'", "_");
            return fileName;
        }
        public string getName()
        {
            return name;
        }
        public BoardSpace_Model getPosition()
        {
            return position;
        }
        public virtual void moveToPosition(BoardSpace_Model newPosition)
        {
            setPosition(newPosition);
            isUnMoved = false;
        }
        public void setPosition(BoardSpace_Model newPosition)
        {
            if (position != null)
                this.position.clearSpace();
            position = newPosition;
            position.setUnit(this);
        }
        public Player getPlayer()
        {
            return player;
        }
        public bool isUnmoved()
        {
            return this.isUnMoved;
        }
        public virtual void pieceInteraction(Unit reciever, ChessBoard_Model cbm)
        {
            return;
            /*if (eventTile.getUnit().getPlayer()/..getAllegiance()./ == selectedPiece.getPlayer()/..getAllegiance()./)
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
            }*/
        }
    }
}
