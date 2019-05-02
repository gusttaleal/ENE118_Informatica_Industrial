/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

#include "stdafx.h"
#include <iostream>

using namespace std;

void main()
{	
	float peso = 0.0, altura = 0.0, IMC = 0.0;
	
	cout << "Entre com o valor do PESO em kilograma: ";
	cin >> peso;

	cout << "Entre com o valor da ALTURA em metros: ";
	cin >> altura;
	
	IMC = peso / (altura*altura);

	if (IMC < 18.50)
	{
		cout << "IMC correspondente aos dados eh: " << IMC << " - Abaixo do peso ideal" << endl;
	}
	
	if (18.50 <= IMC && IMC < 24.90)
	{
		cout << "IMC correspondente aos dados eh: " << IMC << " - Peso ideal" << endl;	
	}
	
	if (24.90 <= IMC && IMC < 29.90)
	{
		cout << "IMC correspondente aos dados eh: " << IMC << " - Acima do peso ideal" << endl;	
	}
	
	if (IMC >= 29.90)
	{
		cout << "IMC correspondente aos dados eh: " << IMC << " - Classificacao de Risco" << endl;
	}
	
	system("pause");
}



