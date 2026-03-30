using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using static MauiAppMinhasCompras.Models.Produto;
using MauiAppMinhasCompras.Models;


namespace MauiAppMinhasCompras.Views
{
    public partial class Relatorio : ContentPage
    {
        ObservableCollection<CategoriaTotal> listaCategorias = new();
        public Relatorio()
        {
            InitializeComponent();
        }

        private async void OnCarregarRelatorio(object sender, EventArgs e)
        {
            // Implemente a lógica para carregar o relatório aqui
            var dados = await App.Db.GetTotalPorCategoria();
            lst_relatorio.ItemsSource = dados;
        }

       
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var dados = await App.Db.GetTotalPorCategoria();

            listaCategorias.Clear();

            foreach (var item in dados)
            {
                listaCategorias.Add(item);
            }

            lst_relatorio.ItemsSource = listaCategorias;
        }
    }

}