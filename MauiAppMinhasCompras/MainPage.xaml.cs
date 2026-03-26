using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnCategoriaSelecionada(object sender, EventArgs e)
        {
            if (filtroCategoria.SelectedItem == null)
                return;

            var categoria = filtroCategoria.SelectedItem.ToString();

            List<Produto> lista;

            if (categoria == "Todos")
                lista = await App.Db.GetAll();
            else
                lista = await App.Db.GetByCategoria(categoria);

            listaProdutos.ItemsSource = lista;
        }

        private async void OnRelatorioClicked(object sender, EventArgs e)
        {
            var dados = await App.Db.GetTotalPorCategoria();

            String msg = "";
            foreach (var item in dados)
            {
                msg += $"{item.Categoria}: {item.TotalGasto:F2}\n";
            }
            await DisplayAlert("Relatório de Gastos por Categoria", msg, "OK");
        }
    }
}

