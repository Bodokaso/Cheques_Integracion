using Cheques_Integracion.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Cheques_Integracion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsientoContableController : Controller
    {
        private readonly string apiUrl = "https://contasys.azurewebsites.net/api/AsientosContables/InsertarAsientoContable";
        private readonly string apiKey = "fhhfjjppppf6666";

        private static AsientoCheques consultaAsiento;
        [HttpPost("EnviarAsiento")]
        public async Task<IActionResult> EnviarAsiento([FromBody] AsientoCheques asiento)
        {
            try
            {
                if (asiento.cuentas.Count != 2)
                {
                    return BadRequest("El asiento debe contener exactamente 2 cuentas.");
                }


                string json = JsonConvert.SerializeObject(asiento);

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        consultaAsiento = asiento;
                        return Ok("Datos enviados exitosamente.");
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Error al enviar los datos.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("ConsultaAsiento")]
        public IActionResult GetConsultaAsiento()
        {
            if (consultaAsiento != null)
            {
                return Ok(consultaAsiento);
            }
            else
            {
                return NotFound("No hay ningún asiento enviado previamente.");
            }
        }
    }
}
