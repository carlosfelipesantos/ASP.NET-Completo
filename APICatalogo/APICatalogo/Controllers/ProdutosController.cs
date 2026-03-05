using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase //controller serve para criar endpoints de API, endpoints são as rotas que a API vai expor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProdutosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //primeiro mettodo action
        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll().ToList();
            if(produtos.Count == 0)
            {
                return NotFound("Nenhum produto encontrado");
            }

            //var destino = _mapper.Map<Destino>(origem);
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDto);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.GetById(id);
            if (produto is null)
            {
                return NotFound("Nenhum produto encontrado pelo id");
            }

            //var destino = _mapper.Map<Destino>(origem);
            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }

        [HttpPost ]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
                      

            var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();

            var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

            return CreatedAtAction(nameof(Get), new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
        {
            if(id != produtoDto.ProdutoId)
            {
                return BadRequest("Id do produto não corresponde ao id da URL");
            }

            var produto = _mapper.Map<Produto>(produtoDto);
            var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();

            var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);
           
            return Ok(produtoAtualizadoDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
           var produto = _unitOfWork.ProdutoRepository.Delete(id);

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }

    }
}
