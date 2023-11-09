using System.IO;

// Programme qui permet de copier le fichier equipes.xml dans le dossier Fichiers-3GP.

char DIR_SEPARATOR = Path.DirectorySeparatorChar;   // Permet d'avoir un séparateur portable
string NOM_FICHIER = "equipes.xml";

if (ConfirmerOperation($"Voulez-vous placer le fichier {NOM_FICHIER} au bon endroit"))
{
    CopierFichier(NOM_FICHIER);
}
else
{
    Console.WriteLine("Aucun changement n'est effectué");
}

// Permet de confirmer si l'utilisateur veut effectuer une opération
bool ConfirmerOperation(string description)
{
    Console.Write($"{description} (oui pour confirmer)? ");
    string? reponse = Console.ReadLine();
    return reponse != null && reponse.ToLower().Equals("oui");
}

// Permet de copier le fichier dans le dossier Fichiers-3GP.
// Le dossier est créé s'il n'existe pas.
void CopierFichier(string nomFichier)
{
    string pathMesDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    string pathDossier = $"{pathMesDocuments}{DIR_SEPARATOR}Fichiers-3GP";
    string pathFichier = $"{pathDossier}{DIR_SEPARATOR}{nomFichier}";

    // Création du dossier s'il n'existe pas
    if (!Directory.Exists(pathDossier))
    {
        Directory.CreateDirectory(pathDossier);
    }

    bool doitCopier = true;

    // On regarde si le fichier existe
    if (File.Exists(pathFichier))
    {
        doitCopier = ConfirmerOperation($"Le fichier {nomFichier} existe. Voulez-vous l'écraser");
    }

    // On regarde s'il faut copier
    if (doitCopier)
    {
        Console.WriteLine($"Écriture du fichier sous {pathFichier}");
        File.Copy("assets" + DIR_SEPARATOR + nomFichier, pathFichier, true);
    }
    else
    {
        Console.WriteLine("Aucun changement n'est effectué");
    }
}