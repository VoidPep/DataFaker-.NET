
### Como rodar em um ambiente local
---
### 1. Preparando ambiente
- Antes de mais nada, para fazer o enviador de e-mail funcionar, deve-se definir 3 variáveis de ambiente com os dados enviado no e-mail, para isso basta definir as variáveis via terminal:
`Exemplo Windows:`
```bash
setx SMTP_PASSWORD "chave_de_acesso_app" 
setx SMTP_EMAIL "email_enviador" 
setx SMTP_TO "email_que_recebera"
```
obs: Caso opte por não preencher o <input> com o email na hora da geração do email o sistema vai pegar o valor que está na variável `SMTP_TO`

Essas variáveis ficam salvas no sistema operacional, se quiser remove-las basta ir até
`Caso esteja utilizando windows:`
`Painel de Controle > Sistema e Segurança > Sistema > Configurações avançadas do sistema > Variáveis de Ambiente.`

Após isso certifique-se que tenha:
- [**.NET SDK 6.0** ou superior](https://dotnet.microsoft.com/download/dotnet/6.0)
- [**Git** para clonar o repositório](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git)
- Um editor de código, como **Visual Studio**/**Rider** caso queira acessar o código fonte

---
### 2. Clonando e rodando a primeira vez

- Pelo terminal, baixe o projeto a partir do repositório remoto utilizando: 
```bash
git clone https://github.com/VoidPep/DataFaker-.NET.git
```

- Navegue até o diretório clonado
```bash
cd DataFaker-.NET
```

- Antes de rodar o projeto, você será necessário restaurar as dependências pelo comando:
```bash
dotnet restore
```
- Existe um banco de dados SQLite no projeto, então para executar a criação das tabelas basta rodar um:
```bash
dotnet ef database update --project DataFaker.Context --startup-project DataFaker.Web
```
- Por fim agora é possível compilar o projeto e após isso rodá-lo:
- Obs.: O comando `dotnet run` já executa um build antes da execução
```bash
dotnet build
dotnet run --project DataFaker.web
```
