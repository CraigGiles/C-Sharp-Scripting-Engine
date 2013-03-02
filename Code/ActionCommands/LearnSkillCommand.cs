using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class LearnSkillCommand : BaseCommand {
        public string ActorID { get; set; }
        public string SkillID { get; set; }

        public LearnSkillCommand(string actorId, string skillId) 
            : base(CommandList.LearnSkill.ToString())
        {
            ActorID = actorId;
            SkillID = skillId;
        }
    }
}
