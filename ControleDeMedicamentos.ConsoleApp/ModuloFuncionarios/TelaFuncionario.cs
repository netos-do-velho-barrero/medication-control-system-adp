using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;


namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class TelaFuncionario : TelaBase<Fornecedor>, ITelaOpcoes, ITelaCrud
{
  public TelaFuncionario(IRepositorio<Fornecedor> repositorio) : base("Funcionario", repositorio)
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

    foreach (Funcionario p in funcionarios)
    {
      Console.WriteLine(
          "{0, -7} | {1, -10} |  {3, -20} | {4, -15}",
          p.Id, p.Nome, p.Telefone, p.Cpf
      );
    }

    if (deveExibirCabecalho)
    {
      Console.WriteLine("---------------------------------");
      Console.Write("Digite ENTER para continuar...");
      Console.ReadLine();
    }
  }

  protected override Fornecedor ObterDadosCadastrais()
  {
    throw new NotImplementedException();
  }
}