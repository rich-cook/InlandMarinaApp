using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{

    //methods for managing the InlandMarina tables
    public static class InlandMarinaSlipsManager
    {

        /// <summary>
        /// get all slips
        /// </summary>
        /// <returns>list of slips</returns>
        public static List<Slip> GetSlips()
        {
            InlandMarinaContext db = new InlandMarinaContext();
            var slipQuery = (from slip in db.Slips
                             join dock in db.Docks
                             on slip.DockID equals dock.ID
                             join lease in db.Leases
                             on slip.ID equals lease.SlipID into leasedSlips
                             from joined in leasedSlips.DefaultIfEmpty()
                             where joined == null
                             select slip).ToList();

            return slipQuery;
            //return db.Slips.ToList();
            //need to filter out slips that are already ordered with a query
        }
        public static Slip FindSlip(int id)
        {
            InlandMarinaContext db = new InlandMarinaContext();
            return db.Slips.Include(m => m.Dock).Where(m => m.ID == id).SingleOrDefault();
        }
        /// <summary>
        /// get slips at a given dock
        /// </summary>
        /// <param name="dockId"></param>
        /// <returns></returns>
        public static List<Slip> GetSlipsByDock(string name)
        {
            InlandMarinaContext db = new InlandMarinaContext();
           
            return db.Slips.Include(m => m.Dock).Where(m => m.Dock.Name == name).ToList();

        }
        public static IList GetSlipsAsKeyValuePairs()
        {
            InlandMarinaContext db = new InlandMarinaContext();
            var types = db.Slips.OrderBy(g => g.ID).
                Select(g => new { Value = g.ID, Text = g.ID }).ToList();
            return types;
        }

    }
}
