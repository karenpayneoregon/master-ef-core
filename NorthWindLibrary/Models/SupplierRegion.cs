﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace NorthWindLibrary.Models
{
    public partial class SupplierRegion
    {
        public SupplierRegion()
        {
            Suppliers = new HashSet<Suppliers>();
        }

        public int RegionId { get; set; }
        public string RegionDescription { get; set; }

        public virtual ICollection<Suppliers> Suppliers { get; set; }
    }
}