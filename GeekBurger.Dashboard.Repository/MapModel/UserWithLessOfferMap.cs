using GeekBurger.Dashboard.Repository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekBurger.Dashboard.Repository.MapModel
{
    internal class UserWithLessOfferMap : IEntityTypeConfiguration<UserWithLessOffer>
    {
        public void Configure(EntityTypeBuilder<UserWithLessOffer> builder)
        {
            builder.ToTable("UserWithLessOffer");
            builder.HasKey(x => x.Id);
        }
    }
}