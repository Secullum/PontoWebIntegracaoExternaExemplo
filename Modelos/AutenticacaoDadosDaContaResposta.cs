namespace PontoWebIntegracaoExterna.Modelos
{
    class AutenticacaoDadosDaContaResposta
    {
        public string email { get; set; }
        public string nome { get; set; }
        public string listaBancos { get; set; }
        public string listaNomesBancos { get; set; }
        public string listaDiasRestantesValidadeBancos { get; set; }
        public string listaServidores { get; set; }
        public string listaQuantidadeFuncionariosBancos { get; set; }
        public string revendaId { get; set; }
        public bool erro { get; set; }
        public string mensagem { get; set; }
    }
}
