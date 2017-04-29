using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    class Kells__Warrior_of_Burning_Blood : King
    {
        const string NAME = "Kells, Warrior of Burning Blood";
        const string FLAVOR_TEXT = "The king rose to his full height and readied his sword.  \"I, Kells of Vaux, sentence you to die in the name of the law.\"";
        const string DESCPRIPTION =
            "The military king of Vaux."
            + "  Despite his old age, he is still respected as a leader and a warrior."
            + "  He earned the title of \"Warrior of Burning Blood\" from his"
            + " ferocious fighting style, which caused such spraying of blood that it"
            + " seemed as if a fire of blood was surging around him.";
        public Kells__Warrior_of_Burning_Blood(Player inPlayer, BoardSpace_Model inPosition)
            : base (NAME,
            FLAVOR_TEXT,
            new Skill(skillType.None),
            inPlayer,
            inPosition)
        {

        }
    }
}
