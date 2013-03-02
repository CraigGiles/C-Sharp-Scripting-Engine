using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class GiveExperienceCommand : BaseCommand {
        public string ActorID { get; set; }
        public int Experience { get; set; }

        public GiveExperienceCommand(string actorId, int exp)
            : base(CommandList.GiveExperience.ToString()) 
        {
            ActorID = actorId;
            Experience = exp;
        }
}
}
