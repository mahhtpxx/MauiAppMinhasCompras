using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    { // tornando a SQLiteDtataBaseHelper em algo utilizavel
        static SqliteDataBaseHelpers _db; //um campo

        //tornando a propriedade pública 
        public static SqliteDataBaseHelpers Db //uma propriedade 
            { 
            get 
            
             {
              if(_db == null) //uma instância do SQLiteDataBaseHelper
                {
                    string path = Path.Combine( //caminho absolutos para nosas edições dentro do aplicativo
                         Environment.GetFolderPath
                         (Environment.SpecialFolder.LocalApplicationData),
                         "banco_sqlite_compras.db3"
                        );

                    _db = new SqliteDataBaseHelpers(path);              
                }
            
                return _db; 
            } 
        }
                                                       

        public App()
        {
            InitializeComponent();
         //redirecionando onde vai começar a tela do app ⬇️
            //MainPage = new AppShell(); pois não utilizamo eles
            MainPage = new NavigationPage(new Views.ListaProduto()); //navigationpage para navegações entre as páginas
        }
    }
}
