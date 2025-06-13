# HOSPISIM - Sistema de Gestão Hospitalar

 O HOSPISIM é um sistema de gestão clínica desenvolvido com o objetivo de modernizar e garantir o controle completo da segurança, rastreabilidade de atendimentos e informações dos pacientes do Hospital Vida Plena.

## Entidades do Sistema

O sistema gerencia um ecossistema de entidades para cobrir as principais operações do hospital:

* **Paciente**: Armazena o cadastro e informações demográficas dos pacientes.
* **Prontuário**: É o registro central de cada paciente, contendo a data de abertura e observações gerais.  Cada prontuário está vinculado a um único paciente.
* **Profissional de Saúde**: Gerencia os dados dos profissionais, como médicos e enfermeiros, incluindo suas especialidades e informações de contato.
* **Especialidade**: Cadastra as especialidades médicas disponíveis (ex: Cardiologia, Pediatria) que podem ser associadas aos profissionais.
* **Atendimento**: Representa qualquer interação clínica (consulta, emergência), relacionando um paciente, um profissional e um prontuário.
* **Internação**: Controla o processo de internação de um paciente, desde a data de entrada e motivo até a localização (setor, quarto e leito).  Uma internação é sempre originada de um atendimento.
* **Exame**: Gerencia os pedidos de exames, suas datas e resultados, sempre vinculados a um atendimento.
* **Prescrição**: Detalha uma prescrição médica, incluindo medicamento, dosagem e frequência, ligada a um atendimento e a um profissional.
* **Alta Hospitalar**: Registro formal da alta de um paciente, finalizando uma internação e contendo as instruções pós-alta.

## Tecnologias Utilizadas

* **Framework Backend**: ASP.NET Core (Model-View-Controller) 
* **Linguagem**: C#
* **Banco de Dados**: SQL Server 
* **ORM**: Entity Framework Core (abordagem Code-First com Migrations)
* **Frontend**: HTML, CSS, JavaScript
* **Framework de Estilo**: Bootstrap 5
* **Gerenciador de Bibliotecas Front-end**: LibMan

## Pré-requisitos

Para executar este projeto em um ambiente de desenvolvimento, você precisará ter instalado:

* .NET SDK (versão 8.0 ou superior)
* SQL Server (qualquer edição, como Express ou Developer)
* Um editor de código como Visual Studio 2022 ou Visual Studio Code.

## Como Executar o Projeto

Siga os passos abaixo para configurar e rodar a aplicação localmente.

### 1. Clone o Repositório
Abra um terminal e clone o repositório do GitHub para sua máquina local.

```bash
# Substitua a URL pela URL do seu repositório
git clone [https://github.com/seu-usuario/hospisim.git](https://github.com/seu-usuario/hospisim.git)
cd hospisim
```

### 2. Configure a Connection String
A "Connection String" é o endereço que sua aplicação usa para se conectar ao banco de dados SQL Server.

Abra o arquivo `appsettings.json` na raiz do projeto.
* Localize a seção `ConnectionStrings`.
* Altere os valores de `Server`, `Database`, `User Id` e `Password` para corresponder à sua configuração local do SQL Server.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=HospisimDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Crie e Atualize o Banco de Dados
Com a connection string configurada, o Entity Framework Core pode criar o banco de dados e todas as tabelas para você.

* Abra um terminal na pasta raiz do projeto.
* Execute o seguinte comando. Ele vai ler todos os arquivos da pasta Migrations e aplicar as alterações no banco de dados.

```bash
dotnet ef database update
```

### 4. Execute a Aplicação
Agora que o código e o banco de dados estão prontos, execute a aplicação.

Você pode pressionar `F5` no Visual Studio.
Ou, pelo terminal, use o comando:

```bash
dotnet run
```

A aplicação estará rodando e acessível no seu navegador, geralmente em um endereço como `https://localhost:7175`.

Estrutura do Banco de Dados
O esquema do banco de dados é gerenciado via Entity Framework Core (Code-First). As regras de relacionamento, como chaves primárias e estrangeiras, são definidas nos arquivos de modelo na pasta `/Models` e configuradas no arquivo `/Data/ApplicationDbContext.cs`.

Todas as migrações necessárias para criar o banco do zero estão contidas na pasta `/Migrations` do projeto
