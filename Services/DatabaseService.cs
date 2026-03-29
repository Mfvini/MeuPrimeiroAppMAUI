using SQLite;
using MeuPrimeiroApp.Models;

namespace MeuPrimeiroApp.Services
{
	public class DatabaseService
	{
		readonly SQLiteAsyncConnection _conn;
		
		public DatabaseService(string path)
		{
			_conn = new SQLiteAsyncConnection(path);
			_conn.CreateTableAsync<Produtos>().Wait();
		}
		
		public Task<int> Insert(Produtos p)
		{
			return _conn.InsertAsync(p);
		}
		
		public Task<List<Produtos>> Update(Produtos p)
		{
			string sql = "UPDATE Produtos SET Descricao=?, Quantidade=?, Preco=?, Categoria=?, DataCadastro=? WHERE Id=?";
			
			return _conn.QueryAsync<Produtos>(
				sql, p.Descricao, p.Quantidade, p.Preco, p.Categoria, p.DataCadastro, p.Id
			);
		}
		
		public Task<int> Delete(int id)
		{
			return _conn.Table<Produtos>().DeleteAsync(i => i.Id == id);
		}
		
		public Task<List<Produtos>> GetAll()
		{
			return _conn.Table<Produtos>().ToListAsync();
		}
		
		public Task<List<Produtos>> Search(string q)
		{
			string sql = "SELECT * FROM Produtos WHERE descricao LIKE '%" + q + "%'";
			return _conn.QueryAsync<Produtos>(sql, $"%{q}%");
		}
		
		//Filtro por categoria específica
		public Task<List<Produtos>> GetByCategoria(string categoria)
		{
			return _conn.Table<Produtos>().Where(i => i.Categoria == categoria).ToListAsync();
		}
		
		//Relatório Soma do Total agrupado por Categoria
		public async Task<List<RelatorioCategoria>> GetGastosPorCategoria(DateTime inicio, DateTime fim)
		{
			string sql = "SELECT Categoria, SUM(Quantidade * Preco) as TotalGasto " +
						 "FROM Produtos " +
						 "WHERE DataCadastro BETWEEN ? AND ? " +
						 "GROUP BY Categoria";
			return await _conn.QueryAsync<RelatorioCategoria>(sql, inicio, fim);
		}
	
		
		public Task<List<Produtos>> GetByPeriodo(DateTime inicio, DateTime fim)
		{
			return _conn.Table<Produtos>()
						.Where(i => i.DataCadastro >= inicio && i.DataCadastro <= fim)
						.ToListAsync();
		}
		
	}
}