using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public enum skillType { None, A, B, C }

    public class Skill
    {
        protected skillType st;
        protected string name, description;
        //the strngeth of a skill and the general type of skill can be used to geenerate random skills.  THIS WILL COME LATER!
        //FOR NOW, NO SKILLS!! (just skill placeholders!)
        public Skill(skillType inSt)
        {
            st = inSt;
            name = "SkillName";
            description = "SkillDescription";
        }
        public string getSkillName()
        {
            return name;
        }
        public string getSkillDescription()
        {
            return description;
        }
    }
}
