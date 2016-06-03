using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAnalisadorTecnicoHandbol
{
    public class PosicaoPictureBox
    {
        public int posicaoX { get; }
        public int posicaoY { get; }

        public PosicaoPictureBox(int X, int Y)
        {
            this.posicaoX = X;
            this.posicaoY = Y;
        }
    }
}
