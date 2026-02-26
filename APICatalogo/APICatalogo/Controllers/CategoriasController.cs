using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
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
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _unitOfWork.CategoriaRepository.GetAll();

           var categoriasDto = categorias.ToCategoriaDTOList(); //método de extensão para converter a lista de categorias em lista de categoriasDTO, para evitar expor as entidades diretamente na API

            return Ok(categoriasDto);
        }



        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(id);
            if (categoria is null)
            {
                return NotFound("Nenhuma categoria encontrada pelo id");
            }

            var categoriaDto2 = categoria.ToCategoriaDTO(); //método de extensão para converter a entidade categoria em categoriaDTO, para evitar expor as entidades diretamente na API

            return Ok(categoria);

        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            
            var categoria = categoriaDto.ToCategoria(); //método de extensão para converter a categoriaDTO em entidade categoria, para evitar expor as entidades diretamente na API

            var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.Commit();//salva as alterações no banco de dados


            var novaCategoriaDto = categoriaCriada.ToCategoriaDTO(); //método de extensão para converter a entidade categoria criada em categoriaDTO, para evitar expor as entidades diretamente na API

            return CreatedAtRoute("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest("Id da categoria não corresponde ao id da URL");
            }

            var categoria = categoriaDto.ToCategoria(); //método de extensão para converter a categoriaDTO em entidade categoria, para evitar expor as entidades diretamente na API

            var categoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();
            
            var categoriaAtualizadaDto = categoriaAtualizada.ToCategoriaDTO(); //método de extensão para converter a entidade categoria atualizada em categoriaDTO, para evitar expor as entidades diretamente na API

            return Ok(categoriaAtualizadaDto);

        }   

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(id);
            if (categoria is null)
            {
                return NotFound("Nenhuma categoria encontrada pelo id");
            }

            var categoriaExcluida = _unitOfWork.CategoriaRepository.Delete(id);

                _unitOfWork.Commit();
    
           var categoriaExcluidaDto = categoriaExcluida.ToCategoriaDTO(); //método de extensão para converter a entidade categoria excluída em categoriaDTO, para evitar expor as entidades diretamente na API

            return Ok(categoriaExcluidaDto);

        }



    }
}
