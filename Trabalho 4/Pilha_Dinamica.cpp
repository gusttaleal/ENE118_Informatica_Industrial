/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORM.TICA INDUSTRIAL
*/

#include "myfunc.h"
#include <iostream>

using namespace std;

void main() {
	// Vari.veis de Controle:
	int opr1 = 0, opr2 = 0, opr3 = 0;


	// Vari.veis de Manipula..o:
	Pilha<char> pilhaChar;
	char valorChar = ' ';

	Pilha<int> pilhaInt;
	int valorInt = 0;

	Pilha<double> pilhaDouble;
	double valorDouble = 0;

	// Loop infinito para aloca..o
	while (1) {
		// Vari.vel de controle para o Menu
		bool flag = true;

		// Estrutura de saida informativa:
		cout << " - Menu - \n\r\t1 - Alocar Dados\n\r\t2 - Desalocar Dados" << endl;
		cout << "\n\rQual operacao deseja realizar: ";
		cin >> opr1;
		
		cout << endl;

		// Loop do Menu:
		while (flag) {
			// Switch para Aloca..o ou Desaloca..o
			switch (opr1) {
				// Caso 1 Aloca..o
			case 1:
				// Estrutura de saida informativa:
				cout << " - Menu - \n\r\t1 - Char\n\r\t2 - Int\n\r\t3 - Double" << endl;
				cout << "\n\rTipo de variavel para alocacao: ";
				cin >> opr2;
				
				// Switch para alocar 3 tipos de variaveis diferentes: CHAR, INT, DOUBLE
				switch (opr2) {
					// Caso para CHAR
				case 1:
					cout << "Digite o dado a ser armazenado: ";
					cin >> valorChar;
					
					cout << endl;
					pilhaChar.insere(valorChar);
					break;
					// Caso para INT
				case 2:
					cout << "Digite o dado a ser armazenado: ";
					cin >> valorInt;
					
					cout << endl;
					pilhaInt.insere(valorInt);
					break;
					// Caso para DOUBLE
				case 3:
					cout << "Digite o dado a ser armazenado: ";
					cin >> valorDouble;
					
					cout << endl;
					pilhaDouble.insere(valorDouble);
					break;
					// Op..o Inv.lida
				default:
					cout << "Opcao invalida" << endl;
					break;
				}
				break;
				// Caso 1 Desaloca..o
			case 2:
				// Estrutura de saida informativa:
				cout << " - Menu - \n\r\t1 - Char\n\r\t2 - Int\n\r\t3 - Double" << endl;
				cout << "\n\rQual tipo de pilha deseja desalocar? ";
				cin >> opr3;
				
				// Switch para desalocar 3 tipos de variaveis diferentes: CHAR, INT, DOUBLE
				switch (opr3) {
					// Caso para CHAR
				case 1:
					pilhaChar.remove();
					break;
					// Caso para INT
				case 2:
					pilhaInt.remove();
					break;
					// Caso para DOUBLE
				case 3:
					pilhaDouble.remove();
					break;
					// Op..o Inv.lida
				default:
					cout << "Opcao invalida" << endl;
					break;
				}
				break;
				// Op..o Inv.lida
			default:
				cout << "Opcao invalida" << endl;
				break;
			}
			// Condi..o de retorno para o Menu
			flag = false;
		}
		// Pausa e limpa a tela
		system("pause");
		system("cls");
	}
}
