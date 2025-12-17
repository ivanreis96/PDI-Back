# 📖 Backend de Receitas Culinárias

API REST desenvolvida em .NET 9.0 para gerenciamento de receitas culinárias com sistema de publicações estilo blog, avaliações e engajamento de usuários.

## 🚀 Tecnologias Utilizadas

- **.NET 9.0** - Framework principal
- **ASP.NET Core Web API** - Criação de endpoints RESTful
- **Entity Framework Core 9.0** - ORM para acesso a dados
- **SQLite** - Banco de dados relacional
- **Swagger/OpenAPI** - Documentação interativa da API

## 📋 Funcionalidades

### Gerenciamento de Receitas
- CRUD completo de receitas
- Relacionamento com ingredientes (N:N)
- Modo de preparo detalhado
- Sistema de publicações com engajamento

### Sistema de Usuários
- Cadastro com nome, apelido, email e redes sociais (Instagram, TikTok)
- Perfis únicos por email e apelido
- Relacionamento com receitas criadas

### Engajamento e Avaliações
- **Publicações**: Cada receita possui uma publicação automática
- **Avaliações**: Sistema de notas de 1 a 5 com média calculada
- **Contador "Delicioso"**: Usuários podem marcar receitas que fizeram e aprovaram
- **Favoritos**: Sistema de receitas amadas pelos usuários

### Ingredientes
- Cadastro e reutilização de ingredientes
- Quantidades personalizadas por receita
- Relacionamento N:N com receitas

## 🏗️ Arquitetura

### Modelos de Dados

**Usuario**
- Informações pessoais e redes sociais
- Relacionamento 1:N com receitas
- Relacionamento N:N com receitas favoritas e aprovadas

**Receita**
- Nome e modo de preparo
- Relacionamento com usuário autor
- Relacionamento 1:1 com publicação
- Relacionamento N:N com ingredientes

**Ingrediente**
- Nome do ingrediente
- Reutilizável entre múltiplas receitas

**PublicacaoReceita**
- Metadados da publicação (data)
- Sistema de avaliação (nota média e quantidade de votos)
- Contador de "deliciosos"

**ReceitaIngrediente** (Tabela de junção)
- Relaciona receitas e ingredientes
- Armazena quantidade específica

### Estrutura de Pastas

```
BackendProject.App/
├── Controllers/          # Endpoints da API
│   ├── ReceitasController.cs
│   ├── UsuariosController.cs
│   └── IngredientesController.cs
├── Data/                 # Contexto e seed de dados
│   ├── AppDbContext.cs
│   ├── AppDbContextFactory.cs
│   └── DataSeeder.cs
├── Models/              # Entidades do domínio
│   ├── Usuario.cs
│   ├── Receita.cs
│   ├── Ingrediente.cs
│   ├── PublicacaoReceita.cs
│   ├── ReceitaIngrediente.cs
│   ├── ReceitaDeliciosa.cs
│   └── ReceitaAmada.cs
├── Migrations/          # Histórico de alterações do banco
└── Program.cs           # Configuração da aplicação
```

## 🔌 Endpoints da API

### Receitas
- `GET /api/receitas` - Lista todas as receitas com ingredientes e publicações
- `GET /api/receitas/{id}` - Busca receita específica por ID
- `GET /api/receitas/top` - Lista receitas mais bem avaliadas
- `POST /api/receitas` - Cria nova receita
- `PUT /api/receitas/{id}` - Atualiza receita existente
- `DELETE /api/receitas/{id}` - Remove receita

### Usuários
- `GET /api/usuarios` - Lista todos os usuários
- `GET /api/usuarios/{id}` - Busca usuário por ID
- `POST /api/usuarios` - Cria novo usuário
- `PUT /api/usuarios/{id}` - Atualiza usuário
- `DELETE /api/usuarios/{id}` - Remove usuário

### Ingredientes
- `GET /api/ingredientes` - Lista todos os ingredientes
- `GET /api/ingredientes/{id}` - Busca ingrediente por ID
- `POST /api/ingredientes` - Cria novo ingrediente
- `PUT /api/ingredientes/{id}` - Atualiza ingrediente
- `DELETE /api/ingredientes/{id}` - Remove ingrediente

## 🚀 Como Executar

### Pré-requisitos
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)

### Passos para Execução

1. **Clone o repositório**
```bash
git clone <url-do-repositorio>
cd backend-project
```

2. **Navegue até o projeto**
```bash
cd src/BackendProject.App
```

3. **Execute a aplicação**
```bash
dotnet run
```

4. **Acesse a API**
- API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

### Migrations

O projeto está configurado para aplicar migrations automaticamente ao iniciar. Caso precise executar manualmente:

```bash
# Criar nova migration
dotnet ef migrations add NomeDaMigration

# Aplicar migrations
dotnet ef database update

# Reverter migration
dotnet ef database update NomeMigrationAnterior
```

## 📊 Banco de Dados

O banco de dados SQLite é criado automaticamente em `backend-project/data/app.db`.

### Seed de Dados

A aplicação inclui dados iniciais (seed) com:
- 3 usuários de exemplo
- 10 receitas completas
- 20 ingredientes diversos
- Publicações com avaliações

## 🔧 Configuração

### Connection String
Configurada em `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Default": "Data Source=../../data/app.db"
  }
}
```

### CORS
Configurado para aceitar requisições de qualquer origem (desenvolvimento):
```csharp
policy.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
```

## 📝 Recursos Técnicos

### Entity Framework Core
- Code-First approach
- Migrations automáticas
- Relacionamentos complexos (1:1, 1:N, N:N)
- Índices para otimização de consultas
- Precisão decimal configurada para notas

### Validações e Constraints
- Emails únicos
- Apelidos únicos
- Índices em campos de busca frequente
- Cascade delete configurado
- Validações de integridade referencial

### Performance
- Índices em campos críticos (Email, Apelido, Nome de ingredientes)
- Eager loading com `.Include()` para evitar N+1 queries
- `.Select()` para projeções eficientes

## 🎯 Casos de Uso

1. **Cadastro de Receita**: Usuário cria receita, sistema automaticamente cria publicação
2. **Avaliação**: Usuários avaliam receitas, nota média é calculada
3. **Marcar como Delicioso**: Usuário que testou a receita pode aprovar
4. **Favoritar Receita**: Usuários podem salvar receitas favoritas
5. **Busca por Ingredientes**: Sistema permite filtrar receitas por ingredientes

## 📚 Documentação da API

A documentação completa e interativa está disponível através do Swagger UI em:
```
http://localhost:5000/swagger
```

## 🤝 Integração com Frontend

Este backend foi desenvolvido para integrar com aplicação React que consome os endpoints REST. Certifique-se de que:
- Backend está rodando em `http://localhost:5000`
- CORS está habilitado
- Frontend está configurado para apontar para a URL correta

## 📄 Licença

Este é um projeto educacional para fins de aprendizado de .NET, Entity Framework Core e desenvolvimento de APIs REST.

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
