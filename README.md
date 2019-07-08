## API do Curso do Balta.IO - 1976

# Criando Sua Primeira API (Com ASP.NET Core e EF Core)

## A quem se aplica?
- Pessoas iniciantes com ASP.NET Core, com cenário mais simples, ou migrando de outras inguagens para o c# e pra o .NET.
- Vamos construir uma APi pequena, mas com qualidade

## Pré-Requisito
- Linguagem de Programação
- Conhecer algum Sistema Operacional
- breve contato de aplicação (não obrigatório)
Ao termino do curso, vou poder fazer o que?
- Criar API Simples, Escalavel e pronta pra nuvem;
- Documentar API;
- Padronizar uma API;
- Versionar uma API;
- Otimizar - performance;
- Escalavel (colocar em varias maquinas - cenário de larga escala) e pronta para nuvem (azure);

## Ferramentas ()
- Windows, Mac ou Linux;
- .NET core (sdk)
- Docker
- SQL Server
- SQL Server Operations Studio - multiplataforma
- VS Code (editor de codigo - não e IDE)

## Configurando o Ambiente: .NET Core ()
- Sdk para desenvolvimento;
- Open Source;
- Multiplataforma e multilinguagem;
- microsoft.com/net (download) - SDK

 ## Configurando o Ambiente: Docker ()
- Abstração da Infraestrutura
- Execução em containers
- Podemos o usar o container que foi usado para desenvolvimento em produção
- Facil de escalar
- Auxilia no ambiente de dev

Para realizar o download: docker.com/get-docker
Vamos pegar a community edition - necessita de cadastro.
Kitematic - Versão visual para o docker. É possivel gerenciar as imagens;
- O balta deixa como preferencia 2 cpu e 2g de run. E não deixa inicializar com a maquina.

## Configurando o Ambiente: SQL Server ()
- Vamos configurar o sqlserver para rodar no docker;
- Sql Server é a solução de banco da Microsoft
- Solução paga
- Tem menor atrito com ASP.NET
- Utilizado por grandes empresas

Com o docker rodando, digitamos no terminal:
docker --version (para checar se esta instalado)

A primeira coisa que temos que fazer é baixar a imagem que a Microsoft disponabiliza do SQL Server:
- é possivel pela interface grafica ou terminal: docker pull microsoft/mssql-server-linux:2017-latest (checar esse comando no windows);

Balta mostrou o Settings da imagem - senha (sera passado na connections tring) contrato e path

Criado a Imagem, vamos roda-la
Podemos rodar na interface grafica ou linha de codigo
- Na linha de comando: usamos o docker run -e ...e as variaveis de ambiente

## Configurando o Ambiente: SQL Server Operations Studio()
- Gerenciamento do SQL Server
- Leve
- Simples de usar
- Free
- Multiplataforma
- docs.microsoft.com/en-US/sql/sql-operations-studio/download
	- Não encotrei, foi redirecionado para o Azure Data Studio

## Configurando o Ambiente: Visual Studio Code()
- VS code é um editor de codigo
- leve e simples de usar
- Multilinguagem
- muitas extensões
- free e open source
- multiplataforma

## Modelagem de Dados (15:17 - 15:49)
- Cenário Data Driven Design (orientado a dados - de/para da app para bd);
- Dominios Anemicos (não nem tem regra de negocios nas entidades);
- Cenário Simples;
- CRUD com facilidade;

Criando o Projeto
- Balta criou a pasta ProductCatalog. Dentro da pasta ele executa no terminal o "dotnet new web" (equivale ao empty).
- Dentro da pasta ProductCatalog, executar o comando no terminal - "dotnet new sln". (cria uma solution).
- Temos que adicionar nosso projeto vazio "ProductCatalog" na solution - "dotnet new sln add ProductCatalog.csproj".
- Balta exclui a pasta wwwroot e remove a referencia <itemgroup> do arquivo csproj;

## Modelagem de Dados - Entidades
- Vamos ter duas# entidades - Produto e categoria;
- Cria a pasta Models (basicamente, os models reprensentam o que temos no banco de dados).
- Dentro de Models, criamos o Category.cs. Dentro de Category, inicialmente, teremos três propriedades - Id (int), Title (string) e Products (IEnumerable<Product>). Dara erro de compilação porque não temos ainda a classe Product.
- Ainda dentro de Models vamos criar a classe Product.cs. Dentro de product vamos ter algumas propriedades. Id (int), Title (string), Description (string), Price (decimal), Quantity (int), Image (string), CreateDate (DateTime), LastaUpdateDate (DateTime), CategoryId (int) e Category (Category) (esses dois ultimos são a referencia para categoria).

