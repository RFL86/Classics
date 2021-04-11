using Classics.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classics.Data.UnitOfWork
{
    public interface IBaseUnitOfWork
    {
        IProfileRepository ProfileRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IAddressRepository AddressRepository { get; set; }
        IBlobFileRepository BlobFileRepository { get; set; }
        IBrandRepository BrandRepository { get; set; }
        ICarModelRepository CarModelRepository { get; set; }
        IMyCarRepository MyCarRepository { get; set; }
        IProductRepository ProductRepository { get; set; }
        ISerieRepository SerieRepository { get; set; }
        ISupplierRepository SupplierRepository { get; set; }
        IAlertRepository AlertRepository { get; set; }
        IUserAlertRepository UserAlertRepository { get; set; }



        void Commit();
    }
}
