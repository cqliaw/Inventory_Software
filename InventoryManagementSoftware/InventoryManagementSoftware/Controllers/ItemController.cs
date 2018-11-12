using Dapper;
using InventoryManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagementSoftware.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            List<Item> itemList = new List<Item>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
            {
                itemList = db.Query<Item>("Select * From Items").ToList();
            }
            return View(itemList);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection, string inQuantity)
        {
            Item itemDetails = new Item();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
            {
                string[] IDs = formCollection["ItemID"].Split(new char[] { ',' });
                //foreach (string id in IDs)
                //{
                //    itemDetails = db.Query<Item>("Select * From Items Where ItemID =" + id, new { id }).SingleOrDefault();

                //    string sqlQuery = "Update Items SET Quantity='" + inQuantity + "'WHERE ItemID='" + itemDetails.ItemID + "'";

                //    int rowsAffected = db.Execute(sqlQuery);
                //}

                itemDetails = db.Query<Item>("Select * From Items Where ItemID =3").SingleOrDefault();

                string sqlQuery = "Update Items SET Quantity='" + IDs.ElementAt(1) + "'WHERE ItemID='" + itemDetails.ItemID + "'";

                int rowsAffected = db.Execute(sqlQuery);

                return RedirectToAction("Index");
            }
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            Item itemDetails = new Item();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
            {
                itemDetails = db.Query<Item>("Select * From Items Where ItemID =" + id, new { id }).SingleOrDefault();
            }
            return View(itemDetails);
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(Item item)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
                {
                    string sqlQuery = "Insert Into Items (ItemBrand, ItemModel, ItemName, Quantity, LastUpdatedBy, LastUpdatedTime) " +
                        "Values('" + item.ItemBrand + "','" + item.ItemModel + "','" + item.ItemName + "','" + item.Quantity + "','" + item.LastUpdatedBy + "','" + DateTime.Now + "')";

                    int rowAffected = db.Execute(sqlQuery);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            Item item = new Item();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
            {
                item = db.Query<Item>("Select * From Items WHERE ItemID=" + id, new { id }).SingleOrDefault();
            }
            return View(item);
        }

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Edit(Item item)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
                {
                    string sqlQuery = "Update Items SET ItemBrand='" + item.ItemBrand + "',ItemModel='" + item.ItemModel + "', ItemName='" + item.ItemName +
                        "',Quantity='" + item.Quantity + "',LastUpdatedBy='" + item.LastUpdatedBy + "',LastUpdatedTime='" + DateTime.Now + "'WHERE ItemID='" + item.ItemID + "'";

                    int rowsAffected = db.Execute(sqlQuery);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            Item item = new Item();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
            {
                item = db.Query<Item>("Select * FROM Items WHERE ItemID=" + id, new { id }).SingleOrDefault();
            }
            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
                {
                    string sqlQuery = "Delete FROM Items WHERE ItemID=" + id;

                    int rowsAffected = db.Execute(sqlQuery);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}