# Medication Control

A pharmacy/medication management system built during the [Academia do Programador](https://www.academiadoprogramador.net) Fullstack course (2026).

## Features

### Suppliers
- Register, view, edit, and delete suppliers
- Required: Name (3–100 chars), Phone, CNPJ (14 digits)
- Duplicate CNPJ is not allowed

### Patients
- Register, view, edit, and delete patients
- Required: Name (3–100 chars), Phone `(XX) XXXX-XXXX` or `(XX) XXXXX-XXXX`, SUS Card (15 digits), CPF (11 digits)
- Duplicate SUS card is not allowed

### Medications
- Register, view, edit, and delete medications
- Required: Name (3–100 chars), Description (5–255 chars), Stock quantity (positive), Supplier
- Items with fewer than 20 units are flagged as **low stock**
- Re-registering an existing medication updates its quantity

### Employees
- Register, view, edit, and delete employees
- Required: Name (3–100 chars), Phone, CPF (11 digits)
- Duplicate CPF is not allowed

### Inventory

**Incoming Requests**
- Register and view stock intake entries
- Required: Date, Medication, Employee, Quantity (positive)
- Stock is updated automatically on entry

**Outgoing Requests**
- Register and view stock withdrawal entries
- Required: Date, Patient, Medications
- Cannot exceed available stock; quantity is deducted automatically

## Getting Started

```bash
# Restore dependencies
dotnet restore

# Run the project
dotnet run --project ControleDeMedicamentos.ConsoleApp
```

## Requirements

- .NET 10.0 SDK
