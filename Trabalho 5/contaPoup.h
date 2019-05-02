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

class contaPoup : public Conta {
private:
	float rendimento;
	float juros;

public:

	contaPoup();
	contaPoup(int, string, double, int, float);
	~contaPoup();

	void setJuros(float);
	float getJuros();
	float getRendimentos(int);


};