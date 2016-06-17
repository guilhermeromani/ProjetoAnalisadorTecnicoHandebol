using AForge.Fuzzy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoAnalisadorTecnicoHandbol
{
    public partial class Form1 : Form
    {
        public Formacao formacao = new Formacao();
        public List<Jogador> jogadores = new List<Jogador>();

        public InferenceSystem ISAnaliseIndividual;
        public InferenceSystem ISAnaliseColetiva;

        string nomeJogadorParaAlterar = String.Empty;
        public Form1()
        {
            InitializeComponent();

            jogadores.Add(new Jogador("A", "", "Ponta Esquerda", 3, 2, 1.7f, 3, 55));
            jogadores.Add(new Jogador("B", "", "Meia Esquerda", 10, 10, 1.8f, 9, 90));
            jogadores.Add(new Jogador("C", "", "Central", 10, 10, 2f, 10, 90));
            jogadores.Add(new Jogador("D", "", "Meia Direita", 10, 9, 1.8f, 9, 80));
            jogadores.Add(new Jogador("E", "", "Ponta Direita", 2, 2, 1.7f, 3, 58));
            jogadores.Add(new Jogador("F", "", "Pivo", 2, 2, 1.7f, 3, 65));

            List<string> nomesJogadores = new List<string>();
            foreach (Jogador jogadores in jogadores)
            {
                nomesJogadores.Add($"{jogadores.nome} - ({jogadores.posicao})");
            }
            lstJogadores.DataSource = nomesJogadores;

            pbxJogador1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador1.BackColor = Color.Transparent;
            pbxJogador2.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador2.BackColor = Color.Transparent;
            pbxJogador3.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador3.BackColor = Color.Transparent;
            pbxJogador4.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador4.BackColor = Color.Transparent;
            pbxJogador5.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador5.BackColor = Color.Transparent;
            pbxJogador6.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador6.BackColor = Color.Transparent;
            

            #region .: Entradas :.

            // Altura
            FuzzySet cfBaixo = new FuzzySet("baixo", new TrapezoidalFunction(1.6f, 1.7f, TrapezoidalFunction.EdgeType.Right));
            FuzzySet cfMedio = new FuzzySet("medio", new TrapezoidalFunction(1.6f, 1.7f, 1.8f, 1.9f));
            FuzzySet cfAlto = new FuzzySet("alto", new TrapezoidalFunction(1.8f, 1.9f, TrapezoidalFunction.EdgeType.Left));
            LinguisticVariable lvAltura = new LinguisticVariable("altura", 1.5f, 2.1f);
            lvAltura.AddLabel(cfBaixo);
            lvAltura.AddLabel(cfMedio);
            lvAltura.AddLabel(cfAlto);

            // Peso
            FuzzySet cfLeve = new FuzzySet("leve", new TrapezoidalFunction(60, 70, TrapezoidalFunction.EdgeType.Right));
            FuzzySet cfEmForma = new FuzzySet("emForma", new TrapezoidalFunction(60, 70, 80, 90));
            FuzzySet cfPesado = new FuzzySet("pesado", new TrapezoidalFunction(80, 90, TrapezoidalFunction.EdgeType.Left));
            LinguisticVariable lvPeso = new LinguisticVariable("peso", 50, 120);
            lvPeso.AddLabel(cfLeve);
            lvPeso.AddLabel(cfEmForma);
            lvPeso.AddLabel(cfPesado);

            // Velocidade
            FuzzySet cfLento = new FuzzySet("lento", new TrapezoidalFunction(2, 4, TrapezoidalFunction.EdgeType.Right));
            FuzzySet cfComum = new FuzzySet("comum", new TrapezoidalFunction(2, 4, 6, 8));
            FuzzySet cfRapido = new FuzzySet("rapido", new TrapezoidalFunction(6, 8, TrapezoidalFunction.EdgeType.Left));
            LinguisticVariable lvVelocidade = new LinguisticVariable("velocidade", 1, 10);
            lvVelocidade.AddLabel(cfLento);
            lvVelocidade.AddLabel(cfComum);
            lvVelocidade.AddLabel(cfRapido);

            // Habilidade
            FuzzySet cfRuim = new FuzzySet("ruim", new TrapezoidalFunction(2, 4, TrapezoidalFunction.EdgeType.Right));
            FuzzySet cfRegular = new FuzzySet("regular", new TrapezoidalFunction(2, 4, 6, 8));
            FuzzySet cfBom = new FuzzySet("bom", new TrapezoidalFunction(6, 8, TrapezoidalFunction.EdgeType.Left));
            LinguisticVariable lvHabilidade = new LinguisticVariable("habilidade", 1, 10);
            lvHabilidade.AddLabel(cfRuim);
            lvHabilidade.AddLabel(cfRegular);
            lvHabilidade.AddLabel(cfBom);

            // Força
            FuzzySet cfFraco = new FuzzySet("fraco", new TrapezoidalFunction(10, 30, TrapezoidalFunction.EdgeType.Right));
            FuzzySet cfMediano = new FuzzySet("mediano", new TrapezoidalFunction(10, 30, 50, 70));
            FuzzySet cfForte = new FuzzySet("forte", new TrapezoidalFunction(50, 70, TrapezoidalFunction.EdgeType.Left));
            LinguisticVariable lvForca = new LinguisticVariable("forca", 5, 90);
            lvForca.AddLabel(cfFraco);
            lvForca.AddLabel(cfMediano);
            lvForca.AddLabel(cfForte);

            #endregion

            #region .: Saidas :.

            // Importância
            FuzzySet cfMuitoBaixa = new FuzzySet("muitoBaixa", new TrapezoidalFunction(1, 2, 3, 4));
            FuzzySet cfBaixa = new FuzzySet("baixa", new TrapezoidalFunction(3, 4, 5, 6));
            FuzzySet cfMedia = new FuzzySet("media", new TrapezoidalFunction(5, 6, 7, 8));
            FuzzySet cfAlta = new FuzzySet("alta", new TrapezoidalFunction(7, 8, 9, 10));
            FuzzySet cfMuitoAlta = new FuzzySet("muitoAlta", new TrapezoidalFunction(9, 10, TrapezoidalFunction.EdgeType.Left));
            LinguisticVariable lvImportancia = new LinguisticVariable("importancia", 0, 11);
            lvImportancia.AddLabel(cfMuitoBaixa);
            lvImportancia.AddLabel(cfBaixa);
            lvImportancia.AddLabel(cfMedia);
            lvImportancia.AddLabel(cfAlta);
            lvImportancia.AddLabel(cfMuitoAlta);
            LinguisticVariable lvImportanciaPontaDireita = new LinguisticVariable("importanciaPontaDireita", 0, 11);
            lvImportanciaPontaDireita.AddLabel(cfMuitoBaixa);
            lvImportanciaPontaDireita.AddLabel(cfBaixa);
            lvImportanciaPontaDireita.AddLabel(cfMedia);
            lvImportanciaPontaDireita.AddLabel(cfAlta);
            lvImportanciaPontaDireita.AddLabel(cfMuitoAlta);
            LinguisticVariable lvImportanciaMeiaDireita = new LinguisticVariable("importanciaMeiaDireita", 0, 11);
            lvImportanciaMeiaDireita.AddLabel(cfMuitoBaixa);
            lvImportanciaMeiaDireita.AddLabel(cfBaixa);
            lvImportanciaMeiaDireita.AddLabel(cfMedia);
            lvImportanciaMeiaDireita.AddLabel(cfAlta);
            lvImportanciaMeiaDireita.AddLabel(cfMuitoAlta);
            LinguisticVariable lvImportanciaCentral = new LinguisticVariable("importanciaCentral", 0, 11);
            lvImportanciaCentral.AddLabel(cfMuitoBaixa);
            lvImportanciaCentral.AddLabel(cfBaixa);
            lvImportanciaCentral.AddLabel(cfMedia);
            lvImportanciaCentral.AddLabel(cfAlta);
            lvImportanciaCentral.AddLabel(cfMuitoAlta);
            LinguisticVariable lvImportanciaMeiaEsquerda = new LinguisticVariable("importanciaMeiaEsquerda", 0, 11);
            lvImportanciaMeiaEsquerda.AddLabel(cfMuitoBaixa);
            lvImportanciaMeiaEsquerda.AddLabel(cfBaixa);
            lvImportanciaMeiaEsquerda.AddLabel(cfMedia);
            lvImportanciaMeiaEsquerda.AddLabel(cfAlta);
            lvImportanciaMeiaEsquerda.AddLabel(cfMuitoAlta);
            LinguisticVariable lvImportanciaPontaEsquerda = new LinguisticVariable("importanciaPontaEsquerda", 0, 11);
            lvImportanciaPontaEsquerda.AddLabel(cfMuitoBaixa);
            lvImportanciaPontaEsquerda.AddLabel(cfBaixa);
            lvImportanciaPontaEsquerda.AddLabel(cfMedia);
            lvImportanciaPontaEsquerda.AddLabel(cfAlta);
            lvImportanciaPontaEsquerda.AddLabel(cfMuitoAlta);
            LinguisticVariable lvImportanciaPivo = new LinguisticVariable("importanciaPivo", 0, 11);
            lvImportanciaPivo.AddLabel(cfMuitoBaixa);
            lvImportanciaPivo.AddLabel(cfBaixa);
            lvImportanciaPivo.AddLabel(cfMedia);
            lvImportanciaPivo.AddLabel(cfAlta);
            lvImportanciaPivo.AddLabel(cfMuitoAlta);

            // Formacao
            FuzzySet cf33 = new FuzzySet("3-3", new TrapezoidalFunction(1, 2, TrapezoidalFunction.EdgeType.Right));
            FuzzySet cf42 = new FuzzySet("4-2", new TrapezoidalFunction(1, 2, 3, 4));
            FuzzySet cf51 = new FuzzySet("5-1", new TrapezoidalFunction(3, 4, 5, 6));
            FuzzySet cf60 = new FuzzySet("6-0", new TrapezoidalFunction(5, 6, 7, 8));
            FuzzySet cf5mais1 = new FuzzySet("5+1", new TrapezoidalFunction(7, 8, 9, 10));
            FuzzySet cf4mais2 = new FuzzySet("4+2", new TrapezoidalFunction(9, 10, TrapezoidalFunction.EdgeType.Left));
            LinguisticVariable lvFormacao = new LinguisticVariable("formacao", 0, 11);
            lvFormacao.AddLabel(cf33);
            lvFormacao.AddLabel(cf42);
            lvFormacao.AddLabel(cf51);
            lvFormacao.AddLabel(cf60);
            lvFormacao.AddLabel(cf5mais1);
            lvFormacao.AddLabel(cf4mais2);

            #endregion

            Database dbIndividual = new Database();
            dbIndividual.AddVariable(lvAltura);
            dbIndividual.AddVariable(lvPeso);
            dbIndividual.AddVariable(lvVelocidade);
            dbIndividual.AddVariable(lvHabilidade);
            dbIndividual.AddVariable(lvForca);
            dbIndividual.AddVariable(lvImportancia);

            Database dbColetivo = new Database();
            dbColetivo.AddVariable(lvImportanciaPontaDireita);
            dbColetivo.AddVariable(lvImportanciaMeiaDireita);
            dbColetivo.AddVariable(lvImportanciaCentral);
            dbColetivo.AddVariable(lvImportanciaMeiaEsquerda);
            dbColetivo.AddVariable(lvImportanciaPontaEsquerda);
            dbColetivo.AddVariable(lvImportanciaPivo);
            dbColetivo.AddVariable(lvFormacao);

            ISAnaliseIndividual = new InferenceSystem(dbIndividual, new CentroidDefuzzifier(1000));
            ISAnaliseColetiva = new InferenceSystem(dbColetivo, new CentroidDefuzzifier(1000));

            #region .: Base de Regras :.

            ISAnaliseIndividual.NewRule("R1", "if altura is alto and forca is forte then importancia is alta");
            ISAnaliseIndividual.NewRule("R2", "if altura is alto and forca is forte and velocidade is rapido and habilidade is bom then importancia is muitoAlta");
            ISAnaliseIndividual.NewRule("R3", "if altura is alto and habilidade is ruim then importancia is media");
            ISAnaliseIndividual.NewRule("R4", "if altura is medio and velocidade is rapido then importancia is alta");
            ISAnaliseIndividual.NewRule("R5", "if altura is medio and forca is mediano and habilidade is bom and velocidade is rapido then importancia is alta");
            ISAnaliseIndividual.NewRule("R6", "if altura is medio and velocidade is rapido then importancia is alta");
            ISAnaliseIndividual.NewRule("R7", "if altura is medio and peso is leve then importancia is media");
            ISAnaliseIndividual.NewRule("R8", "if altura is baixo and velocidade is lento then importancia is baixa");
            ISAnaliseIndividual.NewRule("R9", "if altura is baixo and habilidade is regular and velocidade is rapido then importancia is media");
            ISAnaliseIndividual.NewRule("R11", "if peso is leve and habilidade is bom and velocidade is rapido then importancia is alta");
            ISAnaliseIndividual.NewRule("R12", "if peso is leve and habilidade is regular then importancia is media");
            ISAnaliseIndividual.NewRule("R13", "if peso is emForma and forca is forte and velocidade is rapido then importancia is muitoAlta");
            ISAnaliseIndividual.NewRule("R14", "if peso is pesado and forca is forte then importancia is alta");
            ISAnaliseIndividual.NewRule("R15", "if peso is leve and habilidade is regular and velocidade is comum then importancia is media");
            ISAnaliseIndividual.NewRule("R16", "if peso is pesado and habilidade is regular then importancia is media");
            ISAnaliseIndividual.NewRule("R17", "if peso is emForma and velocidade is lento then importancia is media");
            ISAnaliseIndividual.NewRule("R18", "if peso is emForma and velocidade is lento and habilidade is ruim then importancia is muitoBaixa");
            ISAnaliseIndividual.NewRule("R19", "if velocidade is lento and habilidade is ruim then importancia is baixa");
            ISAnaliseIndividual.NewRule("R20", "if velocidade is comum and habilidade is regular then importancia is media");
            ISAnaliseIndividual.NewRule("R21", "if velocidade is comum and habilidade is ruim then importancia is baixa");
            ISAnaliseIndividual.NewRule("R22", "if velocidade is comum and habilidade is bom then importancia is media");
            ISAnaliseIndividual.NewRule("R23", "if velocidade is rapido and habilidade is regular then importancia is media");
            ISAnaliseIndividual.NewRule("R24", "if velocidade is rapido and habilidade is bom then importancia is alta");
            ISAnaliseIndividual.NewRule("R25", "if velocidade is lento and habilidade is bom then importancia is media");

            ISAnaliseColetiva.NewRule("R1", "if importanciaCentral is alta and importanciaMeiaDireita is alta and importanciaMeiaEsquerda is alta then formacao is 5-1");
            ISAnaliseColetiva.NewRule("R2", "if importanciaPivo is alta or importanciaPivo is muitoAlta then formacao is 6-0");
            ISAnaliseColetiva.NewRule("R3", "if importanciaMeiaDireita is media or importanciaMeiaEsquerda is media then formacao is 6-0");
            ISAnaliseColetiva.NewRule("R4", "if importanciaMeiaDireita is media or importanciaMeiaEsquerda is media then formacao is 6-0");
            ISAnaliseColetiva.NewRule("R5", "if importanciaPontaDireita is alta and importanciaPontaEsquerda is alta then formacao is 6-0");
            ISAnaliseColetiva.NewRule("R6", "if importanciaPontaDireita is muitoAlta and importanciaPontaEsquerda is muitoAlta then formacao is 6-0");
            ISAnaliseColetiva.NewRule("R7", "if importanciaPontaDireita is baixa and importanciaPontaEsquerda is baixa and importanciaPivo is baixa then formacao is 3-3");
            ISAnaliseColetiva.NewRule("R8", "if importanciaPontaDireita is muitoBaixa and importanciaPontaEsquerda is muitoBaixa and importanciaPivo is muitoBaixa then formacao is 3-3");
            ISAnaliseColetiva.NewRule("R9", "if importanciaPivo is media then formacao is 5-1");
            ISAnaliseColetiva.NewRule("R10", "if importanciaCentral is muitoAlta or importanciaMeiaDireita is muitoAlta or importanciaMeiaEsquerda is muitoAlta then formacao is 5+1");
            ISAnaliseColetiva.NewRule("R11", "if importanciaCentral is muitoAlta and importanciaMeiaDireita is muitoAlta then formacao is 4+2");
            ISAnaliseColetiva.NewRule("R12", "if importanciaCentral is muitoAlta and importanciaMeiaEsquerda is muitoAlta then formacao is 4+2");
            ISAnaliseColetiva.NewRule("R13", "if importanciaCentral is muitoAlta or importanciaCentral is alta and importanciaMeiaDireita is muitoBaixa or importanciaMeiaDireita is baixa or importanciaMeiaDireita is media then formacao is 5+1");
            ISAnaliseColetiva.NewRule("R14", "if importanciaCentral is muitoAlta or importanciaCentral is alta and importanciaMeiaEsquerda is muitoBaixa or importanciaMeiaEsquerda is baixa then formacao is 5+1");
            ISAnaliseColetiva.NewRule("R15", "if importanciaCentral is muitoAlta or importanciaCentral is alta and importanciaMeiaDireita is baixa or importanciaMeiaDireita is baixa  then formacao is 5+1");
            ISAnaliseColetiva.NewRule("R16", "if importanciaCentral is muitoAlta or importanciaCentral is alta and importanciaMeiaEsquerda is baixa or importanciaMeiaEsquerda is baixa then formacao is 5+1");
            ISAnaliseColetiva.NewRule("R17", "if importanciaMeiaDireita is muitoAlta and importanciaMeiaEsquerda is muitoAlta then formacao is 4+2");
            ISAnaliseColetiva.NewRule("R18", "if importanciaMeiaDireita is alta or importanciaMeiaDireita is muitoAlta and importanciaMeiaEsquerda is muitoAlta or importanciaMeiaEsquerda is alta then formacao is 4-2");
            ISAnaliseColetiva.NewRule("R19", "if importanciaPivo is media and importanciaPontaEsquerda is media and importanciaPontaDireita is media then formacao is 3-3");
            ISAnaliseColetiva.NewRule("R20", "if importanciaCentral is baixa and importanciaPivo is baixa then formacao is 4-2");

            #endregion
        }

        public void ColocarFormacao(List<PosicaoPictureBox> posicoes, List<Jogador> jogadores)
        {
            pbxJogador1.Location = new Point(posicoes[0].posicaoX, posicoes[0].posicaoY);
            pbxJogador1.Image = Image.FromFile("..//..//imagens//player3.png");
            pbxJogador2.Location = new Point(posicoes[1].posicaoX, posicoes[1].posicaoY);
            pbxJogador2.Image = Image.FromFile("..//..//imagens//player3.png");
            pbxJogador3.Location = new Point(posicoes[2].posicaoX, posicoes[2].posicaoY);
            pbxJogador3.Image = Image.FromFile("..//..//imagens//player3.png");
            pbxJogador4.Location = new Point(posicoes[3].posicaoX, posicoes[3].posicaoY);
            pbxJogador4.Image = Image.FromFile("..//..//imagens//player3.png");
            pbxJogador5.Location = new Point(posicoes[4].posicaoX, posicoes[4].posicaoY);
            pbxJogador5.Image = Image.FromFile("..//..//imagens//player3.png");
            pbxJogador6.Location = new Point(posicoes[5].posicaoX, posicoes[5].posicaoY);
            pbxJogador6.Image = Image.FromFile("..//..//imagens//player3.png");
        }

        private void btnGerarTatica_Click(object sender, EventArgs e)
        {
            // Verificação de quantidade de jogadores exatos
            List<string> posicoes = new List<string>();
            foreach (Jogador jogador in jogadores)
            {
                if (!posicoes.Contains(jogador.posicao))
                    posicoes.Add(jogador.posicao);
            }

            if (posicoes.Count >= 6)
            {
                foreach (var item in jogadores)
                {
                    ISAnaliseIndividual.GetLinguisticVariable("habilidade").NumericInput = item.habilidade;
                    ISAnaliseIndividual.GetLinguisticVariable("velocidade").NumericInput = item.velocidade;
                    ISAnaliseIndividual.GetLinguisticVariable("altura").NumericInput = item.altura;
                    ISAnaliseIndividual.GetLinguisticVariable("forca").NumericInput = item.forca;
                    ISAnaliseIndividual.GetLinguisticVariable("peso").NumericInput = item.peso;

                    var importancia = ISAnaliseIndividual.Evaluate("importancia");

                    switch (item.posicao)
                    {
                        case "Ponta Direita":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaPontaDireita").NumericInput = importancia;
                            break;
                        case "Meia Direita":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaMeiaDireita").NumericInput = importancia;
                            break;
                        case "Central":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaCentral").NumericInput = importancia;
                            break;
                        case "Meia Esquerda":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaMeiaEsquerda").NumericInput = importancia;
                            break;
                        case "Ponta Esquerda":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaPontaEsquerda").NumericInput = importancia;
                            break;
                        case "Pivo":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaPivo").NumericInput = importancia;
                            break;
                    }
                }

                var formacaoFinal = ISAnaliseColetiva.ExecuteInference("formacao");
                Dictionary<string, float> saida = new Dictionary<string, float>();

                foreach (var item in formacaoFinal.OutputList)
                {
                    if (!saida.ContainsKey(item.Label))
                        saida.Add(item.Label, item.FiringStrength);
                    else
                        saida[item.Label] += item.FiringStrength;
                }

                ColocarFormacao(formacao.RetornarFormacao(saida.OrderByDescending(x => x.Value).FirstOrDefault().Key), jogadores);
            }
        }

        private void btnCadastrarJogador_Click(object sender, EventArgs e)
        {
            if (ValidarEntradas())
            {
                Jogador jogador = new Jogador(txtNome.Text,
                                                        String.Empty,
                                                        cmbPosicao.SelectedItem.ToString(),
                                                        float.Parse(txtHabilidade.Text, CultureInfo.InvariantCulture),
                                                        float.Parse(txtVelocidade.Text, CultureInfo.InvariantCulture),
                                                        (float)Convert.ToDouble(txtAltura.Text),
                                                        float.Parse(txtForca.Text, CultureInfo.InvariantCulture),
                                                        float.Parse(txtPeso.Text, CultureInfo.InvariantCulture)
                                                        );

                var jogadorSubstituir = jogadores.Where(x => x.posicao == jogador.posicao).FirstOrDefault();
                if (jogadorSubstituir != null)
                    jogadores.Remove(jogadorSubstituir);

                jogadores.Add(jogador);

                List<string> nomesJogadores = new List<string>();
                foreach (Jogador jogadores in jogadores)
                {
                    nomesJogadores.Add($"{jogadores.nome} - ({jogadores.posicao})");
                }
                lstJogadores.DataSource = nomesJogadores;
                lstJogadores.Refresh();
            }
        }

        private void lstJogadores_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            Jogador auxiliar;

            auxiliar = jogadores[0];
            String nomeBusca = listbox.SelectedItem.ToString();
            nomeBusca = nomeBusca.Split('-')[0].Trim();
            foreach (Jogador jogador in jogadores)
            {
                if (jogador.nome.Equals(nomeBusca))
                {
                    auxiliar = jogador;
                }
            }

            txtNome.Text = auxiliar.nome;
            //txtPosicao.Text = auxiliar.posicao;
            txtHabilidade.Text = auxiliar.habilidade.ToString();
            txtVelocidade.Text = auxiliar.velocidade.ToString();
            txtAltura.Text = auxiliar.altura.ToString();
            txtForca.Text = auxiliar.forca.ToString();
            txtPeso.Text = auxiliar.peso.ToString();

            nomeJogadorParaAlterar = auxiliar.nome;

            btnCadastrarJogador.Text = "Alterar Jogador";
        }

        #region .: Validar :.

        private bool ValidarEntradas()
        {
            bool isValido = true;
            decimal value = 0;

            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Um Nome é necessário");
                isValido = false;
            }
            if (cmbPosicao.SelectedItem == null)
            {
                MessageBox.Show("Uma Posição tem que ser escolhida.");
                isValido = false;
            }
            if (!decimal.TryParse(txtHabilidade.Text, out value))
            {
                MessageBox.Show("Campo Habilidade esta no formato errado.");
                isValido = false;
            }
            if (!decimal.TryParse(txtVelocidade.Text, out value))
            {
                MessageBox.Show("Campo Velocidade esta no formato errado.");
                isValido = false;
            }
            if (!decimal.TryParse(txtAltura.Text, out value))
            {
                MessageBox.Show("Campo Altura esta no formato errado.");
                isValido = false;
            }
            if (!decimal.TryParse(txtForca.Text, out value))
            {
                MessageBox.Show("Campo Força esta no formato errado.");
                isValido = false;
            }
            if (!decimal.TryParse(txtPeso.Text, out value))
            {
                MessageBox.Show("Campo Peso esta no formato errado.");
                isValido = false;
            }

            return isValido;
        }
        #endregion

        #region .: Validar Entradas Numericas :.

        private void txtHabilidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNumerico(sender, e);
        }

        private void txtVelocidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNumerico(sender, e);
        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNumerico(sender, e);
        }

        private void txtForca_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNumerico(sender, e);
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNumerico(sender, e);
        }

        private void ValidarNumerico(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        #endregion

    }
}