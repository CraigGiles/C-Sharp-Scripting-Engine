using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ocarina {
    class CreateActorCommand : BaseCommand {
        public string ActorID { get; set; }
        public string Asset { get; set; }
        public Vector2 Location { get; set; }

        public CreateActorCommand(string actorId, string asset, Vector2 location)
            : base(CommandList.CreateActor.ToString()) 
        {
            ActorID = actorId;
            Asset = asset;
            Location = location;
        }
    }
}
