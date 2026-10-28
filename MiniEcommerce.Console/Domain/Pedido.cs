namespace MiniEcommerce.Console.Domain;

public sealed class Pedido
{
    public Pedido(IEnumerable<ItemPedido> itens)
    {
        Id = Guid.NewGuid();
        CriadoEm = DateTimeOffset.Now;
        Itens = itens.ToArray();
    }

    public Guid Id { get; }

    public DateTimeOffset CriadoEm { get; }

    public IReadOnlyList<ItemPedido> Itens { get; }

    public decimal ValorTotal => Itens.Sum(item => item.Subtotal);
}
