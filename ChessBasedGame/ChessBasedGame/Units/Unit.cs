using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ChessBasedGame
{
    public enum pieceType { King, Queen, Knight, Bishop, Rook, Pawn, Special }//obstacle was removed from here!

    public abstract class Unit
    {
        protected string name = "Some unit";
        protected string flavorText = "Description here";
        protected bool isObstacle_val = false;
        protected BoardSpace_Model position;
        protected Player player;
        protected bool isUnMoved = true;

        public Bitmap getTileImage()
        {
            return (Bitmap)Bitmap.FromFile(this.getFileName() + ".bmp");
            //IMAGES ARE KEPT IN THE WORKING DIRECTORY (DEBUG FOLDER) FOR NOW!!
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
    }
    public abstract class Obstacle : Unit
    {
        enum type {Water, Mountain, Canyon, Magic, Fire, Forest}//more to come!
        //isObstacle_val = true;will need to be done in constructor!
    }
    public abstract class ChessPiece : Unit
    {
        public enum attribute { earth, wind, water, fire, none };
        protected int type;
        protected pieceType piecetype = pieceType.Special;
        //public static enum pieceType { King, Queen, Knight, Bishop, Rook, Pawn, Special }//obstacle was removed from here!
        protected string flavorText = "Description here";
        protected Skill skill = null;
        protected MoveSet moveset;

        public ChessPiece(string inName, string inFlavorText, Skill inSkill, Player inPlayer, BoardSpace_Model inPosition)
        {
            name = inName;
            flavorText = inFlavorText;
            skill = inSkill;
            player = inPlayer;
            setPosition(inPosition);
            isUnMoved = true;//this must be done last!
        }


        public MoveSet getMoveSet()
        {
            return this.moveset;
        }
        public void setMoveSet(MoveSet ms)
        {
            moveset = ms;
        }
        public virtual bool isValidMove(BoardSpace_Model tile)
        {
            return true;
        }
        public pieceType getPieceClass()
        {
            return piecetype;
        }
        public Skill getSkill()
        {
            return skill;
        }
        public string getFlavorText()
        {
            return flavorText;
        }
    }
    public class Mura : ChessPiece
    {
        int movesThisTurn = 0;
        public Mura(string inName, string inFlavorText, Skill inSkill, Player inPlayer, BoardSpace_Model inPosition) : base(inName, inFlavorText, inSkill, inPlayer, inPosition)
        {
            piecetype = pieceType.Special;
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            return QueenMoveSet.isValidMove(this, tile) || KnightMoveSet.isValidMove(this, tile);
        }
        public override void moveToPosition(BoardSpace_Model newPosition)
        {
            base.moveToPosition(newPosition);
            if (movesThisTurn++ < 3)
                getPlayer().extraTurn();
            else
                movesThisTurn = 0;
        }
    }
}
