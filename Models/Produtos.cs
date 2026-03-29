using SQLite;

namespace MeuPrimeiroApp.Models
{
	public class Produtos
	{
		string _descricao;
		
		[PrimaryKey, AutoIncrement]
		public int Id {get; set; }
		public string Descricao {
			get => _descricao;
			set
			{
				if(value == null)
				{
					throw new Exception("Por favor, preencha a descrição");
				}
				
				_descricao = value;
			}
		}
		
		//public int Id {get; set;}
		//public string Descricao {get; set;}
		public double Quantidade {get; set;}
		public double Preco {get; set;}
		public double Total { get => Quantidade * Preco; }
		public string Categoria {get; set; }
		public DateTime DataCadastro {get; set; } = DateTime.Now; //Default para Hoje
		
	}
	
	public class RelatorioCategoria
	{
		public string Categoria { get; set; }
		public double TotalGasto { get; set; }
	}

}