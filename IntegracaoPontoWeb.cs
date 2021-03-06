﻿using Newtonsoft.Json;
using PontoWebIntegracaoExterna.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PontoWebIntegracaoExterna
{
    class IntegracaoPontoWeb
    {
        const string ENDERECO_AUTENTICADOR = "https://autenticador.secullum.com.br";
        const string ENDERECO_PONTOWEB = "https://pontowebintegracaoexterna.secullum.com.br/IntegracaoExterna/";
        const int CLIENT_ID_PONTOWEB = 3;

        enum TipoWebServiceSecullum { Autenticador, PontoWeb };

        public string AccessTokenSelecionado { get; set; }
        public string BancoPontoWebSelecionado { get; set; }

        public AutenticacaoResposta AutenticarContaSecullum(string usuario, string senha)
        {
            var pedido = new
            {
                grant_type = "password",
                username = usuario,
                password = senha,
                client_id = CLIENT_ID_PONTOWEB
            };

            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.Autenticador, "/Token", "POST", pedido);

            AutenticacaoResposta resposta = new AutenticacaoResposta();

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                resposta = JsonConvert.DeserializeObject<AutenticacaoResposta>(respHttp.Conteudo);
            }
            else
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta;
        }

        public AutenticacaoDadosDaContaResposta BuscaDadosContaSecullum(string access_token)
        {
            AccessTokenSelecionado = access_token;

            var pedido = new
            {
                token = access_token
            };

            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.Autenticador, "/ReinvidicacoesToken", "POST", pedido);

            AutenticacaoDadosDaContaResposta resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                resposta = JsonConvert.DeserializeObject<AutenticacaoDadosDaContaResposta>(respHttp.Conteudo);
            }
            else
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            var respostaHttpListaBancos = FazRequisicaoHttp(TipoWebServiceSecullum.Autenticador, "/ContasSecullumExterno/ListarBancos/", "GET", null);

            if (respostaHttpListaBancos.CodigoHttp == HttpStatusCode.OK)
            {
                var listaBancos = JsonConvert.DeserializeObject<List<Banco>>(respostaHttpListaBancos.Conteudo);

                // ClienteId 3 = valor fixo
                resposta.listaBancos = listaBancos.Where(x => x.clienteId == "3").Select(x => new Banco
                {
                    id = x.id,
                    nome = x.nome,
                    clienteId = x.clienteId,
                    identificador = Guid.Parse(x.identificador).ToString("N")
                }).ToList();
            }

            return resposta;
        }

        public List<Empresa> ListarEmpresas(string cnpj)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Empresas?cnpjCpf=" + cnpj, "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<Empresa>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        public string IncluirAlterarEmpresa(Empresa dados)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Empresas", "POST", dados);

            AutenticacaoDadosDaContaResposta resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                resposta = JsonConvert.DeserializeObject<AutenticacaoDadosDaContaResposta>(respHttp.Conteudo);
            }
            else
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        internal List<dynamic> ListarFuncoes(string descricao)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Funcoes?descricao=" + descricao, "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal string ExcluirFuncao(string text)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Funcoes?descricao=" + text, "DELETE", null);

            var resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp != HttpStatusCode.OK)
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        internal string ExcluirHorario(string text)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Horarios?numero=" + text, "DELETE", null);

            var resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp != HttpStatusCode.OK)
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        internal List<dynamic> ListarDepartamentos(string descricao)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Departamentos?descricao=" + descricao, "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal string ExcluirDepartamento(string text)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Departamentos?descricao=" + text, "DELETE", null);

            var resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp != HttpStatusCode.OK)
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        internal List<dynamic> ListarMotivosDemissao(string descricao)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "MotivosDemissao?descricao=" + descricao, "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal object ListarAfastamentos()
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "FuncionariosAfastamentos", "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal object ListarAfastamentos(string inicio, string fim, string pis)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, $"FuncionariosAfastamentos?dataInicio={inicio}&dataFim={fim}&funcionarioPis={pis}", "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal object ListarJustificativas(string descricao)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Justificativas?descricao=" + descricao, "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal object ListarPerguntasAdicionais(string descricao, string grupo)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, $"PerguntasAdicionais?descricao={descricao}&grupo={grupo}", "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal object ListarBatidas(string inicio, string fim, string pis)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, $"Batidas?dataInicio={inicio}&dataFim={fim}&funcionarioPis={pis}", "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal string SalvarAfastamento(Afastamento dados)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "FuncionariosAfastamentos", "POST", dados);

            return respHttp.Conteudo;
        }

        internal string SalvarPendenciaProcessada(PendenciaProcessada dados)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Pendencias", "POST", dados);

            return respHttp.Conteudo;
        }

        internal string ExcluirMotivoDemissao(string text)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "MotivosDemissao?descricao=" + text, "DELETE", null);

            var resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp != HttpStatusCode.OK)
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        internal string ExcluirJustificativa(string text)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Justificativas?descricao=" + text, "DELETE", null);

            var resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp != HttpStatusCode.OK)
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        internal string ExcluirPerguntaAdicional(string descricao, string grupo)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, $"PerguntasAdicionais?descricao={descricao}&grupo={grupo}", "DELETE", null);

            var resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp != HttpStatusCode.OK)
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        internal string ExcluirAfastamento(string inicio, string fim, string pis)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, $"FuncionariosAfastamentos?dataInicio={inicio}&dataFim={fim}&funcionarioPis={pis}", "DELETE", null);

            var resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp != HttpStatusCode.OK)
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        internal object ListarPerguntasAdicionais()
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "PerguntasAdicionais", "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal object ListarPendencias()
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Pendencias", "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal List<dynamic> ListarHorarios(string numero)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Horarios?numero=" + numero, "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        internal List<dynamic> ListarFuncionarios(string pis)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Funcionarios?pis=" + pis, "GET", null);

            if (respHttp.CodigoHttp == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<dynamic>>(respHttp.Conteudo);
            }

            throw new Exception(respHttp.Conteudo);
        }

        public string ExcluirEmpresa(string cnpj)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Empresas?cnpjCpf=" + cnpj, "DELETE", null);

            var resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp != HttpStatusCode.OK)
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        internal string ExcluirFuncionario(string pis)
        {
            var respHttp = FazRequisicaoHttp(TipoWebServiceSecullum.PontoWeb, "Funcionarios?pis=" + pis, "DELETE", null);

            var resposta = new AutenticacaoDadosDaContaResposta();

            if (respHttp.CodigoHttp != HttpStatusCode.OK)
            {
                resposta.erro = true;
                resposta.mensagem = respHttp.Conteudo;
            }

            return resposta.mensagem;
        }

        private RespostaRequisicao FazRequisicaoHttp(TipoWebServiceSecullum webservice, string endereco, string metodo, object dados)
        {
            try
            {
                var url = (webservice == TipoWebServiceSecullum.Autenticador ? ENDERECO_AUTENTICADOR : ENDERECO_PONTOWEB) + endereco;

                RespostaRequisicao resposta = new RespostaRequisicao();

                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = metodo;
                request.KeepAlive = false;
                request.ServicePoint.Expect100Continue = false;
                request.ContentLength = 0;

                if (webservice == TipoWebServiceSecullum.Autenticador)
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }
                else
                {
                    request.ContentType = "application/json; charset=utf-8";
                    request.Headers["Accept-Language"] = "pt-BR";
                    request.Headers["secullumbancoselecionado"] = BancoPontoWebSelecionado;
                }

                if (!string.IsNullOrEmpty(AccessTokenSelecionado))
                {
                    request.Headers["Authorization"] = $"Bearer {AccessTokenSelecionado}";
                }

                if (dados != null)
                {
                    if (webservice == TipoWebServiceSecullum.Autenticador)
                    {
                        var body = codificaFormulario(dados);
                        request.ContentLength = body.Length;

                        using (var requestStream = request.GetRequestStream())
                        using (var streamWriter = new StreamWriter(requestStream))
                        {
                            streamWriter.Write(body);
                        }
                    }
                    else
                    {
                        var stringCorpo = JsonConvert.SerializeObject(dados);
                        var bytes = Encoding.UTF8.GetBytes(stringCorpo);

                        request.ContentLength = bytes.Length;

                        using (var requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                }

                try
                {
                    using (var response = (HttpWebResponse)request.GetResponse())
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        resposta.CodigoHttp = response.StatusCode;
                        resposta.Conteudo = streamReader.ReadToEnd();
                        return resposta;
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response == null)
                    {
                        throw ex;
                    }

                    var response = (HttpWebResponse)ex.Response;

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new Exception("Banco não autorizado", ex);
                    }

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        resposta.CodigoHttp = response.StatusCode;
                        resposta.Conteudo = streamReader.ReadToEnd();

                        return resposta;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string codificaFormulario(object obj)
        {
            var values = new List<string>();
            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj, null);
                var strValue = value == null ? "" : value.ToString();

                values.Add($"{prop.Name}={WebUtility.UrlEncode(strValue)}");
            }

            return string.Join("&", values);
        }
    }
}
