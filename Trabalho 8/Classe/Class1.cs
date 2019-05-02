/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

using System;

namespace CLASSES
{
    [Serializable]
    public class Carro
    {
        public string ID = "Carro";
        public string iten1 = "Modelo";
        public string iten2 = "Ano";
        public string iten3 = "Valor";

        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Valor { get; set; }

        public Carro(string M, int A, double V)
        {
            this.Modelo = M;
            this.Ano = A;
            this.Valor = V;
        }
    }

    [Serializable]
    public class Pessoa
    {
        public string ID = "Pessoa";
        public string iten1 = "Nome";
        public string iten2 = "Sexo";
        public string iten3 = "CPF";

        public string Nome { get; set; }
        public string Sexo { get; set; }
        public int CPF { get; set; }

        public Pessoa(string N, string S, int C)
        {
            this.Nome = N;
            this.Sexo = S;
            this.CPF = C;
        }
    }

    [Serializable]
    public class Conta_Bancaria
    {
        public string ID = "Conta_Bancaria";
        public string iten1 = "Titular";
        public string iten2 = "Agencia";
        public string iten3 = "Conta";

        public string Titular { get; set; }
        public int Agencia { get; set; }
        public int Conta { get; set; }

        public Conta_Bancaria(string T, int A, int C)
        {
            this.Titular = T;
            this.Agencia = A;
            this.Conta = C;
        }
    }
}
