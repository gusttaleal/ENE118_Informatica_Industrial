/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORM.TICA INDUSTRIAL
*/

// DIRETIVA DE COMPILA..O
#include <iostream>
#include "MobileRobot.h"
#include "RoboTerrestre.h"
// Construtor para preset do vetor posicaoAtual herdado da classe Base
RoboTerrestre::RoboTerrestre() : MobileRobot() {

}
// Construtor para set do vetor posicaoAtual herdado da classe Base
RoboTerrestre::RoboTerrestre(double X, double Y, double Z) : MobileRobot(X, Y, Z) {

}
// Destrutor
RoboTerrestre::~RoboTerrestre() {

}
// Implementa..o do m.todo ExecutaMovimento da classe RoboTerrestre
void RoboTerrestre::ExecutaMovimento(MobileRobot *ptr) {
	// Vari.veis inicializadas com valores randomicos
	int Xvel = rand() % 10 + 1;
	int Yvel = rand() % 10 + 1;
	int Zvel = 0;
	int tempo = rand() % 10 + 1;

	cout << "Velocidades Sorteadas: " << endl;
	cout << "Xvel: " << Xvel << endl;
	cout << "Yvel: " << Yvel << endl;
	cout << "Zvel: " << Zvel << endl;

	cout << "Tempo Sorteado: " << endl;
	cout << "Tempo: " << tempo << endl;
	cout << endl;

	ptr->mover(Xvel, Yvel, Zvel, tempo);
}
// Implementa..o do m.todo mover da classe RoboTerrestre
void RoboTerrestre::mover(double Xvel, double Yvel, double Zvel, double tempo) {
	// Para efeito de simplifica..o atribuimos Zvel = 0, independete do valor do argumento para Zvel
	Zvel = 0;
	// Interface com o usu.rio
	cout << "Acionando os motores das rodas" << endl;
	cout << endl;
	cout << "Posicao Inicial [i]: " << endl;
	cout << "Xi: " << getPosicaoAtual('X') << endl;
	cout << "Yi: " << getPosicaoAtual('Y') << endl;
	cout << "Zi: " << getPosicaoAtual('Z') << endl;

	// Claculo da nova posi..o
	setPosicaAtual(getPosicaoAtual('X') + (Xvel * tempo),
				   getPosicaoAtual('Y') + (Yvel * tempo),
				   getPosicaoAtual('Y') + (Zvel * tempo));

	// Interface com o usu.rio
	cout << "Posicao Final [f]: " << endl;
	cout << "Xf: " << getPosicaoAtual('X') << endl;
	cout << "Yf: " << getPosicaoAtual('Y') << endl;
	cout << "Zf: " << getPosicaoAtual('Z') << endl;
}