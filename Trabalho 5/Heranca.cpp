/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORM.TICA INDUSTRIAL

SISTEMA BANCARIO - CONTA POUPAN.A/CORRENTE
*/

// DIRETIVA DE COMPILA..O
#include <iostream>
#include <string>
#include "conta.h"
#include "contaPoup.h"
#include "contaCorrente.h"


using namespace std;

void main() {
	// Conta Poupan.a
	contaPoup Conta1(1, "Lucas", 50, 123, 0.5);
	
	// Conta Corrente
	contaCorrente Conta2(2, "Gustavo", 50, 123, 112233);

	// Interface com o usuario
	cout << "Cliente " << Conta2.titular << ", saldo incial R$" << Conta2.getSaldo() << ", num Card " << Conta2.getCard() << endl;
	cout << endl;

	// Modifica o n.mero do cart.o
	Conta2.setCard(332211);

	// Interface com o usuario
	cout << "Cliente " << Conta2.titular << ", saldo incial R$" << Conta2.getSaldo() << ", num Card " << Conta2.getCard() << endl;
	cout << endl;

	// Interface com o usuario
	cout << "Cliente "<< Conta1.titular <<", saldo incial R$"<< Conta1.getSaldo() <<", taxa de juros "<< 100*Conta1.getJuros() <<"%"<< endl;
	cout << "Rendimento em 12 meses: " << Conta1.getRendimentos(12) << endl;
	cout << endl;
	
	// Modifica a taxa de juros
	Conta1.setJuros(0.3);
	
	// Interface com o usuario
	cout << "Cliente " << Conta1.titular << ", saldo incial R$" << Conta1.getSaldo() << ", taxa de juros " << 100 * Conta1.getJuros() << "%" << endl;
	cout << "Rendimento em 6 meses: "<< Conta1.getRendimentos(6) << endl;
	cout << endl;

	system("pause");
}