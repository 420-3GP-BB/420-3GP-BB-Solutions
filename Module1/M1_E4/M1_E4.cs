﻿using M1_E4;

Livre[] tabLivres = new Livre[5];
tabLivres[0] = new Livre("Game Engine Architecture 3rd ed.", "Jason Grogory", "CRC Press", 1200, 2018);
tabLivres[1] = new Livre("Python for Data Analysis 2nd ed.", "Wes McKinney", "O'Reilly", 523, 2017);
tabLivres[2] = new Livre("Fundamentals of Game Design 3rd ed.", "Ernest Adams", "News Rider", 556, 2014);
tabLivres[3] = new Livre("Game Programming Patterns", "Robert Nystrom", "genever benning", 345, 2014);
tabLivres[4] = new Livre("Hands-On Machine Learning with Scikit-Learn, Keras & TensorFlow 2nd ed.", "Aurélien Géron", "O'Reilly", 814, 2019);

// Sort utilise CompareTo
Array.Sort(tabLivres);

foreach (Livre livre in tabLivres)
{
    Console.WriteLine(livre);
}
Console.WriteLine();
