using calculadoraa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace calculadoraa.Controllers
{
    [RoutePrefix("api")]
    public class CalculadoraMController : ApiController
    {
        [HttpPost]
        [Route("suma/{Numero1}/{Numero2}")]
        public IHttpActionResult Suma([FromBody]Models.ML calculadora)
        {
            var respuesta = calculadora.Numero1 + calculadora.Numero2;
            return Ok(respuesta);
        }

        [HttpPost]
        [Route("resta/{Numero1}/{Numero2}")]
        public IHttpActionResult Resta([FromBody]Models.ML calculadora)
        {
            var respuesta = calculadora.Numero1 - calculadora.Numero2;
            return Ok(respuesta);
        }

        [HttpPost]
        [Route("multiplicacion/{Numero1}/{Numero2}")]
        public IHttpActionResult Multimplcacion([FromBody]Models.ML calculadora)
        {
            var respuesta = calculadora.Numero1 * calculadora.Numero2;
            return Ok(respuesta);
        }

        [HttpPost]
        [Route("divicion/{Numero1}/{Numero2}")]
        public IHttpActionResult Divicion([FromBody]Models.ML calculadora)
        {
            var respuesta = calculadora.Numero1 / calculadora.Numero2;
            return Ok(respuesta);
        }
    }
}
