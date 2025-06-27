using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using TeiasProxy.Data;
using TeiasProxy.Models;

namespace TeiasProxy.Services
{
    public class TeiasProxyService
    {
        private readonly HttpClient _httpClient;
        private readonly ProxyDbContext _dbContext;

        public TeiasProxyService(HttpClient httpClient, ProxyDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task<string> ForwardToTeiasAndLogAsync(string firmRequestXml)
        {
            string plantName = ExtractPlantName(firmRequestXml);

            var plantCreds = await _dbContext.PlantCredentials
                .FirstOrDefaultAsync(p => p.PlantName == plantName);

            if (plantCreds == null)
                throw new Exception($"Plant credentials not found for: {plantName}");

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://ws.teias.gov.tr/apigateway/prod/dgp")
            {
                Content = new StringContent(firmRequestXml, Encoding.UTF8, "text/xml")
            };

            httpRequest.Headers.Add("username", plantCreds.Username);
            httpRequest.Headers.Add("password", plantCreds.Password);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
             
            // 4. İstek gönderilir, yanıt alınır
            // yerine
            //var httpResponse = await _httpClient.SendAsync(httpRequest);
            //var teiasResponseXml = await httpResponse.Content.ReadAsStringAsync();

            // geçici olarak
            var teiasResponseXml = @"<?xml version=""1.0""?>
                        <response>
                            <status>true</status>
                            <plant>FATMA RES</plant>
                        </response>";


            var log = new ProxyLog
            {
                RequestTime = DateTime.UtcNow,
                FirmRequestXml = firmRequestXml,
                TeiasRequestXml = firmRequestXml,
                TeiasResponseXml = teiasResponseXml,
                FirmResponseXml = teiasResponseXml,
                SantralAd = plantName
            };

            _dbContext.ProxyLogs.Add(log);
            await _dbContext.SaveChangesAsync();

            // 6. TEİAŞ cevabı aynen geri döndürülür
            return teiasResponseXml;
        }

        private string ExtractPlantName(string xml)
        {
            try
            {
                var doc = XDocument.Parse(xml);

                var plantName = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "plantName")?.Value;

                return plantName ?? throw new Exception("Santral adı (plantName) XML içinde bulunamadı.");
            }
            catch (Exception ex)
            {
                throw new Exception($"XML çözümleme hatası: {ex.Message}");
            }
        }
    }
}
