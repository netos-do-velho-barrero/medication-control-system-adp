using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class TelaRequisicaoEntrada : TelaBase<RequisicaoEntrada>, ITelaOpcoes, ITelaCrud
{
    private readonly IRepositorio<Medicamento> repositorioMedicamento;
    private readonly IRepositorio<Funcionario> repositorioFuncionario;

    public TelaRequisicaoEntrada(
        IRepositorio<RequisicaoEntrada> repositorio,
        IRepositorio<Medicamento> repositorioMedicamento,
        IRepositorio<Funcionario> repositorioFuncionario)
        : base("Requisição de Entrada", repositorio)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public override string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Requisições de Entrada");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Registrar requisição de entrada");
        Console.WriteLine("4 - Visualizar requisições de entrada");
        Console.WriteLine("S - Voltar");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        return Console.ReadLine()?.ToUpper();
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Requisições de Entrada");

        List<RequisicaoEntrada> requisicoes = repositorio.SelecionarTodos();

        if (requisicoes.Count == 0)
        {
            Console.WriteLine("Nenhuma requisição de entrada registrada.");
            return;
        }

        Console.WriteLine("{0,-7} | {1,-12} | {2,-25} | {3,-20} | {4,-10}",
            "ID", "Data", "Medicamento", "Funcionário", "Qtd.");
        Console.WriteLine(new string('-', 85));

        foreach (RequisicaoEntrada req in requisicoes)
        {
            Console.WriteLine("{0,-7} | {1,-12} | {2,-25} | {3,-20} | {4,-10}",
                req.Id,
                req.Data.ToString("dd/MM/yyyy"),
                req.Medicamento?.Nome ?? "N/A",
                req.Funcionario?.Nome ?? "N/A",
                req.Quantidade);
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine();
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override RequisicaoEntrada ObterDadosCadastrais()
    {
        Console.Write("Data (dd/MM/yyyy): ");
        string dataInput = Console.ReadLine() ?? string.Empty;
        DateTime data = DateTime.ParseExact(dataInput, "dd/MM/yyyy", null);

        Medicamento? medicamento = SelecionarMedicamento();

        Funcionario? funcionario = SelecionarFuncionario();

        Console.Write("Quantidade: ");
        int quantidade = int.Parse(Console.ReadLine() ?? "0");

        return new RequisicaoEntrada
        {
            Data = data,
            Medicamento = medicamento,
            Funcionario = funcionario,
            Quantidade = quantidade
        };
    }

    // Sobrescreve Cadastrar para atualizar o estoque ao registrar a entrada
    public new void Cadastrar()
    {
        ExibirCabecalho("Registrar Requisição de Entrada");

        try
        {
            RequisicaoEntrada novaRequisicao = ObterDadosCadastrais();

            List<string> erros = novaRequisicao.Validar();

            if (erros.Count > 0)
            {
                Notificador.ExibirMensagensErro(erros);
                Cadastrar();
                return;
            }

            // Atualiza o estoque do medicamento
            novaRequisicao.Medicamento!.QuantidadeEmEstoque += novaRequisicao.Quantidade;
            repositorioMedicamento.Editar(novaRequisicao.Medicamento.Id, novaRequisicao.Medicamento);

            repositorio.Cadastrar(novaRequisicao);

            Notificador.ExibirMensagem(
                $"Entrada registrada com sucesso! Estoque de '{novaRequisicao.Medicamento.Nome}' " +
                $"atualizado para {novaRequisicao.Medicamento.QuantidadeEmEstoque} unidades.");
        }
        catch (FormatException)
        {
            Notificador.ExibirMensagem("O formato de um dos campos está inválido. Verifique a data.");
            Cadastrar();
        }
        catch (Exception)
        {
            Notificador.ExibirMensagem("Ocorreu um erro inesperado. Tente novamente.");
            Cadastrar();
        }
    }

    // Requisição de entrada não permite edição nem exclusão — apenas registro e visualização
    public new void Editar()
    {
        Notificador.ExibirMensagem("Requisições de entrada não podem ser editadas.");
    }

    public new void Excluir()
    {
        Notificador.ExibirMensagem("Requisições de entrada não podem ser excluídas.");
    }

    private Medicamento? SelecionarMedicamento()
    {
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        if (medicamentos.Count == 0)
        {
            Console.WriteLine("Nenhum medicamento cadastrado.");
            return null;
        }

        Console.WriteLine();
        Console.WriteLine("Medicamentos disponíveis:");
        Console.WriteLine("{0,-7} | {1,-25} | {2,-10}", "ID", "Nome", "Estoque");
        Console.WriteLine(new string('-', 48));

        foreach (Medicamento m in medicamentos)
            Console.WriteLine("{0,-7} | {1,-25} | {2,-10}", m.Id, m.Nome, m.QuantidadeEmEstoque);

        Console.WriteLine();
        Console.Write("Digite o ID do medicamento: ");
        string id = Console.ReadLine() ?? string.Empty;

        return repositorioMedicamento.SelecionarPorId(id);
    }

    private Funcionario? SelecionarFuncionario()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        if (funcionarios.Count == 0)
        {
            Console.WriteLine("Nenhum funcionário cadastrado.");
            return null;
        }

        Console.WriteLine();
        Console.WriteLine("Funcionários disponíveis:");
        Console.WriteLine("{0,-7} | {1,-25}", "ID", "Nome");
        Console.WriteLine(new string('-', 36));

        foreach (Funcionario f in funcionarios)
            Console.WriteLine("{0,-7} | {1,-25}", f.Id, f.Nome);

        Console.WriteLine();
        Console.Write("Digite o ID do funcionário: ");
        string id = Console.ReadLine() ?? string.Empty;

        return repositorioFuncionario.SelecionarPorId(id);
    }
}