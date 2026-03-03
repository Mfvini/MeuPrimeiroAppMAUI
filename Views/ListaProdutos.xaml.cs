namespace MeuPrimeiroApp.Views
{
	public partial class ListaProdutos : ContentPage
	{
		public ListaProdutos()
		{
			InitializeComponent();
		}
		
		private void ToolbarItem_Clicked (object sender, EventArgs e)
		{
			try
			{
				Navigation.PushAsync (new Views.NovoProduto());
			}catch (Exception ex)
			{
				DisplayAlert("ops", ex.Message, "Ok");
			}
		}
	}
}