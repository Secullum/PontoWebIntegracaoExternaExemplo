namespace PontoWebIntegracaoExterna.Modelos
{
    public class Empresa
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Inscricao { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string Pais { get; set; }
        public string Telefone { get; set; }
        public string Fax { get; set; }
        public string CEI { get; set; }
        public string NFolhaEmpresa { get; set; }
        public string ResponsavelNome { get; set; }
        public string ResponsavelCargo { get; set; }
        public bool UsaCpf { get; set; }
    }
}
