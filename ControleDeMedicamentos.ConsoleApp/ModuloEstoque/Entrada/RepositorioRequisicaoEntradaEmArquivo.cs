using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class RepositorioRequisicaoEntradaEmArquivo : RepositorioBaseEmArquivo<RequisicaoEntrada>, IRepositorio<RequisicaoEntrada>
{
    public RepositorioRequisicaoEntradaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<RequisicaoEntrada> CarregarRegistros()
    {
        return contexto.RequisicoesEntrada;
    }
}