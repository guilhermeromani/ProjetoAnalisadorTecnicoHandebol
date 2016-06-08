using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAnalisadorTecnicoHandbol
{
    public class Jogador
    {       
        public string nome { get; set; }
        public string imagem { get; set; }
        public string posicao { get; set; }
        public float habilidade { get; set; }
        public float velocidade { get; set; }
        public float altura { get; set; }
        public float forca { get; set; }
        public float peso { get; set; }

        public Jogador(string nome, string imagem, string posicao, float habilidade, float velocidade, float altura, float forca, float peso)
        {
            this.nome = nome;
            this.imagem = imagem;
            this.posicao = posicao;
            this.altura = altura;
            this.habilidade = habilidade;
            this.peso = peso;
            this.forca = forca;
            this.velocidade = velocidade;
        }
    }
}
