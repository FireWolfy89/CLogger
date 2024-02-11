using System.Configuration;
using System.Data;
using System.Windows;

namespace CLogger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       static  string databaseName = "Macros.db";
       static  string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
       public static  string databasePath = System.IO.Path.Combine(folderPath, databaseName);
    }

}
