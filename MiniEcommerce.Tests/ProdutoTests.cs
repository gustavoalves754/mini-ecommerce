using MiniEcommerce.Console.Services;

namespace MiniEcommerce.Tests;

public sealed class ProdutoTests
{
    [Fact]
    public void CadastrarProduto_ComDadosValidos_AdicionaAoCatalogo()
    {
        var catalogo = new CatalogoService();

        var produto = catalogo.CadastrarProduto("Teclado", 199.90m, 8);

        Assert.Equal("Teclado", produto.Nome);
        Assert.Equal(199.90m, produto.Preco);
        Assert.Equal(8, produto.QuantidadeEstoque);
        Assert.Contains(produto, catalogo.ListarProdutos());
    }

    [Fact]
    public void CadastrarProduto_ComPrecoNegativo_LancaExcecao()
    {
        var catalogo = new CatalogoService();

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => catalogo.CadastrarProduto("Teclado", -1m, 8));

        Assert.Contains("preço", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(catalogo.ListarProdutos());
    }

    [Fact]
    public void CadastrarProduto_ComEstoqueNegativo_LancaExcecao()
    {
        var catalogo = new CatalogoService();

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => catalogo.CadastrarProduto("Teclado", 199.90m, -1));

        Assert.Contains("estoque", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(catalogo.ListarProdutos());
    }
}
