using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using static MauiAppMinhasCompras.Models.Produto;
using MauiAppMinhasCompras.Models;


namespace MauiAppMinhasCompras.Views
{
    public partial class Relatorio : ContentPage
    {
       

        public Relatorio()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var produtos = await App.Db.GetAll();

            var relatorio = produtos
                .GroupBy(p => p.Categoria)
                .Select(g => new RelatorioCategoria
                {
                    Categoria = g.Key,
                    Total = g.Sum(p => p.Total)
                })
                .ToList();

            listaRelatorio.ItemsSource = relatorio;
        }

      
    }
}