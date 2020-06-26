using System.Net;

namespace PontoWebIntegracaoExterna
{
    class RespostaRequisicao
    {
        public HttpStatusCode CodigoHttp { get; set; }
        public string Conteudo { get; set; }
    }
}
