using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly ICategoriaRepository _repository; //injeção do repositório para acessar os dados

        public CategoriasController(ICategoriaRepository repository) //pedindo injecao do contexto que sera informada pelo container di nativo
        {
            _repository = repository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _repository.GetCategorias();
            return Ok(categorias);
        }



        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _repository.GetCategoria(id);
            if (categoria is null)
            {
                return NotFound("Nenhuma categoria encontrada pelo id");
            }
            return Ok(categoria);

        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
         
            var categoriaCriada = _repository.CreateCategoria(categoria);

            return CreatedAtRoute("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Id da categoria não corresponde ao id da URL");
            }
   
            _repository.UpdateCategoria(categoria);
            return Ok(categoria);

        }   

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _repository.GetCategoria(id);
            if (categoria is null)
            {
                return NotFound("Nenhuma categoria encontrada pelo id");
            }

            var categoriaExcluida = _repository.DeleteCategoria(id);
            return Ok(categoriaExcluida);

        }



    }
}
