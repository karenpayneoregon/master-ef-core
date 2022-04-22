using System;
using System.Collections.Generic;

namespace HasFilterLibrary.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public int CustomerIdentifier { get; set; }
        public string CompanyName { get; set; }
        public int? ContactIdentifier { get; set; }
        public int? ContactTypeIdentifier { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int? CountryIdentfier { get; set; }
        public string Phone { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? InUse { get; set; }

        public virtual Contact ContactIdentifierNavigation { get; set; }
        public virtual ContactType ContactTypeIdentifierNavigation { get; set; }
        public virtual Countries CountryIdentfierNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}