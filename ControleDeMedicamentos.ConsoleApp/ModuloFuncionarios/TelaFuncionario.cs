using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.Utilidades;


namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios
{
  public class TelaFuncionario : TelaBase<Funcionario>, ITelaOpcoes, ITelaCrud
  {
    public TelaFuncionario(IRepositorio<Funcionario> repositorio) : base("Funcionario", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
      if (deveExibirCabecalho)
        ExibirCabecalho("Visualização de Pacientes");

      List<Funcionario> funcionarios = repositorio.SelecionarTodos();

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

    protected override Funcionario ObterDadosCadastrais()
    {
      Console.Write("Digite o nome do Paciente: ");
      string nome = Console.ReadLine() ?? string.Empty;

      Console.Write("Digite o telefone do Paciente. ex: (XX) XXXX-XXXX: ");
      string telefone = Console.ReadLine() ?? string.Empty;

      Console.Write("Digite o CPF. ex: 000.000.000-00: ");
      string cpf = Console.ReadLine() ?? string.Empty;

      return new Funcionario(nome, telefone, cpf);
    }

    protected override List<string> ValidarRegistroDuplicado(Funcionario novaEntidade, string? idIgnorado = null)
    {
      List<string> erros = new List<string>();

      List<Funcionario> funcionarios = repositorio.SelecionarTodos();

      foreach (Funcionario p in funcionarios)
      {
        if (p.Id != idIgnorado && p.Cpf == novaEntidade.Cpf)
        {
          erros.Add($"Já existe um paciente com o Cartao do Sus \"{novaEntidade.Cpf}\"");
          break;
        }

      }
      return erros;
    }
  }
}