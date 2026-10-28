using MiniEcommerce.Console.Domain;

namespace MiniEcommerce.Console.Services;

public sealed class PedidoService
{
    public Pedido FinalizarPedido(CarrinhoDeCompras carrinho)
    {
        ArgumentNullException.ThrowIfNull(carrinho);

        if (carrinho.Itens.Count == 0)
        {
            throw new InvalidOperationException("Não é possível finalizar um carrinho vazio.");
        }

        ValidarEstoque(carrinho.Itens);

        var itensPedido = carrinho.Itens
            .Select(item => new ItemPedido(
                item.Produto.Id,
                item.Produto.Nome,
                item.Produto.Preco,
                item.Quantidade))
            .ToArray();

        foreach (var item in carrinho.Itens)
        {
            item.Produto.BaixarEstoque(item.Quantidade);
        }

        var pedido = new Pedido(itensPedido);
        carrinho.Limpar();
        return pedido;
    }

    private static void ValidarEstoque(IEnumerable<ItemCarrinho> itens)
    {
        foreach (var item in itens)
        {
            if (item.Quantidade > item.Produto.QuantidadeEstoque)
            {
                throw new InvalidOperationException(
                    $"Estoque insuficiente para o produto {item.Produto.Nome}.");
            }
        }
    }
}
