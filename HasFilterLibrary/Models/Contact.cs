using System;
using System.Collections.Generic;

namespace HasFilterLibrary.Models
{
    public partial class Contact
    {
        public Contact()
        {
            ContactContactDevices = new HashSet<ContactContactDevices>();
            Customers = new HashSet<Customers>();
        }

        public int ContactIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? InUse { get; set; }

        public virtual ICollection<ContactContactDevices> ContactContactDevices { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
    }
}