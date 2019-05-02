/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORM.TICA INDUSTRIAL
*/

// DIRETIVA DE COMPILA..O
#pragma once
#include <iostream>

using namespace std;

class MobileRobot {
public:
	// Declara..o do Construtor e Destrutor da Classe
	MobileRobot();
	MobileRobot(double, double, double);
	~MobileRobot();

	// Declara..o dos m.todos da classe
	double getPosicaoAtual(char);
	void setPosicaAtual(double, double, double); 
	virtual void ExecutaMovimento(MobileRobot *) = 0;
	virtual void mover(double, double, double, double) = 0;

private:
	// Declara..o do atributo da classe  base
	// (n.o dispon.vel para as classes derivadas)
	double posicaoAtual[3];

protected:
	// Somente a classe base e suas derivadas podem mover o robo,
	// em uma situa..o de emergencia, para a origem (0, 0, 0)
	
	// Elementos externos n.o tem essa permiss.o 
	void rescueRobot(MobileRobot *);

};