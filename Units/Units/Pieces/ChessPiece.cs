using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame;
using ChessBasedGame.Board;

namespace ChessBasedGame.Units.ChessPieces
{
    public enum pieceType { King, Queen, Knight, Bishop, Rook, Pawn, Special }//obstacle was removed from here!
    public enum attribute { earth, wind, water, fire, none };
    public enum locale { Furth, Vaux, Rorret, Tury, Western_Mountains, Central_Interior_Forests, Northern_Mountains, NA };//more to come (there must be a way to make these a bit more specific!!
    public enum species { Human, Elf, Orc, Centaur, Eaglefolk, Goblin, Chatatrope, Divine_Being, Devil, Merfolk, Sprite, Troll, God };//more to come

    public abstract class ChessPiece : Unit
    {
        protected int type;
        protected BoardSpace_Model startingPosition;
        protected pieceType piecetype = pieceType.Special;
        protected attribute elementAttribute = attribute.none;
        protected locale regione = locale.NA;
        protected species race = species.Human;
        protected string flavorText = "Description here";
        protected Skill skill = null;
        protected MoveSets.MoveSet moveset = null;

        public ChessPiece(string inName,
            species inRace,
            locale inRegion,
            string inFlavorText,
            //Skill inSkill,
            Player inPlayer,
            BoardSpace_Model inPosition)
            :base(inPlayer, inPosition)
        {
            name = inName;
            flavorText = inFlavorText;
            skill = new Skill(this);
            isUnMoved = true;//this must be done last!
        }


        public MoveSets.MoveSet getMoveSet()
        {
            return this.moveset;
        }
        public void setMoveSet(MoveSets.MoveSet ms)
        {
            moveset = ms;
        }
        public virtual bool isValidMove(BoardSpace_Model tile)
        {
            return moveset.isValidMove(this, tile);
        }
        public pieceType getPieceClass()
        {
            return piecetype;
        }
        public Skill getSkill()
        {
            return skill;
        }
        public string getFlavorText()
        {
            return flavorText;
        }
        /*public override void moveToPosition(BoardSpace_Model newPosition)
        {
            base.moveToPosition(newPosition);
            //skill.useSkill();//+_+tmp??
        }*/
        public override void pieceInteraction(Unit reciever, ChessBoard_Model cbm)
        {
            base.pieceInteraction(reciever, cbm);
            if (this.player/*.getAllegiance()*/ == reciever.getPlayer()/*.getAllegiance()*/)
            {
                try
                {
                    if (this.getMoveSet().canCastle()
                       && ((ChessPiece)reciever).getMoveSet().canCastle())
                    {
                        if (moveset.validCastle(this, (ChessPiece)reciever))
                                cbm.performCastle(this, (ChessPiece) reciever);
                    }
                }
                catch (Exception e)
                {
                    int dummy = 0;//NEED TO FIGURE OUT WHAT TO DO HERE!
                }
            }
            else//piece is enemy, and will be taken!
            {
                this.standardTakeOf((ChessPiece)reciever);
            }
            this.player.endTurn();
        }

        public virtual void standardTakeOf(ChessPiece enemyPiece)
        {
            BoardSpace_Model newPosition = enemyPiece.getPosition();
            enemyPiece.getPosition().removeUnit();//remove piece from chessTile (and board)
            enemyPiece.getPlayer().removeUnit(enemyPiece);
            this.moveToPosition(newPosition);
        }
        /*if (eventTile.getUnit().getPlayer()/..getAllegiance()./ == selectedPiece.getPlayer()/..getAllegiance()./)
{
    //eventually will check if the piece is a king or rook, and is trying to take a rook or king, check if castling is possible!)
    //selectedPiece = null;//remove??
    if (((ChessPiece)eventTile.getUnit()).getPieceClass() == pieceType.Rook)
    {
        performCastle((King)selectedPiece, (Rook)eventTile.getUnit());
        deselectPiece();
    }
}
else//piece is enemy, and will be taken!
{
    removePieceFromGame((ChessPiece)eventTile.getUnit());//remove the piece occupying the eventTile from the game;
    movePieceToTile(selectedPiece, eventTile);//move the selectedPiece onto that tile
    deselectPiece();//will eventually involve taking that player's piece!
}*/








