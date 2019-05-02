/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORM.TICA INDUSTRIAL

SISTEMA BANCARIO - CONTA POUPAN.A/CORRENTE
*/

// DIRETIVA DE COMPILA..O
#include <string>
#include<iostream>
#include <stdlib.h>
#include "conta.h"
#include "contaPoup.h"


using namespace std;

contaPoup::contaPoup() {

}

contaPoup::contaPoup(int contaAtual, string titular, double saldo, int senha, float juros) : Conta(contaAtual, titular, saldo, senha) {
	this->juros = juros;
}

contaPoup::~contaPoup() {

}

// Fun..o para calcular o rendimento da conta
float contaPoup::getRendimentos(int meses) {
	
	rendimento = meses * juros*getSaldo();
	return this->rendimento;
}

// Fun..o para pegar a taxa de juros atual
float contaPoup::getJuros() {
	return this->juros;
}

// Fun..o para setar a taxa de juros
void contaPoup::setJuros(float juros) {
	if(juros > 0){
	this->juros = juros;
	}
	else {
		cout << "Juros invalidos" << endl;
	}
}
