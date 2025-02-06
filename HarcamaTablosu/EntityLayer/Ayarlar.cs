using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Ayarlar
    {
        [Key]
        public int AyarId { get; set; }
        public bool BugunOncesi { get; set; }
        public int KacHaftaOnce { get; set; }
    }
}
