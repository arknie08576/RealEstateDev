using RealEstateDev.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDev.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {
        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
    }
}
