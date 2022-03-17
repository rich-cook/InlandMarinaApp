using InlandMarinaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InlandMarinaMVC.Controllers
{
    //limiting access to the slips page
    
    public class SlipController : Controller
    {
        // GET: SlipController
        //public async Task<IActionResult> Index(int searchInt)
        //{
        //    var slips = from m in Slip
        //                 select m;

        //    if (searchInt == null)
        //    {
        //        slips = slips.Where(s => s.DockID.Contains(searchInt));
        //    }

        //    return View(await movies.ToListAsync());
        //}
        public ActionResult Index()
        {
            // List<Slip> slips = InlandMarinaSlipsManager.OrderBy(p => p.DockID).GetSlips();
            List<Slip> slips = null;
            try
            {
                slips = InlandMarinaSlipsManager.GetSlips();
            }
            catch(Exception)
            {
                TempData["Message"] = "Database connection problem. Try again later.";
                TempData["IsError"] = true;
            }
            return View(slips);
        }

        // auxilliary - not an action method
        private IEnumerable GetDocksWithAll() //IEnumerable is an interface
        {
            var types = DockManager.GetDocksAsKeyValuePairs(); // list of anonymous types Value/Text that represent genres
            var list = new SelectList(types, "Value", "Text").ToList();
            list.Insert(0, new SelectListItem { Value = "All", Text = "All" }); // add All option in front
            return list;
        }

        // GET: SlipsController/FilterByDocks/A
        public ActionResult FilterByDock(string id = "All")
        {
            try
            {
                ViewBag.Docks = GetDocksWithAll();
                return View(); // view for the filtering page
            }
            catch (Exception)
            {
                TempData["Message"] = "Database connection problem. Try again later.";
                TempData["IsError"] = true;
                return RedirectToAction("Index");
            }
        }

        // method to be called from Ajax function - invokes the view component
        public ActionResult GetSlipsByDock(string id) // dock id
        {
            return ViewComponent("SlipsByDock", id); // calls the InvokeAsync
                       // produces partial view with filtered slips
        }

        // GET: SlipController/Details/5
        public ActionResult Details(int id)
        {
            Slip slip = InlandMarinaSlipsManager.FindSlip(id);
            return View(slip);
        }

        //// GET: MovieController/Create
        //public ActionResult Create()
        //{
        //    try
        //    {
        //        // prepare list of genres for the drop  down list and pass through the view bag
        //        var types = DockManager.GetDocksAsKeyValuePairs();
        //        SelectList list = new SelectList(types, "Value", "Text");
        //        ViewBag.Docks = list;

        //        //return View(); // by default returns the view named the same as the method
        //        return View("Edit", new Slip()); // MovieId is initialized to zero
        //    }
        //    catch (Exception)
        //    {
        //        TempData["Message"] = "Database connection problem. Try again later.";
        //        TempData["IsError"] = true;
        //        return RedirectToAction("Index");
        //    }
        //}

        //// POST: SlipController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
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


        
        //// GET: SlipController/Edit/5
        
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

       
        //// POST: SlipController/Edit/5
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

        //// GET: SlipController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: SlipController/Delete/5
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
    }
}
