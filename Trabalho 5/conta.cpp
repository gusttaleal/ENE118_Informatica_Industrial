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

using namespace std;

Conta::Conta() {

}

Conta::Conta(int contaAtual, string titular, double saldo, int senha) {

	this->conta = contaAtual;
	this->titular = titular;
	this->saldo = saldo;
	this->senha = senha;
}

Conta::~Conta() {

}

void Conta::exibeDados(int senha) {
	if (senha == this->senha) {
		cout << "Titular: " << titular << endl;
		cout << "Numero da Conta: " << conta << endl;
		cout << "Saldo: " << saldo << endl;
		cout << "Senha: " << senha << endl;
		cout << endl;
	}
}

void Conta::saque(double valor, int senha) {
	if (senha == this->senha) {
		if (valor <= saldo) {
			saldo -= valor;
		}
		else {
			cout << "Saldo Insuficiente" << endl;
		}
	}
	else {
		cout << "Senha Inv.lida" << endl;
	}
}

void Conta::deposito(double valor) {
	if (valor > 0) {
		saldo += valor;
	}
	else {
		cout << "Valor Inv.lido" << endl;
	}
}

double Conta::getSaldo() {
	return this->saldo;
}

int Conta::getSenha() {
	return this->senha;
}

void Conta::setSaldo(double saldo) {
	this->saldo = saldo;
}

void Conta::setSenha(int senha) {
	this->senha = senha;
}

bool Conta::habilitaAcesso(int senha) {
	if (this->senha == senha) {
		//cout << "Acesso habilitdo" << endl;
		return true;
	}
	else {
		cout << "Acesso Negado" << endl;
		return false;
	}
}

// Fun..o para cadastramento de cliente somente acess.vel via "Funcion.rio" (us.ario):
void funcionario(Conta **ptrContaNew, int const numContas) {
	// Inicializa..o das vari.veis utilizadas para setar uma nova conta:
	double saldo = 0;
	int senha = 0, contaAtual = 0;
	//int numCard = 0;
	string titular = " ";

	// Estrutura ant erros -  Caso as informa..es predefinidas forem referentes a filiais do Banco em lugares diferentes por exemplo, e possuirem par.metros de identifica..o inicial pr.prios:
	int contaRescue = 0;
	string titularRescue = " ";
	double saldoRescue = 0;
	int senhaRescue = 0;
	int numCardRescue = 0;

	// Inicializa..o das variav.is de comando:
	int flag1 = 0, flag2 = 1, flag3 = 0;

	// Inicio do acesso do us.ario:
	cout << "Digite sua senha: ";
	cin >> senha;

	// Autentica..o da senha inserida via fun..o habilitaAcesso():
	if (ptrContaNew[0]->habilitaAcesso(senha)) {

		// Interface com o us.ario:
		cout << endl;
		cout << "Seja bem-vindo " << ptrContaNew[0]->titular << endl;
		cout << "\n\rMenu:\t1 - Cadastro\n\r\t2 - Sair" << endl;
		cout << "Qual operacao deseja fazer? ";
		cin >> flag1;

		// Cadastramento:
		if (flag1 == 1) {

			// Interface com o us.ario:
			// Lista de registros do Banco
			while (flag2) {

				// Interface com o us.ario:
				cout << endl;
				cout << "Lista de Registros" << endl;
				for (int i = 0; i < numContas; i++) {
					if (ptrContaNew[i]->titular == "Disponivel") {
						cout << "Conta: " << i << " - > " << ptrContaNew[i]->titular << endl;
					}
					else {
						cout << "Conta: " << i << " - > " << ptrContaNew[i]->titular << " - Indisponivel" << endl;
					}
				}

				// Interface com o us.ario:
				// Parametros da nova conta:
				cout << "\r\nMUITA ATENCAO!" << endl;
				cout << "\r\nEntre com um numero de Conta DISPONIVEL: ";
				cin >> contaAtual;

				// Analise do numero de conta:
				if (ptrContaNew[contaAtual]->titular != "Disponivel") {
					cout << "Conta Indisponivel" << endl;
				}
				else {
					// Utiliza..o da estrutura ant erros:
					contaRescue = ptrContaNew[contaAtual]->conta;
					titularRescue = ptrContaNew[contaAtual]->titular;
					saldoRescue = ptrContaNew[contaAtual]->getSaldo();
					senhaRescue = ptrContaNew[contaAtual]->getSenha();
					//numCardRescue = ptrContaNew[contaAtual]->getCard();

					// cout << contaRescue << "\t" << titularRescue << "\t" << saldoRescue << "\t" << senhaRescue << endl;

					// Entrada de par.metros do registro:
					cout << "Entre com o nome do Titular: ";
					cin >> titular;
					cout << "Entre com a Senha: ";
					cin >> senha;
					cout << "Entre com o saldo inicial: ";
					cin >> saldo;
					//cout << "Entre com o numero do cartao: ";
					//cin >> numCard;
					cout << endl;

					// Passagem dos par.metros
					ptrContaNew[contaAtual]->conta = contaAtual;
					ptrContaNew[contaAtual]->titular = titular;
					ptrContaNew[contaAtual]->setSenha(senha);
					ptrContaNew[contaAtual]->setSaldo(saldo);
					//ptrContaNew[contaAtual]->setCard(numCard);

					// Interface com o us.ario:
					// Confirma..o dos dados da nova conta:
					ptrContaNew[contaAtual]->exibeDados(senha);
					cout << "1 - Confirmar\t2 - Recadastrar" << endl;
					cout << "Qual operacao deseja fazer? ";
					cin >> flag3;
					if (flag3 == 1) {
						flag2 = 0;
					}
					else {
						// Redefini..o dos par.metros
						ptrContaNew[contaAtual]->conta = contaRescue;
						ptrContaNew[contaAtual]->titular = titularRescue;
						ptrContaNew[contaAtual]->setSaldo(saldoRescue);
						ptrContaNew[contaAtual]->setSenha(senhaRescue);
						//ptrContaNew[contaAtual]->setCard(numCardRescue);

						cout << "Redefinicao Concluida!" << endl;
						cout << endl;
						flag2 = 1;
					}
				}
			}
			// Interface com o us.ario:
			// Atualiza..o dos registros:
			cout << endl;
			cout << "Lista de Registros Atualizada" << endl;
			for (int i = 0; i < numContas; i++) {
				if (ptrContaNew[i]->titular == "Disponivel") {
					cout << "Conta: " << i << " - > " << ptrContaNew[i]->titular << endl;
				}
				else {
					cout << "Conta: " << i << " - > " << ptrContaNew[i]->titular << " - Indisponivel" << endl;
				}
			}
		}
		cout << endl;
		system("pause");
		system("cls");
	}


}

