using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace Ocarina {
    class SetTextColorCommand : BaseCommand {
        public Color Color { get; set; }

        public SetTextColorCommand(string color)
            : base("SetTextColor") 
        {
            Color = GetColorByName.GetColor(color);
        }
    }
}
