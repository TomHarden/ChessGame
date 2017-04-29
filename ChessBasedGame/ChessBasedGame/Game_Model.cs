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
    public class Game_Model
    {
        const int DEFAULT_ROWS = 8;
        const int DEFAULT_COLUMNS = 8;
        const int DEFAULT_PLAYERS = 2;

        int numRows = DEFAULT_ROWS;
        int numCols = DEFAULT_COLUMNS;
        int numPlayers = DEFAULT_PLAYERS;
        Player[] players;
        ChessGame parent;
        ChessBoard_Model cbm;
        Turnset turnset;

        public Game_Model(ChessGame inParent,
            int inNumRows = DEFAULT_ROWS,
            int inNumCols = DEFAULT_COLUMNS,
            int inNumPlayers = DEFAULT_PLAYERS)//, ChessGame inParent)//, Game_Controller inGc)
        {
            numRows = inNumRows;
            numCols = inNumCols;
            numPlayers = inNumPlayers;
            parent = inParent;
            initializeBoard();
            initializePlayers();
            initializeTurnset();
        }
        public void initializeTurnset()
        {
            turnset = new Turnset(players);
        }
        public void initializePlayers()
        {
            players = new Player[numPlayers];
            for (int playerNum = 0; playerNum < players.Length; playerNum++)
            {
                List<BoardSpace_Model> availableSpaces = new List<BoardSpace_Model>();
                if (!cbm.getRow(-1).ElementAt(0).isOccupied())
                {
                    foreach (BoardSpace_Model tile in cbm.getRow(-1))
                        availableSpaces.Add(tile);
                    foreach (BoardSpace_Model tile in cbm.getRow(-2))
                        availableSpaces.Add(tile);
                }
                else if (!cbm.getRow(0).ElementAt(0).isOccupied())
                {
                    foreach (BoardSpace_Model tile in cbm.getRow(0))
                        availableSpaces.Add(tile);
                    foreach (BoardSpace_Model tile in cbm.getRow(1))
                        availableSpaces.Add(tile);
                }

                Color c;
                switch (playerNum + 1)
                {
                    case 1:
                        c = Color.White;
                        break;
                    default:
                        c = Color.Black;
                        break;
                }
                List<Player> playerEnemies = new List<Player>();
                players[playerNum] = new Player(playerNum + 1, availableSpaces, c, this);
                /*if (playerNum + 1 == 1)
                    players[playerNum] = new Player1(playerNum + 1, availableSpaces, c, this);
                else if (playerNum + 1 == 2)
                    players[playerNum] = new Player2(playerNum + 1, availableSpaces, c, this);*/
            }
            foreach (Player p1 in players)
                foreach (Player p2 in players)
                    if (p1 != p2)
                        if (p1.getAllegiance() == p2.getAllegiance())
                            p1.addAlly(p2);
                        else
                            p1.addEnemy(p2);
        }
        public void initializeBoard()
        {
            cbm = new ChessBoard_Model(numRows, numCols);
        }

        public ChessBoard_Model getChessBoardModel()
        {
            return cbm;
        }
        public Turnset getTurnset()
        {
            return turnset;
        }
        /*public void newGame()
        {
            //old_gm = new Game_Model(numRows, numCols, numPlayers);
            //gc.setModel(new Game_Model(gc));//ChessBoard_Model(numRows, numCols)
            //new Game_Model(numRows, numCols, numPlayers);
            initialize();
        }*/
        /*public void gameOver(Player defeatedPlayer)//this assumes a two player game!
        {
            Player winner = defeatedPlayer;
            Player loser = defeatedPlayer;
            foreach (Player player in players)
                if (player != defeatedPlayer)
                {
                    //message/status panel.write("Player " + p.getPlayerNum() + " has been defeated!")
                    //check to see if the enemies of the defeated player have won the game
                    winner = player;
                    break;
                }
            Game_View.messageToPlayer(winner, loser, "Sorry, you lose!", MessageBoxButtons.OK);
            if (Game_View.messageToPlayer(winner, loser, "Would you like to play again?", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
                this.newGame();
            else
                Game_View.messageToPlayer("Goodbye!", MessageBoxButtons.OK);
        }*/
        public void gameOver(Player defeatedPlayer)
        {
            parent.gameOver(defeatedPlayer);
        }
        public Player[] getPlayers()
        {
            return players;
        }
    }
}
