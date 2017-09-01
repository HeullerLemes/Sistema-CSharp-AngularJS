using aulaWebApi.Authorization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using aulaWebApi.Models;

namespace aulaWebApi.Controllers
{
    public class LoginController : ApiController
    {
        private aulaWebApiContext db = new aulaWebApiContext();
        Cliente cliente = new Cliente();
        Prestador prestador = new Prestador();

        [HttpPost]
        public IHttpActionResult Login([FromBody] JObject data)
        {
            string username = data["username"]?.ToString();
            string password = data["password"]?.ToString();

            IAuthProvider provider = new JWTSimple();
            Dictionary<string, string> fields = new Dictionary<string, string>();

            cliente = db.Clientes.SingleOrDefault(user => user.Username == username);
            if (cliente != null && cliente.Senha.Equals(password))
            {
                string tipo = "cliente";
                fields["sub"] = username;
                fields["tipo"] = tipo;

                string token = provider.CreateToken(new string[] {"list"}, fields);

                return new LoginOkResult<string>("", this)
                {
                    Token = token
                };
            }
            else
            {
                prestador = db.Prestadors.SingleOrDefault(userPres => userPres.Username == username);
                if (prestador != null && prestador.Senha.Equals(password))
                {
                    string tipo = "prestador";
                    fields["sub"] = username;
                    fields["tipo"] = tipo;

                    string token = provider.CreateToken(new string[] { "list" }, fields);

                    return new LoginOkResult<string>("", this)
                    {
                        Token = token
                    };
                }
            }

            return StatusCode(System.Net.HttpStatusCode.Forbidden);

        }
    }
}