// Fun..o somente acess.vel "Clientes" (us.arios):
void cliente(Conta **ptrConta, int const numContas) {
		// Inicializa..o das vari.veis utilizadas para setar uma nova conta:
		int contaAtual = 0, ContaDestino = 0, senha = 0;
		//int numCard = 0;
		double saldo = 0;

		// Inicializa..o das variav.is de comando:
		int entrada = 0, flag1 = 3;
		bool contaInvalida = true, atendimento = true, principal = true;

		// Identifica..o do Cliente:
		cout << "Digite o numero da sua conta: ";
		cin >> contaAtual;
		cout << "Digite sua senha: ";
		cin >> senha;
		while (principal) {

		// Acesso a conta do cliente via autentica..o:
		if (ptrConta[contaAtual]->habilitaAcesso(senha)) {
			system("cls");
			// Acesso ao Menu
			while (atendimento) {
				cout << "\n\rOla " << ptrConta[contaAtual]->titular << ", suas opcoes sao: " << endl;
				cout << "\n\rMenu:\t1 - Saque\n\r\t2 - Deposito\n\r\t3 - Transferencia\n\r\t4 - Ver Saldo\n\r\t5 - Sair" << endl;
				cout << "Qual operacao deseja fazer? ";
				cin >> entrada;
				// Acesso ao menu do Sistema:
				switch (entrada) {
				case 1:
					// Fun..o Saque:
					cout << "Digite o valor: ";
					cin >> saldo;
					ptrConta[contaAtual]->saque(saldo, senha);
					cout << "Saldo atual: R$" << ptrConta[contaAtual]->getSaldo() << endl;
					break;
				case 2:
					// Fun..o Deposito:
					cout << "Digite o valor: ";
					cin >> saldo;
					ptrConta[contaAtual]->deposito(saldo);
					cout << "Saldo atual: R$" << ptrConta[contaAtual]->getSaldo() << endl;
					break;
				case 3:
					// Fun..o Transfer.ncia:
					cout << "Digite o numero da conta destino: ";
					cin >> ContaDestino;
					// Verifica se a conta destino digitada existe no sistema:
					for (int i = 0; i < numContas; ++i) {
						if (ContaDestino == ptrConta[i]->conta) {
							ContaDestino = i;
							contaInvalida = false;
						}
					}
					// Caso n.o exista:
					if (contaInvalida) {
						cout << "Conta Invalida" << endl;
						break;
					}
					// Caso exista:
					else {
						cout << "Digite o valor: ";
						cin >> saldo;
						ptrConta[contaAtual]->saque(saldo, senha);
						cout << "Saldo atual: R$" << ptrConta[contaAtual]->getSaldo() << endl;
						ptrConta[ContaDestino]->deposito(saldo);
					}
					break;
				case 4:
					// Fun..o Saldo
					cout << "Saldo: R$" << ptrConta[contaAtual]->getSaldo() << endl;
					break;
				case 5:
					// Saida da area de trabalho do cliente:
					atendimento = false;
					principal = false;
					break;
				default:
					// Caso algum comando invalido seja teclado retorna ao Menu:
					if (entrada != (1, 2, 3, 4, 5)) {
						cout << "Operacao Invalida" << endl;
					}
					break;
				}
				system("pause");
				system("cls");
			}
		}
		else
		{
			while (flag1 > 1) {
				flag1--;
				if (flag1 > 1) {
					cout << "\n\rSenha invalida, voce tem mais " << flag1 << " tentativas" << endl;
					cout << "Digite sua senha:";
					cin >> senha;
					principal = false;
					if (ptrConta[contaAtual]->habilitaAcesso(senha)) {
						flag1 = 0;
						principal = true;;
					}
				}
				if (flag1 == 1) {
					cout << "\n\rSenha invalida, voce tem mais " << flag1 << " tentativa" << endl;
					cout << "Digite sua senha:";
					cin >> senha;
					principal = false;
					if (ptrConta[contaAtual]->habilitaAcesso(senha)) {
						flag1 = 0;
						principal = true;
					}
				}
			}
		}
	}
}