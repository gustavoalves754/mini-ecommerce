namespace MiniEcommerce.Console.Domain;

public sealed class ItemCarrinho
{
    public ItemCarrinho(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
    }

    public Produto Produto { get; }

    public int Quantidade { get; private set; }

    public decimal Subtotal => Produto.Preco * Quantidade;

    internal void AdicionarQuantidade(int quantidade)
    {
        Quantidade += quantidade;
    }
}
