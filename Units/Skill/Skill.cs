using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessBasedGame.Abilities;
using ChessBasedGame.Executions;
using ChessBasedGame.Units.ChessPieces;

namespace ChessBasedGame
{
    //public enum skillType { None, A, B, C }//test+_+
    public enum skillType { None, MultipleMove }
    public enum skillExecution { None, MultipleMove }

    public class Skill
    {
        //protected skillType st;//+_+

        protected ChessPiece cp;
        protected string name = "Skill_Name";
        protected string description = "Skill_Description";
        protected Executions.Execution execution;
        protected Abilities.Ability ability;

        //the strngeth of a skill and the general type of skill can be used to geenerate random skills.  THIS WILL COME LATER!
        //FOR NOW, NO SKILLS!! (just skill placeholders!)
        /*public Skill(skillType inSt, )
        {
            st = inSt;
            name = "SkillName";
            description = "SkillDescription";
        }*///+_+
        public Skill(ChessPiece inCP)
        {
            //new Skill(new Abilities.None(inCP), new Executions.None(inCP), inCP);
            cp = inCP;
            ability = new Abilities.None(cp);
            execution = new Executions.None(cp);
        }
        public Skill(Abilities.Ability inAbility, Executions.Execution inExecution, ChessPiece inCP)
        {
            ability = inAbility;
            execution = inExecution;
            cp = inCP;
        }
        public string getSkillName()
        {
            return name;
        }
        public string getSkillDescription()
        {
            return description;
        }
        protected virtual bool canUseSkill(ChessPiece target = null)//THis will eventually be incorporated into "useSkill()"
        {
            //target is not needed for any conditions at the moment! will worry about it later!
            return execution.canExecute();
        }
        public void useSkill(ChessPiece target = null)
        {
            if (canUseSkill(target))
                if (ability.actOutAbility(target))//returns true if ability is acted out
                    execution.abilityUsed();
            else
                ability.retractAbility();
        }
        public Ability getAbility()
        {
            return ability;
        }
        public Execution getExecution()
        {
            return execution;
        }
        //protected Execution initializeExecution(
    }
    public class DualConditionSkill : Skill
    {
        Executions.Execution execution2;
        public DualConditionSkill(Abilities.Ability inAbility,
            Executions.Execution inExecution1,
            Executions.Execution inExecution2,
            ChessPiece inCP)
            : base(inAbility, inExecution1, inCP)
        {
            execution2 = inExecution2;
        }
        protected override bool canUseSkill(ChessPiece target = null)
        {
            return base.canUseSkill(target) && execution2.canExecute();
        }
    }
}
