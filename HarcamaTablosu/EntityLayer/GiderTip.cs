namespace EntityLayer
{
    public class GiderTip
    {
        public int GiderTipId { get; set; }
        public string GiderTipi { get; set; }
        public string RenkKodu { get; set; }
        public ICollection<GiderTablosu> Giderler { get; set; } = new List<GiderTablosu>();
    }
}