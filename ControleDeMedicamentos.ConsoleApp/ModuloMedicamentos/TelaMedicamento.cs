using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaOpcoes, ITelaCrud
{
    private readonly IRepositorio<Fornecedor> repositorioFornecedor;

    public TelaMedicamento(
        IRepositorio<Medicamento> repositorio,
        IRepositorio<Fornecedor> repositorioFornecedor)
        : base("Medicamento", repositorio)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Medicamentos");

        List<Medicamento> medicamentos = repositorio.SelecionarTodos();

        if (medicamentos.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum medicamento cadastrado.");
            return;
        }

        Console.WriteLine("{0,-7} | {1,-25} | {2,-30} | {3,-10} | {4,-20} | {5,-10}",
            "ID", "Nome", "Descrição", "Qtd.", "Fornecedor", "Situação");
        Console.WriteLine(new string('-', 115));

        foreach (Medicamento medicamento in medicamentos)
        {
            string situacao = medicamento.EstaEmFalta ? "EM FALTA" : "OK";
            string nomeFornecedor = medicamento.Fornecedor?.Nome ?? "N/A";

            if (medicamento.EstaEmFalta)
                Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("{0,-7} | {1,-25} | {2,-30} | {3,-10} | {4,-20} | {5,-10}",
                medicamento.Id,
                medicamento.Nome,
                medicamento.Descricao.Length > 28 ? medicamento.Descricao.Substring(0, 28) + ".." : medicamento.Descricao,
                medicamento.QuantidadeEmEstoque,
                nomeFornecedor,
                situacao);

            Console.ResetColor();
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine();
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Medicamento ObterDadosCadastrais()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Descrição: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Console.Write("Quantidade em estoque: ");
        int quantidade = int.Parse(Console.ReadLine() ?? "0");

        Fornecedor? fornecedorSelecionado = SelecionarFornecedor();

        return new Medicamento
        {
            Nome = nome,
            Descricao = descricao,
            QuantidadeEmEstoque = quantidade,
            Fornecedor = fornecedorSelecionado
        };
    }

    // Regra: ao cadastrar, se o medicamento já existir (mesmo nome), apenas atualiza a quantidade
    public new void Cadastrar()
    {
        ExibirCabecalho($"Cadastro de {nomeEntidade}");

        try
        {
            Medicamento novaEntidade = ObterDadosCadastrais();

            List<string> erros = novaEntidade.Validar();

            if (erros.Count > 0)
            {
                Notificador.ExibirMensagensErro(erros);
                Cadastrar();
                return;
            }

            // Regra: se já existir medicamento com mesmo nome, apenas soma a quantidade
            Medicamento? existente = repositorio.SelecionarTodos()
                .FirstOrDefault(m => m.Nome.Equals(novaEntidade.Nome, StringComparison.OrdinalIgnoreCase));

            if (existente != null)
            {
                existente.QuantidadeEmEstoque += novaEntidade.QuantidadeEmEstoque;
                // Persiste via editar para acionar o Salvar() no repositório em arquivo
                repositorio.Editar(existente.Id, existente);
                Notificador.ExibirMensagem(
                    $"O medicamento '{existente.Nome}' já estava cadastrado. Quantidade atualizada para {existente.QuantidadeEmEstoque} unidades.");
                return;
            }

            repositorio.Cadastrar(novaEntidade);

            Notificador.ExibirMensagem($"O registro \"{novaEntidade.Id}\" foi cadastrado com sucesso!");
        }
        catch (FormatException)
        {
            Utilidades.Notificador.ExibirMensagem("O formato do valor de um dos campos está inválido.");
            Cadastrar();
        }
        catch (Exception)
        {
            Utilidades.Notificador.ExibirMensagem("Ocorreu um erro inesperado. Tente novamente.");
            Cadastrar();
        }
    }

    private Fornecedor? SelecionarFornecedor()
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

        if (fornecedores.Count == 0)
        {
            Console.WriteLine("Nenhum fornecedor cadastrado. Cadastre um fornecedor primeiro.");
            return null;
        }

        Console.WriteLine();
        Console.WriteLine("Fornecedores disponíveis:");
        Console.WriteLine("{0,-7} | {1,-30}", "ID", "Nome");
        Console.WriteLine(new string('-', 42));

        foreach (Fornecedor fornecedor in fornecedores)
            Console.WriteLine("{0,-7} | {1,-30}", fornecedor.Id, fornecedor.Nome);

        Console.WriteLine();
        Console.Write("Digite o ID do fornecedor: ");
        string idFornecedor = Console.ReadLine() ?? string.Empty;

        return repositorioFornecedor.SelecionarPorId(idFornecedor);
    }
}