using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DatabaseContext _context;

        private GenericRepository<Collection> _collections;

        private GenericRepository<FilesEntity> _files;

        private GenericRepository<ApiUser> _users;

        private GenericRepository<IdentityRole> _roles;


        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public GenericRepository<Collection> Collections => _collections ??= new GenericRepository<Collection>(_context);
        public GenericRepository<FilesEntity> Files => _files ??= new GenericRepository<FilesEntity>(_context);
        public GenericRepository<ApiUser> ApiUsers => _users ??= new GenericRepository<ApiUser>(_context);
        public GenericRepository<IdentityRole> Roles => _roles ??= new GenericRepository<IdentityRole>(_context);

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
