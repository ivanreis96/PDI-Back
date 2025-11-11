using BackendProject.App.Data;
using BackendProject.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
// Added reference to extensions configuration assembly via implicit global usings of SDK; no extra package needed.

// Load configuration (appsettings.json)
// Build configuration via ConfigurationBuilder (requires Microsoft.Extensions.Configuration)
var configuration = new ConfigurationBuilder()
	.SetBasePath(AppContext.BaseDirectory)
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();

var connectionString = configuration.GetConnectionString("Default")
	?? throw new InvalidOperationException("Connection string 'Default' not found.");

var options = new DbContextOptionsBuilder<AppDbContext>()
	.UseSqlite(connectionString)
	.EnableSensitiveDataLogging() // helpful for estudos
    .Options;

// Garante que o diretório data existe antes de tentar criar o banco
var connectionStringForPath = configuration.GetConnectionString("Default")!;
var dbFilePath = connectionStringForPath.Replace("Data Source=", "").Trim();
var dbDir = Path.GetDirectoryName(dbFilePath);
if (!string.IsNullOrEmpty(dbDir))
{
    Directory.CreateDirectory(dbDir);
}

using var db = new AppDbContext(options);

// Usar migrations em vez de EnsureCreated (comentado para demonstração; descomentar se preferir)
// db.Database.EnsureCreated();
db.Database.Migrate(); // Aplica migrations pendentes

// ========================================
// SEED: Dados iniciais do app de receitas
// ========================================

if (!db.Usuarios.Any())
{
    // Criar usuários
    var usuario1 = new Usuario 
    { 
        Nome = "Maria Silva",
        Apelido = "Maria Chef",
        Email = "maria@exemplo.com", 
        Senha = "senha123",
        Instagram = "@maria_cozinha",
        Whatsapp = "+55 11 98765-4321"
    };
    
    var usuario2 = new Usuario 
    { 
        Nome = "João Santos",
        Apelido = "João Sabor",
        Email = "joao@exemplo.com", 
        Senha = "senha456",
        TikTok = "@joao_chef"
    };

    var usuario3 = new Usuario
    {
        Nome = "Ana Paula",
        Apelido = "Ana Cozinha",
        Email = "ana@exemplo.com",
        Senha = "senha789"
    };

    db.Usuarios.AddRange(usuario1, usuario2, usuario3);
    db.SaveChanges();

    // Criar ingredientes
    var ovo = new Ingrediente { Nome = "Ovo" };
    var farinha = new Ingrediente { Nome = "Farinha de trigo" };
    var acucar = new Ingrediente { Nome = "Açúcar" };
    var leite = new Ingrediente { Nome = "Leite" };
    var tomate = new Ingrediente { Nome = "Tomate" };
    var cebola = new Ingrediente { Nome = "Cebola" };
    var alho = new Ingrediente { Nome = "Alho" };

    db.Ingredientes.AddRange(ovo, farinha, acucar, leite, tomate, cebola, alho);
    db.SaveChanges();

    // Criar receitas
    var receitaBolo = new Receita
    {
        Nome = "Bolo de Chocolate",
        ModoPreparo = "1. Bata os ovos com açúcar\n2. Adicione farinha e leite\n3. Asse por 40min a 180°C",
        UsuarioId = usuario1.Id
    };

    var receitaMolho = new Receita
    {
        Nome = "Molho de Tomate Caseiro",
        ModoPreparo = "1. Refogue alho e cebola\n2. Adicione tomates picados\n3. Tempere e cozinhe por 20min",
        UsuarioId = usuario2.Id
    };

    db.Receitas.AddRange(receitaBolo, receitaMolho);
    db.SaveChanges();

    // Criar publicações automaticamente para cada receita
    var publicacaoBolo = new PublicacaoReceita
    {
        ReceitaId = receitaBolo.Id,
        DataPublicacao = DateTime.Now.AddDays(-10),
        NotaMedia = 4.5m,
        QuantidadeVotos = 25,
        Deliciosos = 18
    };

    var publicacaoMolho = new PublicacaoReceita
    {
        ReceitaId = receitaMolho.Id,
        DataPublicacao = DateTime.Now.AddDays(-5),
        NotaMedia = 4.8m,
        QuantidadeVotos = 32,
        Deliciosos = 28
    };

    db.PublicacoesReceitas.AddRange(publicacaoBolo, publicacaoMolho);
    db.SaveChanges();

    // Relacionar ingredientes com receitas (N:N)
    db.ReceitaIngredientes.AddRange(
        new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = ovo.Id, Quantidade = "3 unidades" },
        new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = farinha.Id, Quantidade = "2 xícaras" },
        new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = acucar.Id, Quantidade = "1 xícara" },
        new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = leite.Id, Quantidade = "200ml" },
        
        new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = tomate.Id, Quantidade = "5 unidades" },
        new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = cebola.Id, Quantidade = "1 unidade" },
        new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = alho.Id, Quantidade = "3 dentes" }
    );
    db.SaveChanges();

    // Criar interações de usuários: "Delicioso" e "Amei"
    // Ana marcou o bolo da Maria como delicioso (fez e aprovou)
    db.ReceitasDeliciosas.Add(new ReceitaDeliciosa
    {
        UsuarioId = usuario3.Id,
        ReceitaId = receitaBolo.Id,
        DataMarcacao = DateTime.Now.AddDays(-3)
    });

    // João marcou o bolo da Maria como "Amei" (favorito)
    db.ReceitasAmadas.Add(new ReceitaAmada
    {
        UsuarioId = usuario2.Id,
        ReceitaId = receitaBolo.Id,
        DataMarcacao = DateTime.Now.AddDays(-2)
    });

    // Ana também amou o molho do João
    db.ReceitasAmadas.Add(new ReceitaAmada
    {
        UsuarioId = usuario3.Id,
        ReceitaId = receitaMolho.Id,
        DataMarcacao = DateTime.Now.AddDays(-1)
    });

    // Maria marcou o molho do João como delicioso
    db.ReceitasDeliciosas.Add(new ReceitaDeliciosa
    {
        UsuarioId = usuario1.Id,
        ReceitaId = receitaMolho.Id,
        DataMarcacao = DateTime.Now
    });

    db.SaveChanges();

    Console.WriteLine("✅ Dados iniciais criados com sucesso!\n");
}

