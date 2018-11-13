using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSoftware.Models
{

    public class SummaryItem
    {
        public string ItemBrand { get; set; }
        public string ItemModel { get; set; }
        public string ItemName { get; set; }
        public int QuantityIn { get; set; }
        public int QuantityOut { get; set; }

    }
}