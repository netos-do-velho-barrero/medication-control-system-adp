using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;


namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaCrud, ITelaOpcoes
{
  public TelaPaciente(IRepositorio<Paciente> repositorio) : base("Paciente", repositorio)
  {
  }

  public override void VisualizarTodos(bool deveExibirCabecalho)
  {
    if (deveExibirCabecalho)
      ExibirCabecalho("Visualização de Pacientes");

    List<Paciente> pacientes = repositorio.SelecionarTodos();

    if (pacientes.Count == 0)
    {
      Notificador.ExibirMensagem("Nenhum item registrado.");
      return;
    }

    Console.WriteLine(
        "{0, -7} | {1, -10} | {2, -15} | {3, -20} | {4, -15}",
        "Id", "Nome", "Telefone", "Cartao do Sus", "CPF"
    );

    foreach (Paciente p in pacientes)
    {
      Console.WriteLine(
          "{0, -7} | {1, -10} | {2, -15} | {3, -20} | {4, -15}",
          p.Id, p.Nome, p.Telefone, p.CartaoSus, p.Cpf
      );
    }

    if (deveExibirCabecalho)
    {
      Console.WriteLine("---------------------------------");
      Console.Write("Digite ENTER para continuar...");
      Console.ReadLine();
    }

  }

  protected override Paciente ObterDadosCadastrais()
  {
    Console.Write("Digite o nome do Paciente: ");
    string nome = Console.ReadLine() ?? string.Empty;

    Console.Write("Digite o telefone do Paciente. ex: (XX) XXXX-XXXX: ");
    string telefone = Console.ReadLine() ?? string.Empty;

    Console.Write("Digite o Cartao do Sus do Paciente. ex: 0000 0000 0000 000: ");
    string cartaoSus = Console.ReadLine() ?? string.Empty;

    Console.Write("Digite o CPF. ex: 000.000.000-00: ");
    string cpf = Console.ReadLine() ?? string.Empty;

    return new Paciente(nome, telefone, cartaoSus, cpf);
  }

  protected override List<string> ValidarRegistroDuplicado(Paciente novaEntidade, string? idIgnorado = null)
  {
    List<string> erros = new List<string>();

    List<Paciente> pacientes = repositorio.SelecionarTodos();

    foreach (Paciente p in pacientes)
    {
      if (p.Id != idIgnorado && p.CartaoSus == novaEntidade.CartaoSus)
      {
        erros.Add($"Já existe um paciente com o Cartao do Sus \"{novaEntidade.CartaoSus}\"");
        break;
      }

    }
    return erros;
  }

}