// ========================================
// CONSULTAS LINQ - Exemplos do domínio
// ========================================

Console.WriteLine("========== APP DE RECEITAS ==========\n");

// 1. Listar todos os usuários com suas receitas
Console.WriteLine("== Usuários cadastrados ==");
var usuarios = db.Usuarios
    .Include(u => u.Receitas)
    .OrderBy(u => u.Nome)
    .ToList();

foreach (var u in usuarios)
{
    Console.WriteLine($"👤 {u.Nome} (@{u.Apelido}) - {u.Email}");
    if (!string.IsNullOrEmpty(u.Instagram)) Console.WriteLine($"   📷 Instagram: {u.Instagram}");
    if (!string.IsNullOrEmpty(u.Whatsapp)) Console.WriteLine($"   📱 WhatsApp: {u.Whatsapp}");
    if (!string.IsNullOrEmpty(u.TikTok)) Console.WriteLine($"   🎵 TikTok: {u.TikTok}");
    Console.WriteLine($"   🍳 Receitas: {u.Receitas.Count}");
    Console.WriteLine();
}

// 2. Listar receitas com seus ingredientes e estatísticas da publicação
Console.WriteLine("== Receitas cadastradas ==");
var receitas = db.Receitas
    .Include(r => r.Usuario)
    .Include(r => r.Publicacao)
    .Include(r => r.ReceitaIngredientes)
        .ThenInclude(ri => ri.Ingrediente)
    .OrderBy(r => r.Nome)
    .ToList();

