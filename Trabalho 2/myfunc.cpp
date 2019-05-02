/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

#include "stdafx.h"

// Fun��o para calcular pot�ncia com expoentes inteiros
float power(float base, int expo){
	float flag = 1;
	for (int i = 0; i < expo; i++){
		flag *= base;
	}
	return flag;
}

// Fun��o para calcular fatorial
int fator(int fat){
	for (int i = fat - 1; i > 0; --i){
		fat *= i;
	}
	return fat;
}

// Fun��o para calcular a express�o
float expressao(float var){
	var = fator(5)*power(var, 3) + fator(4)*power(var, 2) + fator(3)*var + fator(2);
	return var;
}