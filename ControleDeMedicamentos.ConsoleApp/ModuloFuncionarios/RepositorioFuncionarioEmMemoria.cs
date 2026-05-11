using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Memoria;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;


namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class RepositorioFuncionarioEmMemoria : RepositorioBaseEmMemoria<Funcionario>, IRepositorio<Funcionario>;