using Oracle.DataAccess.Client;
using System;

namespace APITCC.Models
{
    //Classe de controle para acesso ao banco
    public class AlunosControle
    {
        public AlunosControle()
        {

        }
        public Alunos Registra(string email, string password)
        {
            // função para retornar o aluno conectando no banco pela string do arquivo webconfig
            try
            {                                    
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Source"].ConnectionString;
                using (var conn = new OracleConnection(connectionString))
                {
                    //abre conexão
                    conn.Open();
                    using (var comm = new OracleCommand())
                    {
                        comm.Connection = conn;
                        //prepara a sql
                        comm.CommandText = "SELECT * FROM ALUNOS WHERE EMAIL = :email AND SENHA = :senha";                    
                        comm.Parameters.Add("email", email);
                        comm.Parameters.Add("senha", password);
                        //sql executada e passada para o adaptador receber os dados da tabela selecionada
                        var adapter = new OracleDataAdapter(comm);
                        var dataTable = new System.Data.DataTable();
                        adapter.Fill(dataTable);
                        //realiza um loop caso tenha dado no retorno da consulta e popula objeto aluno
                        foreach (System.Data.DataRow row in dataTable.Rows)
                        {
                            Alunos aluno = new Alunos();
                            aluno.Cpf = (string)row["CPF"];
                            aluno.Nome = (string)row["NOME"];
                            aluno.Endereco = (string)row["ENDERECO"];
                            aluno.Bairro = (string)row["BAIRRO"];
                            aluno.Municipio = (string)row["MUNICIPIO"];
                            aluno.Uf = (string)row["UF"];
                            aluno.Telefone = (string)row["TELEFONE"];
                            aluno.Email = (string)row["EMAIL"];
                            aluno.Senha = (string)row["SENHA"];
                            //retorna o objeto aluno com os dados caso encontre por email e senha, senão retorna null
                            return aluno;
                        }
                    }                    
                }                           
                return null;
            }
            //caso ocorra algum erro realiza o throw na exception para a classe que a invocou
            catch (Exception e)
            {                
                throw e;
            }           
        }
    }
}