using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using HasFilterLibrary.Interfaces;

namespace HasFilterLibrary.Models
{
    public partial class Suppliers : ISoftDelete, INotifyPropertyChanged
    {
        private bool? _deleted;
        private string _companyName;

        public Suppliers()
        {
            Products = new HashSet<Products>();
        }

        public int SupplierId { get; set; }

        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged();
            }
        }

        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// This column is part of working with soft deletes
        /// </summary>
        public bool? Deleted
        {
            get => _deleted;
            set
            {
                _deleted = value;
                OnPropertyChanged();
            }
        }
        public virtual ICollection<Products> Products { get; set; }

        /// <summary>
        /// This column is part of working with soft deletes
        /// </summary>
        [NotMapped]
        public bool IsDeleted
        { 
            get => Deleted != null && (bool) Deleted;
            set => Deleted = value;
        }

        public override string ToString() => CompanyName;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}