using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class SayCommand : BaseCommand {
        public string Text { get; set; }
        public SayCommand(string text)
            : base("Say") {
                this.Text = text;
        }
    }
}
