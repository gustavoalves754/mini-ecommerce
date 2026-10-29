# Mini E-commerce

Aplicação de console em .NET 10 que simula o fluxo básico de uma loja: cadastro e consulta de produtos, gerenciamento do carrinho e fechamento de pedidos com baixa automática de estoque.

## Funcionalidades

- Cadastro de produtos com nome, preço e quantidade em estoque.
- Validação contra preço ou estoque negativos.
- Catálogo com preços e estoques atualizados.
- Adição e remoção de itens do carrinho.
- Validação da quantidade solicitada contra o estoque disponível.
- Cálculo de subtotais e total do carrinho.
- Checkout com nova validação e baixa de estoque.
- Resumo financeiro imutável do pedido.
- Testes unitários desenvolvidos com xUnit e fluxo TDD.

## Arquitetura

```text
MiniEcommerce.Console/
├── Domain/
│   ├── Produto.cs          Entidade de produto e regras de estoque
│   ├── ItemCarrinho.cs     Item mantido durante a compra
│   ├── ItemPedido.cs       Registro imutável de um item comprado
│   └── Pedido.cs           Resumo financeiro do pedido
├── Services/
│   ├── CatalogoService.cs       Cadastro e consulta de produtos
│   ├── CarrinhoDeCompras.cs     Regras do carrinho
│   └── PedidoService.cs         Checkout e baixa de estoque
└── Program.cs              Menu interativo

MiniEcommerce.Tests/
├── ProdutoTests.cs
├── CarrinhoDeComprasTests.cs
└── PedidoTests.cs
```

As entidades concentram o estado e as regras fundamentais. Os serviços coordenam os casos de uso, enquanto o projeto de testes valida cada etapa de forma isolada.

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)

Confirme a instalação:

```bash
dotnet --version
```

## Compilar

Na raiz do repositório, execute:

```bash
dotnet build MiniEcommerce.sln
```

## Executar a loja

```bash
dotnet run --project MiniEcommerce.Console/MiniEcommerce.Console.csproj
```

Use o menu para cadastrar produtos, consultar o catálogo, adicionar ou remover itens do carrinho e finalizar o pedido. Os dados ficam somente em memória e são reiniciados quando a aplicação é encerrada.

## Executar os testes

```bash
dotnet test MiniEcommerce.sln
```

A suíte cobre validação de produtos, limites de estoque no carrinho, cálculo de valores, checkout e baixa de estoque.
