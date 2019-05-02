/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

#include "stdafx.h"
#include <iostream>
#include "myfunc.h"

using namespace std;

void main(){
	float var = 25;

	// Resultado para x = 25 em y(x)
	cout << "y(25) = " << expressao(var) << endl;
	cout << endl;

	var = fator(7);

	// Resultado para x = 7! em y(x)
	cout << "y(7!) = " << expressao(var) << endl;
	cout << endl;

	var = power(2.5, 3);

	// Resultado para x = 2.5³ em y(x)
	cout << "y(2.5^3) = " << expressao(var) << endl;
	cout << endl;

	system("pause");
}

