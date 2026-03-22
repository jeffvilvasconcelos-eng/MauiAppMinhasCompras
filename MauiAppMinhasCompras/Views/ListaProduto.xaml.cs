using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

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
            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
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


            lst_produtos.IsRefreshing = true;
             

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
    try
    {
        double soma = lista.Sum(i => i.Total);
        string msg = $"O total é {soma:C}";
    }
    catch (Exception ex)
    {
        String Message = $"Ops, ocorreu um erro: {ex.Message}";
        DisplayAlert("Total dos Produtos", Message, "OK");
    }
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
}
