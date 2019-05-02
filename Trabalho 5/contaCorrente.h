/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORM.TICA INDUSTRIAL

SISTEMA BANCARIO - CONTA POUPAN.A/CORRENTE
*/

#pragma once
// DIRETIVA DE COMPILA..O
#include <string>
#include<iostream>
#include "conta.h"

using namespace std;

class contaCorrente : public Conta {
private:
	int numCard;

public:
	contaCorrente();
	contaCorrente(int, string, double, int, int);
	~contaCorrente();

	int getCard();
	void setCard(int);
};