<div align="center">

# 📚 Controle de Medicamentos

### Sistema de gerenciamento de controles de medicamentos desenvolvido em **C# com POO**
Controle de pacientes, funcionários, medicamentos, fornecedores e estoque com separação de responsabilidades e regras de negócio reais.

---

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Console App](https://img.shields.io/badge/Console-Application-black?style=for-the-badge&logo=windows-terminal&logoColor=white)
![OOP](https://img.shields.io/badge/Paradigm-OOP-blue?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Concluído-brightgreen?style=for-the-badge)

</div>

---

# 📌 Sobre o Projeto

O **Medication Control** foi desenvolvido com foco em:

- Organização em camadas
- Separação de responsabilidades
- Regras de negócio reais
- Encapsulamento
- Comunicação entre objetos
- Estrutura limpa e legível

A aplicação permite registrar entradas e saídas de medicamentos, controlar estoque automaticamente e validar operações importantes do sistema.

---

# ✅ Funcionalidades

## 💊 Medicamentos

- Cadastro de medicamentos
- Visualização, edição e exclusão
- Controle automático de estoque
- Associação com fornecedores
- Validações de campos obrigatórios
- Identificação de estoque baixo

### Regras

- Nome obrigatório
- Descrição obrigatória
- Quantidade deve ser positiva
- Medicamentos com menos de 20 unidades são sinalizados

---

## 🏢 Fornecedores

- Cadastro de fornecedores
- Visualização, edição e exclusão
- Controle de CNPJ

### Regras

- Nome obrigatório
- Telefone obrigatório
- CNPJ deve ser único

---

## 🧑‍🤝‍🧑 Pacientes

- Cadastro de pacientes
- Visualização, edição e exclusão
- Controle de CPF e cartão SUS

### Regras

- Nome obrigatório
- CPF válido
- Cartão SUS único

---

## 👨‍⚕️ Funcionários

- Cadastro de funcionários
- Visualização, edição e exclusão
- Controle de responsáveis pelas movimentações

### Regras

- Nome obrigatório
- CPF único
- Telefone obrigatório

---

# 📦 Controle de Estoque

## 📥 Requisições de Entrada

- Registro de entrada de medicamentos
- Controle automático de estoque
- Associação com funcionário responsável

### Regras

- Data válida
- Medicamento obrigatório
- Funcionário obrigatório
- Quantidade positiva

---

## 📤 Requisições de Saída

- Registro de saída de medicamentos
- Controle automático de baixa no estoque
- Associação com paciente

### Regras

- Não permitir retirada acima do estoque disponível
- Quantidade removida automaticamente
- Paciente obrigatório

---

# 🧠 Conceitos Aplicados

| Conceito | Aplicação |
|---|---|
| 🏗️ Classes e Objetos | Modelagem das entidades do sistema |
| 🔒 Encapsulamento | Proteção dos dados internos |
| 📐 Separação de Responsabilidades | Divisão entre telas, regras e entidades |
| ⚙️ Regras de Negócio | Controle de estoque e validações |
| 🔗 Comunicação entre Classes | Integração entre módulos |
| 🖥️ Console Application | Interface interativa via terminal |

---

# 📂 Estrutura do Projeto

```bash
📦 medication-control-system-adp
 ┣ 📁 ControleDeMedicamentos.ConsoleApp
 ┃ ┣ 📁 ModuloMedicamento
 ┃ ┣ 📁 ModuloFornecedor
 ┃ ┣ 📁 ModuloPaciente
 ┃ ┣ 📁 ModuloFuncionario
 ┃ ┣ 📁 ModuloRequisicaoEntrada
 ┃ ┣ 📁 ModuloRequisicaoSaida
 ┃ ┣ 📜 TelaPrincipal.cs
 ┃ ┗ 📜 Program.cs
 ┗ 📜 README.md
```

---

# ⚙️ Tecnologias Utilizadas

- C#
- .NET
- Console Application
- Programação Orientada a Objetos (POO)

---

# ▶️ Como Executar

## 1. Clone o repositório

```bash
git clone https://github.com/netos-do-velho-barrero/medication-control-system-adp.git
```

## 2. Acesse a pasta do projeto

```bash
cd medication-control-system-adp
```

## 3. Execute o projeto

```bash
dotnet run --project ControleDeMedicamentos.ConsoleApp
```

---

# 📋 Requisitos

- .NET SDK instalado
- Visual Studio 2022 ou superior

---

# 🎯 Objetivo de Aprendizado

Este projeto foi desenvolvido para praticar:

- ✔️ Programação Orientada a Objetos
- ✔️ Estruturação de projetos em C#
- ✔️ Separação de responsabilidades
- ✔️ Modelagem de entidades reais
- ✔️ Aplicação de regras de negócio
- ✔️ Manipulação de dados em aplicações console
- ✔️ Organização limpa e reutilizável do código

---


## 👨‍💻 Autores

<div align="center">

Desenvolvido por **Pedro Henrique** e **Marco Oliveira** como parte dos estudos em **C# e Programação Orientada a Objetos**.

[![GitHub](https://img.shields.io/badge/GitHub-pedrohenriquedsdev-181717?style=for-the-badge&logo=github)](https://github.com/pedrohenriquedsdev)

[![GitHub](https://img.shields.io/badge/GitHub-Marco--Oliver-181717?style=for-the-badge&logo=github)](https://github.com/Marco-Oliver)

</div>
