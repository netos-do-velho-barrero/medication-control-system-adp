using System.Text.RegularExpressions;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class Fornecedor : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Fornecedor fornecedorAtualizado = (Fornecedor)entidadeAtualizada;

        Nome = fornecedorAtualizado.Nome;
        Telefone = fornecedorAtualizado.Telefone;
        Cnpj = fornecedorAtualizado.Cnpj;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo 'Nome' é obrigatório.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo 'Nome' deve ter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo 'Telefone' é obrigatório.");
        else if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$"))
            erros.Add("O campo 'Telefone' está em formato inválido. Ex: (49) 99999-9999");

        if (string.IsNullOrWhiteSpace(Cnpj))
            erros.Add("O campo 'CNPJ' é obrigatório.");
        else if (!Regex.IsMatch(Cnpj.Replace(".", "").Replace("/", "").Replace("-", ""), @"^\d{14}$"))
            erros.Add("O campo 'CNPJ' deve conter 14 dígitos numéricos.");

        return erros;
    }

    public string CnpjSemFormatacao()
    {
        return Regex.Replace(Cnpj, @"[^\d]", "");
    }
}