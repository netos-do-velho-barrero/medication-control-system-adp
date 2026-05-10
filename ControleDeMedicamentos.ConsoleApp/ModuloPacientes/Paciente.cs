
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class Paciente : EntidadeBase
{
  public string Nome { get; set; }

  public string Telefone { get; set; }

  public string CartaoSus { get; set; }

  public string Cpf { get; set; }

  public Paciente()
  {
  }

  public Paciente(
    string nome,
    string telefone,
    string cartaoSus,
    string cpf
   )
  {
    Nome = nome;
    Telefone = telefone;
    CartaoSus = cartaoSus;
    Cpf = cpf;
  }

  public override List<string> Validar()
  {
    List<string> erros = new List<string>();

    if (Nome.Length < 3 || Nome.Length > 100)
      erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

    if (Telefone.Length < 14 || Telefone.Length > 15)
      erros.Add("O campo \"Telefone\" deve ser preenchido corretamente e seguir essa estrutura: (XX) XXXX-XXXX");

    if (CartaoSus.Length != 18)
      erros.Add("O campo \"CartaoSus\" deve conter 15 digitos e seguir essa estrutura: 0000 0000 0000 000");

    if (Cpf.Length != 14)
      erros.Add("O campo \"Cpf\" deve conter 11 digitos e seguir essa estrutura: 000.000.000-00");

    return erros;
  }


  public override void AtualizarDados(EntidadeBase entidadeAtualizada)
  {
      Paciente pacienteAtualizado = (Paciente)entidadeAtualizada;

      Nome = pacienteAtualizado.Nome;
      Telefone = pacienteAtualizado.Telefone;
      CartaoSus = pacienteAtualizado.CartaoSus;
      Cpf = pacienteAtualizado.Cpf;
  }

}
