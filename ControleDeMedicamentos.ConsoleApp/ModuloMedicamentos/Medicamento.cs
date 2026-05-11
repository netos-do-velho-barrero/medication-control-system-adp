using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class Medicamento : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int QuantidadeEmEstoque { get; set; }
    public Fornecedor? Fornecedor { get; set; }

    public bool EstaEmFalta => QuantidadeEmEstoque < 20;

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Medicamento medicamentoAtualizado = (Medicamento)entidadeAtualizada;

        Nome = medicamentoAtualizado.Nome;
        Descricao = medicamentoAtualizado.Descricao;
        QuantidadeEmEstoque = medicamentoAtualizado.QuantidadeEmEstoque;
        Fornecedor = medicamentoAtualizado.Fornecedor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo 'Nome' é obrigatório.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo 'Nome' deve ter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Descricao))
            erros.Add("O campo 'Descrição' é obrigatório.");
        else if (Descricao.Length < 5 || Descricao.Length > 255)
            erros.Add("O campo 'Descrição' deve ter entre 5 e 255 caracteres.");

        if (QuantidadeEmEstoque < 0)
            erros.Add("O campo 'Quantidade em Estoque' deve ser um número positivo.");

        if (Fornecedor == null)
            erros.Add("O campo 'Fornecedor' é obrigatório.");

        return erros;
    }
}