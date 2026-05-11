using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaOpcoes, ITelaCrud
{
    public TelaFornecedor(IRepositorio<Fornecedor> repositorio) : base("Fornecedor", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Fornecedores");

        List<Fornecedor> fornecedores = repositorio.SelecionarTodos();

        if (fornecedores.Count == 0)
        {
            Console.WriteLine("Nenhum fornecedor cadastrado.");
            return;
        }

        Console.WriteLine("{0,-7} | {1,-30} | {2,-20} | {3,-18}",
            "ID", "Nome", "Telefone", "CNPJ");
        Console.WriteLine(new string('-', 85));

        foreach (Fornecedor fornecedor in fornecedores)
        {
            Console.WriteLine("{0,-7} | {1,-30} | {2,-20} | {3,-18}",
                fornecedor.Id,
                fornecedor.Nome,
                fornecedor.Telefone,
                fornecedor.Cnpj);
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine();
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Fornecedor ObterDadosCadastrais()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Telefone: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("CNPJ: ");
        string cnpj = Console.ReadLine() ?? string.Empty;

        return new Fornecedor
        {
            Nome = nome,
            Telefone = telefone,
            Cnpj = cnpj
        };
    }

    protected override List<string> ValidarRegistroDuplicado(Fornecedor novaEntidade, string? idIgnorado = null)
    {
        List<string> erros = new List<string>();

        string cnpjNovo = novaEntidade.CnpjSemFormatacao();

        foreach (Fornecedor fornecedor in repositorio.SelecionarTodos())
        {
            if (idIgnorado != null && fornecedor.Id == idIgnorado)
                continue;

            if (fornecedor.CnpjSemFormatacao() == cnpjNovo)
            {
                erros.Add($"Já existe um fornecedor cadastrado com o CNPJ '{novaEntidade.Cnpj}'.");
                break;
            }
        }

        return erros;
    }
}