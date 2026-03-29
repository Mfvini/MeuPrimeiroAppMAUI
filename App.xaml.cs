using MeuPrimeiroApp.Services;

namespace MeuPrimeiroApp
{
	public partial class App : Application
	{
	
		static DatabaseService? _db;
	
		public static DatabaseService Db
		{
			get
			{
				if(_db == null)
				{
					string path = Path.Combine(
						Environment.GetFolderPath(
							Environment.SpecialFolder.LocalApplicationData),
							"banco_sqlite_compras_v2.db3");
					
					_db = new DatabaseService(path);
				}
			
				return _db;
			}
		}
	
	
		public App()
		{
			InitializeComponent();
			
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
		
			MainPage = new NavigationPage(new Views.ListaProdutos());
		}
	}
	
}