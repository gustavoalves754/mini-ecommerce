using MiniEcommerce.Console.Domain;
using MiniEcommerce.Console.Services;

var catalogo = new CatalogoService();
var carrinho = new CarrinhoDeCompras();
var pedidoService = new PedidoService();
var executando = true;

while (executando)
{
    ExibirMenu();
    var opcao = System.Console.ReadLine()?.Trim();
    System.Console.WriteLine();

    try
    {
        switch (opcao)
        {
            case "1":
                CadastrarProduto(catalogo);
                break;
            case "2":
                ExibirCatalogo(catalogo);
                break;
            case "3":
                AdicionarAoCarrinho(catalogo, carrinho);
                break;
            case "4":
                RemoverDoCarrinho(carrinho);
                break;
            case "5":
                ExibirCarrinho(carrinho);
                break;
            case "6":
                FinalizarPedido(carrinho, pedidoService);
                break;
            case "0":
                executando = false;
                System.Console.WriteLine("Loja encerrada. Até logo!");
                break;
            default:
                System.Console.WriteLine("Opção inválida.");
                break;
        }
    }
    catch (Exception exception) when (exception is ArgumentException or InvalidOperationException)
    {
        System.Console.WriteLine($"Não foi possível concluir a operação: {exception.Message}");
    }

    System.Console.WriteLine();
}

static void ExibirMenu()
{
    System.Console.WriteLine("================================");
    System.Console.WriteLine("        MINI E-COMMERCE");
    System.Console.WriteLine("================================");
    System.Console.WriteLine("1 - Cadastrar produto");
    System.Console.WriteLine("2 - Listar catálogo");
    System.Console.WriteLine("3 - Adicionar produto ao carrinho");
    System.Console.WriteLine("4 - Remover produto do carrinho");
    System.Console.WriteLine("5 - Exibir carrinho");
    System.Console.WriteLine("6 - Finalizar pedido");
    System.Console.WriteLine("0 - Sair");
    System.Console.Write("Escolha uma opção: ");
}

static void CadastrarProduto(CatalogoService catalogo)
{
    System.Console.Write("Nome do produto: ");
    var nome = System.Console.ReadLine();

    if (!LerDecimal("Preço: R$ ", out var preco))
    {
        return;
    }

    if (!LerInteiro("Quantidade em estoque: ", out var estoque))
    {
        return;
    }

    var produto = catalogo.CadastrarProduto(nome, preco, estoque);
    System.Console.WriteLine($"Produto cadastrado com o ID {produto.Id}.");
}

static void ExibirCatalogo(CatalogoService catalogo)
{
    var produtos = catalogo.ListarProdutos();

    if (produtos.Count == 0)
    {
        System.Console.WriteLine("O catálogo está vazio.");
        return;
    }

    System.Console.WriteLine("--- Catálogo ---");

    foreach (var produto in produtos)
    {
        System.Console.WriteLine(
            $"ID {produto.Id} | {produto.Nome} | {produto.Preco:C} | Estoque: {produto.QuantidadeEstoque}");
    }
}

static void AdicionarAoCarrinho(CatalogoService catalogo, CarrinhoDeCompras carrinho)
{
    ExibirCatalogo(catalogo);

    if (!LerInteiro("ID do produto: ", out var produtoId) ||
        !LerInteiro("Quantidade desejada: ", out var quantidade))
    {
        return;
    }

    var produto = catalogo.BuscarPorId(produtoId)
        ?? throw new InvalidOperationException("Produto não encontrado.");

    carrinho.AdicionarItem(produto, quantidade);
    System.Console.WriteLine("Produto adicionado ao carrinho.");
}

static void RemoverDoCarrinho(CarrinhoDeCompras carrinho)
{
    ExibirCarrinho(carrinho);

    if (!LerInteiro("ID do produto que deseja remover: ", out var produtoId))
    {
        return;
    }

    System.Console.WriteLine(carrinho.RemoverItem(produtoId)
        ? "Produto removido do carrinho."
        : "Produto não encontrado no carrinho.");
}

static void ExibirCarrinho(CarrinhoDeCompras carrinho)
{
    if (carrinho.Itens.Count == 0)
    {
        System.Console.WriteLine("O carrinho está vazio.");
        return;
    }

    System.Console.WriteLine("--- Carrinho ---");

    foreach (var item in carrinho.Itens)
    {
        System.Console.WriteLine(
            $"ID {item.Produto.Id} | {item.Produto.Nome} | {item.Quantidade} x {item.Produto.Preco:C} = {item.Subtotal:C}");
    }

    System.Console.WriteLine($"Total atual: {carrinho.Total:C}");
}

static void FinalizarPedido(CarrinhoDeCompras carrinho, PedidoService pedidoService)
{
    var pedido = pedidoService.FinalizarPedido(carrinho);

    System.Console.WriteLine("--- Pedido confirmado ---");
    System.Console.WriteLine($"Número: {pedido.Id}");

    foreach (var item in pedido.Itens)
    {
        System.Console.WriteLine(
            $"{item.NomeProduto}: {item.Quantidade} x {item.PrecoUnitario:C} = {item.Subtotal:C}");
    }

    System.Console.WriteLine($"Valor total: {pedido.ValorTotal:C}");
}

static bool LerInteiro(string mensagem, out int valor)
{
    System.Console.Write(mensagem);

    if (int.TryParse(System.Console.ReadLine(), out valor))
    {
        return true;
    }

    System.Console.WriteLine("Informe um número inteiro válido.");
    return false;
}

static bool LerDecimal(string mensagem, out decimal valor)
{
    System.Console.Write(mensagem);

    if (decimal.TryParse(System.Console.ReadLine(), out valor))
    {
        return true;
    }

    System.Console.WriteLine("Informe um valor decimal válido.");
    return false;
}
