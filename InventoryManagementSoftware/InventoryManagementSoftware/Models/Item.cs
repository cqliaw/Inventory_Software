﻿using System;

namespace InventoryManagementSoftware.Models
{
    public class Item
    {
        public int ItemID { get; set; }

        public string ItemBrand { get; set; }

        public string ItemModel { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdatedTime { get; set; }
    }
}