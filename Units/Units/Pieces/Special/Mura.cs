using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;
using ChessBasedGame.Board;

namespace ChessBasedGame
{
    public class Mura__Goddess_of_Life_and_Death : ChessPiece
    {
        private const string FLAVOR_TEXT = "\"To meet her gaze is to understand the meaning of life and the mystery of death\"";
        private const string DESCPRIPTION =
            "The physical embodiment of Mura, the powerful Goddess of Life and Death.";
        public Mura__Goddess_of_Life_and_Death(Player inPlayer, BoardSpace_Model inPosition)
            : base("Mura, Goddess of Life and Death", species.God, locale.NA, FLAVOR_TEXT, inPlayer, inPosition)
        {
            piecetype = Units.ChessPieces.pieceType.Special;
            moveset = new MoveSets.DualMoveSets(new MoveSets.Knight(), new MoveSets.Queen());
            skill = new Skill(new Abilities.MultipleMoves(3, this), new Executions.None(this), this);
        }
        public override bool isValidMove(BoardSpace_Model tile)
        {
            return moveset.isValidMove(this, tile);
        }
        public override void moveToPosition(BoardSpace_Model newPosition)
        {
            base.moveToPosition(newPosition);
            skill.useSkill();
        }
    }
}
