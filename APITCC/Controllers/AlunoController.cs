using APITCC.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APITCC.Controllers
{
    public class AlunoController : ApiController
    {
        //constroi a classe de controle dos dados do aluno
        AlunosControle controle = new AlunosControle();

        //endoint de solicitação de registro do aluno
        //*1 se o aluno existir retorna OK e objeto do aluno
        //*2 se não encontrar retorna not found
        //*3 se não ocorreu algum erro
        public HttpResponseMessage GetRegistro(string email, string password)
        {
            try
            {
                Alunos aluno = controle.Registra(email, password);
                if (aluno == null)
                {
                    //*2
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhum registro encontrado");
                }
                //*1
                return Request.CreateResponse(HttpStatusCode.OK, aluno);
            }
            catch (Exception ex)
            {
                //*3
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }
    }
}
