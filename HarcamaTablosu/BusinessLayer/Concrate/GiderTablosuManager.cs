using BusinessLayer.Abstract;
using DataLayer.Abstract;
using DataLayer.GenericRepository;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class GiderTablosuManager : IService<GiderTablosu>
    {
        //GenericRepository<GiderTablosu> _gider = new GenericRepository<GiderTablosu>();
        private readonly IRepository<GiderTablosu> _gider;

        public GiderTablosuManager(IRepository<GiderTablosu> gider)
        {
            _gider = gider;
        }

        public void Add(GiderTablosu entity)
        {
            _gider.Insert(entity);
        }

        public void Delete(GiderTablosu entity)
        {
            _gider.Delete(entity);
        }

        public GiderTablosu GetById(int id)
        {
            return _gider.Get(x => x.GiderId == id);
        }

        public List<GiderTablosu> List()
        {
            return _gider.List();
        }

        public List<GiderTablosu> List(Expression<Func<GiderTablosu, bool>> filter)
        {
            return _gider.List();
        }

        public void Update(GiderTablosu entity)
        {
            _gider.Update(entity);
        }
    }
}
