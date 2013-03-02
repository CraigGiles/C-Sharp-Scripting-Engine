using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class LoopCommand : BaseCommand {
        public List<BaseCommand> BlockCommands { get; set; }
        public int Loops { get; private set; }

        public LoopCommand(object[] parameters)
            : base(CommandList.Loop.ToString()) {
            //this.Command = ScriptController.CommandList.Loop.ToString();
            Loops = int.Parse(parameters[0].ToString());
            BlockCommands = new List<BaseCommand>();
            for (int i = 1; i < parameters.Length; ++i) 
                BlockCommands.Add((BaseCommand)parameters[i]);
        }
    }
}
