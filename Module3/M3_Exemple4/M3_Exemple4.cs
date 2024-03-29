﻿const int NOMBRE_THREADS = 100;
const int NOMBRE_BOUCLES = 100000;

int valeurPartagee = 0;

Thread[] tabThread = new Thread[NOMBRE_THREADS];

for (int i = 0; i < NOMBRE_THREADS; i++)
{
    tabThread[i] = new Thread(new ThreadStart(IncrementerValeur));
}

foreach (Thread th in tabThread)
{
    th.Start();
}

foreach (Thread th in tabThread)
{
    th.Join();
}

Console.WriteLine("{0} * {1} = {2} .... ben oui !!!", NOMBRE_BOUCLES, NOMBRE_THREADS, valeurPartagee);

void IncrementerValeur()
{
    for (int i = 0; i < NOMBRE_BOUCLES; i++)
    {
        valeurPartagee = valeurPartagee + 1;
    }
}