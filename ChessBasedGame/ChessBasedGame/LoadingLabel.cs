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
    public class LoadingLabel : Label
    {
        //Timer timer;//Every few seconds will update the text Label!
        public LoadingLabel()
            : base()
        {
            this.Text = "LOADING...";
            //timer = new Timer();
            //timer.Enabled = true;
        }
    }
}
