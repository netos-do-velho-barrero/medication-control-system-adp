using System.Text.Json;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

ContextoJson contexto = new ContextoJson();

try
{
    contexto.Carregar();
}
catch (JsonException)
{
    Notificador.ExibirMensagem("O arquivo de armazenamento está corrompido! Contate o suporte.");
    return;
}

IRepositorio<Paciente> repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
IRepositorio<Fornecedor> repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
IRepositorio<Medicamento> repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
IRepositorio<Funcionario> repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
IRepositorio<RequisicaoEntrada> repositorioRequisicaoEntrada = new RepositorioRequisicaoEntradaEmArquivo(contexto);
IRepositorio<RequisicaoSaida> repositorioRequisicaoSaida = new RepositorioRequisicaoSaidaEmArquivo(contexto);

TelaPrincipal telaPrincipal = new TelaPrincipal(
    repositorioPaciente,
    repositorioFornecedor,
    repositorioMedicamento,
    repositorioFuncionario,
    repositorioRequisicaoEntrada,
    repositorioRequisicaoSaida
);

while (true)
{
    ITelaOpcoes? telaSelecionada = telaPrincipal.ApresentarMenuOpcoesPrincipal();

    if (telaSelecionada == null)
    {
        Console.Clear();
        break;
    }

    // TelaEstoque gerencia seu próprio submenu internamente,
    // por isso só precisa ficar em loop chamando ObterOpcaoMenu()
    if (telaSelecionada is TelaEstoque)
    {
        while (true)
        {
            string? opcao = telaSelecionada.ObterOpcaoMenu();

            if (opcao == "S")
            {
                Console.Clear();
                break;
            }
        }

        continue;
    }

    while (true)
    {
        string? opcaoSubMenu = telaSelecionada.ObterOpcaoMenu();

        if (opcaoSubMenu == "S")
        {
            Console.Clear();
            break;
        }

        if (telaSelecionada is ITelaCrud telaCrud)
        {
            if (opcaoSubMenu == "1")
                telaCrud.Cadastrar();

            else if (opcaoSubMenu == "2")
                telaCrud.Editar();

            else if (opcaoSubMenu == "3")
                telaCrud.Excluir();

            else if (opcaoSubMenu == "4")
                telaCrud.VisualizarTodos(deveExibirCabecalho: true);
        }
    }
}