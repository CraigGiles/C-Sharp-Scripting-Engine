using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    abstract class BaseCondition : BaseCommand {
        public BaseCondition(string name) : base(name) { }
        public abstract bool Evaluate(); 
    }
}
