using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HasFilterLibrary.Interfaces;

namespace HasFilterLibrary.Models
{
    public partial class Shippers : ISoftDelete
    {
        public Shippers()
        {
            Orders = new HashSet<Orders>();
        }

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        [NotMapped]
        public bool IsDeleted
        {
            get => (bool)Deleted;
            set => Deleted = value;
        }
    }
}