## Introdução Ao Entity Framework Core (16:07  - 16:30)
- Solução ORM da Microsoft;
- Faz o dê-para das classes para tabelas;
- Facilita a vida do desenvolvedor
- Facil instalação;

Algumas resalvas da versão atual - Mapeamento NxN ainda não está legal (precisa de 3 classes para NxN);
- Instalando o EF Core pelo console
- No Console do Visual Studio Code (No Visual Studio é diferente)-  dotnet add package Microsoft.EntityFrameworkCore;

Agora vamos trabalhar com contexto de dados - Representação do Banco de Dados em memória - "DataContext". O DataContext será o unico arquivo que o EF Core precisa pra funcionar. É definido as Entidades que irão para o banco de dados.
- Cria a pasta "Data". Dentro de Data criamos um arquivo - "StoreDataContext.cs".
- StoreDataContex herda de DbContext.
- Temos duas propriedades do tipo DbSet, para cada entidade - Products e catagoris;
- Sobrescrevemos o metodo OnConfiguring, usando o optionsBuilder.UseSqlServer passando a ConnectionString do banco de dados. (protected override void OnConfiguring...)

## Mapeamento Objeto Relacional (16: 50 - 18:00 )
- SQL server trabalha de forma diferente do c#;
- Classes vs Tabelas
- Foco do código (code first)
- Mudanças no código vão para o banco;
- Migrações.	

Trabalhando com as Migrações
- Para trabalhar com as migrations temos que instalar uma ferramenta - DotNetCli para o EF.
<ItemGroup>
<DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.Dotnet" Version="2.0.1" />
</ItemGroup>
Agora, no terminal - dotnet restore (restaura todos os pacotes).
Com isso podemos executar o comando: 
- dotnet ef migrations add initial (estamos criando a migração inicial - que cria o banco de dados e tambéms as tabelas a partir do datacontext - cria um script sql a partir do DataContext).
	- é criada a uma tabela nova - Migrationsb
- dotnet ef database update - extecuta e cria o banco de dados;
(OBS: Os comandos no Visual Studio são diferentes).
- Dica, criar uma migration e já fazer o Update. Não criar varias e só depois dar o update.

- Com as operações anteriores, foi criado o banco com as duas tabelas, porém com um problema - As colunas estão como Nvarchar (muito ruim para performance), o nomes das tabelas estão no plural. NÃO QUEREMOS ASSIM.
Vamos arrumar isso:
- Vamos melhorar a geração do banco.

Criando o Mapeamento
- Dentro da pasta "Data", vamos criar uma pasta "Maps". Dentro de Maps, criamos um arquivo"CategoryMap.cs" - Nesse arquivos vamos configurar o nome da tabela e as dimensões das colunas.
- A classe CategoryMap.cs:
	- Nossa classe deve herdar de IEntityTypeConfiguration<Category>
	- E possui o metodo Configure
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable("Category");
		builder.HasKey(x => x.Id);
		builder.property(x=>x.Title).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)");
	}
- Nessa classe não foi mapeado os "produtos". Iremos mapear "cateoria" no "ProductMap.cs".

- Também temos o "ProductMap.cs":
	- Herda de IEntityTypeConfiguration<Product>
	- E implementa o método Configure:

	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("Product");
		builder.HasKey(x => x.Id);
		builder.Property(x=>x.CreateDate).IsRequired();
		builder.Property(x=>x.Description).IsRequired().HasMaxLenght(1024).HasColumnType("varchar(1024)");
		builder.Property(x=>x.Image).IsRequired().HasMaxLenght(1024).HasColumnType("varchar(1024)");
		builder.Property(x=>x.LastUpdateDate).IsRequired();
		builder.Property(x=>x.Price).IsRequired().HasColumnType("money");
		builder.Property(x=>x.Quantity).IsRequired();
		builder.Property(x=>x.Title).IsRequired().HasMaxLenght(120).HasColumnType("varchar(120)");
		builder.HasOne(x=>x.Category).WithMany(x=>x.Products);
	}
	Oberserve que  o relacionamento esta feito apenas de um lado. Na classe ProductMap. Não precisa fazer nos dois lados. 
	

