# agenda-fiap

Este repositório armazena os códigos-fonte das aplicações da primeira atividade _Tech Challenge_ do curso **Arquitetura de Sistemas .NET com Azure**, da Pós Tech da FIAP.

## 1. Sobre
Segue um breve resumo do arcabouço técnico e tecnológico utilizado no desenvolvimento das aplicações:

### 1.1 _frontend_
Todas as aplicações foram desenvolvidas dentro do escopo do [_.NET 8_](https://learn.microsoft.com/en-us/dotnet/). Dessa forma, para criar a aplicação _frontend_ e desenvolver todas as telas, utilizamos o [_Blazor WebAssembly_](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor). E para desenvolver todas as regras de negócio (como, por exemplo, as chamadas à API), utilizamos o próprio C#.

### 1.2 _backend_
Para o _backend_, desenvolvemos uma API e, para construí-la, nos baseamos na arquitetura de software _Clean Architecture_, com cinco camadas: _Infrastructure_, _Domain_, _Application_ e _Api_. Segue um breve resumo de como foi desenvolvida cada camada:

#### 1.2.1 _Infrastructure_
Nessa camada, é realizado todo tipo de comunicação externa à aplicação. No caso deste projeto, a única comunicação externa realizada é para a persistência (o banco de dados). Para realizar o controle de persistência, utilizamos o ORM [_Entity Framework_](https://learn.microsoft.com/en-us/ef/) (mantido pela Microsoft) e, como tecnologia de banco de dados, escolhemos o [_Microsoft SQL Server_](https://learn.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver16).

Também utilizamos dois _patterns_ para estruturação do código: o _design pattern_ _Repository Pattern_ para realizar as operações de _CRUD_ (_Create_, _Read_, _Update_ and _Delete_) em um repositório genérico, e o _behavior pattern_ _Unit of work_, para garantir a unidade das operações realizadas em uma mesma transação de banco de dados.

#### 1.2.2 _Domain_
Camada é responsável por armazenar as principais classes de domínio. A principal classe de domínio deste projeto é a classe `Contato.cs`, que foi construída, inclusive, com base em princípios de _DDD_ (_Domain-Driven Design_). Além de classes de domínio, essa camada armazena as abstrações dos repositórios e outras classes utilizadas ao longo da aplicação.

#### 1.2.3 _Application_
Nessa camada, estão as classes de caso de uso. Essas classes são responsáveis por orquestrar as classes de domínio e executar operações de negócio. Para os casos de uso, construímos as classes implementando o _CQRS pattern_ com a biblioteca [_MediatR_](https://github.com/jbogard/MediatR). Com esse _pattern_ e essa biblioteca, é possível segregar os casos de uso, cada um com sua lógica específica e suas responsabilidades exclusivas. As operações de leitura foram separadas em _Queries_ (`ObterContatos`) e as outras operações foram separadas em _Commands_ (`AdicionarContatos`, `AtualizarContato` e `ExcluirContato`).

Para mapear as classes de domínio em classes de transferência de dados, utilizamos a biblioteca [_Mapters_](https://github.com/MapsterMapper/Mapster). Essas classes de transferência de dados possuem [_Data Annotations_](https://learn.microsoft.com/pt-br/dotnet/api/system.componentmodel.dataannotations?view=net-8.0), que utilizamos dentro das classes de _Commands_ (`AdicionarContatos` e `AtualizarContato`) para validar os _payloads_.

#### 1.2.4 _Api_
Decidimos por criar os _endpoints_ utilizando os recursos da [_Minimal API_](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-8.0) (em vez das _controllers_). Essa camada armazena as classes de _endpoints_ (`AdicionarContato.cs`, `AlterarContato.cs`, `ExcluirContato.cs` e `ObterContatos.cs`), registradas através da biblioteca [_Carter_](https://github.com/CarterCommunity/Carter).

Além dos _endpoints_, essa camada armazena uma classe de _middleware_ chamada `ExceptionHandler.cs`, responsável por gerenciar qualquer exceção (`Exception`) lançada pela aplicação.

#### 1.2.5 Testes unitários
Para testes unitários (do projeto _backend_), decidimos por utilizar [_xUnit_](https://xunit.net/), juntamente com as bibliotecas [_Moq_](https://github.com/devlooped/moq) e [_FluentAssertions_](https://fluentassertions.com/). Os testes foram divididos em dois testes: testes dos casos de uso e testes da classe de transferência de dados (todas são classes presentes na camada de _Application_).

### 1.3 _common_
Esse projeto é responsável por armazenar todas as classes compartilhadas entre os dois projetos, _frontend_ e _backend_. 