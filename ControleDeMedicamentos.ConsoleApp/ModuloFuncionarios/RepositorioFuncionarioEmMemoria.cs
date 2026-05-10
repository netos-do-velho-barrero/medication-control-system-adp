using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Memoria;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;


namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class RepositorioFuncionarioEmMemoria : RepositorioBaseEmMemoria<Funcionario>, IRepositorio<Funcionario>;