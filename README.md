# API .NET 5

Este é um projeto de API construído com .NET 5, utilizando Entity Framework Core para acesso ao banco de dados SQL Server e JWT para autenticação.

## Requisitos

- .NET 5 SDK
- SQL Server (ou SQL Server LocalDB)
- Visual Studio ou qualquer editor de código com suporte a .NET

- ## Dependências do Projeto

## Configuração do Projeto

### 1. Clone o Repositório

Clone este repositório para sua máquina local:

```bash
git clone https://github.com/SeuUsuario/seu-repositorio.git
cd seu-repositorio
```

Estes comandos criarão as tabelas e aplicarão as configurações iniciais ao banco de dados.

Antes de executar o projeto, certifique-se de que as seguintes dependências estejam instaladas:

- `BCrypt` versão 1.0.0
- `BCrypt.Net` versão 0.1.0
- `Microsoft.AspNetCore.Authentication.JwtBearer` versão 5.0.17
- `Microsoft.AspNetCore.Components` versão 5.0.17
- `Microsoft.EntityFrameworkCore` versão 5.0.17
- `Microsoft.EntityFrameworkCore.SqlServer` versão 5.0.17
- `Microsoft.EntityFrameworkCore.Tools` versão 5.0.17
- `Swashbuckle.AspNetCore` versão 6.2.1

### 2. Configurar o Banco de Dados

Certifique-se de ter o SQL Server ou SQL Server LocalDB instalado.

- 2.1. Configurar a String de Conexão
- Abra o arquivo appsettings.json e configure a string de conexão para o seu banco de dados SQL Server. A configuração padrão é:

```bash
json
"ConnectionStrings": {
  "CLIENT": "server=LOCALHOST; database=APIClientDB; Integrated Security=True; MultipleActiveResultSets=true; TrustServerCertificate=True;"
}
```
#### Componentes da String de Conexão
`Lembrando LOCALHOST é um endereço fictício`
- server: O nome do servidor ou o endereço IP do banco de dados. No exemplo acima, LOCALHOST é um placeholder para o servidor local ou uma instância do SQL Server.
- database: O nome do banco de dados ao qual a aplicação se conecta. No exemplo, é APIClientDB.
- Integrated Security=True: Indica que a autenticação do Windows será usada para se conectar ao banco de dados. Para autenticação com nome de usuário e senha, use User ID e Password em vez disso.
- MultipleActiveResultSets=true: Permite múltiplas leituras simultâneas no mesmo banco de dados.
### 2.2. Continuação do arquivo appsettings.json
- adicione a chave do jwt OBS: podeser qualquer chava que voce ache melhor da sua preferencia.

```bash
"Jwt": {
  "Secret": "eyJhbGciOiJIUzI1NiJ9.@eyJSb2xlIjoiQWRt!aW4i"
}
```

### 2.3. Incluir tambem o parâmetro AllowedHosts no arquivo appsettings.json é utilizado para configurar quais hosts são permitidos para acessar sua aplicação ASP.NET Core.

```bash
"AllowedHosts": "www.exemplo.com"
```

Substitua os valores server e database conforme necessário para o seu ambiente.

### 2.4. Criar o Banco de Dados

Para criar o banco de dados e aplicar as migrations, siga os passos abaixo:

#### 1. Abra o Console do Gerenciador de Pacotes no Visual Studio ou use o terminal com o comando:

```bash
Add-Migration
```

Atualize o banco de dados com a migration criada:

```bash
Update-database
```

#### 3. Executar o Projeto

Para rodar o projeto localmente, execute o seguinte comando na raiz do projeto:

```bash
dotnet run
```

#### O projeto será iniciado no https://localhost:5001 (ou no URL especificado no seu ambiente).

### Testando a API

#### 1. Endpoints Disponíveis

- Registro de Usuário: POST /api/auth/registration
- Login: POST /api/auth/login
- Obter Clientes: GET /api/clientes
- Obter Cliente por ID: GET /api/clientes/{id}
- Criar Cliente: POST /api/clientes
- Atualizar Cliente: PUT /api/clientes/{id}
- Excluir Cliente: DELETE /api/clientes/{id}
- Buscar Clientes: GET /api/clientes/search?cpf={cpf}&name={name}
- Obter Usuário Atual: GET /api/user/me (requer autenticação)

#### 2. Documentação da API

A documentação da API está disponível em Swagger UI, acessível em https://localhost:5001/swagger.

### Contribuindo

- Faça um fork do repositório.
- Crie uma branch para suas alterações (git checkout -b minha-alteracao).
- Faça commit das suas alterações (git commit -am 'Adiciona nova funcionalidade').
- Faça push para a branch (git push origin minha-alteracao).
- Crie um Pull Request.
