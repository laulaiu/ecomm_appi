using Newtonsoft.Json;

namespace E_commerce.Models
{
    public class json
    {
        public class Rootobject
        {

            private int id;
            private string imagem;
            private string img_convert;
            private string nome;
            private float preco;
            private int quantidade;
            private string descricao;
            private int categoria;
            private int fk_id_produto;


            [JsonProperty("Id")]
            public int Id { get { return id; } set { id = value; } }
            [JsonProperty("Fk_id_produto")]
            public int Fk_id_produto { get { return fk_id_produto; } set { fk_id_produto = value; } }
            [JsonProperty("Imagem")]
            public string Imagem { get { return imagem; } set { imagem = value; } }
            [JsonProperty("Img_convert")]
            public string Img_convert { get { return img_convert; } set { img_convert = value; } }
            [JsonProperty("Nome")]
            public string Nome { get { return nome; } set { nome = value; } }
            [JsonProperty("Preco")]
            public float Preco { get { return preco; } set { preco = value; } }
            [JsonProperty("Quantidade")]
            public int Quantidade { get { return quantidade; } set { quantidade= value; } }
            [JsonProperty("Descricao")]
            public string Descricao { get { return descricao; } set { descricao = value; } }
            [JsonProperty("Categoria")]
            public int Categoria { get { return categoria; } set { categoria= value; } }
        }
    }
}