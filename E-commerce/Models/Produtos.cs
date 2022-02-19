
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace E_commerce.Models
{
    public class Produtos
    {

        //private string link2 = "http://localhost:4433/";
        private string link1 = ReadSetting("api_link");

        private int id;
        private byte[] imagem;        
        private String img_convert;
        private String nome;
        private float preco;
        private int quantidade;
        private String descricao;
        private int categoria;
        private int fk_id_produto;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public byte[] Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }

        public string Img_convert
        {
            get { return img_convert; }
            set { img_convert = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public float Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public int Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public int Fk_id_produto
        {
            get { return fk_id_produto; }
            set { fk_id_produto = value; }
        }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                return "Error reading app settings";
            }
        }

        public async Task<List<Produtos>> getProdutosAsync()
        {
            try
            {
                HttpClient cl = new HttpClient { BaseAddress = new Uri(link1) };
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await cl.GetAsync("/api/Cadastros");
                var content = await response.Content.ReadAsStringAsync();
                var prods = JsonConvert.DeserializeObject<Produtos[]>(content);
                List<Produtos> lista_retorno = new List<Produtos>();
                foreach (Produtos item in prods)
                {
                    Produtos obj = new Produtos();
                    obj.Id = item.Id;
                    obj.Nome = item.Nome;
                    obj.Preco = item.Preco;
                    obj.Descricao = item.Descricao;
                    obj.Categoria = item.Categoria;
                    obj.Imagem = item.Imagem;
                    obj.Img_convert = item.Img_convert;
                    obj.Quantidade = item.Quantidade;
                    lista_retorno.Add(obj);
                }
                return lista_retorno;    
            }
            catch (Exception ae)
            {
                return null;
            }
            
            
        }

        public String deleteProduto( int id)
        {
            HttpClient cl = new HttpClient { BaseAddress = new Uri(link1) };
            try{
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = cl.DeleteAsync($"/api/Cadastros?id={id}");
                return response.Result + "";
            }
            catch (Exception ae)
            {
                return ae+ "";
            }
  
        }


        //enviar solicitação por post mas esta dando ruim
        public String cadastroProduto(string nome_produto, float preco_produto, int quantidade_produto,
            string descricao_produto, int categoria_produto, byte[]  imagem_produto)
        {
            HttpClient cl = new HttpClient { BaseAddress = new Uri(link1) };
            try
            {
                
                //string img = Convert.ToBase64String(bytesArquivo);
                               
                //Cria um objeto de produtos para tranformar aquele objeto em json               
                Produtos produtos_c = new Produtos() { Nome = nome_produto,Preco = preco_produto, Quantidade = quantidade_produto,
                    Descricao = descricao_produto, Categoria =categoria_produto ,imagem = imagem_produto};                
                var json = JsonConvert.SerializeObject(produtos_c);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = cl.PostAsync($"/api/Cadastros", data);
                return "Cadastrado";
            }
            catch (Exception ae)
            {
                return "Erro: "+ae;
            }

        }

    }

}