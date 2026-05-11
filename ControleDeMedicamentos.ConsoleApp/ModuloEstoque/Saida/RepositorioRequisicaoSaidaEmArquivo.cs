using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class RepositorioRequisicaoSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicaoSaida>, IRepositorio<RequisicaoSaida>
{
    public RepositorioRequisicaoSaidaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<RequisicaoSaida> CarregarRegistros()
    {
        return contexto.RequisicoesSaida;
    }
}