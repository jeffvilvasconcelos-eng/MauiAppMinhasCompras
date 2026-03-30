using MauiAppMinhasCompras.Helpers;


namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db;
        // _db = Campo

        public static SQLiteDatabaseHelper Db
        // Db = Propriedade
        {
            get
             {
                if (_db == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                        LocalApplicationData), "banco_sqlite_compras.db3");

                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db;
            }


        }
        public App()
        {
            InitializeComponent();

            // Definir a cultura para pt-BR (Português do Brasil) para formatação de datas, números e moedas
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}