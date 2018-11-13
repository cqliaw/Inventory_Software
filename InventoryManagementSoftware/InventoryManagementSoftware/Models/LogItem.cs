using System;

namespace InventoryManagementSoftware.Models
{
    public class LogItem
    {
        public int LogID { get; set; }
        public int ItemID { get; set; }
        public string ItemBrand { get; set; }
        public string ItemModel { get; set; }
        public string ItemName { get; set; }
        public int QuantityIn { get; set; }
        public int QuantityOut { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}