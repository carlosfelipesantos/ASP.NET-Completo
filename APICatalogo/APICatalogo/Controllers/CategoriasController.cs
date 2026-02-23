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
        private readonly IUnitOfWork _unitOfWork; //injeção do unit of work para acessar os repositórios
        private readonly IRepository<Categoria> _repository; //injeção do repositório para acessar os dados

        public CategoriasController(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _unitOfWork.CategoriaRepository.GetAll();
            return Ok(categorias);
        }



        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(id);
            if (categoria is null)
            {
                return NotFound("Nenhuma categoria encontrada pelo id");
            }
            return Ok(categoria);

        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
         
            var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);

            return CreatedAtRoute("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Id da categoria não corresponde ao id da URL");
            }

            _unitOfWork.CategoriaRepository.Update(categoria);
            return Ok(categoria);

        }   

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(id);
            if (categoria is null)
            {
                return NotFound("Nenhuma categoria encontrada pelo id");
            }

            var categoriaExcluida = _unitOfWork.CategoriaRepository.Delete(id);
            return Ok(categoriaExcluida);

        }



    }
}
