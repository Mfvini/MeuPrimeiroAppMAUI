using SQLite;

namespace MeuPrimeiroApp.Models
{
	public class Produtos
	{
		[PrimaryKey, AutoIncrement]
		
		public int Id {get; set;}
		public string Descricao {get; set;} = string.Empty;
		public double Quantidade {get; set;}
		public double Preco {get; set;}
	}
}