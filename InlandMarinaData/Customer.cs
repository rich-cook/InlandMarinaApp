using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public class Customer
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        //[RegularExpression(\d{3}-?\s?\d{3}-?\s?\d{4}\s?)?(x\d{4})?]
        //[DataType(DataType.PhoneNumber)]
        //will put the code below into the phone number entry in the customer page
        //<input class="form-control text-box single-line" id="PhoneNumber" name="PhoneNumber" type="tel" value="">
        [Phone] public string Phone { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }

        // navigation property
        public virtual ICollection<Lease> Leases { get; set; }

        //required public properties for the username and password authentication

        [Required(ErrorMessage = "Please enter username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(30)]
        public string Password { get; set; } 
    }
}

