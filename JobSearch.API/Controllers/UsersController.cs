using JobSearch.API.Database;
using JobSearch.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private JobSearchContext _data;
        public UsersController(JobSearchContext data)
        {
            _data = data;
        }

        //http://seusite.com.br/api/Users?email=alex@mtcsoft.com&password=1234
        [HttpGet]
        public IActionResult GetUser(string email, string password)
        {
            User UserDb = _data.Users.FirstOrDefault(
                s => s.Email == email && s.Password == password);

            if (UserDb is null)
            {
                return NotFound(); //HTTP - 404 - Erro do Cliente - nao encontrado o usuario
            }
            else
            {
                return new JsonResult(UserDb); // Retorna um JSON com os dados
            }

        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            //TODO VALIDACAO...

            _data.Users.Add(user);
            _data.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { email = user.Email,
                password = user.Password }, user); //200- OK carregou a tela 201 - Criou um elemento 
        }
    }
}
