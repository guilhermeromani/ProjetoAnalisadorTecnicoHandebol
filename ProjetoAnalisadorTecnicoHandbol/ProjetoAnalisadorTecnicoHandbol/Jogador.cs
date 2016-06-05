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
        public decimal habilidade { get; set; }
        public decimal velocidade { get; set; }
        public decimal altura { get; set; }
        public decimal forca { get; set; }
        public decimal peso { get; set; }

        public Jogador(string nome, string imagem, string posicao, decimal habilidade, decimal velocidade, decimal altura, decimal forca, decimal peso)
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
