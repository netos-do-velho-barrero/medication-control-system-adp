
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
      erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

    if (Telefone.Length < 14 || Telefone.Length > 15)
      erros.Add("O campo \"Telefone\" deve ser preenchido corretamente: (XX) XXXX-XXXX");

    if (CartaoSus.Length != 15)
      erros.Add("O campo \"CartaoSus\" deve conter 15 digitos.");

    if (Cpf.Length != 11)
      erros.Add("O campo \"Cpf\" deve conter 11 digitos.");

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
