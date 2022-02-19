using E_commerce.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_commerce.Controllers
{
    public class Controle_webController : Controller
    {

        int msg;
        bool vdd = false;

        // GET: controle_web
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login_dados(String email, String senha, string grupo1)
        {
            String vdd;
            String retorno ;


            if (grupo1.Equals("2"))
            {
                retorno = "Cadastro";
            }
            else
            {
                Cadastro_cliente cc = new Cadastro_cliente();
                cc.Email = email;
                cc.Senha = senha;

                vdd = cc.verifica_login();
                TempData["msg_login"] = vdd;
                if (vdd.Equals("true"))
                {
                    retorno = "index";
                }
                else
                {
                    retorno = " Login";
                }

            }


            return RedirectToAction(retorno);
        }


        public ActionResult Cadastro_cliente(String nome_completo_cliente, String cpf_cliente, String telefone_cliente, String cep_cliente, String data_nasciment_clientes, String email_cliente, String senha_cliente)
        {
            Cadastro_cliente cc = new Cadastro_cliente();

            cc.Nome = nome_completo_cliente;
            cc.Cpf = cpf_cliente;
            cc.Telefone = telefone_cliente;
            cc.Cep = cep_cliente;
            cc.Data_nascimento = data_nasciment_clientes;
            cc.Email = email_cliente;
            cc.Senha = senha_cliente;
            cc.cadastro();
            return RedirectToAction("Index");

        }
        
        public ActionResult excluir_produto(int id)
        {
            Produtos pd = new Produtos();
            pd.deleteProduto(id);
            return RedirectToAction("Adm"); 

        }

        public async Task<ActionResult> Adm()
        {            
            Produtos pd = new Produtos();
            List<Produtos> lista_retorno = new List<Produtos>();
            lista_retorno  = await pd.getProdutosAsync();
            return View(lista_retorno);            
        }

        [HttpPost]
        public ActionResult Adm(string nome_produto, float preco_produto, int quantidade_produto,
                                string descricao_produto, int categoria_produto, HttpPostedFileBase imagem_produto)
        {
    
            Produtos pd= new Produtos();             
            if ((imagem_produto.ContentLength< 2081745) & (imagem_produto != null))
            {
                byte[] bytesArquivo = new byte[imagem_produto.ContentLength];
                imagem_produto.InputStream.Read(bytesArquivo, 0, imagem_produto.ContentLength);
                pd.cadastroProduto(nome_produto,preco_produto,quantidade_produto,descricao_produto,categoria_produto, bytesArquivo); 
            }
            else
            {
                TempData["msg_tamanho_foto"] = "Erro tamanho da imagem maior que 2mb: " + this.msg;
            }
            
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public async Task<ActionResult> Produto_categoria1()
        {
            /*List<modelo_web> lista_produto = modelo_web.getProdutos();
            return View(lista_produto);*/
            
            Produtos pd = new Produtos();
            List<Produtos> lista_retorno = new List<Produtos>();
            lista_retorno = await pd.getProdutosAsync();
            return View(lista_retorno);
        }

        public async Task<ActionResult> Produto_categoria2()
        {
            /*List<modelo_web> lista_produto = modelo_web.getProdutos();
            return View(lista_produto);*/
            
            Produtos pd = new Produtos();
            List<Produtos> lista_retorno = new List<Produtos>();
            lista_retorno = await pd.getProdutosAsync();
            return View(lista_retorno);
        }

        public ActionResult Carrinho()
        {
            return View();
        }

    }
}