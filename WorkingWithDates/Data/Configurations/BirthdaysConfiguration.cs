﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingWithDates.Models;

#nullable disable

namespace WorkingWithDates.Data.Configurations
{
    public class BirthdaysConfiguration : IEntityTypeConfiguration<Birthdays>
    {
        public void Configure(EntityTypeBuilder<Birthdays> entity)
        {
            entity.Property(e => e.BirthDate).HasColumnType("date");
        }
    }
}