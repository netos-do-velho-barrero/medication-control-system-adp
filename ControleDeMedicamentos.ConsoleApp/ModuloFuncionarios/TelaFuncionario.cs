using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class TelaFuncionario : TelaBase<Fornecedor>, ITelaOpcoes, ITelaCrud
{
    public TelaFuncionario(IRepositorio<Fornecedor> repositorio) : base("Funcionario", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        throw new NotImplementedException();
    }

    protected override Fornecedor ObterDadosCadastrais()
    {
        throw new NotImplementedException();
    }
}