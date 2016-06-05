using AForge.Fuzzy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            pbxJogador1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador2.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador3.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador4.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador5.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxJogador6.SizeMode = PictureBoxSizeMode.StretchImage;

            //jogadores.Add(new Jogador("Player1", "playerhandbol.jpg", "pivo", 10,10,10,10,10));
            //jogadores.Add(new Jogador("Player2", "player2.png", "pivo", 10, 10, 10, 10, 10));
            //jogadores.Add(new Jogador("Player3", "playerhandbol.jpg", "pivo", 10, 10, 10, 10, 10));
            //jogadores.Add(new Jogador("Player4", "player2.png", "pivo", 10, 10, 10, 10, 10));
            //jogadores.Add(new Jogador("Player5", "playerhandbol.jpg", "pivo", 10, 10, 10, 10, 10));
            //jogadores.Add(new Jogador("Player6", "player2.png", "pivo", 10, 10, 10, 10, 10));

            #region .: Entradas :.

            //// Posicao
            //FuzzySet cfPontaDireita = new FuzzySet("pontaDireita", new TrapezoidalFunction(1, 1, 1));
            //FuzzySet cfMeiaDireita = new FuzzySet("meiaDireita", new TrapezoidalFunction(2, 2, 2, 2));
            //FuzzySet cfCentral = new FuzzySet("central", new TrapezoidalFunction(3, 3, 3));
            //FuzzySet cfMeiaEsquerda = new FuzzySet("meiaEsquerda", new TrapezoidalFunction(4, 4, 4));
            //FuzzySet cfPontaEsquerda = new FuzzySet("pontaEsquerda", new TrapezoidalFunction(5, 5, 5));
            //FuzzySet cfPivo = new FuzzySet("pivo", new TrapezoidalFunction(6, 6, 6));
            //LinguisticVariable lvPosicao = new LinguisticVariable("posicao", 1, 6);
            //lvPosicao.AddLabel(cfPontaDireita);
            //lvPosicao.AddLabel(cfMeiaDireita);
            //lvPosicao.AddLabel(cfCentral);
            //lvPosicao.AddLabel(cfMeiaEsquerda);
            //lvPosicao.AddLabel(cfPontaEsquerda);
            //lvPosicao.AddLabel(cfPivo);

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

            //ISAnaliseIndividual.NewRule("R10", "if altura is alto and forca is forte then importancia is 5-1");
            //ISAnaliseIndividual.NewRule("R11", "if altura is medio and velocidade is rapido then importancia is 6-0");
            //ISAnaliseIndividual.NewRule("R12", "if altura is medio and velocidade is rapido then importancia is 6-0");

            //ISAnaliseColetiva.NewRule("R12", "if posicao is meiaDireita and altura is medio and velocidade is rapido then formacao is 6-0");

            #endregion
        }

        public void ColocarFormacao(List<PosicaoPictureBox> posicoes, List<Jogador> jogadores)
        {
            pbxJogador1.Location = new Point(posicoes[0].posicaoX, posicoes[0].posicaoY);
            pbxJogador1.Image = Image.FromFile("..//..//imagens//playerhandbol.jpg");
            pbxJogador2.Location = new Point(posicoes[1].posicaoX, posicoes[1].posicaoY);
            pbxJogador2.Image = Image.FromFile("..//..//imagens//playerhandbol.jpg");
            pbxJogador3.Location = new Point(posicoes[2].posicaoX, posicoes[2].posicaoY);
            pbxJogador3.Image = Image.FromFile("..//..//imagens//playerhandbol.jpg");
            pbxJogador4.Location = new Point(posicoes[3].posicaoX, posicoes[3].posicaoY);
            pbxJogador4.Image = Image.FromFile("..//..//imagens//playerhandbol.jpg");
            pbxJogador5.Location = new Point(posicoes[4].posicaoX, posicoes[4].posicaoY);
            pbxJogador5.Image = Image.FromFile("..//..//imagens//playerhandbol.jpg");
            pbxJogador6.Location = new Point(posicoes[5].posicaoX, posicoes[5].posicaoY);
            pbxJogador6.Image = Image.FromFile("..//..//imagens//playerhandbol.jpg");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColocarFormacao(formacao.formacao6x0, jogadores);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColocarFormacao(formacao.formacao5x1, jogadores);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColocarFormacao(formacao.formacao4x2, jogadores);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColocarFormacao(formacao.formacao3x3, jogadores);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColocarFormacao(formacao.formacao5plus1, jogadores);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ColocarFormacao(formacao.formacao4plus2, jogadores);
        }

        private void btnGerarTatica_Click(object sender, EventArgs e)
        {
            // Verificação de quantidade de jogadores exatos
            List<string> posicoes = new List<string>();
            foreach (Jogador jogador in jogadores)
            {
                if(!posicoes.Contains(jogador.posicao))
                    posicoes.Add(jogador.posicao);
            }

            if(posicoes.Count >= 6)
            {
                foreach (var item in jogadores)
                {
                    ISAnaliseIndividual.GetLinguisticVariable("habilidade").NumericInput = Convert.ToInt32("x1");
                    ISAnaliseIndividual.GetLinguisticVariable("velocidade").NumericInput = Convert.ToInt32("x2");
                    ISAnaliseIndividual.GetLinguisticVariable("altura").NumericInput = Convert.ToInt32("x3");
                    ISAnaliseIndividual.GetLinguisticVariable("forca").NumericInput = Convert.ToInt32("x4");
                    ISAnaliseIndividual.GetLinguisticVariable("peso").NumericInput = Convert.ToInt32("x5");

                    var importancia = ISAnaliseIndividual.Evaluate("importancia");

                    switch (item.posicao)
                    {
                        case "pontaDireita":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaPontaDireita").NumericInput = importancia;
                            break;
                        case "meiaDireita":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaMeiaDireita").NumericInput = importancia;
                            break;
                        case "central":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaCentral").NumericInput = importancia;
                            break;
                        case "meiaEsquerda":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaMeiaEsquerda").NumericInput = importancia;
                            break;
                        case "pontaEsquerda":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaPontaEsquerda").NumericInput = importancia;
                            break;
                        case "pivo":
                            ISAnaliseColetiva.GetLinguisticVariable("importanciaPivo").NumericInput = importancia;
                            break;
                    }
                }

                //var formacao = ISAnaliseColetiva.ExecuteInference("formacao");
                //formacao.OutputVariable.Name
            }
        }

        private void btnCadastrarJogador_Click(object sender, EventArgs e)
        {

            Jogador jogador = new Jogador(txtNome.Text,
                                                    String.Empty,
                                                    txtPosicao.Text,
                                                    Convert.ToInt32(txtHabilidade.Text),
                                                    Convert.ToInt32(txtVelocidade.Text),
                                                    Convert.ToInt32(txtAltura.Text),
                                                    Convert.ToInt32(txtForca.Text),
                                                    Convert.ToInt32(txtPeso.Text)
                                                    );
            if (btnCadastrarJogador.Text.Equals("Cadastrar Jogador"))
                jogadores.Add(jogador);
            else
            {
                for (int i = 0; i < jogadores.Count; i++)
                {
                    if (jogadores[i].nome.Equals(nomeJogadorParaAlterar))
                    {
                        jogadores[i] = jogador;
                    }
                }
                btnCadastrarJogador.Text = "Cadastrar Jogador";
            }

            List<string> nomesJogadores = new List<string>();
            foreach (Jogador jogadores in jogadores)
            {
                nomesJogadores.Add(jogadores.nome);
            }
            lstJogadores.DataSource = nomesJogadores;
            lstJogadores.Refresh();
        }

        private void lstJogadores_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            Jogador auxiliar;

            auxiliar = jogadores[0];
            String nomeBusca = listbox.SelectedItem.ToString();
            foreach (Jogador jogador in jogadores)
            {
                if (jogador.nome.Equals(nomeBusca))
                {
                    auxiliar = jogador;
                }
            }

            txtNome.Text = auxiliar.nome;
            txtPosicao.Text = auxiliar.posicao;
            txtHabilidade.Text = auxiliar.habilidade.ToString();
            txtVelocidade.Text = auxiliar.velocidade.ToString();
            txtAltura.Text = auxiliar.altura.ToString();
            txtForca.Text = auxiliar.forca.ToString();
            txtPeso.Text = auxiliar.peso.ToString();

            nomeJogadorParaAlterar = auxiliar.nome;

            btnCadastrarJogador.Text = "Alterar Jogador";
        }
    }
}