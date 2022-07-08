using HotelListing.Data;
using HotelListing.IRepository;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DatabaseContext _context;

        private GenericRepository<Country> _countries;

        private GenericRepository<Hotel> _hotels;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public GenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);
        public GenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); 
        }
    }
}
