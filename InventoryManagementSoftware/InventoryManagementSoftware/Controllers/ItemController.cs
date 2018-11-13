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
        public static List<Item> itemList;

        // GET: Item
        public ActionResult Index()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
            {
                itemList = db.Query<Item>("Select * From Items").ToList();
            }
            return View(itemList);
        }

        // Update Table (POST)
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            string[] inQuantity = formCollection["inQuantity"].Split(new char[] { ',' });
            string[] outQuantity = formCollection["outQuantity"].Split(new char[] { ',' });
            string updateBy = formCollection["updatedBy"];
            for (int i = 0; i < itemList.Count; i++)
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
                    {
                        if (Convert.ToInt32(outQuantity[i]) > 0 || Convert.ToInt32(inQuantity[i]) > 0)
                        {
                            Item currentItem = itemList[i];
                            int newQuantity = currentItem.Quantity + Convert.ToInt32(inQuantity[i]) - Convert.ToInt32(outQuantity[i]);
                            string sqlQuery = "Update Items SET Quantity='" + newQuantity + "',LastUpdatedBy='" + updateBy + "'WHERE ItemID='" + itemList[i].ItemID + "'";
                            int rowsAffected = db.Execute(sqlQuery);
                            string sqlQuery2 = "Insert Into LogItems (ItemID, ItemBrand, ItemModel, ItemName, QuantityIn, QuantityOut, LastUpdatedBy, LastUpdatedTime) " +
                            "Values('" + itemList[i].ItemID + "','" + itemList[i].ItemBrand + "','" + itemList[i].ItemModel + "','" + itemList[i].ItemName + "','" + inQuantity[i] + "','" + outQuantity[i] + "','" + itemList[i].LastUpdatedBy + "','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "')";
                            int rowsAffected2 = db.Execute(sqlQuery2);
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Print(DateTime.Now.ToString("MM/dd/yyyy ss:mm:HH"));
                }
            }
            return RedirectToAction("Index");
        }

        //GET: Item
        public ActionResult LogItemIndex()
        {
            List<LogItem> logItemList = new List<LogItem>();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
            {
                logItemList = db.Query<LogItem>("Select * From LogItems").ToList();
            }
            return View(logItemList);
        }

        [HttpPost]
        public ActionResult LogItemIndex(DateTime searchDate)
        {
            List<LogItem> logItemList = new List<LogItem>();
            string date = searchDate.ToString("yyyy-MM-dd");

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryManagementSoftware.Properties.Settings.ProductConnectionString"].ConnectionString))
            {
                logItemList = db.Query<LogItem>("Select * From LogItems Where CAST(LastUpdatedTime as DATE)='" + date + "'").ToList();
            }
            return View(logItemList);
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
                        "Values('" + item.ItemBrand + "','" + item.ItemModel + "','" + item.ItemName + "','" + item.Quantity + "','" + item.LastUpdatedBy + "','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "')";

                    int rowAffected = db.Execute(sqlQuery);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return null;
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