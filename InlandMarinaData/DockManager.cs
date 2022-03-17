using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public static class DockManager
    {

        /// <summary>
        /// get all docks
        /// </summary>
        /// <returns>list of docks</returns>
        public static List<Dock> GetAll()
        {
            InlandMarinaContext db = new InlandMarinaContext();
            return db.Docks.ToList();

        }
        /// <summary>
        /// retrieves list of only ids and names
        /// </summary>
        /// <returns>list of anonymous type </returns>
        public static IList GetDocksAsKeyValuePairs()
        {
            InlandMarinaContext db = new InlandMarinaContext();
            var types = db.Docks.OrderBy(g => g.Name).
                Select(g => new { Value = g.Name, Text = g.Name }).ToList();
            return types;
        }
        /// <summary>
        /// find dock with given name
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>docks with the id or null if not found</returns>

        public static Dock Find(string name)
        {
            InlandMarinaContext db = new InlandMarinaContext();
            //return db.Docks.Include(m =>m.Slips).Where(m =>m.Name == name).SingleOrDefault();
            //return db.Docks.Include(r => r.ID)
            return db.Docks.Find(name);
         
        }

       

    }
}
