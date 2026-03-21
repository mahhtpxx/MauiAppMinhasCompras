using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
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
            lista.Clear();  // para limpar e não deixar amontuado todos os itens

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops...", ex.Message, "Fechar");
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
            DisplayAlert("Ops...", ex.Message, "Fechar");
        }

    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lista.Clear(); // para limpar e não deixar amontuado todos os itens

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops...", ex.Message, "Fechar");
        }

    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {   // expressão lambida: para cada item na lista, quero somar através do total 
            double soma = lista.Sum(i => i.Total);
            //sum:ação de somar
            string msg = $"O total é {soma:C}";
                                           //Reconhe que é em reais R$
            DisplayAlert("Total dos Produtos", msg, "Ok");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops...", ex.Message, "Fechar");
        }

    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem; // Sender tem certeza da linha (ou produto)
                                                       // que estamos selecionando para excluir
            Produto p = selecionado.BindingContext as Produto; //é aqui tem certeza de qual produto iremos remover

            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não"); //pergunta ao usuário se deseja fazer essa ação

            if (confirm)
            {
                await App.Db.Delete(p.Id); //se for excluído (true), ele vai tirar da lista(View) e do SQL
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops...", ex.Message, "Fechar");

        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        { //navega para a página de editar o produto
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops...", ex.Message, "Fechar");

        }
    }

}