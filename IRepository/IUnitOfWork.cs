using HotelListing.Data;
using HotelListing.Repository;

namespace HotelListing.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        GenericRepository<Country> Countries { get; }
        GenericRepository<Hotel> Hotels { get; }

        Task SaveAsync();

    }
}
