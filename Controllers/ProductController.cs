using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using NEW_CRUD_MVC.DataAccess;
using NEW_CRUD_MVC.Models;

namespace NEW_CRUD_MVC.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL _productDAL=new ProductDAL();
        // GET: Product
        public ActionResult Index()
        {
            var productList=_productDAL.GetAllProducts();
            if(productList.Count==0)
            {
                TempData["InfoMessage"] = "Currently Products are not available in the db";
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var product = _productDAL.GetProductById(id);

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not avilable with Id" + id.ToString();
                    return RedirectToAction("Index");
                }

                return View(product);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool IsInserted=false;
            try
            {

                if (ModelState.IsValid)
                {
                    IsInserted = _productDAL.InsertProduct(product);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully....";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the product details!...";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _productDAL.GetProductById(id).FirstOrDefault();

            if(product==null)
            {
                TempData["InfoMessage"] = "Product not avilable with Id" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        // POST: Product/Edit/5
        [HttpPost , ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdate = _productDAL.UpdateProduct(product);

                    if (IsUpdate)
                    {
                        TempData["SuccessMessage"] = "Product details Updated successfully....";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Update the product details!...";
                    }
                }
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }    
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = _productDAL.GetProductById(id).FirstOrDefault();

                    if (product==null)
                    {
                        TempData["InfoMessage"] = "Product details Deleted successfully....";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to delete  the product details!...";
                        return RedirectToAction("Index");
                    }
                  
                }
               return RedirectToAction("Index");

            }
           

            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        // POST: Product/Delete/5
        [HttpPost , ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id )
        {

            try
            {
                string result = _productDAL.DeleteProduct(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }
    }
}
