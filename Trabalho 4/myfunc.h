/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

#pragma 
#include <iostream>

using namespace std;

template <typename T>
class Pilha
{
public:
	Pilha();
	~Pilha();

	void insere(const T&);
	void remove();

private:
	int tam;
	T *pPtr;

};

template <typename T>
Pilha<T>::Pilha() : tam(0), pPtr(new T[tam]) {
}

template <typename T>
Pilha<T>::~Pilha() {
	delete[] pPtr;
}

// Função para inserir dados na PILHA (CHAR, INT, DOUBLE)
template <typename T>
void Pilha<T>::insere(const T &pushValue) {
	// Ponteiro auxiliar do tipo TEMPLATE de tamanho TAM+1 - Dinâmico
	T *pAux = new T[tam + 1];

	// Se TAM for 0 (estado inicial/final) não faz nada
	if (tam == 0) {
	}
	// Caso contrário plota elementos da pilha antiga
	else {
		cout << "Pilha Antiga: " << endl;
		for (int i = 0; i < tam; i++) {
			cout << "Elemento " << i << ": " << pPtr[i] << endl;
		}
		cout << endl;
	}

	// Atribui os elementos da pilha antiga para a nova pilha, localizada numa nova porção de memória
	for (int i = 0; i < tam; i++) {
		pAux[i] = pPtr[i];
	}

	// Atribui a posição TAM da pilha nova o valor digitado pelo usuário
	pAux[tam] = pushValue;

	// Deixa disponivel o espaço de memoria para o qual pPtr apontava
	delete[] pPtr;
	// Faz com que pPtr agora aponte para o endereço de pAux
	pPtr = pAux;
	// Aumenta o TAM em uma unidade
	tam++;

	// Plota elementos da pilha atual
	cout << "Pilha Atual: " << endl;
	for (int i = 0; i < tam; i++) {
		cout << "Elemento " << i << ": " << pPtr[i] << endl;
	}
	cout << endl;

}
// Função para remover dados na PILHA (CHAR, INT, DOUBLE)
template <typename T>
void Pilha<T>::remove() {
	// Se TAM for 0 (estado inicial/final) indica pilha vazia
	if (tam == 0) {
		cout << "Pilha Atual: Vazia" << endl;
	}
	// Caso contrário:
	else {
		// Ponteiro auxiliar do tipo TEMPLATE de tamanho TAM-1 - Dinâmico
		T *pAux = new T[tam - 1];;

		// Plota elementos da pilha antiga
		cout << "Pilha Antiga: " << endl;
		for (int i = 0; i < tam; i++) {
			cout << "Elemento " << i << ": " << pPtr[i] << endl;
		}
		cout << endl;

		// Diminue o TAM em uma unidade
		tam--;

		// Atribui os elementos da pilha antiga para a nova pilha, localizada numa nova porção de memória
		for (int i = 0; i < tam; i++) {
			pAux[i] = pPtr[i];
		}

		// Deixa disponivel o espaço de memoria para o qual pPtr apontava
		delete[] pPtr;
		// Faz com que pPtr agora aponte para o endereço de pAux
		pPtr = pAux;

		// Se TAM for 0 (estado inicial/final) indica pilha vazia
		if (tam == 0) {
			cout << "Pilha Atual: Vazia" << endl;
		}
		// Caso contrário:
		else{
			// Plota elementos da pilha atual
			cout << "Pilha Atual: " << endl;
			for (int i = 0; i < tam; i++) {
				cout << "Elemento " << i << ": " << pPtr[i] << endl;
			}
			cout << endl;
		}
	}
}