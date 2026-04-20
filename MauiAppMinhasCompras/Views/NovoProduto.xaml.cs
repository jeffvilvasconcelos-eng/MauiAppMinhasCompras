using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // VALIDAR CATEGORIA
                string categoria = pickerCategoria.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(categoria))
                {
                    await DisplayAlert("Erro", "Selecione uma categoria", "OK");
                    return;
                }

                // VALIDAR NÚMEROS
                if (!double.TryParse(txt_quantidade.Text, out double quantidade) ||
                    !double.TryParse(txt_preco.Text, out double preco))
                {
                    await DisplayAlert("Erro", "Quantidade ou preço inválido", "OK");
                    return;
                }

                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text,
                    Quantidade = quantidade,
                    Preco = preco,
                    Categoria = categoria,
                    DataCadastro = dtData.Date
                };

                await App.Db.Insert(p);

                await DisplayAlert("Sucesso", "Produto cadastrado com sucesso!", "OK");

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }


    