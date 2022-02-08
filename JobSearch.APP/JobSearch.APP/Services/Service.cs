using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace JobSearch.APP.Services
{
    public  abstract class Service
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "https://xamarinforms2020apialex.azurewebsites.net";

        public Service()
        {
            _client = new HttpClient();
        }
    }



}
