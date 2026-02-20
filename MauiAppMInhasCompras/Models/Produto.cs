using SQLite;

namespace MauiAppMinhasCompras.Models
{

    internal class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Quantidade { get; set; }
        public string Preco { get; set; }

    }
}
