# MyRecipeBook.Api
Camada responsável por expor a API REST da aplicação.
- Contém Controllers e configuração do Swagger.
- Depende da camada Application para executar regras de negócio.
- Trata exceções e retorna respostas padronizadas.

# Função:

- É a camada de entrada da aplicação.
- Expõe endpoints HTTP (Controllers) para clientes (Web, Mobile, etc.).
- Faz a orquestração: recebe requisições, chama serviços da camada Application, retorna respostas.
- Não deve conter lógica de negócio complexa — apenas controle de fluxo e validação básica.