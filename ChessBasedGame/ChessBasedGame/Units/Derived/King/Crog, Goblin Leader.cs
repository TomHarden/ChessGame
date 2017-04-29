using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    class Crog__Goblin_Leader : King
    {
        const string NAME = "Crog, Goblin Leader";
        const string FLAVOR_TEXT = "\"...I'm surrounded by IDIOTS!!!\"";
        const string DESCPRIPTION =
            "A Goblin from an unknown den.  Given his love of picture books,"
            + " he is arguably among the most scholarly of goblins, but his"
            + " father's wishes to have a warrior son led to his current career.";
        public Crog__Goblin_Leader(Player inPlayer, BoardSpace_Model inPosition)
            : base(NAME,
            FLAVOR_TEXT,
            new Skill(skillType.None), inPlayer, inPosition)
        {
            
        }
    }
}
