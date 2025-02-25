# TCD Plataforma .NET

## Tecnologias Utilizadas

### Back-end

- .NET Core
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Docker
- Swagger
- xUnit

### Front-end

- Angular
- TypeScript
- RxJS
- Angular CLI
- Bootstrap

## Como Iniciar o Projeto

1. **Clone o repositório:**

```bash
git clone https://github.com/seu-usuario/tcd-plataforma-dotNet.git
cd tcd-plataforma-dotNet
```

1. **Configure o banco de dados:**

- Certifique-se de que o SQL Server está em execução.
- Atualize a string de conexão no arquivo `appsettings.json`.

1. **Execute as migrações do Entity Framework:**

```bash
dotnet ef database update
```

1. **Inicie o projeto Back-end:**

```bash
dotnet run
```

1. **Inicie o projeto Front-end:**

```bash
cd frontend
npm install
ng serve
```

1. **Acesse a aplicação:**

- Abra o navegador e vá para `http://localhost:4200`.

1. **Documentação da API:**

- A documentação da API estará disponível em `http://localhost:5000/swagger`.

## Testes

Para executar os testes, utilize o comando:

```bash
dotnet test
```
