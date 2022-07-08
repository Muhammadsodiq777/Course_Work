using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Uzbekistan Hotel",
                    Address = "Amir Temur square",
                    CountryId = 1,
                    Rating = 4
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Golden Hotel",
                    Address = "SA",
                    CountryId = 2,
                    Rating = 5
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Effendi Hotel",
                    Address = "TK",
                    CountryId = 3,
                    Rating = 5
                });
        }
    }
}
