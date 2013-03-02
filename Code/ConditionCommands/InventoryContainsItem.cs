using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocarina {
    class InventoryContainsItemCommand : BaseCondition {
        public string ItemID { get; set; }
        public InventoryContainsItemCommand(string itemId) 
            : base(ConditionList.InventoryContainsItem.ToString())
        {
            ItemID = itemId;
        }
        public override bool Evaluate() {
            throw new NotImplementedException();
        }
    }
}
