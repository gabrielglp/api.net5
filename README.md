# API .NET 5

Este é um projeto de API construído com .NET 5, utilizando Entity Framework Core para acesso ao banco de dados SQL Server e JWT para autenticação.

## Requisitos

- .NET 5 SDK
- SQL Server (ou SQL Server LocalDB)
- Visual Studio ou qualquer editor de código com suporte a .NET

## Configuração do Projeto

### 1. Clone o Repositório

Clone este repositório para sua máquina local:

```bash
git clone https://github.com/SeuUsuario/seu-repositorio.git
cd seu-repositorio
```

### 2. Configurar o Banco de Dados
Certifique-se de ter o SQL Server ou SQL Server LocalDB instalado.

- 2.1. Configurar a String de Conexão
- Abra o arquivo appsettings.json e configure a string de conexão para o seu banco de dados SQL Server. A configuração padrão é:
```bash
json
"ConnectionStrings": {
  "CLIENT": "server=DESKTOP-TPTBQMU; database=APIClientDB; Integrated Security=True; MultipleActiveResultSets=true; TrustServerCertificate=True;"
}
```
Substitua os valores server e database conforme necessário para o seu ambiente.

### 2.2. Criar o Banco de Dados
Para criar o banco de dados e aplicar as migrations, siga os passos abaixo:

#### 1. Abra o Console do Gerenciador de Pacotes no Visual Studio ou use o terminal com o comando:

```bash
dotnet ef migrations add InitialCreate
```
Atualize o banco de dados com a migration criada:
```bash
dotnet ef database update
```
Estes comandos criarão as tabelas e aplicarão as configurações iniciais ao banco de dados.

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
