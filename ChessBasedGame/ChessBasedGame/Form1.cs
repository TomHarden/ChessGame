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
    //This game is based on chess, but it has a few major exceptions.  THe game description is given below:
    //This game requires a minimum of two players.  It is turn based.  Players may move one piece per turn (as a general rule).
    //There are 6 different general pieces:  King, Queen, Bishop, Knight, Rook, and Pawn.  Each has their own special way of moving
    //1. Kings are piece representations of the player.  There can only be one king per player.  A King may move one adjacent space in any direction.  Castling with a rook is allowed (at any time?)
    //2. Queens are generally the most powerful pieces.  Generally, only one queen is allowed per player.  A queen may move any number of spaces in any direction
    //3. Bishops (white and black) can move any number of spaces on a diagonal.
    //4. Rooks can move any number of spaces on a straight line. Castling with a king is allowed (at any time?)
    //5. Knights move in an "L" shape - two spaces in a straight line and one space 90 degrees of that line.
    //6. Pawns (in this game) can attack in any adjacent space diagonal of them.  However, if they desire to move (while not taking a piece), they must exhaust a player's move by "turning " in the direction the player wishes them to move.  By default, all pawns start facing the forward direction.  En passante is not allowed.
    //
    //The objective of a default game is to take the enemy king, catch him/her in a stalemate with more captured pieces, force him/her into a threefold repitition, or to reduce the opposing army down to only the king.
    //Each unit falls into the super unit of King, Queen, Bishop, Knight, Rook, or Pawn, but is also a unique, individual unit with unique abilites. (see below for examples and further explanation):
    /*Ex.
     *Unit Name = "Thurgor, Ram's Blood"
     *Type = Rook
     *Attribute = Fire
     *Flavor Text = "Thurgor Smash!!"
     *Ability = Take 3 enemy pieces with Thurgor, Ram's Blood to use this ability. When desired, Thrugor, Ram's Blood will attack all units (friend and foe) in a straight line from his position (obstacles will stop this attack).
     */
    //Each unit has a unique name, and fits into a chess piece type.  It essentially operates like a regular chess piece until its ability is activated, at which point, it has abilities that extend beyond the regular chess rules. (cont)
    //In the case of Thurgor, Ram's Blood, Once Thurgor takes three enemy pieces, the player may choose to obliterate any pieces in a straight line (ie, the ultimate rook attack).
    //Every time a piece is taken, units are "charged" up.
    //All units have abilities that relate to the board/field. Kings' abilities, are a little different, however.
    //Kings' abilities relate not just to the field, but to the player. For instance, for every enemy unit taken, you may revive a unit at the end of the game.  This is useful in campaigning.
    //Lastly, Pawns that reach the end of the board have a dilemma to face - sacrifice themselves for another, taken piece of yours, or to have their skill fully charged, and be automatically facing any direction you want.  The double jump rule now reapplies to this pawn's next move.
    //
    //The boards do not need to be 8x8.  Also, obstacles may be on the board (such as lakes, Mountains, etc.) which prevent moving onto that space.  Some unit's abilities may enable traversing of certain obstacles (ex. Fishman can swim across a river;  Birdman can fly over the mountains;  etc.)
    //
    //In campaigning, the player accumulates and loses pieces with each battle. The goal is (essentially) to try to lead your army to victory, and still be battle-worthy for the next fight.  The units you accumulate/lose will affect your narrative in the campaign!

    public partial class Form1 : Form
    {
        //const int DEFAULT_PLAYERS = 2;
        Game_Controller game_controller;
        Game_Model game_model;// = new Game_Model();
        Game_View game_view;// = new Game_View(model);
        
        UnitInfo_Model unitInfo_model;
        UnitInfo_View unitInfo_view;
        //ChessBoard_View chessBoard;
        //Player[] players = new Player[DEFAULT_PLAYERS];
        //Turnset turnset;// = new Circular_Queue<Player>(players);
        //ChessPiece selectedPiece;

        public Form1()
        {
            InitializeComponent();
            //game_controller = new Game_Controller(null, null);
            /*game_model = new Game_Model();//game_controller);
            game_view = new Game_View(game_model);//, game_controller);
            //game_controller.setModelAndView(game_model, game_view);
            
            this.Board_Panel.Controls.Add(game_view);
            Board_Panel.Size = game_view.Size;*/
            ChessGame cg = new ChessGame();
            Board_Panel.Controls.Add(cg);
            Board_Panel.Size = cg.Size;

            //unitInfo_model = new UnitInfo_Model();
            //unitInfo_view = new UnitInfo_View(/*unitInfo_model*/);
            //this.UnitInfo_Panel.Controls.Add(unitInfo_view);
            //UnitInfo_Panel.Size = unitInfo_view.Size;
            /*ChessBoard_Model cbm = new ChessBoard_Model(8, 8);

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
                switch(playerNum + 1)
                {
                    case 1:
                        c = Color.White;
                        break;
                    default:
                        c = Color.Black;
                        break;
                }
                List<Player> playerEnemies = new List<Player>();
                players[playerNum] = new Player(playerNum + 1, availableSpaces, c);
            }
            foreach (Player p1 in players)
            {
                foreach (Player p2 in players)
                {
                    if (p1 != p2)
                    {
                        if (p1.getAllegiance() == p2.getAllegiance())
                        {
                            p1.addAlly(p2);
                        }
                        else
                        {
                            p1.addEnemy(p2);
                        }
                    }
                }
            }

            turnset = new Turnset(new Circular_Queue<Player>(players));
            chessBoard = new ChessBoard_View(cbm, turnset);*/
            //this.Board_Panel.Controls.Add(chessBoard);
            //this.Board_Panel.Size = chessBoard.Size;
            //chessBoard.drawBoard();
        }

        /*public void updateBoard()
        {
            chessBoard.drawPieces();
        }*/

        /*public void appendArray(ref object[] a1, Object[] a2)
        {
            int j = 0;
            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] == null)
                {
                    a1[i] = a2[j++];
                    if (j >= a2.Length)
                        return;
                }
            }
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /*public void ChessBoard_View_Click(BoardSpace_Model eventTile, EventArgs e)
        {
            if (eventTile.isOccupied() && selectedPiece == null)
            {
                selectedPiece = (ChessPiece) eventTile.getUnit();
            }
            else if (eventTile.isOccupied() && selectedPiece != null)
            {
                selectedPiece.getPosition().setUnit(null);//remove piece from tile
                selectedPiece.setPosition(eventTile);//tell piece its new location
                eventTile.setUnit(selectedPiece);//set piece on the new tile;
                selectedPiece = null;//the piece is no longer selected (be careful this does not obliterate the piece to be moved!)
            }
            else if (eventTile.isOccupied() && selectedPiece != null)
            {
                return;//will eventually involve taking that player's piece!
            }
        }*/
    }
}
