using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

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
		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(i => lista.Add(i));
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
		string q = e.NewTextValue;

		lista.Clear(); // para limpara e não deixar amontuado todos os itens

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {     // expressão lambida: para cada item na lista, quero somar através do total 
		double soma = lista.Sum(i => i.Total);
		           //sum:ação de somar
		string msg = $"O total é {soma:C}";
		                              //Reconhe que é em reais R$
		DisplayAlert("Total dos Produtos", msg, "Ok");
	}
}