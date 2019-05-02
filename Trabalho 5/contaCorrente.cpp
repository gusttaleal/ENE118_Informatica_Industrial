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
#include "contaCorrente.h"

using namespace std;

contaCorrente::contaCorrente() {

}

contaCorrente::contaCorrente(int contaAtual, string titular, double saldo, int senha, int numCard) : Conta(contaAtual, titular, saldo, senha){
	this->numCard = numCard;
}

contaCorrente::~contaCorrente() {

}

// Fun..o para pegar o n.mero do cart.o atual
int contaCorrente::getCard() {
	return this->numCard;
}

// Fun..o para setar o n.mero do cart.o
void contaCorrente::setCard(int numCard) {
	this->numCard = numCard;
}