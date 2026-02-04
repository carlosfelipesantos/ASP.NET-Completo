using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "VALUES ('Coca-Cola', 'Refrigerante de cola 350ml', 5.00, 'coca_cola.png', 100, GETDATE(), " +
                "(SELECT Id FROM Categorias WHERE Nome = 'Bebidas'))");

            migrationBuilder.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "VALUES ('Hambúrguer', 'Hambúrguer artesanal com queijo e alface', 15.00, 'hamburguer.png', 50, GETDATE(), " +
                "(SELECT Id FROM Categorias WHERE Nome = 'Lanches'))");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Produtos");
        }
    }
}
