namespace TeiasProxy.Models
{
    public class ProxyLog
    {
        public int Id { get; set; }
        public DateTime RequestTime { get; set; }

        public string FirmRequestXml { get; set; }
        public string TeiasRequestXml { get; set; }

        public string TeiasResponseXml { get; set; }
        public string FirmResponseXml { get; set; }

        public string SantralAd { get; set; }
    }
}
