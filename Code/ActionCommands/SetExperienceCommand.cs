using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class SetExperienceCommand : BaseCommand {
        public string ActorID { get; set; }
        public int Experience { get; set; }

        public SetExperienceCommand(string actorId, int exp)
            : base(CommandList.SetExperience.ToString()) {
                this.ActorID = actorId;
                this.Experience = exp;
        }
    }
}
