using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Memoria;


namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class RepositorioProdutoEmMemoria : RepositorioBaseEmMemoria<Paciente>, IRepositorio<Paciente>;

