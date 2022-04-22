namespace HasFilterLibrary.Models
{
    public partial class ContactContactDevices
    {
        public int Identifier { get; set; }
        public int? ContactIdentifier { get; set; }
        public int? PhoneTypeIdenitfier { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Active { get; set; }

        public virtual Contact ContactIdentifierNavigation { get; set; }
        public virtual PhoneType PhoneTypeIdenitfierNavigation { get; set; }
    }
}