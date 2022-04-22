using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithDates.Classes
{
    /// <summary>
    /// Suited to holding current and original values from ChangeTracker  
    /// </summary>
    public class ChangeContainer
    {
        public int Id { get; set; }
        public string OriginalFirstName { get; set; }
        public string CurrentFirstName { get; set; }
        public string OriginalLastName { get; set; }
        public string CurrentLastName { get; set; }
        public DateTime? OriginalBirthDate { get; set; }
        public DateTime? CurrentBirthDate { get; set; }
        public EntityState EntityState { get; set; }
    }
}
