/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL

SISTEMA BANCARIO DE AUTO ATENDIMENTO
*/

#pragma once
// DIRETIVA DE COMPILAÇÃO
#include <string>
#include<iostream>
#include"stdafx.h"
#include "funcoes.h"

using namespace std;

class Conta{
private:
	int senha;
	double saldo;


public:
	int conta;
	string titular;

	Conta();
	Conta(int, string, double, int);
	~Conta();
	
	int getSenha();

	double getSaldo();
	
	void setSenha(int);
	void exibeDados(int);
	void deposito(double);
	void setSaldo(double);
	void saque(double, int);
	
	bool habilitaAcesso(int);
};

void funcionario(Conta**, int const);
void cliente(Conta**, int const);