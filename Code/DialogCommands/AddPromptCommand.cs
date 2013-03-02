using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class AddPromptCommand : BaseCommand {
        public string Text { get; set; }
        public string Script { get; set; }
        public AddPromptCommand(string text, string script)
            : base("AddPrompt") {
                this.Text = text;
                this.Script = script;
        }
    }
}
