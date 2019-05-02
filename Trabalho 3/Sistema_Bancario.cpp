/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL

SISTEMA BANCARIO DE AUTO ATENDIMENTO
*/

// DIRETIVA DE COMPILAÇÃO
#include "stdafx.h"
#include <iostream>
#include <string>
#include "funcoes.h"

using namespace std;

void main(){

	// Inicializa��o das variav�is de comando:
	int flag = 1, entrada = 0;

	// Inicializa��o da variav�l respons�vel pelo n�mero de contas:
	int const numContas = 10;

	// Inicializa��o das variav�is de registro:
	// Vetor de contas disponiveis no sistema:
	Conta contaNew[numContas];
	// Variavel  responsavel pela conta do Funcionario:
	Conta Conta01(0, "Funcionario", 0, 158065);
	// Vetor de ponteiros respons�vel por armazenar os endere�os das variaveis de registro:
	Conta *ptrConta[numContas] = { &Conta01 };

	// Loop respons�vel por preencher o vetor de ponteiros com os endere�os e par�metros das contas:
	for (int i = 1; i < numContas; i++){
		ptrConta[i] = &contaNew[i];
		ptrConta[i]->conta = i;
		ptrConta[i]->titular = "Disponivel";
		ptrConta[i]->setSenha(0);
		ptrConta[i]->setSaldo(0);
	}

	// Interface com o us�ario:
	while (flag){
		cout << "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ " << endl;
		cout << "\n\rBem-vindo ao sistema de atendimento!\r\n" << endl;
		cout << "Digite:\n\r\t1 - Funcionario\n\r\t2 - Cliente\n\r\t3 - Sair" << endl;
		cout << "Qual operacao deseja fazer? ";
		cin >> entrada;

		// Sistema de sele��o para o Menu:
		switch (entrada){
		case 1:
			// Fun��o respons�vel pelo cadastro de novas contas:
			funcionario(ptrConta, numContas);
			break;
		case 2:
			// Fun��o direcionada para os usu�rios:
			cliente(ptrConta, numContas);
			break;
		case 3:
			// Saida do sistema:
			cout << endl;
			cout << "Ate breve!" << endl;
			cout << endl;
			flag = 0;
			break;
		default:
			// Caso algum comando invalido seja teclado retorna ao Menu:
			if (entrada != (1, 2, 3)){
				cout << "Operacao Invalida" << endl;
			}
			break;
		}
	}
	system("pause");
}
