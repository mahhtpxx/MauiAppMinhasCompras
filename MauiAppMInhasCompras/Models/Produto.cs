using SQLite;

namespace MauiAppMinhasCompras.Models
{

    public class Produto
    {
        [PrimaryKey, AutoIncrement] // que vem do SQLite
        public int Id { get; set; }
        public string Descricao { get; set; }  
        public double Quantidade { get; set; }
        public double Preco { get; set; }

    }
}
