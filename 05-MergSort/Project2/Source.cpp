#include <iostream>
#include <stdlib.h>
#include <conio.h>
using namespace std;

int n;

void merge(int A[], int p, int q, int r)
{
	int i, j, k;
	int n1 = q - p + 1;
	int n2 = r - q;
	int *L = new int[n1];
	int *R = new int[n2];

	//copy the contents of the array into a temp array
	for (i = 0; i<n1; i++)
		L[i] = A[p + i];

	for (j = 0; j<n2; j++)
		R[j] = A[q + 1 + j];
	//initialize the values for sorting
	i = 0;
	j = 0;
	k = p;

	while (i<n1 && j<n2)
	{
		if (L[i] <= R[j])
		{
			A[k] = L[i];
			i++;
		}
		else
		{
			A[k] = R[j];
			j++;
		}
		k++;
	}
	//copy all contents from L
	while (i<n1)
	{
		A[k] = L[i];
		i++;
		k++;
	}

	//copy all the contents from H
	while (j<n2)
	{
		A[k] = R[j];
		k++;
		j++;
	}
}
void mergesort(int A[], int p, int r)
{
	cout << "p=" << p;
	cout << " r=" << r;
	cout << endl;
	if (p < r)
	{
		int q = floor (p + r) / 2;
		mergesort(A, p, q);
		mergesort(A, q + 1, r);
		merge(A, p, q, r);
	}
}
void printdata(int A[], int size)
{
	for (int i = 0; i<size; i++)
	{
		cout << A[i] << "\t";
	}
	cout << endl;
}

int main()
{
	int A[] = {23, 12, 8, 56, 7, 33, 42, 63};
	printdata(A, 8);
	mergesort(A, 0, 7);
	printdata(A, 8);
	_getch();
}
