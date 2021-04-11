using System;
using Classics.Data.Models;
using Classics.Data.Repository;

namespace Classics.Data.UnitOfWork
{
    public class BaseUnitOfWork : IBaseUnitOfWork, IDisposable
    {
        public readonly ClassicsContext _context;

        public BaseUnitOfWork(ExtraInfo extra = null)
        {
            _context = new ClassicsContext(extra);
    
            ProfileRepository = new ProfileRepository(_context);
            UserRepository = new UserRepository(_context);
            AddressRepository = new AddressRepository(_context);
            BlobFileRepository = new BlobFileRepository(_context);
            BrandRepository = new BrandRepository(_context);
            CarModelRepository = new CarModelRepository(_context);
            MyCarRepository = new MyCarRepository(_context);
            ProductRepository = new ProductRepository(_context);
            SerieRepository = new SerieRepository(_context);
            SupplierRepository = new SupplierRepository(_context);
            AlertRepository = new AlertRepository(_context);
            UserAlertRepository = new UserAlertRepository(_context);
        }
   
        public IProfileRepository ProfileRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IAddressRepository AddressRepository { get; set; }
        public IBlobFileRepository BlobFileRepository { get; set; }
        public IBrandRepository BrandRepository { get; set; }
        public ICarModelRepository CarModelRepository { get; set; }
        public IMyCarRepository MyCarRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public ISerieRepository SerieRepository { get; set; }
        public ISupplierRepository SupplierRepository { get; set; }
        public IAlertRepository AlertRepository { get; set; }

        public IUserAlertRepository UserAlertRepository { get; set; }

        private bool _disposed;

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        ~BaseUnitOfWork()
        {
            Clear(false);
        }
    }
}

