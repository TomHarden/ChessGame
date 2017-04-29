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
    public abstract class ContainedPanel : Panel
    {
        protected ContainedPanel parent;
        public virtual void updateView(Object sender)
        {
            return;
        }
    }
}
