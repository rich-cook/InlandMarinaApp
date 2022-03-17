using InlandMarinaData;
using InlandMarinaMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
 * This controller was created for the lease page view, and will be protected.
 * It will provide users with ability to see the slips and select and lease a slip along
 * with booking a lease and updating the database.
 * 
 */

namespace InlandMarinaMVC.Controllers
{
    [Authorize]
    //create a lease page that allows users to 
    public class LeaseController : Controller
    {
        //public List<Slip> InlandMarinaSlipsManager { get; private set; }

        // GET: LeaseController
        public ActionResult Index()
        {
            
            //I dont need this code, will delete once i figure the rest out
            List<Lease> leases = LeaseManager.GetAll();
            List<LeaseViewModel> viewModels = leases.Select(r => new LeaseViewModel
            {
                ID = r.ID,
                SlipID = r.SlipID,
                CustomerID = r.CustomerID,
                Customer = r.Customer.ToString(),
                
                
            }).ToList();
            return View(viewModels);
        }

        /// <summary>
        /// if customer is logged in, they can see their slips that are leased, filtering the rest out 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: LeaseController/Details/5
        [Authorize]
        public ActionResult List()
        {
            List<Lease> leases = null; //=InlandMarinaSlipsManager.GetAll();
            //get id of the current owner
            int? customerID = HttpContext.Session.GetInt32("CurrentCustomer");
            if (customerID == null)
                customerID = 0;
            leases = LeaseManager.GetLeasesByCustomer((int)customerID);
            return View(leases);
        }

        // GET: LeaseController/Create
        [Authorize]
        public ActionResult Create()
        {
            
            int? customerID = HttpContext.Session.GetInt32("CurrentCustomer");
            //ViewBag.OwnerId = 1; // NEEDS TO BE CHANGED TO CURRENTLY LOGGED IN OWNER
            ViewBag.CustomerID = customerID; // NEEDS TO BE CHANGED TO CURRENTLY LOGGED IN OWNER
            //ViewBag.Docks = GetDockTypes(); // property types to populate drop down
            ViewBag.Slips = GetSlipsforView();
            
            return View(new Lease());
            
        }
        //posting the new lease the customer will "buy"/rent
        // POST: LeaseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lease lease)
        {
            try
            {
                LeaseManager.Add(lease);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }



        // GET: LeaseController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LeaseController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LeaseController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LeaseController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }


        //}
        // auxilliary method for Create 
        protected IEnumerable GetDockTypes()
        {
            // call dock types manager to get key/value pairs for property types drop down list 
            var types = DockManager.GetDocksAsKeyValuePairs();
            // convert it to a form that drop down list can use, and add to the bag
            var styles = new SelectList(types, "Value", "Text");
            var list = styles.ToList(); // to be able to use Insert method
            list.Insert(0, new SelectListItem
            {
                Value = "0",
                Text = "All Styles"
            });
            return list;
        }

        //GetSlips
        protected IEnumerable GetSlipsforView()
        {
            // call dock types manager to get key/value pairs for property types drop down list 
            var types = InlandMarinaSlipsManager.GetSlipsAsKeyValuePairs();
            // convert it to a form that drop down list can use, and add to the bag
            var styles = new SelectList(types, "Value", "Text");
            var list = styles.ToList(); // to be able to use Insert method
            list.Insert(0, new SelectListItem
            {
                Value = "1",
                Text = "Slips"
            });
            return list;
        }

    }
}
