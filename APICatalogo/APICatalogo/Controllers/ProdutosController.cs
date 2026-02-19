using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase //controller serve para criar endpoints de API, endpoints são as rotas que a API vai expor
    {
        private readonly IProdutoRepository _repository; //injetando o repositório de produtos no controlador, para que ele possa ser usado para acessar os dados dos produtos

        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        //primeiro mettodo action
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _repository.GetProdutos().ToList();
            if(produtos.Count == 0)
            {
                return NotFound("Nenhum produto encontrado");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _repository.GetProduto(id);
            if (produto is null)
            {
                return NotFound("Nenhum produto encontrado pelo id");
            }
            return Ok(produto);
        }

        [HttpPost ]
        public ActionResult<Produto> Post(Produto produto)
        {
            var novoProduto = _repository.Create(produto);
          
            return CreatedAtAction(nameof(Get), new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Produto> Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest("Id do produto não corresponde ao id da URL");
            }
            _repository.Update(produto);
            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Produto> Delete(int id)
        {
           var produto = _repository.Delete(id);
          
            return Ok(produto);
        }

    }
}
