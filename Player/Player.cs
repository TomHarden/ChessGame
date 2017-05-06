using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame
{
    public class Player
    {
        
        protected List<Player> allies = new List<Player>();
        protected List<Player> enemies = new List<Player>();
        protected int allegiance = 0;
        protected int playerNum = 0;
        protected List<ChessPiece> army;
        protected List<ChessPiece> graveyard;
        protected ChessPiece focusPiece;//this is the unit that the player has selected.
        protected bool lockedFocus = false;//This is false by default, but for special abilities, it may be necessary to lock a players focus!
        protected List<BoardSpace_Model> focusTiles = new List<BoardSpace_Model>();
        protected List<BoardSpace_Model> startingTiles;
        protected Color playerColor;
        protected Game_Model gameModel;
        public Player(int inPlayerNum, List<BoardSpace_Model> inStartingTiles, Color inPlayerColor, Game_Model inGameModel)
        {
            startingTiles = inStartingTiles;
            playerNum = inPlayerNum;
            allegiance = playerNum;
            playerColor = inPlayerColor;
            gameModel = inGameModel;

            int direction = 1;//direction = otherPlayer's row - your row.  if neg, = direction!)
            if (playerNum % 2 == 1)
                direction = 1;
            else if (playerNum % 2 == 0)
                direction = -1;

            army = new List<ChessPiece>();
            int spaceNum = 0;
            /*army.Add(new Rook("Rook", species.Human, locale.Furth, "Your Rook", new Skill(), this, startingTiles[spaceNum++]));
            army.Add(new Knight("Knight", species.Human, locale.Furth, "Your Knight", new Skill(), this, startingTiles[spaceNum++]));
            army.Add(new Bishop("Bishop", species.Human, locale.Furth, "Your Bishop", new Skill(), this, startingTiles[spaceNum++]));
            army.Add(new Queen("Queen", species.Human, locale.Furth, "Your Queen", new Skill(), this, startingTiles[spaceNum++]));
            army.Add(new King("King", species.Human, locale.Furth, "Your King", new Skill(), this, startingTiles[spaceNum++]));
            army.Add(new Bishop("Bishop", species.Human, locale.Furth, "Your Bishop", new Skill(), this, startingTiles[spaceNum++]));
            army.Add(new Knight("Knight", species.Human, locale.Furth, "Your Knight", new Skill(), this, startingTiles[spaceNum++]));
            army.Add(new Rook("Rook", species.Human, locale.Furth, "Your Rook", new Skill(), this, startingTiles[spaceNum++]));
            for (; spaceNum < gameModel.getChessBoardModel().getNumCols() + 8; spaceNum++)
                army.Add(new Pawn("Pawn", species.Human, locale.Furth, "One of your Pawns", new Skill(), this, startingTiles[spaceNum], direction));*/
            graveyard = new List<ChessPiece>();
        }
        public Player(int inPlayerNum, List<BoardSpace_Model> inStartingTiles, Color inPlayerColor, List<ChessPiece> inArmy)
        {
            startingTiles = inStartingTiles;
            playerNum = inPlayerNum;
            allegiance = playerNum;
            playerColor = inPlayerColor;
            army = inArmy;
        }

        public Color getColor()
        {
            return playerColor;
        }
        public virtual void removeUnit(ChessPiece cp)//, bool isEvolving = false)
        {
            army.Remove(cp);
            graveyard.Add(cp);
            if (cp.getPieceClass() == pieceType.King
                || this.army.Count == 0)//VICTORY CONDITIONS!!  THESE DOES NOT BELONG HERE!!!!
                gameModel.gameOver(this);
        }
        public List<ChessPiece> getArmy()
        {
            return army;
        }
        public List<BoardSpace_Model> getStartingTiles()
        {
            return startingTiles;
        }
        public void replacePiece(ChessPiece oldPiece, ChessPiece newPiece)
        {
            try
            {
                army.Add((ChessPiece)newPiece);
            }
            catch
            {
                int dummy = 0;//what to do here!?
                //replaces a unit you control with something that cannot be added to your army (like an obstacle)
            }
            this.removeUnit(oldPiece);//should this be in the try/catch block?
        }
        public int getAllegiance()
        {
            return allegiance;
        }
        public bool isAlly(Player otherPlayer)
        {
            if (otherPlayer == this)
                return true;
            return allies.Contains(otherPlayer);
        }
        public bool isEnemy(Player otherPlayer)
        {
            if (otherPlayer == this)
                return false;
            return enemies.Contains(otherPlayer);
        }
        public void addEnemy(Player p)
        {
            enemies.Add(p);
        }
        public void addAlly(Player p)
        {
            allies.Add(p);
        }
        public List<Player> getAllies()
        {
            return allies;
        }
        public List<Player> getEnemies()
        {
            return enemies;
        }
        public int getPlayerNumber()
        {
            return playerNum;
        }
        public void extraTurn(ChessPiece newFocusPiece, List<BoardSpace_Model> newFocusTiles, bool isLockedFocus = true)
        {
            setFocusTiles(newFocusTiles);
            extraTurn(newFocusPiece, isLockedFocus);
        }
        public void extraTurn(ChessPiece newFocusPiece, bool isLockedFocus = true)
        {
            lockedFocus = true;
            setFocusPiece(newFocusPiece);
            extraTurn();
        }
        public void extraTurn()
        {
            gameModel.getTurnset().extraTurn();
        }
        public void endTurn()
        {
            gameModel.getTurnset().endPlayerTurn();
            if (gameModel.getTurnset().getCurrentPlayer() != this)//ie if the player turn is really over, then lose focus, otherwise, retain focus!
                this.clearFocusPiece();
        }
        public ChessPiece getFocusPiece()
        {
            return focusPiece;
        }
        public void setFocusPiece(ChessPiece newFocusPiece)
        {
            if (!lockedFocus)
                focusPiece = newFocusPiece;
        }
        public bool hasFocusPiece()
        {
            return focusPiece != null;
        }
        public bool isFocusPiece(ChessPiece cp)
        {
            return focusPiece == cp;
        }
        protected void clearFocusPiece()//should this be public?? is there any reason?
        {
            lockedFocus = false;
            focusPiece = null;
            focusTiles.Clear();
        }
        public bool hasLockedFocus()
        {
            return lockedFocus;
        }
        public void setLockedFocus(bool b)
        {
            lockedFocus = b;
        }

        public bool hasFocusTiles()
        {
            return focusTiles.Count != 0;
        }
        public bool isFocusTile(BoardSpace_Model t)
        {
            return focusTiles.Contains(t);
        }
        public void setFocusTiles(List<BoardSpace_Model> fts)
        {
            focusTiles = fts;
        }
        public List<BoardSpace_Model> getFocusTiles()
        {
            return focusTiles;
        }
        public List<ChessPiece> getGraveyard()
        {
            return graveyard;
        }
        public void revivePiece(ChessPiece cp, BoardSpace_Model revivalSpace)
        {
            graveyard.Remove(cp);
            cp.setPosition(revivalSpace);
            army.Add(cp);
        }
        /*public void performStandardTake(ChessPiece piece, ChessPiece enemy)
        {
            piece.standardTake(enemy);
        }*/
        //may consider having the Player move the pieces instead of the piece being in charge of this!
    }
}
