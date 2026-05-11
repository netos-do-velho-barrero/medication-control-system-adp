using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class RequisicaoEntrada : EntidadeBase
{
    public DateTime Data { get; set; }
    public Medicamento? Medicamento { get; set; }
    public Funcionario? Funcionario { get; set; }
    public int Quantidade { get; set; }

    public RequisicaoEntrada()
    {
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        RequisicaoEntrada entradaAtualizada = (RequisicaoEntrada)entidadeAtualizada;

        Data = entradaAtualizada.Data;
        Medicamento = entradaAtualizada.Medicamento;
        Funcionario = entradaAtualizada.Funcionario;
        Quantidade = entradaAtualizada.Quantidade;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Data == default || Data > DateTime.Now)
            erros.Add("O campo 'Data' deve ser uma data válida e não pode ser futura.");

        if (Medicamento == null)
            erros.Add("O campo 'Medicamento' é obrigatório.");

        if (Funcionario == null)
            erros.Add("O campo 'Funcionário' é obrigatório.");

        if (Quantidade <= 0)
            erros.Add("O campo 'Quantidade' deve ser um número positivo.");

        return erros;
    }
}