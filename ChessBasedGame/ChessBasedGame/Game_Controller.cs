using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChessBasedGame
{
    public class Game_Controller //This class is the go between for the model and the view.  It allows the model and view to be changed and intercommunicate more intimately (where appropriate!)
    {
        protected Game_Model gm;
        protected Game_View gv;
        public Game_Controller(Game_Model inGm, Game_View inGv)
        {
            gm = inGm;
            gv = inGv;
        }
        public void setModelAndView(Game_Model newGm, Game_View newGv)
        {
            this.setModel(newGm);
            this.setView(newGv);
        }
        public void setView(Game_View newGv)
        {
            if (gv != null)
                gv.Dispose();
            gv = newGv;
            if (gm!= null)
                gv.setModel(gm);
        }
        public void setModel(Game_Model newGm)
        {
            gm = newGm;
            if (gv != null)
                gv.setModel(gm);
        }
        public Game_View getView()
        {
            return gv;
        }
        public Game_Model getModel()
        {
            return gm;
        }
        /*public DialogResult messageToPlayer(Player speaker, Player listener, String message, MessageBoxButtons mbb)
        {
            return gv.messageToPlayer("Player " + speaker.getPlayerNumber() + ":\r\n" + "Hey Player " + listener.getPlayerNumber() + "! " + message, mbb);
        }
        public DialogResult messageToPlayer(Player speaker, String message, MessageBoxButtons mbb)
        {
            return gv.messageToPlayer("Player " + speaker.getPlayerNumber() + ":\r\n" + message, mbb);
        }
        public DialogResult messageToPlayer(String message, MessageBoxButtons mbb)
        {
            return gv.messageToPlayer(message, mbb);
        }*/
    }
}
