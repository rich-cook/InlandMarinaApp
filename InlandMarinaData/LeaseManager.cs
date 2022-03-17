using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public class LeaseManager
    {


        public static List<Lease> GetAll()
        {
            InlandMarinaContext db = new InlandMarinaContext();
            List<Lease> leases = db.Leases.
                Include(r => r.Customer).Include(r => r.Slip).ToList();
            return leases;
        }
        /// <summary>
        /// adds lease property
        /// </summary>
        /// <param name="lease"></param>
        public static void Add(Lease lease)
        {
            InlandMarinaContext db = new InlandMarinaContext();
            db.Leases.Add(lease);
            db.SaveChanges();
        }
        public static List<Lease> GetLeasesByCustomer(int id = 0)
        {
            List<Lease> leases = null;
            InlandMarinaContext db = new InlandMarinaContext();
            if (id == 0) // no filtering
            {
                
                leases = db.Leases.Include(r => r.Customer).Include(r => r.Slip).ToList();

            }
            else // filter by customer
            {
                leases = db.Leases.
                Where(r => r.CustomerID == id).
                Include(r => r.Customer).Include(r => r.Slip).ToList();
            }
            return leases;
        }

    }
}