foreach (var r in receitas)
{
    Console.WriteLine($"📖 {r.Nome}");
    Console.WriteLine($"   Por: @{r.Usuario.Apelido}");
    Console.WriteLine($"   📅 Publicado: {r.Publicacao?.DataPublicacao:dd/MM/yyyy}");
    Console.WriteLine($"   ⭐ Nota: {r.Publicacao?.NotaMedia:F2} ({r.Publicacao?.QuantidadeVotos} votos)");
    Console.WriteLine($"   😋 Deliciosos: {r.Publicacao?.Deliciosos}");
    Console.WriteLine($"   Ingredientes:");
    foreach (var ri in r.ReceitaIngredientes)
    {
        Console.WriteLine($"      • {ri.Ingrediente.Nome} - {ri.Quantidade}");
    }
    Console.WriteLine($"   Modo de preparo:\n   {r.ModoPreparo.Replace("\n", "\n   ")}");
    Console.WriteLine();
}

// 3. Buscar receitas por ingrediente específico
Console.WriteLine("== Receitas que usam 'Ovo' ==");
var receitasComOvo = db.Receitas
    .Where(r => r.ReceitaIngredientes.Any(ri => ri.Ingrediente.Nome.Contains("Ovo")))
    .Select(r => new { r.Nome, AutorApelido = r.Usuario.Apelido })
    .ToList();

foreach (var r in receitasComOvo)
    Console.WriteLine($"• {r.Nome} (por @{r.AutorApelido})");

Console.WriteLine();

// 4. Receitas favoritas (Amei) de um usuário específico
Console.WriteLine("== Receitas que Ana amou (favoritos) ==");
var favoritosAna = db.Usuarios
    .Where(u => u.Nome == "Ana Paula")
    .SelectMany(u => u.ReceitasAmadas)
    .Include(ra => ra.Receita)
        .ThenInclude(r => r.Usuario)
    .ToList();

foreach (var ra in favoritosAna)
    Console.WriteLine($"❤️ {ra.Receita.Nome} (por @{ra.Receita.Usuario.Apelido}) - marcado em {ra.DataMarcacao:dd/MM/yyyy}");

Console.WriteLine();

// 5. Receitas que usuários marcaram como "Delicioso" (fizeram e aprovaram)
Console.WriteLine("== Receitas marcadas como Delicioso ==");
var receitasDeliciosas = db.ReceitasDeliciosas
    .Include(rd => rd.Usuario)
    .Include(rd => rd.Receita)
        .ThenInclude(r => r.Usuario)
    .OrderBy(rd => rd.DataMarcacao)
    .ToList();

foreach (var rd in receitasDeliciosas)
    Console.WriteLine($"😋 @{rd.Usuario.Apelido} aprovou: {rd.Receita.Nome} (de @{rd.Receita.Usuario.Apelido})");

Console.WriteLine();

// 6. Receitas mais bem avaliadas (ranking)
Console.WriteLine("== Top 3 receitas mais bem avaliadas ==");
var topReceitas = db.PublicacoesReceitas
    .Include(p => p.Receita)
        .ThenInclude(r => r.Usuario)
    .AsEnumerable() // Executa no cliente para contornar limitação do SQLite com decimal em ORDER BY
    .OrderByDescending(p => p.NotaMedia)
    .Take(3)
    .ToList();

foreach (var p in topReceitas)
    Console.WriteLine($"🏆 {p.Receita.Nome} (@{p.Receita.Usuario.Apelido}) - ⭐ {p.NotaMedia:F2} - 😋 {p.Deliciosos} deliciosos");

Console.WriteLine();

// 7. Ingredientes mais usados (agrupamento)
Console.WriteLine("== Ingredientes mais populares ==");
var ingredientesPopulares = db.ReceitaIngredientes
    .GroupBy(ri => ri.Ingrediente.Nome)
    .Select(g => new { Ingrediente = g.Key, QuantidadeReceitas = g.Count() })
    .OrderByDescending(x => x.QuantidadeReceitas)
    .Take(5)
    .ToList();

foreach (var ing in ingredientesPopulares)
    Console.WriteLine($"🥇 {ing.Ingrediente}: usado em {ing.QuantidadeReceitas} receita(s)");

Console.WriteLine("\n✅ Aplicação concluída!");
