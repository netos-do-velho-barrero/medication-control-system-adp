using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class ItemRequisicaoSaida
{
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }
}

public class RequisicaoSaida : EntidadeBase
{
    public DateTime Data { get; set; }
    public Paciente? Paciente { get; set; }
    public List<ItemRequisicaoSaida> MedicamentosRequisitados { get; set; } = new List<ItemRequisicaoSaida>();

    public RequisicaoSaida()
    {
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        RequisicaoSaida saidaAtualizada = (RequisicaoSaida)entidadeAtualizada;

        Data = saidaAtualizada.Data;
        Paciente = saidaAtualizada.Paciente;
        MedicamentosRequisitados = saidaAtualizada.MedicamentosRequisitados;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Data == default || Data > DateTime.Now)
            erros.Add("O campo 'Data' deve ser uma data válida e não pode ser futura.");

        if (Paciente == null)
            erros.Add("O campo 'Paciente' é obrigatório.");

        if (MedicamentosRequisitados == null || MedicamentosRequisitados.Count == 0)
            erros.Add("É obrigatório selecionar ao menos um medicamento.");

        return erros;
    }
}