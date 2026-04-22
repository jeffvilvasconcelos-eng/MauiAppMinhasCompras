using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic; // Adicionado para List<T>
using System.Threading.Tasks;
using MauiAppMinhasCompras.Views;
using System.Linq;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    List<Produto> listaOriginal = new List<Produto>();

    public ListaProduto()
    {
          InitializeComponent();

          lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();
            listaOriginal = await App.Db.GetAll();

            listaOriginal.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
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
            String q = e.NewTextValue;
                     
             

          lista.Clear();

          List<Produto> tmp = await App.Db.Search(q);

          tmp.ForEach(i => lista.Add(i));
    }
        catch (Exception ex)
         {
            await DisplayAlert("Ops", ex.Message, "OK");
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
        DisplayAlert("Valor Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
    try
    {
        MenuItem Selecionado = sender as MenuItem;
        Produto p = Selecionado.BindingContext as Produto;
        bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "sim", "Não");

        if (confirm)
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

    private async void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
    try
    {
        Produto p = e.SelectedItem as Produto;
        Navigation.PushAsync(new Views.EditarProduto
        {
            BindingContext = p,
        });
        }
    catch (Exception ex)
    {
        await DisplayAlert("Ops", ex.Message, "OK");
    }
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
    try
    {
        lista.Clear();
        List<Produto> tmp = await App.Db.GetAll();

        tmp.ForEach(i => lista.Add(i));
    }
    catch (Exception ex)
    {
        await DisplayAlert("Ops", ex.Message, "OK");
    }
    finally
    {
        lst_produtos.IsRefreshing = false;
    }
  
    }

    private void OnFiltroCategoriaChanged(object sender, EventArgs e)
    {
        string categoria = filtroCategoria.SelectedItem?.ToString();

        lista.Clear();

        if (string.IsNullOrEmpty(categoria) || categoria == "Todos")
        {
            listaOriginal.ForEach(i => lista.Add(i));
            return;
        }

        var filtrados = listaOriginal
            .Where(p => p.Categoria == categoria)
            .ToList();

        filtrados.ForEach(i => lista.Add(i));
    }

    private async void OnAbrirRelatorio(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Relatorio());
    }

}
