using PontoWebIntegracaoExterna.Filtros;
using PontoWebIntegracaoExterna.Modelos;
using System;
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

        private bool ConsistirDados()
        {
            if (string.IsNullOrEmpty(txtCS_Access_Token.Text))
            {
                MessageBox.Show("É necessário fazer login na Conta Secullum");

                tabControl1.SelectedIndex = 0;

                return false;
            }

            return true;
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

                cboCS_Bancos.DisplayMember = "nome";
                cboCS_Bancos.ValueMember = "identificador";
                cboCS_Bancos.DataSource = new BindingSource(resp.listaBancos, null);
            }
        }

        private void btnEmpresas_Listar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvEmpresas.DataSource = integracao.ListarEmpresas(string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ConsistirDados())
            {
                MessageBox.Show(integracao.ExcluirEmpresa(txtCnpjEmpresa.Text));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvHorarios.DataSource = integracao.ListarHorarios(string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ConsistirDados())
            {
                MessageBox.Show(integracao.ExcluirHorario(txthorarioNumero.Text));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvFuncoes.DataSource = integracao.ListarFuncoes(string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ConsistirDados())
            {
                MessageBox.Show(integracao.ExcluirFuncao(txtFuncaoDescricao.Text));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvDepartamentos.DataSource = integracao.ListarDepartamentos(string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ConsistirDados())
            {
                MessageBox.Show(integracao.ExcluirDepartamento(txtDepartamentoDescricao.Text));
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvFuncionarios.DataSource = integracao.ListarFuncionarios(string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (ConsistirDados())
            {
                MessageBox.Show(integracao.ExcluirFuncionario(txtPis.Text));
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvEmpresas.DataSource = integracao.ListarEmpresas(txtCnpjEmpresa.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvHorarios.DataSource = integracao.ListarHorarios(txthorarioNumero.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvFuncoes.DataSource = integracao.ListarFuncoes(txtFuncaoDescricao.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvDepartamentos.DataSource = integracao.ListarDepartamentos(txtDepartamentoDescricao.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvFuncionarios.DataSource = integracao.ListarFuncionarios(txtPis.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvMotivosDemissao.DataSource = integracao.ListarMotivosDemissao(string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvMotivosDemissao.DataSource = integracao.ListarMotivosDemissao(txtMotivoDemissaoDescricao.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvAfastamentos.DataSource = integracao.ListarAfastamentos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvAfastamentos.DataSource = integracao.ListarAfastamentos(txtAfastamentoDataInicio.Text, txtAfastamentoDataFim.Text, txtAfastamentoFuncionarioPis.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvJustificativas.DataSource = integracao.ListarJustificativas(string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvJustificativas.DataSource = integracao.ListarJustificativas(txtJustificativaDescricao.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvPerguntasAdicionais.DataSource = integracao.ListarPerguntasAdicionais();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvPerguntasAdicionais.DataSource = integracao.ListarPerguntasAdicionais(txtPerguntaDescricao.Text, txtPerguntaGrupo.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvBatidas.DataSource = integracao.ListarBatidas(new BatidaFiltro()
                    {
                        DataInicio = txtBatidasDataInicio.Text,
                        DataFim = txtBatidasDataFim.Text,
                        HoraInicio = txtBatidasHoraInicio.Text,
                        HoraFim = txtBatidasHoraFim.Text,
                        FuncionarioPis = txtBatidasFuncionarioPis.Text,
                        EmpresaDocumento = txtBatidasEmpresaDocumento.Text
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (!ConsistirDados())
            {
                return;
            }

            var dados = new Afastamento()
            {
                NumeroPis = txtAfastamentoFuncionarioPis.Text,
                Inicio = Convert.ToDateTime(txtAfastamentoDataInicio.Text),
                Fim = Convert.ToDateTime(txtAfastamentoDataFim.Text),
                JustificativaNome = txtAfastamentoJustificativaNome.Text,
                Motivo = txtAfastamentoMotivo.Text
            };

            MessageBox.Show(integracao.SalvarAfastamento(dados));
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (ConsistirDados())
            {
                MessageBox.Show(integracao.ExcluirMotivoDemissao(txtMotivoDemissaoDescricao.Text));
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (ConsistirDados())
            {
                MessageBox.Show(integracao.ExcluirAfastamento(txtAfastamentoDataInicio.Text, txtAfastamentoDataFim.Text, txtAfastamentoFuncionarioPis.Text));
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (ConsistirDados())
            {
                MessageBox.Show(integracao.ExcluirJustificativa(txtJustificativaDescricao.Text));
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (ConsistirDados())
            {
                MessageBox.Show(integracao.ExcluirPerguntaAdicional(txtPerguntaDescricao.Text, txtPerguntaGrupo.Text));
            }
        }

        private void btnLISTAR_PENDENCIAS_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsistirDados())
                {
                    dgvPENDENCIAS.DataSource = integracao.ListarPendencias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSALVAR_PENDENCIA_Click(object sender, EventArgs e)
        {
            if (!ConsistirDados())
            {
                return;
            }

            var dados = new PendenciaProcessada()
            {
                NomeComputador = txtNOME_COMPUTADOR.Text,
                Erro = txtERRO.Text
            };

            int.TryParse(txtPENDENCIA_ID.Text, out int pendenciaId);

            dados.Id = pendenciaId;

            MessageBox.Show(integracao.SalvarPendenciaProcessada(dados));
        }

        private void cboCS_Bancos_SelectedIndexChanged(object sender, EventArgs e)
        {
            integracao.BancoPontoWebSelecionado = cboCS_Bancos.SelectedValue.ToString();
        }
    }
}
