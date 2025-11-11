# Backend Project (Estudos C# LINQ + SQL)

Este repositório será usado para estudos de C#, LINQ e acesso a banco de dados (Entity Framework Core). Abaixo estão os primeiros passos para configurar o ambiente.

## 1. Instalar .NET SDK
Baixe e instale a versão LTS ou STS mais recente do .NET SDK:
- https://dotnet.microsoft.com/en-us/download

Verifique depois da instalação:
```
dotnet --version
```

## 2. (Opcional) Instalar via Winget no Windows
Se preferir usar o gerenciador de pacotes:
```
winget install Microsoft.DotNet.SDK.8
```
(Substitua `8` pela major version desejada.)

## 3. Estrutura inicial sugerida
```
backend-project/
  backend-project.sln              (solution - será criada)
  src/
    BackendProject.App/            (projeto console para testes LINQ/EF)
  data/                            (arquivos de banco local, ex: SQLite)
```

## 4. Criar solution e projeto (depois de instalar SDK)
```
cd backend-project
dotnet new sln -n backend-project
cd src
dotnet new console -n BackendProject.App
cd ..
dotnet sln add ./src/BackendProject.App/BackendProject.App.csproj
```

## 5. Adicionar EF Core (ex: SQLite)
Dentro de `src/BackendProject.App`:
```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

## 6. Ferramenta dotnet-ef
Instalar globalmente ou como ferramenta local:
```
dotnet tool install --global dotnet-ef
```
Verificar:
```
dotnet ef --help
```

## 7. Configuração de conexão (SQLite exemplo)
Criar `appsettings.json` com:
```json
{
  "ConnectionStrings": {
    "Default": "Data Source=../data/app.db"
  }
}
```

## 8. Criar modelos e DbContext
Exemplo de classe `Student` e `AppDbContext` (será adicionado posteriormente):
```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}
```

## 9. Migrations
```
cd src/BackendProject.App
mkdir ..\data
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 10. Exemplos LINQ
- Consultas em listas in-memory
- Consultas via EF Core (Where, Select, OrderBy, GroupBy, projeções anônimas)

## 11. Próximos Passos
- Adicionar entidades adicionais.
- Explorar Include, filtragem, paginação, projeções com DTOs.
- Testes unitários (xUnit / NUnit) para queries.

## 12. Alternativa: SQL Server Local
Instalar SQL Server Developer e usar provider `Microsoft.EntityFrameworkCore.SqlServer`.

---
Checklist rápido pós instalação:
[ ] `dotnet --version` funcionando
[ ] Solution criada
[ ] Projeto console criado
[ ] Pacotes EF Core adicionados
[ ] Migration aplicada
[ ] LINQ exemplos rodando
