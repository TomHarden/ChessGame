using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChessBasedGame.Board
{
    public class BoardSpace_View : Button
    {
        BoardSpace_Model model;
        public BoardSpace_View(BoardSpace_Model inModel)
        {
            model = inModel;
        }
        public BoardSpace_Model getModel()
        {
            return model;
        }
    }
}
