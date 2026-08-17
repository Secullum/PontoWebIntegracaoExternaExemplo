using System.Net;

namespace IntegracaoExternaExemplo
{
    class RespostaRequisicao
    {
        public HttpStatusCode CodigoHttp { get; set; }
        public string Conteudo { get; set; }
    }
}
