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
    public class Game_View : TableLayoutPanel
    {
        private Game_Model model;
        protected ChessBoard_View chessBoard;
        protected UnitInfo_View unitInfoPanel;
        //protected Game_Controller gc;
            //unitInfo_model = new UnitInfo_Model();
            //unitInfo_view = new UnitInfo_View(/*unitInfo_model*/);
            //this.UnitInfo_Panel.Controls.Add(unitInfo_view);
            //UnitInfo_Panel.Size = unitInfo_view.Size;

        public Game_View(Game_Model inModel)//, Game_Controller inGc)
        {
            model = inModel;
            //gc = inGc;
            initializeView();
        }
        public void initializeView()
        {
            chessBoard = new ChessBoard_View(model.getChessBoardModel(), model.getTurnset(), this);
            chessBoard.drawBoard();
            unitInfoPanel = new UnitInfo_View(this);
            //IS THERE A WAY TO CHANGE THE WAY CONTROLS ARE ADDED?  EX. isVertical or isHorizontal?
            this.Controls.Add(chessBoard);
            this.Controls.Add(unitInfoPanel);
            this.Size = chessBoard.Size + unitInfoPanel.Size;
        }
        public void updateView(Object sender)
        {
            if (sender == chessBoard)
            {
                unitInfoPanel.displayUnitInfo(((ChessBoard_View)sender).getSelectedPiece());
            }
        }
        public void setModel(Game_Model newModel)
        {
            model = newModel;
            this.chessBoard.Dispose();
            this.unitInfoPanel.Dispose();
            initializeView();
        }
        public static DialogResult messageToPlayer(Player speaker, Player listener, string message, MessageBoxButtons mbb)
        {
            return messageToPlayer("Player " + speaker.getPlayerNumber() + ":\r\n" + "Hey Player " + listener.getPlayerNumber() + "! " + message, mbb);
        }
        public static DialogResult messageToPlayer(Player speaker, string message, MessageBoxButtons mbb)
        {
            return messageToPlayer("Player " + speaker.getPlayerNumber() + ":\r\n" + message, mbb);
        }
        public static DialogResult messageToPlayer(string message, MessageBoxButtons mbb)
        {
            return MessageBox.Show(message, "Hear ye, Hear ye!", mbb);
        }
    }
}
