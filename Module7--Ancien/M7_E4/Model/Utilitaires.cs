using System.IO;

namespace Model
{
    public class Utilitaires
    {
        public static char DIR_SEPARATOR = Path.DirectorySeparatorChar;
        public static string DOSSIER_BASE = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                                           $"Fichiers-3GP{DIR_SEPARATOR}";
    }
}
