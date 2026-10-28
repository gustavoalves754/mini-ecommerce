namespace MiniEcommerce.Console.Domain;

public sealed record ItemPedido(
    int ProdutoId,
    string NomeProduto,
    decimal PrecoUnitario,
    int Quantidade)
{
    public decimal Subtotal => PrecoUnitario * Quantidade;
}
