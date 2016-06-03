using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAnalisadorTecnicoHandbol
{
    public class Formacao
    {
        public List<PosicaoPictureBox> formacao6x0;
        public List<PosicaoPictureBox> formacao5x1;
        public List<PosicaoPictureBox> formacao4x2;
        public List<PosicaoPictureBox> formacao3x3;
        public List<PosicaoPictureBox> formacao5plus1;
        public List<PosicaoPictureBox> formacao4plus2;

        public Formacao()
        {
            formacao6x0 = new List<PosicaoPictureBox>();
            formacao5x1 = new List<PosicaoPictureBox>();
            formacao4x2 = new List<PosicaoPictureBox>();
            formacao3x3 = new List<PosicaoPictureBox>();
            formacao5plus1 = new List<PosicaoPictureBox>();
            formacao4plus2 = new List<PosicaoPictureBox>();

            //Formação6-0
            formacao6x0.Add(new PosicaoPictureBox(383, 445));
            formacao6x0.Add(new PosicaoPictureBox(490, 369));
            formacao6x0.Add(new PosicaoPictureBox(614, 337));
            formacao6x0.Add(new PosicaoPictureBox(722, 337));
            formacao6x0.Add(new PosicaoPictureBox(832, 369));
            formacao6x0.Add(new PosicaoPictureBox(940, 445));

            //Formação5-1
            formacao5x1.Add(new PosicaoPictureBox(383, 445));
            formacao5x1.Add(new PosicaoPictureBox(526, 357));
            formacao5x1.Add(new PosicaoPictureBox(659, 202));
            formacao5x1.Add(new PosicaoPictureBox(659, 338));
            formacao5x1.Add(new PosicaoPictureBox(809, 367));
            formacao5x1.Add(new PosicaoPictureBox(940, 445));

            //Formação4-2
            formacao4x2.Add(new PosicaoPictureBox(451, 413));
            formacao4x2.Add(new PosicaoPictureBox(577, 354));
            formacao4x2.Add(new PosicaoPictureBox(606, 227));
            formacao4x2.Add(new PosicaoPictureBox(728, 188));
            formacao4x2.Add(new PosicaoPictureBox(746, 354));
            formacao4x2.Add(new PosicaoPictureBox(876, 413));

            //Formação3-3
            formacao3x3.Add(new PosicaoPictureBox(492, 369));
            formacao3x3.Add(new PosicaoPictureBox(459, 198));
            formacao3x3.Add(new PosicaoPictureBox(683, 150));
            formacao3x3.Add(new PosicaoPictureBox(683, 349));
            formacao3x3.Add(new PosicaoPictureBox(880, 198));
            formacao3x3.Add(new PosicaoPictureBox(853, 369));

            //Formação5plus1
            formacao5plus1.Add(new PosicaoPictureBox(383, 445));
            formacao5plus1.Add(new PosicaoPictureBox(526, 357));
            formacao5plus1.Add(new PosicaoPictureBox(614, 96));
            formacao5plus1.Add(new PosicaoPictureBox(659, 338));
            formacao5plus1.Add(new PosicaoPictureBox(809, 367));
            formacao5plus1.Add(new PosicaoPictureBox(940, 445));

            //Formação5plus1
            formacao4plus2.Add(new PosicaoPictureBox(451, 413));
            formacao4plus2.Add(new PosicaoPictureBox(577, 354));
            formacao4plus2.Add(new PosicaoPictureBox(613, 106));
            formacao4plus2.Add(new PosicaoPictureBox(758, 91));
            formacao4plus2.Add(new PosicaoPictureBox(746, 354));
            formacao4plus2.Add(new PosicaoPictureBox(876, 413));

        }
        //ExibirFormacao
    }
}


