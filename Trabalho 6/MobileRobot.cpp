/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORM.TICA INDUSTRIAL
*/

// DIRETIVA DE COMPILA..O
#include <iostream>
#include "MobileRobot.h"

using namespace std;
// Construtor para preset do vetor posicaoAtual caso os argumentos n.o sejam inicializados
MobileRobot::MobileRobot(){
	this->posicaoAtual[0] = 0;
	this->posicaoAtual[1] = 0;
	this->posicaoAtual[2] = 0;
}
// Construtor para set do vetor posicaoAtual caso os argumentos sejam inicializados
MobileRobot::MobileRobot(double X, double Y, double Z) {
	this->posicaoAtual[0] = X;
	this->posicaoAtual[1] = Y;
	this->posicaoAtual[2] = Z;
}
// Destrutor
MobileRobot::~MobileRobot() {

}
// Fun..o getPosicaoAtual para capturar o valor das variaveis X, Y e Z armazenadas no vetor posicaoAtual
double MobileRobot::getPosicaoAtual(char coordenada) {
	switch (coordenada)
	{
	case 'X':
		return posicaoAtual[0];
		break;
	case 'Y':
		return posicaoAtual[1];
		break;
	case 'Z':
		return posicaoAtual[2];
		break;
	default:
		cout << "Coordenada Invalida" << endl;
		break;
	}
}
// Fun..o setPosicaoAtual para modificar as variaveis X, Y e Z armazenadas no vetor posicaoAtual
void MobileRobot::setPosicaAtual(double X, double Y, double Z) {
	posicaoAtual[0] = X;
	posicaoAtual[1] = Y;
	posicaoAtual[2] = Z;
}
// Fun..o para trazer o robo de volta a origem
void MobileRobot::rescueRobot(MobileRobot *ptr) {
	ptr->posicaoAtual[0] = 0;
	ptr->posicaoAtual[1] = 0;
	ptr->posicaoAtual[2] = 0;
}