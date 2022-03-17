using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * This web app creates a website of a fictional company called Inland Marina.
    The website allows users to lease slips, view slips and dock information.
 *  This is the class responsible for handling the task 3 data from the leases, docks and slips from the data model.
 * Author: Richard Cook
 * SAIT, .NET WEB APPLICATIONS ASP.NET 
 * When: February 2022
 */
namespace InlandMarinaData
{
    public class LeaseViewManager
    {
        public static List<Lease> GetLeases()
        {
            InlandMarinaContext db = new InlandMarinaContext();
            return db.Leases.ToList();
        }

        public static List<Slip> GetSlips()
        {
            InlandMarinaContext db = new InlandMarinaContext();
            return db.Slips.ToList();
        }

        public static List<Dock> GetDocks()
        {
            InlandMarinaContext db = new InlandMarinaContext();
            return db.Docks.ToList();
        }

        //public static List<Lease> GetAll()
        //{
        //    InlandMarinaContext db = new InlandMarinaContext();
        //    List<Lease> leases = db.Leases.
        //        Include(r => r.Customer).Include(r => r.Slip).ToList();
        //    return leases;
        //}
    }
}
