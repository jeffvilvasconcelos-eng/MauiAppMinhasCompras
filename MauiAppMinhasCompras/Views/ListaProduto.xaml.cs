using MauiAppMinhasCompras.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System;
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
            List<Produto> temporario = await App.Db.GetAll();
            temporario.ForEach(i => lista.Add(i));

            try
            {
                Navigation.PushAsync(new Views.NovoProduto());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }
        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue;

            lista.Clear();

            List<Produto> temporario = await App.Db.Search(q);

            temporario.ForEach(i => lista.Add(i));
        }
        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            double soma = lista.Sum(i => i.Total);

            string msg = $"O total é {soma:C}";

            DisplayAlert("Total dos Produtos", msg, "Ok");
                  
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }




    }

