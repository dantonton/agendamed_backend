using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamed_backend.Domain.Entities;
using agendamed_backend.Domain.Results;
using agendamed_backend.Domain.Services;
using agendamed_backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace agendamed_backend.Controllers
{
    //[EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompromissosController : ControllerBase
    {
        private readonly CompromissoService compromissoService;

        public CompromissosController(CompromissoService compromissoService)
        {
            this.compromissoService = compromissoService;
        }
        // GET api/Compromissos
        [HttpGet]
        public async Task<ActionResult<Result<List<Compromisso>>>> Get()
        {
            return await compromissoService.GetAllAsync();
        }

        // GET api/Compromissos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<Compromisso>>> Get(Guid id)
        {
            return await compromissoService.GetByIDAsync(id);
        }

        // POST api/Compromissos
        /*
        [HttpPost]
        public async Task<ActionResult<String>> Post()
        {
            var cc = this.HttpContext;
            return "oioi";
        }*/
        
        [HttpPost]
        public async Task<ActionResult<Result<Compromisso>>> Post([FromBody] Compromisso value)
        {
            return await compromissoService.CreateAsync(value);
        }

        // PUT api/Compromissos/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<Compromisso>>> Put(Guid id, [FromBody] Compromisso value)
        {
            if (id != value.ID)
            {
                var r = new Result<Compromisso>(value);
                r.AddErro("all", "idUrl", "Não é possível alterar esse compromisso apartir desta Url");
                return r;
            }
            return await compromissoService.UpdateAsync(value);
        }

        // DELETE api/Compromissos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<Compromisso>>> Delete(Guid id)
        {

            return await compromissoService.DeleteAsync(id);

        }
    }
}
