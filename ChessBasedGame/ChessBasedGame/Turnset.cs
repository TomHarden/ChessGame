using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class Turnset
    {
        Circular_Queue<Player> players;
        Player currentPlayer;
        int numberOfTurns = 0;

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
            setCurrentPlayer(players.Dequeue());
        }
        public void skipTurn()
        {
            players.Dequeue();
            setCurrentPlayer(players.Dequeue());
        }
        public void extraTurn()
        {
            setCurrentPlayer(players.Requeue());
        }
        public void setCurrentPlayer(Player p)
        {
            currentPlayer = p;
        }
    }
}
