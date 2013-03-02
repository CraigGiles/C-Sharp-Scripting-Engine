using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class DialogCommand : BaseCommand {
        public string Asset { get; set; }
        
        public DialogCommand(string asset)
            : base("Dialog") {
                this.Asset = asset;
        }
    }
}
