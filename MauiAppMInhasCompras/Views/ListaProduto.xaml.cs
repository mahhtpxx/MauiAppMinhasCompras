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

            lst_produtos.IsRefreshing = true;

            lista.Clear(); // para limpar e não deixar amontuado todos os itens

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops...", ex.Message, "Fechar");
        }
        finally
        {
            lst_produtos.IsRefreshing = false; // para tirar o circulo de carregamento
        }

    }

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            // pega todos os produtos do banco
            List<Produto> lista = await App.Db.GetAll();

            // agrupa os produtos pela categoria (tipo separar por caixinhas)
            var relatorio = lista
                .GroupBy(p => p.Categoria) // agrupa por categoria
                .Select(g => $"{g.Key}: R$ {g.Sum(x => x.Total):F2}");
            // g.Key = nome da categoria
            // g.Sum = soma os totais daquela categoria

            // junta tudo em uma mensagem só
            string mensagem = string.Join("\n", relatorio);

            // mostra na tela (o famoso popup)
            await DisplayAlert("Relatório por Categoria", mensagem, "OK");
        }
        catch (Exception ex)
        {
            // caso dê ruim (porque sempre existe essa possibilidade né 🙄)
            await DisplayAlert("Erro", ex.Message, "OK");
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

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
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
        finally
        {
            lst_produtos.IsRefreshing = false; // para tirar o circulo de carregamento
        }
    }
}