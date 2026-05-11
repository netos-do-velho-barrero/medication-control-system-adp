using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Memoria;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class RepositorioMedicamentoEmMemoria : RepositorioBaseEmMemoria<Medicamento>, IRepositorio<Medicamento>
{
}