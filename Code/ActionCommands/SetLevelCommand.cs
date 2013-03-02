using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class SetLevelCommand : BaseCommand {
        public string ActorID { get; set; }
        public int Level { get; set; }

        public SetLevelCommand(string actorId, int level)
            : base(CommandList.SetLevel.ToString()) {
            this.ActorID = actorId;
            this.Level = level;
        }
    }
}
