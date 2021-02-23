using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodingChallenge.Models;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CodingChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _client;

        private readonly ILogger<HomeController> _logger;
        public IOptions<ApplicationSettings> _applicationSettings { get; }

        public HomeController(ILogger<HomeController> logger, IOptions<ApplicationSettings> applicationSettings, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _applicationSettings = applicationSettings;
            _client = clientFactory;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var apiUrl = this._applicationSettings.Value.CodingChallengeApiUrl;


            // CALL getASum
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl + "/Home/getASum/2/3");
            var client = _client.CreateClient();

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<int>(result);

            // CALL getAProduct
            request = new HttpRequestMessage(HttpMethod.Get, apiUrl + "/Home/getAProduct/" + value);
            response = await client.SendAsync(request);
            result = await response.Content.ReadAsStringAsync();
            value = JsonConvert.DeserializeObject<int>(result);


            // CALL getAPower
            request = new HttpRequestMessage(HttpMethod.Get, apiUrl + "/Home/getAPower/" + value);
            response = await client.SendAsync(request);
            result = await response.Content.ReadAsStringAsync();
            value = JsonConvert.DeserializeObject<int>(result);

            return View(new ChallengeModel() { Result = value });
        }
    }
}
