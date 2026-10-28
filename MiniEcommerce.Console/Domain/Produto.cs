namespace MiniEcommerce.Console.Domain;

public sealed class Produto
{
    public Produto(int id, string nome, decimal preco, int quantidadeEstoque)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do produto é obrigatório.", nameof(nome));
        }

        if (preco < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(preco), "O preço não pode ser negativo.");
        }

        if (quantidadeEstoque < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantidadeEstoque),
                "A quantidade em estoque não pode ser negativa.");
        }

        Id = id;
        Nome = nome.Trim();
        Preco = preco;
        QuantidadeEstoque = quantidadeEstoque;
    }

    public int Id { get; }

    public string Nome { get; }

    public decimal Preco { get; }

    public int QuantidadeEstoque { get; private set; }

    internal void BaixarEstoque(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser positiva.");
        }

        if (quantidade > QuantidadeEstoque)
        {
            throw new InvalidOperationException($"Estoque insuficiente para o produto {Nome}.");
        }

        QuantidadeEstoque -= quantidade;
    }
}
