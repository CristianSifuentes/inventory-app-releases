# Inventory App - .NET Fundamentals Course

This repository contains the source code for the **InventoryApp** application, organized by course modules.

## Contents

- [Current Module: Module 2 - Inputs, Outputs, and Types](#current-module-module-2---inputs-outputs-and-types)
  - [Requirements](#requirements)
  - [How to run](#how-to-run)
  - [Interactive commands](#interactive-commands)
  - [Current project structure](#current-project-structure)
  - [Progress checklist](#progress-checklist)
  - [Author](#author)
- [Repository Structure](#repository-structure)
- [How to download a specific module](#how-to-download-a-specific-module)
  - [Option 1: Clone a specific branch](#option-1-clone-a-specific-branch)
  - [Option 2: Switch between modules](#option-2-switch-between-modules-if-you-already-cloned-the-repo)
- [How to compare modules](#how-to-compare-modules)
  - [Compare on GitHub](#compare-on-github)

## Current Module: Module 2 - Inputs, Outputs, and Types

Project for the **.NET Fundamentals** course - Platzi.

### Requirements

- .NET 9 SDK

### How to run

```bash
dotnet run
dotnet run -- --help
dotnet run -- --version
```

### Interactive commands

- `list` - Lists inventory products
- `add` - Adds a new product (Module 3)
- `search` - Searches products (Module 4)
- `exit` - Exits the program

### Current project structure

```text
InventoryApp/
|-- Program.cs
|-- InventoryApp.csproj
|-- .gitignore
|-- README.md
|-- src/
|   |-- Factories/
|   |   `-- ProductFactory.cs
|   `-- Models/
|       |-- Product.cs
|       |-- ProductCategory.cs
|       |-- ProductStatus.cs
|       `-- Supplier.cs
```

### Progress checklist

- [x] Module 1: The .NET Ecosystem
- [x] Module 2: Inputs, Outputs, and Types
- [ ] Module 3: Functions and Domain Modeling
- [ ] Module 4: Collections and LINQ
- [ ] Module 5: Files and Processing

### Author

Sebastian Martinez

## Repository Structure

Each course module is maintained in its own branch:

| Branch | Module | Description |
|--------|--------|-------------|
| `modulo-1` | Module 1 | Basic fundamentals |
| `modulo-2` | Module 2 | Project evolution |
| `modulo-3` | Module 3 | New features |
| `modulo-4` | Module 4 | Advanced features |
| `modulo-5` | Module 5 | Final project version |

## How to download a specific module

### Option 1: Clone a specific branch

```bash
git clone -b modulo-1 https://github.com/CristianSifuentes/inventory-app-releases.git
```

Replace `modulo-1` with the branch for the module you want.

### Option 2: Switch between modules (if you already cloned the repo)

```bash
git checkout modulo-1
```

## How to compare modules

To see the differences between two modules, use:

```bash
git diff modulo-1..modulo-2
```

This shows all changes introduced between Module 1 and Module 2.

### Compare on GitHub

You can also compare directly on GitHub by visiting:

```
https://github.com/CristianSifuentes/inventory-app-releases/compare/modulo-1...modulo-2
```
