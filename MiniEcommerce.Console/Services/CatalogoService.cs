using MiniEcommerce.Console.Domain;

namespace MiniEcommerce.Console.Services;

public sealed class CatalogoService
{
    private readonly List<Produto> _produtos = [];
    private int _proximoId = 1;

    public Produto CadastrarProduto(string? nome, decimal preco, int quantidadeEstoque)
    {
        var produto = new Produto(_proximoId, nome ?? string.Empty, preco, quantidadeEstoque);
        _produtos.Add(produto);
        _proximoId++;
        return produto;
    }

    public IReadOnlyList<Produto> ListarProdutos()
    {
        return _produtos.AsReadOnly();
    }

    public Produto? BuscarPorId(int id)
    {
        return _produtos.FirstOrDefault(produto => produto.Id == id);
    }
}
