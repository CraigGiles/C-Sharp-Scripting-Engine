using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class RandomCommand : BaseCommand {
        public int MaxValue { get; set; }

        public RandomCommand(int value)
            : base(CommandList.Random.ToString()) {
                this.MaxValue = value;
        }
    }
}
