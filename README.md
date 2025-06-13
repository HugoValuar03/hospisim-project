# HOSPISIM - Sistema de Gestão Hospitalar

 O HOSPISIM é um sistema de gestão clínica desenvolvido com o objetivo de modernizar e garantir o controle completo da segurança, rastreabilidade de atendimentos e informações dos pacientes do Hospital Vida Plena. 

## Entidades do Sistema

O sistema gerencia as seguintes entidades principais para o funcionamento do hospital:

*  **Paciente**: Cadastro e informações demográficas dos pacientes. 
*  **Prontuário**: Registro central de cada paciente, contendo a data de abertura e observações gerais. 
*  **Profissional de Saúde**: Cadastro dos profissionais, como médicos e enfermeiros, incluindo suas especialidades e informações de trabalho. 
*  **Especialidade**: Cadastra as especialidades médicas disponíveis (ex: Cardiologia, Pediatria). 
*  **Atendimento**: Representa qualquer interação clínica (consulta, emergência), relacionando um paciente, um profissional e um prontuário. 
*  **Internação**: Controla o processo de internação de um paciente, desde a data de entrada e motivo até a localização. 
*  **Exame**: Gerencia os pedidos de exames, suas datas e resultados, vinculados a um atendimento. 
*  **Prescrição**: Detalha uma prescrição médica, incluindo medicamento, dosagem e frequência, ligada a um atendimento. 
*  **Alta Hospitalar**: Registro formal da alta de um paciente, finalizando uma internação. 

## Tecnologias Utilizadas

* **Framework Backend**: ASP.NET Core (Model-View-Controller)
* **Linguagem**: C#
*  **Banco de Dados**: SQL Server 
* **ORM**: Entity Framework Core (abordagem Code-First com Migrations)
* **Frontend**: HTML, CSS, JavaScript
* **Framework de Estilo**: Bootstrap 5
* **Gerenciador de Bibliotecas Front-end**: LibMan

## Instruções de Execução da Aplicação

Para executar este projeto em um ambiente de desenvolvimento local, siga os passos abaixo.

### Pré-requisitos

* .NET SDK (versão 8.0 ou superior)
* SQL Server (versão Express, Developer ou superior)
* Um editor de código como Visual Studio 2022 ou Visual Studio Code.

### Passos para Instalação

1.  **Clone o Repositório**
    Abra um terminal e clone o repositório do GitHub para sua máquina local.
    ```bash
    # Substitua a URL pela URL do seu repositório
    git clone [https://github.com/seu-usuario/hospisim.git](https://github.com/seu-usuario/hospisim.git)
    cd hospisim
    ```

2.  **Configure a Connection String**
    * Abra o arquivo `appsettings.json` na raiz do projeto.
    * Localize a seção `ConnectionStrings`.
    * Altere o valor de `Server`, `Database`, `User Id` e `Password` para corresponder à sua configuração local do SQL Server.

3.  **Aplique as Migrações do Banco de Dados**
    * Abra um terminal na pasta raiz do projeto.
    *  Execute o comando abaixo para que o Entity Framework crie o banco de dados e todas as tabelas. 
        ```bash
        dotnet ef database update
        ```

4.  **Execute a Aplicação**
    * Você pode executar o projeto pressionando `F5` no Visual Studio.
    * Ou, pelo terminal, com o comando:
        ```bash
        dotnet run
        ```
    A aplicação estará disponível no seu navegador, geralmente em um endereço como `https://localhost:7175`.

## Texto Explicativo dos Relacionamentos

Os relacionamentos entre as entidades foram configurados no `ApplicationDbContext` para garantir a integridade dos dados:

*  **Paciente e Prontuário (1 para N):** Um `Paciente` pode ter vários `Prontuarios` ao longo do tempo, mas cada `Prontuario` pertence a um único `Paciente`. 

* **Atendimento (entidade centralizadora):** O `Atendimento` conecta as outras entidades.
    * Um `Atendimento` está sempre ligado a 1 `Paciente`, 1 `ProfissionalSaude` e 1 `Prontuario`.
    * A exclusão de um Paciente, Profissional ou Prontuário é restrita (`OnDelete(DeleteBehavior.Restrict)`) se houver atendimentos vinculados, protegendo o histórico.

* **Atendimento e Internação (1 para 0..1):** Um `Atendimento` pode gerar no máximo uma `Internacao`.  A chave primária da `Internacao` também é uma chave estrangeira para `Atendimento`, garantindo essa relação de um-para-um. 

* **Internação e Alta Hospitalar (1 para 0..1):** Uma `Internacao` pode ter no máximo uma `AltaHospitalar`.  Isso é garantido fazendo da `InternacaoId` a chave primária da tabela `AltasHospitalares`. 

*  **Atendimento, Prescrição e Exame (1 para N):** Um `Atendimento` pode ter várias `Prescricoes` e vários `Exames` associados a ele. 

* **Profissional e Especialidade (N para N):** Um `ProfissionalSaude` pode ter várias `Especialidades`, e uma `Especialidade` pode ser atribuída a vários profissionais.  Esta relação é implementada através de uma tabela de junção (`ProfissionalSaudeEspecialidade`). 
