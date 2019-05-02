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

class RoboTerrestre : public MobileRobot {
public:
	// Declara..o do Construtor e Destrutor da Classe derivada
	RoboTerrestre();
	RoboTerrestre(double, double, double);
	~RoboTerrestre();

	// Declara..o dos m.todos da classe
	void ExecutaMovimento(MobileRobot *);
	void mover(double, double, double, double);
};