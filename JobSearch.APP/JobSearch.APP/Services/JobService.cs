using JobSearch.APP.Models;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.APP.Services
{
    public class JobService : Service
    {
        public async Task<ResponseService<List<Job>>> GetJobs(string word, string cityState, int numberOfPage = 1)
        {
          
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Jobs?word={word}&cityState={cityState}&numberOfPage={numberOfPage}");

            ResponseService<List<Job>> responseService = new ResponseService<List<Job>>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;//Retorna o codigo // no tostring retorna a mensagem do erro

            if (response.IsSuccessStatusCode)
            {
                //Precisa do Jsonstring content =await response.Content.ReadAsStringAsync();
                responseService.Data = await response.Content.ReadAsAsync<List<Job>>();

                var pagination = new Pagination()
                {
                    IsPagination = true,
                    TotalItems = int.Parse(response.Headers.GetValues("x-total-itens").FirstOrDefault())
                };

                responseService.Pagination = pagination;

            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<List<Job>>>(problemResponse);               
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<Job>> GetJob(int id)
        {
            ///api/Jobs/2

            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Jobs/{id}");

            ResponseService<Job> responseService = new ResponseService<Job>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;//Retorna o codigo // no tostring retorna a mensagem do erro

            if (response.IsSuccessStatusCode)
            {
                //Precisa do Jsonstring content =await response.Content.ReadAsStringAsync();
                responseService.Data = await response.Content.ReadAsAsync<Job>();

            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<Job>>(problemResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;




        }

        public async Task<ResponseService<Job>> AddJob(Job job)
        {
            //_client.PostAsync()
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Jobs", job);

            ResponseService<Job> responseService = new ResponseService<Job>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;//Retorna o codigo // no tostring retorna a mensagem do erro

            if (response.IsSuccessStatusCode)
            {
                //Precisa do Jsonstring content =await response.Content.ReadAsStringAsync();
                responseService.Data = await response.Content.ReadAsAsync<Job>();

            }
            else
            {
                string problemResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<User>>(problemResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }
    }
}
