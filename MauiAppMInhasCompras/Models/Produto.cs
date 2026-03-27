using SQLite;

namespace MauiAppMinhasCompras.Models
{

    public class Produto
    {
        string _descricao;

        [PrimaryKey, AutoIncrement] // que vem do SQLite
        public int Id { get; set; }
        public string Descricao {
            get => _descricao; //montamos uma restrição para não criar um produto sem descrição
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição!");
                }

                _descricao = value; //caso houver valor, o aviso não aparecerá
            }
        }  
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public string Categoria { get; set; }

        public double Total { get => Quantidade * Preco; }

    }
}
