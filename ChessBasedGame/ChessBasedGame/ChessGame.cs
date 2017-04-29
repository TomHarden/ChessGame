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
    public class ChessGame : Panel
    {
        //Game_Controller game_controller;
        Game_Model game_model;// = new Game_Model();
        Game_View game_view;// = new Game_View(model);

        UnitInfo_Model unitInfo_model;
        UnitInfo_View unitInfo_view;
        public ChessGame()
        {
            initialize();//ie initialize
        }
        private void initialize()
        {
            //this.Controls.Add(new LoadingLabel());
            game_model = new Game_Model(this);
            game_view = new Game_View(game_model);
            //this.Controls.Clear();
            this.Controls.Add(game_view);
            this.Size = game_view.Size;
        }
        private void newGame()
        {
            this.Controls.Clear();
            initialize();
        }
        public void gameOver(Player defeatedPlayer)//this assumes a two player game!
        {
            Player winner = defeatedPlayer;
            Player loser = defeatedPlayer;
            foreach (Player player in game_model.getPlayers())
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
                newGame();
            else
                Game_View.messageToPlayer("Goodbye!", MessageBoxButtons.OK);
        }
    }
}
