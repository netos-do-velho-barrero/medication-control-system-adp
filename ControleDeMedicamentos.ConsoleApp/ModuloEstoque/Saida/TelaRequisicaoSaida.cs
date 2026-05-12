using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaOpcoes, ITelaCrud
{
    private readonly IRepositorio<Paciente> repositorioPaciente;
    private readonly IRepositorio<Medicamento> repositorioMedicamento;

    public TelaRequisicaoSaida(
        IRepositorio<RequisicaoSaida> repositorio,
        IRepositorio<Paciente> repositorioPaciente,
        IRepositorio<Medicamento> repositorioMedicamento)
        : base("Requisição de Saída", repositorio)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioMedicamento = repositorioMedicamento;
    }

    public override string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Requisições de Saída");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Registrar requisição de saída");
        Console.WriteLine("4 - Visualizar requisições de saída");
        Console.WriteLine("S - Voltar");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        return Console.ReadLine()?.ToUpper();
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Requisições de Saída");

        List<RequisicaoSaida> requisicoes = repositorio.SelecionarTodos();

        if (requisicoes.Count == 0)
        {
            Console.WriteLine("Nenhuma requisição de saída registrada.");
            return;
        }

        foreach (RequisicaoSaida req in requisicoes)
        {
            Console.WriteLine("Id: {0,-7} | Data: {1,-12} | Paciente: {2}",
                req.Id,
                req.Data.ToString("dd/MM/yyyy"),
                req.Paciente?.Nome ?? "N/A");

            Console.WriteLine("  Medicamentos requisitados:");

            foreach (ItemRequisicaoSaida item in req.MedicamentosRequisitados)
            {
                Console.WriteLine("    - {0} | Qtd: {1}",
                    item.Medicamento?.Nome ?? "N/A",
                    item.Quantidade);
            }

            Console.WriteLine(new string('-', 70));
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine();
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override RequisicaoSaida ObterDadosCadastrais()
    {
        Console.Write("Data (dd/MM/yyyy): ");
        string dataInput = Console.ReadLine() ?? string.Empty;
        DateTime data = DateTime.ParseExact(dataInput, "dd/MM/yyyy", null);

        Paciente? paciente = SelecionarPaciente();

        List<ItemRequisicaoSaida> itens = SelecionarMedicamentos();

        return new RequisicaoSaida
        {
            Data = data,
            Paciente = paciente,
            MedicamentosRequisitados = itens
        };
    }

    // Sobrescreve Cadastrar para validar estoque e descontar ao registrar saída
    public new void Cadastrar()
    {
        ExibirCabecalho("Registrar Requisição de Saída");

        try
        {
            RequisicaoSaida novaRequisicao = ObterDadosCadastrais();

            List<string> erros = novaRequisicao.Validar();

            if (erros.Count > 0)
            {
                Notificador.ExibirMensagensErro(erros);
                Cadastrar();
                return;
            }

            // Valida se há estoque suficiente para todos os itens antes de confirmar
            List<string> errosEstoque = new List<string>();

            foreach (ItemRequisicaoSaida item in novaRequisicao.MedicamentosRequisitados)
            {
                if (item.Quantidade > item.Medicamento.QuantidadeEmEstoque)
                {
                    errosEstoque.Add(
                        $"Estoque insuficiente para '{item.Medicamento.Nome}'. " +
                        $"Disponível: {item.Medicamento.QuantidadeEmEstoque}, Solicitado: {item.Quantidade}.");
                }
            }

            if (errosEstoque.Count > 0)
            {
                Notificador.ExibirMensagensErro(errosEstoque);
                Cadastrar();
                return;
            }

            // Desconta o estoque de cada medicamento
            foreach (ItemRequisicaoSaida item in novaRequisicao.MedicamentosRequisitados)
            {
                item.Medicamento.QuantidadeEmEstoque -= item.Quantidade;
                repositorioMedicamento.Editar(item.Medicamento.Id, item.Medicamento);
            }

            repositorio.Cadastrar(novaRequisicao);

            Notificador.ExibirMensagem($"Requisição de saída \"{novaRequisicao.Id}\" registrada com sucesso!");
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

    // Requisição de saída não permite edição nem exclusão
    public new void Editar()
    {
        Notificador.ExibirMensagem("Requisições de saída não podem ser editadas.");
    }

    public new void Excluir()
    {
        Notificador.ExibirMensagem("Requisições de saída não podem ser excluídas.");
    }

    private Paciente? SelecionarPaciente()
    {
        List<Paciente> pacientes = repositorioPaciente.SelecionarTodos();

        if (pacientes.Count == 0)
        {
            Console.WriteLine("Nenhum paciente cadastrado.");
            return null;
        }

        Console.WriteLine();
        Console.WriteLine("Pacientes disponíveis:");
        Console.WriteLine("{0,-7} | {1,-25}", "ID", "Nome");
        Console.WriteLine(new string('-', 36));

        foreach (Paciente p in pacientes)
            Console.WriteLine("{0,-7} | {1,-25}", p.Id, p.Nome);

        Console.WriteLine();
        Console.Write("Digite o ID do paciente: ");
        string id = Console.ReadLine() ?? string.Empty;

        return repositorioPaciente.SelecionarPorId(id);
    }

    private List<ItemRequisicaoSaida> SelecionarMedicamentos()
    {
        List<ItemRequisicaoSaida> itens = new List<ItemRequisicaoSaida>();
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        if (medicamentos.Count == 0)
        {
            Console.WriteLine("Nenhum medicamento cadastrado.");
            return itens;
        }

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Medicamentos disponíveis:");
            Console.WriteLine("{0,-7} | {1,-25} | {2,-10}", "ID", "Nome", "Estoque");
            Console.WriteLine(new string('-', 48));

            foreach (Medicamento m in medicamentos)
            {
                // Desconta do estoque exibido os itens já adicionados nesta requisição
                int qtdJaAdicionada = itens
                    .Where(i => i.Medicamento.Id == m.Id)
                    .Sum(i => i.Quantidade);

                int estoqueDisponivel = m.QuantidadeEmEstoque - qtdJaAdicionada;

                if (m.EstaEmFalta || estoqueDisponivel <= 0)
                    Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("{0,-7} | {1,-25} | {2,-10}", m.Id, m.Nome, estoqueDisponivel);
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.Write("Digite o ID do medicamento (ou ENTER para finalizar): ");
            string idMedicamento = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(idMedicamento))
                break;

            Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(idMedicamento);

            if (medicamento == null)
            {
                Console.WriteLine("Medicamento não encontrado. Tente novamente.");
                continue;
            }

            Console.Write($"Quantidade de '{medicamento.Nome}': ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade <= 0)
            {
                Console.WriteLine("Quantidade inválida. Tente novamente.");
                continue;
            }

            // Verifica se o medicamento já foi adicionado nesta requisição
            ItemRequisicaoSaida? itemExistente = itens.FirstOrDefault(i => i.Medicamento.Id == medicamento.Id);

            if (itemExistente != null)
                itemExistente.Quantidade += quantidade;
            else
                itens.Add(new ItemRequisicaoSaida { Medicamento = medicamento, Quantidade = quantidade });

            Console.WriteLine($"'{medicamento.Nome}' adicionado.");
        }

        return itens;
    }
}