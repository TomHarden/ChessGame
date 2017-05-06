using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class Turnset
    {
        protected Circular_Queue<Player> players;
        protected Player currentPlayer;
        protected bool grantPlayerExtraTurn = false;
        protected int numberOfTurns = 0;

        public Turnset(Player[] inPlayers)
        {
            players = new Circular_Queue<Player>(inPlayers);
            currentPlayer = players.Dequeue();
        }
        public Player getCurrentPlayer()
        {
            return currentPlayer;
        }
        public void endPlayerTurn()
        {
            if (!grantPlayerExtraTurn)
                setCurrentPlayer(players.Dequeue());
            else
                grantPlayerExtraTurn = false;
        }
        public void skipTurn()
        {
            players.Dequeue();
            setCurrentPlayer(players.Dequeue());
        }
        public void extraTurn()
        {
            grantPlayerExtraTurn = true;
            //setCurrentPlayer(players.Requeue());
        }
        public void setCurrentPlayer(Player p)
        {
            currentPlayer = p;
        }
    }
}
