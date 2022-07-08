using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course_Project.Configurations.DAO.Loaders;

public class UserConfigs : IEntityTypeConfiguration<ApiUser>
{
    public void Configure(EntityTypeBuilder<ApiUser> builder)
    {
        builder.HasData(
            new ApiUser
            {
                FirstName = "Jack",
                LastName = "Joe",
                UserName = "_jack",
                PasswordHash = "Password123",
                Email = "jackjoe@gmail.com",
                UserStatus = true
            },
            new ApiUser
            {
                FirstName = "Broun",
                LastName = "James",
                UserName = "_broun",
                PasswordHash= "Password123",
                Email = "brouns@gmail.com",
                UserStatus = true
            },
            new ApiUser
            {
                FirstName = "Guest",
                LastName = "Guest",
                UserName = "SuperUser",
                PasswordHash = "Guest123",
                Email = "noEmail@gmail.com",
                UserStatus = true,
            }

            );

    }
}
