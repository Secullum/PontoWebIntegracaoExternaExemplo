using System;

namespace PontoWebIntegracaoExterna.Modelos
{
    public class Afastamento
    {
        public string NumeroPis { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Motivo { get; set; }
        public string JustificativaNome { get; set; }
    }
}
