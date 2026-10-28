using MiniEcommerce.Console.Domain;
using MiniEcommerce.Console.Services;

namespace MiniEcommerce.Tests;

public sealed class PedidoTests
{
    [Fact]
    public void FinalizarPedido_ComItens_DecrementaEstoqueEGeraResumoFinanceiro()
    {
        var teclado = new Produto(1, "Teclado", 200m, 5);
        var mouse = new Produto(2, "Mouse", 80m, 4);
        var carrinho = new CarrinhoDeCompras();
        carrinho.AdicionarItem(teclado, 2);
        carrinho.AdicionarItem(mouse, 1);
        var checkout = new PedidoService();

        var pedido = checkout.FinalizarPedido(carrinho);

        Assert.Equal(480m, pedido.ValorTotal);
        Assert.Equal(2, pedido.Itens.Count);
        Assert.Equal(3, teclado.QuantidadeEstoque);
        Assert.Equal(3, mouse.QuantidadeEstoque);
        Assert.Empty(carrinho.Itens);
    }

    [Fact]
    public void FinalizarPedido_ComCarrinhoVazio_LancaExcecao()
    {
        var carrinho = new CarrinhoDeCompras();
        var checkout = new PedidoService();

        var exception = Assert.Throws<InvalidOperationException>(
            () => checkout.FinalizarPedido(carrinho));

        Assert.Contains("vazio", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void FinalizarPedido_QuandoEstoqueMudou_NaoProcessaCompra()
    {
        var produto = new Produto(1, "Mouse", 80m, 2);
        var primeiroCarrinho = new CarrinhoDeCompras();
        primeiroCarrinho.AdicionarItem(produto, 2);
        var segundoCarrinho = new CarrinhoDeCompras();
        segundoCarrinho.AdicionarItem(produto, 1);
        var checkout = new PedidoService();
        checkout.FinalizarPedido(primeiroCarrinho);

        Assert.Throws<InvalidOperationException>(() => checkout.FinalizarPedido(segundoCarrinho));
        Assert.Equal(0, produto.QuantidadeEstoque);
        Assert.Single(segundoCarrinho.Itens);
    }
}