        public List<BoardSpace_Model> getAllValidMoveTiles()
        {
            List<BoardSpace_Model> finalList = new List<BoardSpace_Model>();
            List<BoardSpace_Model> prelimList = new List<BoardSpace_Model>();

            //get all possible valid spaces that the chess piece COULD move to!
            for (int r = 0; r < this.boardModel.getNumRows(); r++)
                for (int c = 0; c < this.boardModel.getNumCols(); c++)
                    if (this.isValidMove(this.boardModel.getTile(r, c)))
                        prelimList.Add(this.boardModel.getTile(r, c));

            removeInvalidMoveTiles(separateIntoVectors(prelimList), ref finalList);

            ChessBoard_Model.parseUntakeables(ref finalList, this);//remove any untakeable pieces such as allies and obstacles

            if (this.moveset.canCastle())
                addCastlingTiles(ref finalList, this.boardModel);
            return finalList;
        }
        protected List<List<BoardSpace_Model>> separateIntoVectors(List<BoardSpace_Model> list)
        {
            List<BoardSpace_Model> RightTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> LeftTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> TopTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> BottomTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> TopRightTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> TopLeftTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> BottomRightTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> BottomLeftTiles = new List<BoardSpace_Model>();
            List<BoardSpace_Model> StandaloneTiles = new List<BoardSpace_Model>();
            foreach (BoardSpace_Model tile in list)//look for obstructions
            {
                this.CategorizeTileIntoDirectionBasedList(
                    ref RightTiles,
                    ref LeftTiles,
                    ref TopTiles,
                    ref BottomTiles,
                    ref TopRightTiles,
                    ref TopLeftTiles,
                    ref BottomRightTiles,
                    ref BottomLeftTiles,
                    ref StandaloneTiles,
                    tile,
                    this.position);
            }
            List<List<BoardSpace_Model>> allVectors = new List<List<BoardSpace_Model>>();
            allVectors.Add(RightTiles);
            allVectors.Add(LeftTiles);
            allVectors.Add(TopTiles);
            allVectors.Add(BottomTiles);
            allVectors.Add(TopRightTiles);
            allVectors.Add(TopLeftTiles);
            allVectors.Add(BottomRightTiles);
            allVectors.Add(BottomLeftTiles);
            allVectors.Add(StandaloneTiles);
            return allVectors;
        }
        protected void addCastlingTiles(ref List<BoardSpace_Model> finalList, ChessBoard_Model cbm)
        {
            foreach (ChessPiece piece in this.player.getArmy())
                if (piece.getPieceClass() == pieceType.Rook)//rooks can castle by default!
                    if (this.moveset.validCastle(this, piece) && cbm.tileStripIsClearBetween(this.position, piece.getPosition()))
                        finalList.Add(piece.getPosition());
        }
        protected virtual void removeInvalidMoveTiles(List<List<BoardSpace_Model>> vectorList, ref List<BoardSpace_Model> finalList)
        {
            foreach (List<BoardSpace_Model> tileVectorList in vectorList)
                foreach (BoardSpace_Model tile in ChessBoard_Model.unobstructedTiles(tileVectorList, this.position))
                    finalList.Add(tile);
            foreach (BoardSpace_Model tile in vectorList.ElementAt(vectorList.Count - 1))
                if (!tile.isOccupied() || tile.getUnit().getPlayer() != this.player)//ie "if tile not occupied or occupied by another player"...something like this will eventually have to be replaced with "getAllegiance()"
                    finalList.Add(tile);
        }



        protected void CategorizeTileIntoDirectionBasedList(
            ref List<BoardSpace_Model> RightTiles,
            ref List<BoardSpace_Model> LeftTiles,
            ref List<BoardSpace_Model> TopTiles,
            ref List<BoardSpace_Model> BottomTiles,
            ref List<BoardSpace_Model> TopRightTiles,
            ref List<BoardSpace_Model> TopLeftTiles,
            ref List<BoardSpace_Model> BottomRightTiles,
            ref List<BoardSpace_Model> BottomLeftTiles,
            ref List<BoardSpace_Model> StandaloneTiles,
            BoardSpace_Model tile,
            BoardSpace_Model originTile)
        {
            if (tile.getRow() == originTile.getRow())
            {
                if (tile.getColumn() > originTile.getColumn())//go same row to the right;
                    RightTiles.Add(tile);
                else if (tile.getColumn() < originTile.getColumn())//go same row to the left;
                    LeftTiles.Add(tile);
            }
            else if (tile.getColumn() == originTile.getColumn())
            {
                if (tile.getRow() > originTile.getRow())//go same column to the top;
                    TopTiles.Add(tile);
                else if (tile.getRow() < originTile.getRow())//go same column to the bottom;
                    BottomTiles.Add(tile);
            }
            else if (ChessBoard_Model.isOnDiagonal(originTile, tile))
            {
                if (tile.getRow() > originTile.getRow())//upwards diagonals
                {
                    if (tile.getColumn() > originTile.getColumn())//right, upwards diagonal
                        TopRightTiles.Add(tile);
                    else if (tile.getColumn() < originTile.getColumn())//left, upwards diagonal
                        TopLeftTiles.Add(tile);
                }
                else if (tile.getRow() < originTile.getRow())//downwards diagonals
                {
                    if (tile.getColumn() > originTile.getColumn())//right, downwards diagonal
                        BottomRightTiles.Add(tile);
                    else if (tile.getColumn() < originTile.getColumn())//left, downwards diagonal
                        BottomLeftTiles.Add(tile);
                }
            }
            else
                StandaloneTiles.Add(tile);

            return;
        }
    }
}
