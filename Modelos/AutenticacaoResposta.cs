using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoWebIntegracaoExterna.Modelos
{
    class AutenticacaoResposta
    {
        public string access_token;
        public string token_type;
        public int expires_in;
        public string refresh_token;
        public bool erro;
        public string mensagem;
    }
}
