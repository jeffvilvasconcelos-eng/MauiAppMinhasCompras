using MauiAppMinhasCompras.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
namespace MauiAppMinhasCompras.Views
{
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
                List<Produto> temporario = await App.Db.GetAll();
                temporario.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");

                try
                {
                    Navigation.PushAsync(new Views.NovoProduto());
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ops", ex.Message, "Ok");
                }


        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue;

            lista.Clear();

            List<Produto> temporario = await App.Db.Search(q);

            temporario.ForEach(i => lista.Add(i));
        }
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            double soma = lista.Sum(i => i.Total);

            string msg = $"O total é {soma:C}";

            DisplayAlert("Total dos Produtos", msg, "Ok");

        }

        private async Task MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = sender as MenuItem;
                Produto p = selecionado.BindingContext as Produto;
                boll confirm = await DisplayAlest(
                    "Tem Certeza?" "Remover Produto?","Sim", "Não");
                if(confirm)
                {
                    await App.Db.Delete(p.Id);
                    lista.Remove(p);
                }


            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
    
            }
        }

        private void ToolbarItem_clicked(object sender, EventArgs e)
        {
            // Adicione aqui a lógica para o evento de adicionar produto
        }
    }




}

