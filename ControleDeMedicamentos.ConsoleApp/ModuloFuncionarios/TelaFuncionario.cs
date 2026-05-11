using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaOpcoes, ITelaCrud
{
    public TelaFuncionario(IRepositorio<Funcionario> repositorio) : base("Funcionario", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Pacientes");

        List<Funcionario> funcionarios = repositorio.SelecionarTodos(); //repositorio.SelecionarTodos();

        if (funcionarios.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum item registrado.");
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -10} | {2, -15} | {3, -20} | {4, -15}",
            "Id", "Nome", "Telefone", "Cartao do Sus", "CPF"
        );

        foreach (Funcionario f in funcionarios)
        {
            Console.WriteLine(
                "{0, -7} | {1, -10} |  {3, -20} | {4, -15}",
                f.Id, f.Nome, f.Telefone, f.Cpf
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Funcionario ObterDadosCadastrais()
    {
        throw new NotImplementedException();
    }
}