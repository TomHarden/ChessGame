using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame.Abilities
{
    public abstract class Ability
    {
        protected ChessPiece cp;
        public Ability(ChessPiece inCP)
        {
            cp = inCP;
        }
        /// <summary>
        /// returns true if done executing ability
        /// </summary>
        /// <returns></returns>
        public virtual bool actOutAbility(ChessPiece target = null)
        {
            new Exception("Unable to carry out ability, method \"actOutAbility()\" needs to be overriden first!");
            return true;//
        }
        public virtual void retractAbility()
        {
            return;//ie do nothing.  For some abilites/executions this is a legitimate answer to being unable to use ability!
             //new Exception("Unable to retract ability, method \"rtractAbility()\" needs to be overriden first!");
        }
    }
}
