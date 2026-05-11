using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;


namespace ControleDeMedicamentos.ConsoleApp.Utilidades;

public class TelaPrincipal
{
    private readonly IRepositorio<Fornecedor> repositorioFornecedor;

    private readonly IRepositorio<Paciente> repositorioPaciente;

    private readonly IRepositorio<Funcionario> repositorioFuncionario;

    public TelaPrincipal(IRepositorio<Paciente> repositorioPaciente,
     IRepositorio<Fornecedor> repositorioFornecedor,
    IRepositorio<Funcionario> repositorioFuncionario)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public ITelaOpcoes? ApresentarMenuOpcoesPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Controle de Medicamentos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gerenciar Pacientes");
        Console.WriteLine("2 - Gestão de Fornecedores");
        Console.WriteLine("3 - Gestão de Funcionarios");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaPaciente(repositorioPaciente);

        if (opcaoMenuPrincipal == "2")
            return new TelaFornecedor(repositorioFornecedor);

        if (opcaoMenuPrincipal == "3")
            return new TelaFuncionario(repositorioFuncionario);

        return null;
    }
}