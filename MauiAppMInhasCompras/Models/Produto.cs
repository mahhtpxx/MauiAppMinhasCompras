using SQLite;

namespace MauiAppMinhasCompras.Models
{

    public class Produto
    {
        string _descricao;

        [PrimaryKey, AutoIncrement] // que vem do SQLite
        public int Id { get; set; }
        public string Descricao {
            get => _descricao; //montamos uma restrição para não criae um produto sem descrição
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preenvha a descrção!");
                }

                _descricao = value;
            }
        }  
        public double Quantidade { get; set; }
        public double Preco { get; set; }

        public double Total { get => Quantidade * Preco; }

    }
}
