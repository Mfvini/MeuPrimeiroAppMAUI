using MeuPrimeiroApp.Models;    // Para reconhecer a classe Produtos e RelatorioCategoria
using MeuPrimeiroApp.Services;  // Para o C# entender o que é o DatabaseService (se você usar o App.Db)
//using System.Collections.ObjectModel;

namespace MeuPrimeiroApp.Views
{
    public partial class RelatorioPage : ContentPage
    {
        public RelatorioPage()
        {
            InitializeComponent();
            
            // Define as datas padrão como o mês atual
            dtp_inicio.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtp_fim.Date = DateTime.Now;
        }

        private async void BtnGerar_Clicked(object sender, EventArgs e)
        {
            try 
            {
                // Busca os dados usando o método que criamos no DatabaseService
                var dados = await App.Db.GetGastosPorCategoria(dtp_inicio.Date, dtp_fim.Date);
                
                lst_relatorio.ItemsSource = dados;

                if (dados == null || dados.Count == 0)
                {
                    await DisplayAlert("Informação", "Nenhum registro encontrado para este período.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}