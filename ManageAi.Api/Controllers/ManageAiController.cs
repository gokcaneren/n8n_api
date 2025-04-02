using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ManageAi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageAiController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string n8nUrl = "http://localhost:5678/webhook/task-recived";

        public ManageAiController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> SendTaskToAi([FromBody]MovieRecommendation movieRecommendation)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(movieRecommendation), Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            var result = await client.PostAsync(n8nUrl, jsonContent);

            if (result.IsSuccessStatusCode)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
