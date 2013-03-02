using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    public enum ConditionList {
        InventoryContainsItem,
    }
    public enum CommandList {
        // Block Commands
        Loop,
        IfThen,
        Else,

        // Misc Commands
        Break,//do i need this
        Continue,//do i need this
        Random,

        // Unit Commands
        LearnSkill,
        CreateActor,
        GiveExperience,
        UnLearnSkill,
        SetExperience,
        SetLevel,
        RemoveActor,
        Wait,
    }

    public enum BlockCommands {
        Loop,
        IfThen,
        Else,
        Dialog,
        DialogPrompt,
    }

    abstract class BaseCommand {
        public string Command { get; set; }

        public BaseCommand(string name) {
            this.Command = name;
        }
    }
}
