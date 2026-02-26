using APICatalogo.Context;
using APICatalogo.DTOs;
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

            var categoriasDto = new List<CategoriaDTO>();   
            foreach (var categoria in categorias)
                            {
                var categoriaDto = new CategoriaDTO
                {
                    CategoriaId = categoria.CategoriaId,
                    Nome = categoria.Nome,
                    ImagemUrl = categoria.ImagemUrl
                };
                categoriasDto.Add(categoriaDto);
            }

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

            //mapeamento manual de Categoria para CategoriaDTO, para evitar expor a entidade diretamente na API
            var categoriaDto = new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

            return Ok(categoria);

        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            
            var categoria = new Categoria
            {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl
            };

            var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.Commit();//salva as alterações no banco de dados


            var NovacategoriaDto = new CategoriaDTO
            {
                CategoriaId = categoriaCriada.CategoriaId,
                Nome = categoriaCriada.Nome,
                ImagemUrl = categoriaCriada.ImagemUrl
            };

            return CreatedAtRoute("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest("Id da categoria não corresponde ao id da URL");
            }

            var categoria = new Categoria
            {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl
            };

           var categoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();
            

            var categoriaAtualizadaDto = new CategoriaDTO
            {
                CategoriaId = categoriaAtualizada.CategoriaId,
                Nome = categoriaAtualizada.Nome,
                ImagemUrl = categoriaAtualizada.ImagemUrl
            };
           
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
    
                var categoriaExcluidaDto = new CategoriaDTO
                {
                    CategoriaId = categoriaExcluida.CategoriaId,
                    Nome = categoriaExcluida.Nome,
                    ImagemUrl = categoriaExcluida.ImagemUrl
                };
            return Ok(categoriaExcluidaDto);

        }



    }
}
