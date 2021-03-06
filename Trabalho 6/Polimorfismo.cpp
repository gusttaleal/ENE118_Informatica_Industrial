/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

// DIRETIVA DE COMPILAÇÃO
#include <iostream>
#include "MobileRobot.h"
#include "RoboAereo.h"
#include "RoboTerrestre.h"

using namespace std;


void main() {
	
	RoboAereo VANT(2, 2, 2);
	RoboTerrestre VTNT(2, 2, 0);
	
	cout << "Posicao atual [VANT]: " << endl;
	cout << "Xo: " << VANT.getPosicaoAtual('X') << endl;
	cout << "Yo: " << VANT.getPosicaoAtual('Y') << endl;
	cout << "Zo: " << VANT.getPosicaoAtual('Z') << endl;
	
	cout << "Posicao atual [VTNT]: " << endl;
	cout << "Xo: " << VTNT.getPosicaoAtual('X') << endl;
	cout << "Yo: " << VTNT.getPosicaoAtual('Y') << endl;
	cout << "Zo: " << VTNT.getPosicaoAtual('Z') << endl;
	
	system("pause");
	system("cls");
	
	cout << "Sistema Aleatorio Iniciado VANT" << endl;
	cout << endl;
	VANT.ExecutaMovimento(&VANT);
	
	cout << "Sistema Aleatorio Iniciado VTNT" << endl;
	cout << endl;
	VTNT.ExecutaMovimento(&VTNT);

	system("pause");
	system("cls");

	cout << "Sistema inciado" << endl;
	cout << "Xvel = Yvel = Zvel = 10" << endl;
	cout << "Tempo = 1" << endl;
	cout << endl;
	VANT.mover(10, 10, 10, 1);
	VTNT.mover(10, 10, 10, 1);

	system("pause");
}
