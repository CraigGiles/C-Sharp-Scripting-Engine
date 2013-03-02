using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class IfThenCommand : BaseCommand {
        public BaseCondition Condition { get; set; }
        public List<BaseCommand> BlockCommands { get; set; }

        public IfThenCommand(object[] parameters)
            : base(CommandList.IfThen.ToString()) 
        {
            BlockCommands = new List<BaseCommand>();
            Condition = (BaseCondition)parameters[0];
            for (int i = 1; i < parameters.Length; ++i)
                BlockCommands.Add((BaseCommand)parameters[i]);
        }  
    }
}
