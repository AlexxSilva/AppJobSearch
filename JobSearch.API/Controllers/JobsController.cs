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
    public class JobsController : ControllerBase
    {
        private const int numberOfRegistryByPage = 5;
        private JobSearchContext _data;
        public JobsController(JobSearchContext data)
        {
            _data = data;
        }

        [HttpGet]
        public IEnumerable<Job> GetJobs(string word, string cityState, int numberOfPage = 1)
        {

            //Tratar situação em que os parametros são nulos
            if (word == null)
                word = "";

            if (cityState == null)
                cityState = "";

            //TODO ADD - PAGINACAO

            //Limitar pesquisa por periodo

            var totalItems =
                 _data.Jobs.Where(a =>
            a.PublicationDate > DateTime.Now.AddDays(-15) &&
            a.CityState.ToLower().Contains(cityState.ToLower()) &&
            (
                    a.JobTitle.ToLower().Contains(word.ToLower()) ||
                    a.TecnologyTools.ToLower().Contains(word.ToLower()) ||
                    a.Company.ToLower().Contains(word.ToLower())
            )).Count();

            Response.Headers.Add("X-Total-Itens", totalItems.ToString());

            return _data.Jobs.Where(a =>
            a.PublicationDate > DateTime.Now.AddDays(-15) &&
            a.CityState.ToLower().Contains(cityState.ToLower()) &&
            (
                    a.JobTitle.ToLower().Contains(word.ToLower()) ||
                    a.TecnologyTools.ToLower().Contains(word.ToLower()) ||
                    a.Company.ToLower().Contains(word.ToLower())
            ))
                .Skip(numberOfRegistryByPage * (numberOfPage-1))
                .Take(numberOfRegistryByPage)    
               .ToList<Job>();
        }

        [HttpGet("{id}")]
        public IActionResult GetJob(int id)
        {


            Job JobDb = _data.Jobs.Find(id);
            if (JobDb == null)
            {
                return NotFound();
            }
            return new JsonResult(JobDb);
        }



        [HttpPost]
        public IActionResult AddJob(Job job)
        {
            job.PublicationDate = DateTime.Now;
            _data.Jobs.Add(job);
            _data.SaveChanges();

            return CreatedAtAction(nameof(GetJob), new
            {
               id = job.Id,
            }, job);
        }
    }
}
