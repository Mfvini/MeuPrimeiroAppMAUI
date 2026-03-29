using MeuPrimeiroApp.Models;
using System.Collections.ObjectModel;

namespace MeuPrimeiroApp.Views
{
	public partial class ListaProdutos : ContentPage
	{
		ObservableCollection<Produtos> lista = new ObservableCollection<Produtos>(); 

		public ListaProdutos()
		{
			InitializeComponent();

			lst_produtos.ItemsSource = lista;
		}

		protected async override void OnAppearing()
		{
			try
			{
				lista.Clear();
				
				List<Produtos> tmp = await App.Db.GetAll();

				tmp.ForEach(i => lista.Add(i));
			}
			catch (Exception ex)
			{
				await DisplayAlert("Ops", ex.Message, "Ok");
			}
		}

		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			try
			{
				Navigation.PushAsync(new Views.NovoProduto());

			} 
			catch (Exception ex)
			{
				DisplayAlert("Ops", ex.Message, "OK");
			}
		}
			
		private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				string q = e.NewTextValue;
				lst_produtos.IsRefreshing = true;

				lista.Clear();

				List<Produtos> tmp = await App.Db.Search(q);

				tmp.ForEach(i => lista.Add(i));
			}
			catch (Exception ex)
			{
				await DisplayAlert("Ops", ex.Message, "Ok");
			}
			finally
			{
				lst_produtos.IsRefreshing = false;
			}
		}
		
		private void ToolbarItem_Clicked_1(object sender, EventArgs e)
		{
			double soma = lista.Sum(i => i.Total);

			string msg = $"O total é {soma:C}";

			DisplayAlert("Total dos Produtos", msg, "OK");
		}
		
		private async void MenuItem_Clicked(object sender, EventArgs e)
		{
			try
			{
				MenuItem selecionado = sender as MenuItem;
				
				Produtos p = selecionado.BindingContext as Produtos;
				
				bool confirm = await DisplayAlert(
					"Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");
					
				if(confirm)
				{
					await App.Db.Delete(p.Id);
					lista.Remove(p);
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Ops", ex.Message, "OK");
			}
		}
		
		private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			try
			{
				Produtos p = e.SelectedItem as Produtos;
					
				Navigation.PushAsync(new Views.EditarProduto
				{
					BindingContext = p,
				});
			}
			catch (Exception ex)
			{
				DisplayAlert("Ops", ex.Message, "OK");  
			}

		}
		
		private  async void lst_produtos_Refreshing(object sender, EventArgs e)
		{
			try
			{
				lista.Clear();
				
				List<Produtos> tmp = await App.Db.GetAll();

				tmp.ForEach(i => lista.Add(i));
			}
			catch (Exception ex)
			{
				await DisplayAlert("Ops", ex.Message, "Ok");
			} finally
			{
				lst_produtos.IsRefreshing = false;
			}
		}
		
		private async void BtnVerRelatorio_Clicked(object sender, EventArgs e)
		{
			try 
			{
				await Navigation.PushAsync(new Views.RelatorioPage());
			}
			catch (Exception ex)
			{
				await DisplayAlert("Erro", "Não foi possível abrir o relatório: " + ex.Message, "OK");
			}
		}
	}
}