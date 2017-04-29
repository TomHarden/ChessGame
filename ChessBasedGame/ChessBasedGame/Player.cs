using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ChessBasedGame
{
    public class Player
    {
        public List<Player> allies = new List<Player>();
        public List<Player> enemies = new List<Player>();
        int allegiance = 0;
        int playerNum = 0;
        List<ChessPiece> army;
        List<ChessPiece> graveyard;
        ChessPiece focusPiece;//this is the unit that the player has selected.
        List<BoardSpace_Model> startingTiles;
        Color playerColor;
        Game_Model gameModel;
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
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, startingTiles[spaceNum++]));
            army.Add(new Knight("Knight", "Your Knight", new Skill(skillType.None), this, startingTiles[spaceNum++]));
            army.Add(new Bishop("Bishop", "Your Bishop", new Skill(skillType.None), this, startingTiles[spaceNum++]));
            army.Add(new Queen("Queen", "Your Queen", new Skill(skillType.None), this, startingTiles[spaceNum++]));
            army.Add(new King("King", "Your King", new Skill(skillType.None), this, startingTiles[spaceNum++]));
            army.Add(new Bishop("Bishop", "Your Bishop", new Skill(skillType.None), this, startingTiles[spaceNum++]));
            army.Add(new Knight("Knight", "Your Knight", new Skill(skillType.None), this, startingTiles[spaceNum++]));
            army.Add(new Rook("Rook", "Your Rook", new Skill(skillType.None), this, startingTiles[spaceNum++]));
            for (; spaceNum < gameModel.getChessBoardModel().getNumCols() + 8; spaceNum++)
                army.Add(new Pawn("Pawn", "One of your Pawns", new Skill(skillType.None), this, startingTiles[spaceNum], direction));
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
        public void removeUnit(ChessPiece cp, bool isEvolving = false)
        {
            army.Remove(cp);
            graveyard.Add(cp);
            if (cp.getPieceClass() == pieceType.King)
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
        public void replaceUnit(ChessPiece oldUnit, Unit newUnit)
        {
            try
            {
                army.Add((ChessPiece)newUnit);
            }
            catch
            {
                //replaces a unit you control with something that cannot be added to your army (like an obstacle)
            }
            army.Remove(oldUnit);
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
        public void extraTurn()
        {
            gameModel.getTurnset().extraTurn();
        }

        //may consider having the Player move the pieces instead of the piece being in charge of this!
    }
}
