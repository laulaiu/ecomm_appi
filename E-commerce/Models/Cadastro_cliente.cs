using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerce.Models
{
    public class Cadastro_cliente
    {

        private String nome;
        private String cpf;
        private String telefone;
        private String cep;
        private String email;
        private String senha;
        private String endereço;
        private String data_nascimento;        
        String emailC, senhaC;
        private String msg;

        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public String Cpf
        {
            get { return cpf; }
            set {cpf = value; }
        }

        public String Telefone
        {
            get {return telefone; }
            set { telefone = value; }
        }

        public String Cep
        {
            get { return cep; }
            set { cep = value; }
        }
        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        public String Endereco
        {
            get { return endereço; }
            set { endereço = value; }
        }

        public String Data_nascimento
        {
            get { return Data_nascimento;  }
            set { Data_nascimento = value; }
        }


        private const String conexao = "server=localhost; database = e_comerce; user id= root; password= admin";

        public String cadastro()
        {
            MySqlConnection conecta = new MySqlConnection(conexao);
            try
            {
                conecta.Open();
                MySqlCommand query = new MySqlCommand("insert into cliente (cpf, nome_completo, telefone, endereco, cep, data_nascimento, email, senha )" +
                    "value (@cpf, @nome_completo, @telefone ,@endereco, @cep, @data_nascimento, @email, @senha)", conecta);
                query.Parameters.AddWithValue("cpf",this.cpf);
                query.Parameters.AddWithValue("nome_completo",this.nome);
                query.Parameters.AddWithValue("telefone",this.telefone);
                query.Parameters.AddWithValue("endereco",this.Endereco);
                query.Parameters.AddWithValue("cep",this.cep);
                query.Parameters.AddWithValue("data_nascimento",this.data_nascimento);
                query.Parameters.AddWithValue("email",this.email);
                query.Parameters.AddWithValue("senha",this.senha);

                query.ExecuteNonQuery();

                this.msg = "Cadastro efetuado com secesso!";
                

            }catch(Exception ae)
            {
                this.msg = "Erro ao efetuar cadastro: " + ae;
            }
            finally
            {
                conecta.Close();
            }

            return this.msg;
        }

        public String verifica_login()
        {
            MySqlConnection conecta = new MySqlConnection(conexao);
            

            try
            {
                MySqlCommand query = new MySqlCommand("select email,senha from cliente where email = @email and senha = @senha", conecta);
                query.Parameters.AddWithValue("@email",this.email);
                query.Parameters.AddWithValue("@senha",this.senha);
                MySqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {                    
                    emailC = leitor["email"].ToString();
                    senhaC = leitor["senha"].ToString();
                }

                if((this.email.Equals(this.emailC)) & (this.senha.Equals(this.senhaC))){
                    this.msg = "true";
                }

            }catch(Exception ae)
            {
                this.msg = "false "+ ae;
            }
            finally
            {
                conecta.Close();
            }


            return this.msg;
        }





    }
}