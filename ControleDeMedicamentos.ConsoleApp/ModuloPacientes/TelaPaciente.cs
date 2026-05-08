using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaCrud, ITelaOpcoes
{
  public TelaPaciente(IRepositorio<Paciente> repositorio) : base("Paciente", repositorio)
  {
  }

  public override void VisualizarTodos(bool deveExibirCabecalho)
  {
    Console.WriteLine("Visualizando todos os pacientes...");
  }

  protected override Paciente ObterDadosCadastrais()
  {
    return new Paciente();
  }
}
