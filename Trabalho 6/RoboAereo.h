/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORM.TICA INDUSTRIAL
*/

// DIRETIVA DE COMPILA..O
#pragma once
#include <iostream>
#include "MobileRobot.h"

using namespace std;

class RoboAereo : public MobileRobot {
public:
	// Declara..o do Construtor e Destrutor da Classe derivada
	RoboAereo();
	RoboAereo(double, double, double);
	~RoboAereo();

	// Declara..o dos m.todos da classe
	void ExecutaMovimento(MobileRobot *);
	void mover(double, double, double, double);

};