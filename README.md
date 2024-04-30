# Desafio-Backend

Objetivo é criar uma aplicação para gerenciar aluguel de motos e entregadores.

## 💻 Pré-requisitos

Antes de começar, verifique se você atendeu aos seguintes requisitos:

- Você instalou a versão `.NET 6.0`
- Você instalou `PostgreSQL`
- Você instalou o `Docker`.

## 🚀 Iniciando o Projeto

### Para iniciar `Desafio-Backend`, siga estas etapas:

- Faça o clone do projeto:

```
git clone https://github.com/gabrielwottawa/Desafio-Backend.git
```

### 🐘 Migrations

- Abra o terminal da sua máquina e siga os passos:

```
cd {RAIZ ONDE FOI FEITO O CLONE}\Desafio-Backend\MotorbikeRental\MotorbikeRental.Infrastructure.Migrations
```
- Execute o seguinte comando:
    - DATABASE: base de dados criada no seu PostgreSQL (Exemplo: `MotorbikeRental`)
    - USERNAME: usuário do seu PostgreSQL
    - PASSWORD: senha do seu PostgreSQL
```
dotnet run "Host=localhost;Database={DATABASE};Username={USERNAME};Password={PASSWORD};"
```

<b>OBS: Altere a `ConnectionString` nos arquivos de configuração dos projetos `MotorbikeRental` e `MotorbikeRental.Background`</b>

### 🐰 Baixando e iniciando o RabbitMq

- Abra o terminal da sua máquina e siga os passos:

```
cd {RAIZ ONDE FOI FEITO O CLONE}\Desafio-Backend\Docker-RabbitMq
```
- Execute o seguinte comando:
```
docker-compose up
```

Os comandos acima irão baixar e criar um servidor em Docker para o RabbitMq.

Usuário e senha para acessar o servidor:
- Usuário: admin
- Senha: 123

Link: http://localhost:15672

### 🐰 Abrindo o canal de comunicação com o RabbitMq

- Abra o terminal da sua máquina e siga os passos:

```
cd {RAIZ ONDE FOI FEITO O CLONE}\Desafio-Backend\MotorbikeRental\MotorbikeRental.Background
```
- Execute o seguinte comando:
```
dotnet run 
```

Os comandos acima irão iniciar a comunicação da aplicação de backend com o servidor do RabbitMq.

### 💻 Iniciando a API

- Abra o terminal da sua máquina e siga os passos:

```
cd {RAIZ ONDE FOI FEITO O CLONE}\Desafio-Backend\MotorbikeRental\MotorbikeRental
```
- Execute o seguinte comando:
```
dotnet run 
```

Os comandos acima irão iniciar a API.

Link: https://localhost:5103/

## 📙 Documentação

Lembrando que para acessar a documentação é necessário ter executado os passos acima.

A documentação da API pode ser acessada neste link: https://localhost:5103/swagger/

## 👀 Testando a API

Seguindo as regras do desafio, nem todos os endpoints podem ser acessados por usuários `entregadores` ou `administradores`.

Então para acessar os endpoints é necessário gerar um token. Para gerar o token é necessário ter um usuário e senha, já deixei 2 usuários criados para esses testes, abaixo vou deixar o nome e senha.

Usuário admin:
```
{
  "name": "admin",
  "password": "123"
}
```

Usuário entregador:
```
{
  "name": "entregador",
  "password": "123"
}
```