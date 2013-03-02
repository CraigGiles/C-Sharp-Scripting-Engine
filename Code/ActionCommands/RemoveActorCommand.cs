using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ocarina {
    class RemoveActorCommand : BaseCommand {
        public string ActorID { get; set; }

        public RemoveActorCommand(string actorId)
            : base(CommandList.RemoveActor.ToString()) {
            ActorID = actorId;
        }
    }
}
