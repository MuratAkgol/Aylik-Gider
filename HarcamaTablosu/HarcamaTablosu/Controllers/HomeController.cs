using DataLayer;
using EntityLayer;
using HarcamaTablosu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HarcamaTablosu.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var ayinIlkGunu = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ayarlar_Hafta = _context.tbl_ayarlar.Select(x => x.KacHaftaOnce).FirstOrDefault();

            DateTime baslangicTarihi = DateTime.Now.AddDays(-7 * ayarlar_Hafta);
            var toplamHarcama = _context.tbl_Giderler
       .Where(g => g.Tarih >= baslangicTarihi && g.Tarih <= DateTime.Now)
       .Sum(g => (decimal?)g.Tutar) ?? 0;
            ViewBag.ToplamHarcama = toplamHarcama;

            var giderTipleri = _context.tbl_GiderTipleri.ToList();
            ViewBag.GiderTipleri = giderTipleri;

            ViewBag.HarcamaToplamı= _context.tbl_Giderler.Where(x=>x.Tarih >= ayinIlkGunu).Select(x => x.Tutar).Sum();
            ViewBag.Ayarlar_Hafta = ayarlar_Hafta;

            return View();
        }
        [HttpPost]
        public IActionResult Index(GiderTablosu gider)
        {
            var ayarlar_GunIzini = _context.tbl_ayarlar.Select(x=>x.BugunOncesi).FirstOrDefault();

            if (!ayarlar_GunIzini && gider.Tarih > DateTime.Today)
            {
                TempData["TimeError"] = "Bugünden sonraki güne kayıt atamazsınız. Lütfen ayarlarlarınız kontrol ediniz";
                return RedirectToAction("Index");
            }
            else
            {
                _context.tbl_Giderler.Add(gider);
                _context.SaveChanges();
                var giderTipleri = _context.tbl_GiderTipleri.ToList();
                ViewBag.GiderTipleri = giderTipleri;
                TempData["TimeSuccess"] = "İşlem başarılı. Detaylar sayfasından işleme ulaşabilirsiniz";
                return RedirectToAction("Index");
            }

           
        }
        [HttpGet]
        public IActionResult Settings() {
            var giderTipleri = _context.tbl_GiderTipleri.ToList();
            ViewBag.GiderTipleri = giderTipleri;

            
            var ayar = _context.tbl_ayarlar.FirstOrDefault() ?? new Ayarlar
            {
                BugunOncesi = false,
                KacHaftaOnce = 1
            };

            return View(ayar);

        }
        [HttpPost]
        public IActionResult ApplySettings(GiderTip giderTip) {
            _context.tbl_GiderTipleri.Add(giderTip);
            _context.SaveChanges();
            return RedirectToAction("Settings");
        }
        [HttpPost]
        public IActionResult UpdateSettings(Ayarlar ayarlar)
        {
            
            var ayar = _context.tbl_ayarlar.FirstOrDefault();
            ayar.BugunOncesi = ayarlar.BugunOncesi;
            ayar.KacHaftaOnce = ayarlar.KacHaftaOnce;
            _context.SaveChanges();
            return RedirectToAction("Settings");
        }
        [HttpPost]
        public IActionResult UpdateExpense(List<GiderTip> giderTipleri)
        {
            try
            {
                if (giderTipleri != null && giderTipleri.Any())
                {
                    foreach (var giderTip in giderTipleri)
                    {
                        var existingGiderTip = _context.tbl_GiderTipleri.FirstOrDefault(x => x.GiderTipId == giderTip.GiderTipId);
                        if (existingGiderTip != null)
                        {
                            if (existingGiderTip != giderTip)
                            {
                                existingGiderTip.GiderTipi = giderTip.GiderTipi;
                                existingGiderTip.RenkKodu = giderTip.RenkKodu;
                            }

                        }
                    }

                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Güncelleme işlemi başarılı!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Güncelleme işlemi başarısız! Bilgileri kontol ediniz.";
                }

                return RedirectToAction("Settings");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Güncelleme işlemi başarısız! Lütfen teknikle iletişime geçiniz." + ex.Message;
            }
            return RedirectToAction("Settings");
        }

        public IActionResult Details()
        {
            var details = _context.tbl_Giderler.ToList();
            var giderTipleri = _context.tbl_GiderTipleri.ToList();
            ViewBag.GiderTipleri = giderTipleri;
            return View(details);
        }

        [HttpGet]
        public JsonResult GetPieChartData()
        {
            var ayinIlkGunu = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var pieChartData = _context.tbl_Giderler
                .Include(g => g.GiderTip)
                .Where(g => g.Tarih >= ayinIlkGunu)
                .GroupBy(g => new { g.GiderTip.GiderTipi, g.GiderTip.RenkKodu })
                .Select(g => new
                {
                    GiderTipi = g.Key.GiderTipi ?? "Bilinmeyen",
                    ToplamTutar = g.Sum(x => x.Tutar),
                    Renk = g.Key.RenkKodu ?? "#cccccc"
                })
                .ToList();

            return Json(pieChartData);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}