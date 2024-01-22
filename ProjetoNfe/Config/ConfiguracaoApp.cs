using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Utils.Email;
using NFe.Utils;
using System.Net;
using DFe.Utils;
using DFe.Classes.Entidades;

namespace ProjetoNfe.NovaPasta
{
    public class ConfiguracaoApp
    {
        private ConfiguracaoServico _cfgServico;

        public ConfiguracaoApp()
        {
            CfgServico = ConfiguracaoServico.Instancia;
            CfgServico.tpAmb = TipoAmbiente.Producao;
            CfgServico.tpEmis = TipoEmissao.teNormal;
            CfgServico.ProtocoloDeSeguranca = ServicePointManager.SecurityProtocol;
            CfgServico.VersaoNFeDistribuicaoDFe = VersaoServico.Versao100;
            CfgServico.cUF = Estado.TO;
            Emitente = new emit { CPF = "", CRT = CRT.SimplesNacional };
            EnderecoEmitente = new enderEmit();
            ConfiguracaoEmail = new ConfiguracaoEmail("email@dominio.com", "senha", "Envio de NFE", "Conexão", "smtp.dominio.com", 587, true, true);
        }

        public ConfiguracaoServico CfgServico
        {
            get
            {
                ConfiguracaoServico.Instancia.CopiarPropriedades(_cfgServico);
                return _cfgServico;
            }
            set
            {
                _cfgServico = value;
                ConfiguracaoServico.Instancia.CopiarPropriedades(value);
            }
        }

        public emit Emitente { get; set; }
        public enderEmit EnderecoEmitente { get; set; }
        public ConfiguracaoEmail ConfiguracaoEmail { get; set; }
    }
}
