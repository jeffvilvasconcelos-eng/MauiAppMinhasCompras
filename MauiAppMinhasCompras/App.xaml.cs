using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db;
        // _db = Campo

        public static SQLiteDatabaseHelper Db
            // Db = Propriedade

        public App()
        {
            InitializeComponent();
        
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}