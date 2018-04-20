using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PontoWebIntegracaoExterna
{
    public partial class frmExemplo : Form
    {
        IntegracaoPontoWeb integracao = new IntegracaoPontoWeb();

        public frmExemplo()
        {
            InitializeComponent();
        }

        private void frmExemplo_Load(object sender, EventArgs e)
        {

        }

        private void btnCS_Autenticar_Click(object sender, EventArgs e)
        {
            var resp = integracao.AutenticarContaSecullum(txtCS_Usuario.Text, txtCS_Senha.Text);

            if (resp.erro)
            {
                txtCS_Access_Token.Text = "";
                txtCS_Refresh_Token.Text = "";
                txtCS_NomeUsuario.Text = "";
                cboCS_Bancos.DataSource = null;
                MessageBox.Show(resp.mensagem);
            }
            else
            {
                txtCS_Access_Token.Text = resp.access_token;
                txtCS_Refresh_Token.Text = resp.refresh_token;
            }
        }

        private void btnCS_ListarDados_Click(object sender, EventArgs e)
        {
            var resp = integracao.BuscaDadosContaSecullum(txtCS_Access_Token.Text);

            if (resp.erro)
            {
                txtCS_NomeUsuario.Text = "";
                cboCS_Bancos.Items.Clear();
                MessageBox.Show(resp.mensagem);
            }
            else
            {
                txtCS_NomeUsuario.Text = resp.nome;

                var arrayBancos = resp.listaBancos.Split(';');
                var arrayNomes = resp.listaNomesBancos.Split(';');

                Dictionary<string, string> bancos = new Dictionary<string, string>();

                for (int i = 0; i < arrayBancos.Length; i++)
                {
                    bancos.Add(arrayBancos[i], arrayNomes[i]);
                }
                
                cboCS_Bancos.DataSource = new BindingSource(bancos, null);
                cboCS_Bancos.DisplayMember = "Value";
                cboCS_Bancos.ValueMember = "Key";
            }
        }

        private void btnEmpresas_Listar_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvEmpresas.DataSource = integracao.ListarEmpresas(string.Empty);
        }

        private void AtualizaPropriedades()
        {
            integracao.AccessTokenSelecionado = txtCS_Access_Token.Text;
            integracao.BancoPontoWebSelecionado = cboCS_Bancos.SelectedValue.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(integracao.ExcluirEmpresa(txtCnpjEmpresa.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvHorarios.DataSource = integracao.ListarHorarios(string.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(integracao.ExcluirHorario(txthorarioNumero.Text));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvFuncoes.DataSource = integracao.ListarFuncoes(string.Empty);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(integracao.ExcluirFuncao(txtFuncaoDescricao.Text));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvDepartamentos.DataSource = integracao.ListarDepartamentos(string.Empty);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(integracao.ExcluirDepartamento(txtDepartamentoDescricao.Text));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvFuncionarios.DataSource = integracao.ListarFuncionarios(string.Empty);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(integracao.ExcluirFuncionario(txtPis.Text));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvEmpresas.DataSource = integracao.ListarEmpresas(txtCnpjEmpresa.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvHorarios.DataSource = integracao.ListarHorarios(txthorarioNumero.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvFuncoes.DataSource = integracao.ListarFuncoes(txtFuncaoDescricao.Text);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvDepartamentos.DataSource = integracao.ListarDepartamentos(txtDepartamentoDescricao.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvFuncionarios.DataSource = integracao.ListarFuncionarios(txtPis.Text);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvMotivosDemissao.DataSource = integracao.ListarMotivosDemissao(string.Empty);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvMotivosDemissao.DataSource = integracao.ListarMotivosDemissao(txtMotivoDemissaoDescricao.Text);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvAfastamentos.DataSource = integracao.ListarAfastamentos();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvAfastamentos.DataSource = integracao.ListarAfastamentos(txtAfastamentoDataInicio.Text, txtAfastamentoDataFim.Text, txtAfastamentoFuncionarioPis.Text);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvJustificativas.DataSource = integracao.ListarJustificativas(string.Empty);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvJustificativas.DataSource = integracao.ListarJustificativas(txtJustificativaDescricao.Text);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvPerguntasAdicionais.DataSource = integracao.ListarPerguntasAdicionais();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvPerguntasAdicionais.DataSource = integracao.ListarPerguntasAdicionais(txtPerguntaDescricao.Text, txtPerguntaGrupo.Text);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            dgvBatidas.DataSource = integracao.ListarBatidas(txtBatidasDataInicio.Text, txtBatidasDataFim.Text, txtBatidasFuncionarioPis.Text);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            var dados = new IntegracaoPontoWeb.AfastamentoIntegracaoExterna()
            {
                NumeroPis = txtAfastamentoFuncionarioPis.Text,
                Inicio = Convert.ToDateTime(txtAfastamentoDataInicio.Text),
                Fim = Convert.ToDateTime(txtAfastamentoDataFim.Text),
                JustificativaNome = txtAfastamentoJustificativaNome.Text,
                Motivo = txtAfastamentoMotivo.Text
            };

            AtualizaPropriedades();

            MessageBox.Show(integracao.SalvarAfastamento(dados));
        }

        private void button16_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            MessageBox.Show(integracao.ExcluirMotivoDemissao(txtMotivoDemissaoDescricao.Text));
        }

        private void button19_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            MessageBox.Show(integracao.ExcluirAfastamento(txtAfastamentoDataInicio.Text, txtAfastamentoDataFim.Text, txtAfastamentoFuncionarioPis.Text));
        }

        private void button22_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            MessageBox.Show(integracao.ExcluirJustificativa(txtJustificativaDescricao.Text));
        }

        private void button25_Click(object sender, EventArgs e)
        {
            AtualizaPropriedades();

            MessageBox.Show(integracao.ExcluirPerguntaAdicional(txtPerguntaDescricao.Text, txtPerguntaGrupo.Text));
        }
    }
}