Ajustando o StoreDataContext
Com as duas classes de mapeamento criadas, devemos abrir o arquivo de contexto "StoreDataContext". Sobreescrvemos o metodo OnModelCreating:
protected override void OnModelCreating(ModelBuilder builder)
{
builder.ApplyConfiguration(new ProductMap());
builder.ApplyConfiguration(new Categorymap());
}
Com essa alteração, quando o banco for criado ele irá usar essas configurações.

Gerando migração para atualizar nosso Banco.
- dotnet ef migrations add v1
- dotnet ef database update


## Criando a API (18:34 )
- Permite a Comuniação entre aplicações;
- Concentra regra de negócio do seu software;
- Necessário para comunicação com dispositivos móveis (app moveis não se conectam aos bancos de dados);
- Utilizada por Facebook, Instragam, Twitter;

ASP.NET Core -  Permite a criação de api de forma facil, sendo muito performático, self-host (auto gerenciavel), multiplataforma, e modularizado.

Middleware
- toda app possui um pipeline;
- No ASP.NET Core ele começa vazio;
- O que é adicionado entre o Request/Response são os middlewares.

- Nossa propria aplicação é nosso servidor Web (classe program.cs podemos ver isso). Antes dependiamos no IIS. Agora não mais. Temos o IWebHost que é uma abstração do servidor web. Assim nossa aplicação é autogerenciavel. Ela esta acoplada de qualquer modelo de servidor.
- Na classe startup temos dois metodos, configureservices e configure. Add e configuramos os middleware nesses metodos.

-dotnet build
-dotnet run.

- OBS. Nossa api já esta pronta, Balta explicou alguns conceitos apenas.


## Adicioando MVC (19:00 - 19:10)
- Padrão para organização do projeto - Padrão Arquitetural.
- Model View Controller;
- Nossas models já existem, vamos criar o controller. Não existira a View.

Adicionando o MVC:
- No nosso terminal digtamos: dotnet add package Microsoft.AspNetCore.Mvc
- dotnet restore;

Dentro da classe Startup:
- No metodo ConfigureServices:
	- services.addMvc(); (adiciona o MVC)
- No metodo Configure:
	- app.UseMvc(); (Configura o  MVC)

## Rotas e CRUD (15:37)
- Rotas são importantes
- Podemos Utilizar as Rotas e Verbos
- Podemos receber parametros pela url e corpo da requisição

-Parametro na url - máximo de 1024 caracteres e são encodados/codificado (espaço em branco = %20) - MVC ASP.NET core já converte automáticamente
- parametros por url não são recomendados. É mais recomendado usar um verbo onde podemos passar os parametros no corpo;

Verbos HTTP (CRUD)
- Get - Cabeçalho apenas (obter)
- Post - Cabeçalho e corpo (criar)
- Delete - Cabeçalho e corpo (apagar)
- Put - Cabeçalho e corpo (atualizar)

ROTEAMENTO
Podemos ter varias rotas iguais, porém com verbos diferentes, fazendo coisas diferentes
GET - .../v1/products => lista produtos
POST - .../v1/products => Cria produto
PUT - .../v1/products => Atualiza produto
Delete  - .../v1/products => Deleta produto
Perceba que products está no plural - boa pratica do padrão REST

URL é muito importante

CRUD
- Create, read, update, delete
- Refletem os verbos http

Mão na massa - código
- Nova pasta - Controllers
- Cria o arquivo CategoryController.cs
- Essa classe será responsavel pelo crud da nossa categoria
- Balta passou o código pronto - checar no arquivo (esta no GitHub)

Execução de Teste
- Será utilizado o Postman - getpostman
- Para executar o teste, temos que ajustar a injeção de dependecia do _context
- na classe Startup.cs, no metodo ConfigureServices:
	- services.AddScoped<StoreDataContext, StoreDataContext>();
(Scoped - um item por requsição) - checa se já existe, se já existir usa o mesmo
(Transient - Cria varios)

-dotnet run
- No Postman
	- headers - content-type - application/json
	- body - raw 
	{
		"title":"Jogos Digitais"
	}
	- Não precisa passar o Id,

- No Put, ele passa o Id e Title (alterando o titulo para "novos Jogos Digitais"

## ViewModels ()
