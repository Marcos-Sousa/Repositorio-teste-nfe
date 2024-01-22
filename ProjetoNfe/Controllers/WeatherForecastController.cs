using DFe.Utils;
using Microsoft.AspNetCore.Mvc;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using ProjetoNfe.NovaPasta;
using System.Security.Cryptography.X509Certificates;

namespace ProjetoNfe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static ConfiguracaoApp _configuracoes;
        //string ultimoNsu = Console.ReadLine();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _configuracoes = new ConfiguracaoApp();

        }

        [HttpGet(Name = "testeNfe")]

        public async Task CarregarNSUs()
        {
            try
            {

                do
                {
                    RetornoNfeDistDFeInt retornoNFeDistDFe = null;
                    X509Certificate2 x509Certificate = new X509Certificate2("", "");
                    using (var servicoNFe = new ServicosNFe(_configuracoes.CfgServico, x509Certificate))
                        retornoNFeDistDFe = servicoNFe.NfeDistDFeInteresse(ufAutor: "17",
                                                                           documento: "20870746000163",
                                                                           ultNSU: 0.ToString());

                    var lote = retornoNFeDistDFe.Retorno.loteDistDFeInt;
                    if (lote == null || !lote.Any())
                        break;

                    Console.WriteLine($"{"NSU".PadRight(44, ' ')} | Xml");

                    foreach (var item in lote)
                    {
                        string linha = string.Empty;

                        string xmlStr = string.Empty;

                        if (item.XmlNfe != null)
                        {
                            xmlStr = Compressao.Unzip(item.XmlNfe);

                            Console.WriteLine($"{item.NSU.ToString().PadRight(44, ' ')} | {xmlStr}");
                        }
                    }

                    await Task.Delay(2000); //https://github.com/ZeusAutomacao/DFe.NET/issues/568#issuecomment-339862458

                } while (true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
