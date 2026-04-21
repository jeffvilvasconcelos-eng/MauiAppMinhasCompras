using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Views;

namespace MauiAppMinhasCompras
{
    public partial class MainPage : ContentPage
    {
        List<Produto> listaOriginal = new List<Produto>();

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listaOriginal = await App.Db.GetAll();
            listaProdutos.ItemsSource = listaOriginal;
        }
        
          private void OnFiltroCategoriaChanged(object sender, EventArgs e)
          {
              string? categoriaSelecionada = filtroCategoria.SelectedItem?.ToString();

              // Se for "Todos", mostra tudo
              if (string.IsNullOrEmpty(categoriaSelecionada) || categoriaSelecionada == "Todos")
              {
                  listaProdutos.ItemsSource = listaOriginal;
                  return;
              }

              // Filtrar lista
              var filtrados = listaOriginal
                  .Where(p => p.Categoria == categoriaSelecionada)
                  .ToList();

              listaProdutos.ItemsSource = filtrados;
          }
        private async void OnAbrirRelatorio(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Relatorio());
        }


        public MainPage()
        {
            InitializeComponent();
        }
    }
}
