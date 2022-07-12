using HotelListing.Data;
using HotelListing.Repository;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        GenericRepository<Collection> Collections { get; }
        GenericRepository<FilesEntity> Files { get; }
        GenericRepository<ApiUser> ApiUsers { get; }
        GenericRepository<IdentityRole> Roles { get; }

        Task SaveAsync();

    }
}
