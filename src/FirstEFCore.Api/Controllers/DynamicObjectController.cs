using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FirstEFCore.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FirstEFCore.Api.Controllers
{
    [ApiController]
    public class DynamicObjectController : ControllerBase
    {
        [HttpPost]
        [Route("api/v1/dynamic")]
        public async Task<IActionResult> Post()
        {
            if (Request == null)
            {
                return BadRequest();
            }

            try
            {
                var recv = await new StreamReader(Request.Body).ReadToEndAsync();
                if (string.IsNullOrEmpty(recv))
                {
                    return BadRequest();
                }

                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(recv);
                ObjectToDictionaryHelper.ConvertJsonToDictionary(json);
                
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
