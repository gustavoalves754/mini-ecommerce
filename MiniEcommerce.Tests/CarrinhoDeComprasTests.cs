using MiniEcommerce.Console.Domain;
using MiniEcommerce.Console.Services;

namespace MiniEcommerce.Tests;

public sealed class CarrinhoDeComprasTests
{
    [Fact]
    public void AdicionarItem_ComEstoqueDisponivel_AdicionaECalculaSubtotal()
    {
        var produto = new Produto(1, "Mouse", 80m, 5);
        var carrinho = new CarrinhoDeCompras();

        carrinho.AdicionarItem(produto, 2);

        var item = Assert.Single(carrinho.Itens);
        Assert.Equal(2, item.Quantidade);
        Assert.Equal(160m, item.Subtotal);
        Assert.Equal(160m, carrinho.Total);
    }

    [Fact]
    public void AdicionarItem_AcimaDoEstoque_LancaExcecaoENaoAdiciona()
    {
        var produto = new Produto(1, "Mouse", 80m, 2);
        var carrinho = new CarrinhoDeCompras();

        var exception = Assert.Throws<InvalidOperationException>(
            () => carrinho.AdicionarItem(produto, 3));

        Assert.Contains("estoque", exception.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(carrinho.Itens);
    }

    [Fact]
    public void AdicionarMesmoProduto_AcimaDoEstoqueTotal_LancaExcecao()
    {
        var produto = new Produto(1, "Mouse", 80m, 3);
        var carrinho = new CarrinhoDeCompras();
        carrinho.AdicionarItem(produto, 2);

        Assert.Throws<InvalidOperationException>(() => carrinho.AdicionarItem(produto, 2));
        Assert.Equal(2, Assert.Single(carrinho.Itens).Quantidade);
    }

    [Fact]
    public void RemoverItem_Existente_RetiraProdutoDoCarrinho()
    {
        var produto = new Produto(1, "Mouse", 80m, 3);
        var carrinho = new CarrinhoDeCompras();
        carrinho.AdicionarItem(produto, 1);

        var removido = carrinho.RemoverItem(produto.Id);

        Assert.True(removido);
        Assert.Empty(carrinho.Itens);
        Assert.Equal(0m, carrinho.Total);
    }
}
