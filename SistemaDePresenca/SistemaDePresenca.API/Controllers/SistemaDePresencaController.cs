using Microsoft.AspNetCore.Mvc;
using SistemaDePresenca.API.Entities;
using SistemaDePresenca.API.Persistence;
using System;

namespace SistemaDePrenca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaDePresencaController : ControllerBase
    {
        private readonly SistemaDePresencaDbContext _context;
        public SistemaDePresencaController(SistemaDePresencaDbContext context)
        {
            this._context = context;
        }

        // api/Materias GET
        //Busca todas matérias
        [HttpGet]
        public IActionResult GetAll()
        {
            var materias = _context.Materias.Where(m => m.EstaAtiva).ToList();
            return Ok(materias);
        }

        // api/Materias/123 GET
        //Busca a matéria pelo ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var materia = _context.Materias.SingleOrDefault(m => m.Id == id);
            
            if (materia == null || materia.EstaAtiva == false)
            {
                return NotFound();
            }

            return Ok(materia);
        }

        // api/Materias POST
        //Adiciona matéria ao banco
        [HttpPost]
        public IActionResult Post(Materia materia)
        {
            _context.Materias.Add(materia);

            return CreatedAtAction(nameof(GetById), new { id = materia.Id }, materia);
        }

        // api/Materias/123 PUT
        //Altera matéria do banco chamando o método Update da classe matéria
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Materia materia)
        {
            var m = _context.Materias.SingleOrDefault(m => m.Id == id);

            if (m == null)
            {
                return NotFound();
            }

            m.Update(materia.Nome,
                     materia.FrequenciaExigida,
                     materia.NumeroDeFaltas,
                     materia.TotalDeAulas,
                     materia.EstaAtiva);
            
            return NoContent();
        }

        // api/Materias/123 DELETE
        //Altera o atributo bool IsDeleted da materia p/ true chamando a classe Delete da classe materia
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var m = _context.Materias.SingleOrDefault(m => m.Id == id);

            if (m == null)
            {
                return NotFound();
            }

            m.Delete();
            return NoContent();
        }
    
    }
}
