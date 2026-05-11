using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

namespace ControleDeMedicamentos.ConsoleApp.Utilidades;

public class TelaPrincipal
{
    private readonly IRepositorio<Paciente> repositorioPaciente;
    private readonly IRepositorio<Fornecedor> repositorioFornecedor;
    private readonly IRepositorio<Medicamento> repositorioMedicamento;
    private readonly IRepositorio<Funcionario> repositorioFuncionario;
    private readonly IRepositorio<RequisicaoEntrada> repositorioRequisicaoEntrada;
    private readonly IRepositorio<RequisicaoSaida> repositorioRequisicaoSaida;

    public TelaPrincipal(
        IRepositorio<Paciente> repositorioPaciente,
        IRepositorio<Fornecedor> repositorioFornecedor,
        IRepositorio<Medicamento> repositorioMedicamento,
        IRepositorio<Funcionario> repositorioFuncionario,
        IRepositorio<RequisicaoEntrada> repositorioRequisicaoEntrada,
        IRepositorio<RequisicaoSaida> repositorioRequisicaoSaida)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
        this.repositorioRequisicaoSaida = repositorioRequisicaoSaida;
    }

    public ITelaOpcoes? ApresentarMenuOpcoesPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Controle de Medicamentos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gestão de Fornecedores");
        Console.WriteLine("2 - Gestão de Pacientes");
        Console.WriteLine("3 - Gestão de Medicamentos");
        Console.WriteLine("4 - Gestão de Funcionários");
        Console.WriteLine("5 - Gestão de Estoque");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaFornecedor(repositorioFornecedor);

        if (opcaoMenuPrincipal == "2")
            return new TelaPaciente(repositorioPaciente);

        if (opcaoMenuPrincipal == "3")
            return new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);

        if (opcaoMenuPrincipal == "4")
            return new TelaFuncionario(repositorioFuncionario);

        if (opcaoMenuPrincipal == "5")
            return new TelaEstoque(
                repositorioRequisicaoEntrada,
                repositorioRequisicaoSaida,
                repositorioMedicamento,
                repositorioFuncionario,
                repositorioPaciente);

        return null;
    }
}