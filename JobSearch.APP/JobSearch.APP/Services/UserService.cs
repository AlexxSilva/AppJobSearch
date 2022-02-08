using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JobSearch.APP.Models;
using JobSearch.Domain.Models;
using Newtonsoft.Json;

namespace JobSearch.APP.Services
{
    public class UserService : Service
    {
        public async Task<ResponseService<User>> GetUser(string email, string password)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Users?email={email}&password={password}");

            ResponseService<User> responseService = new ResponseService<User>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;//Retorna o codigo // no tostring retorna a mensagem do erro

            if (response.IsSuccessStatusCode)
            {
                //Precisa do Jsonstring content =await response.Content.ReadAsStringAsync();
                responseService.Data = await response.Content.ReadAsAsync<User>();
                
            }
            else 
            {
                string problemResponse= await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<User>>(problemResponse);
                responseService.Errors = erros.Errors; 
            }
            return responseService;
        }

        public async Task<ResponseService<User>> AddUser(User user)
        {
            //_client.PostAsync()
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Users", user);

            ResponseService<User> responseService = new ResponseService<User>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode;//Retorna o codigo // no tostring retorna a mensagem do erro

            if (response.IsSuccessStatusCode)   
            {
                //Precisa do Jsonstring content =await response.Content.ReadAsStringAsync();
                responseService.Data = await response.Content.ReadAsAsync<User>();

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
