/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

using System;

namespace Supervisorio
{
    public class Vazao
    {
        // Construtor
        public Vazao(double v0)
        {
            vazao = v0;
        }

        // Propriedades da Classe
        public double a { get; set; }
        public double b { get; set; }
        public double vazao { get; set; }
        public int contador { get; set; }

        // Método da Classe
        public void calculaVazao()
        {
            if (this.contador == 0)
            {
                Random rnd1 = new Random();
                a = rnd1.Next(-1, 1) + (double)0.1 * (rnd1.Next(1, 10)) + (double)0.01 * (rnd1.Next(1, 10));
                b = this.vazao;
            }
            this.vazao = ((a * contador + b) > 0) ? Math.Round((a * contador + b), 2) : Math.Round(-(a * contador + b), 2);
        }
    }
}
