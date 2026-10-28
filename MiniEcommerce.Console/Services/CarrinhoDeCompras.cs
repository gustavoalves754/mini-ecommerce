using MiniEcommerce.Console.Domain;

namespace MiniEcommerce.Console.Services;

public sealed class CarrinhoDeCompras
{
    private readonly List<ItemCarrinho> _itens = [];

    public IReadOnlyList<ItemCarrinho> Itens => _itens.AsReadOnly();

    public decimal Total => _itens.Sum(item => item.Subtotal);

    public void AdicionarItem(Produto produto, int quantidade)
    {
        ArgumentNullException.ThrowIfNull(produto);

        if (quantidade <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser positiva.");
        }

        var itemExistente = _itens.FirstOrDefault(item => item.Produto.Id == produto.Id);
        var quantidadeNoCarrinho = itemExistente?.Quantidade ?? 0;

        if (quantidadeNoCarrinho + quantidade > produto.QuantidadeEstoque)
        {
            throw new InvalidOperationException("A quantidade solicitada excede o estoque disponível.");
        }

        if (itemExistente is null)
        {
            _itens.Add(new ItemCarrinho(produto, quantidade));
            return;
        }

        itemExistente.AdicionarQuantidade(quantidade);
    }

    public bool RemoverItem(int produtoId)
    {
        var item = _itens.FirstOrDefault(item => item.Produto.Id == produtoId);
        return item is not null && _itens.Remove(item);
    }

    internal void Limpar()
    {
        _itens.Clear();
    }
}
