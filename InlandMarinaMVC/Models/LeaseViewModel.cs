using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InlandMarinaMVC.Models
{
    public class LeaseViewModel
    {
        public int ID { get; set; }
        public int SlipID { get; set; }
        public int CustomerID { get; set; }

        public string Customer { get; set; }
        public string DockID { get; set; }
    }
}
