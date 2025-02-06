using BusinessLayer.Abstract;
using DataLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class AyarlarManager : IService<Ayarlar>
    {
        private readonly IRepository<Ayarlar> _ayar;

        public AyarlarManager(IRepository<Ayarlar> ayar)
        {
            _ayar = ayar;
        }

        public void Add(Ayarlar entity)
        {
            _ayar.Insert(entity);
        }

        public void Delete(Ayarlar entity)
        {
            _ayar.Delete(entity);
        }

        public Ayarlar GetById(int id)
        {
            return _ayar.Get(x => x.AyarId == id);
        }

        public List<Ayarlar> List()
        {
            return _ayar.List();
        }

        public List<Ayarlar> List(Expression<Func<Ayarlar, bool>> filter)
        {
            return _ayar.List();
        }

        public void Update(Ayarlar entity)
        {
            _ayar.Update(entity);
        }
    }
}
