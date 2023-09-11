// Programme qui illustre le fonctionnement de deux threads avec le principe producteur/consommateur

using System.Collections.Concurrent;
using M3_E3;

Random rng = new Random();
ConcurrentQueue<Commande> lesCommandes = new ConcurrentQueue<Commande>();

// Thread pour le création des commandes
Thread threadCreationCommande = new Thread(new ThreadStart(CreerCommandes));
threadCreationCommande.Start();

// Thread pour l'exécution des commandes
Thread threadExecutionCommande = new Thread(new ThreadStart(ExecuterCommandes));
threadExecutionCommande.Start();

// Méthode qui crée des commandes. Doit être déclenchée avec un thread.
void CreerCommandes()
{
    while (true)
    {
        int tempsRepos = rng.Next(2000, 4000);
        Thread.Sleep(tempsRepos);
        int tempsExecution = rng.Next(1000, 3999);
        Commande nouvelleCommande = new Commande(tempsExecution);
        string creationCommande = $"Commande {nouvelleCommande.NoCommande} est créée, temps exécution {nouvelleCommande.TempExecution}";
        Console.WriteLine(creationCommande);
        lesCommandes.Enqueue(nouvelleCommande);
        AfficherNombreCommandes();
    }
}

// Méthode qui exécute des commandes. Doit être déclenchée avec un thread.
void ExecuterCommandes()
{
    while (true)
    {
        Commande uneCommande;
        if (lesCommandes.TryDequeue(out uneCommande))
        {
            int tempsExecution = uneCommande.TempExecution;
            Thread.Sleep(tempsExecution);
            Console.WriteLine($"Commande {uneCommande.NoCommande} est exécutée");
            AfficherNombreCommandes();
        }
        else
        {
            Console.WriteLine("Aucune commande à exécuter");
            Thread.Sleep(1000);
        }
    }
}

// Méthode qui affiche le nombre de commandes
void AfficherNombreCommandes()
{
    Console.WriteLine($"Le nombre de commandes en attente est de : {lesCommandes.Count}");
}
