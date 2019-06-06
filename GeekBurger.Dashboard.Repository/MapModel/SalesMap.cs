using GeekBurger.Dashboard.Repository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.Repository.MapModel
{
    public class SalesMap : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(x => x.Id);
        }
    }
}
