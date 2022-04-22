using System.Collections.Generic;

namespace HasFilterLibrary.Models
{
    public partial class PhoneType
    {
        public PhoneType()
        {
            ContactContactDevices = new HashSet<ContactContactDevices>();
        }

        public int PhoneTypeIdenitfier { get; set; }
        public string PhoneTypeDescription { get; set; }

        public virtual ICollection<ContactContactDevices> ContactContactDevices { get; set; }
    }
}