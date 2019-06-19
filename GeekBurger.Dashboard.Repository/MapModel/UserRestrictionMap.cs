using GeekBurger.Dashboard.Repository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.Repository.MapModel
{
    public class UserRestrictionMap : IEntityTypeConfiguration<UserRestriction>
    {
        public void Configure(EntityTypeBuilder<UserRestriction> builder)
        {
            builder.ToTable("UserRestriction");
            builder.HasKey(x => x.Id);
        }
    }
}