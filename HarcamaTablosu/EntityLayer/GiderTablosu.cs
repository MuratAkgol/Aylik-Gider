using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class GiderTablosu
    {
        [Key]
        public int GiderId { get; set; }
        public string? GiderAciklama { get; set; }
        public DateTime Tarih { get; set; }
        public int GiderTipId { get; set; }

        // Navigation Property
        public GiderTip GiderTip { get; set; }
        public decimal Tutar { get; set; }
    }
}
