using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class WaitCommand : BaseCommand {
        public float Delay { get; set; }
        public WaitCommand(float delay)
            : base(CommandList.Wait.ToString()) {
                this.Delay = delay;
        }
    }
}
