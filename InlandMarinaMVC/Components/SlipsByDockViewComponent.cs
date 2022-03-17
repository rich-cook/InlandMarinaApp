using InlandMarinaData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InlandMarinaMVC.Components
{
    public class SlipsByDockViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string id) //invoke dock id asynchronous method
        {
            List<Slip> slips = null;
            if (id == "All")
                slips = InlandMarinaSlipsManager.GetSlips(); //all slips
            else //filter by dock
                slips = InlandMarinaSlipsManager.GetSlipsByDock(id);

            return View(slips); //calls Default.cshtml in Views/Shared/Components/SlipsByDock
        }
    }
}
