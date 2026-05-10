using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Cpf{ get; set; }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        throw new NotImplementedException();
    }

    public override List<string> Validar()
    {
        throw new NotImplementedException();
    }
}