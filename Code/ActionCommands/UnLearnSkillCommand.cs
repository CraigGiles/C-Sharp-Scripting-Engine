using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class UnLearnSkillCommand: BaseCommand {
        public string ActorID { get; set; }
        public string SkillID { get; set; }

        public UnLearnSkillCommand(string actorId, string skillId) 
            : base(CommandList.UnLearnSkill.ToString())
        {
            ActorID = actorId;
            SkillID = skillId;
        }
}
}
