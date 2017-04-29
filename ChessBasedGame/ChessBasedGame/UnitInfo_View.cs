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
    public class UnitInfo_View : FlowLayoutPanel
    {
        //UnitInfo_Model model;
        ChessPiece displayUnit;
        VScrollBar scrollBar;
        TextBox textBox;
        FlowLayoutPanel textPanel;
        public UnitInfo_View(Game_View inParent/*UnitInfo_Model inModel*/)
        {
            //model = inModel;
            scrollBar = new VScrollBar();
            textPanel = new FlowLayoutPanel();
            textBox = new TextBox();
            this.displayUnitInfo(null);
            textBox.Multiline = true;
            textBox.Height *= 5;
            textBox.Enabled = true;
            this.Controls.Add(textBox);
            this.Controls.Add(scrollBar);
        }
        /*public void displayUnitInfo(Unit u)
        {
            
        }*/
        public void displayUnitInfo(ChessPiece cp)
        {
            //model.setUnit(cp);
            displayUnit = cp;

            String displayText = "";
            if (cp != null)
            {
                displayText += "Name: ";
                displayText += displayUnit.getName() + "\r\n";
                displayText += "Class: ";
                displayText += displayUnit.getPieceClass() + "\r\n";//D2!
                displayText += "Skill: ";
                displayText += displayUnit.getSkill().getSkillName() + " - " + displayUnit.getSkill().getSkillDescription() + "\r\n";//D2!
                displayText += "Description: ";
                displayText += displayUnit.getFlavorText();//D2!
            }
            else
            {
                displayText = "NO UNIT SELECTED\r\n\r\n\r\n\r\n";
            }
            textBox.Text = displayText;
        }
        //create a function that will take a unit and write all info to the label
    }
}
