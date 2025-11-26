# MyRecipeBook.Application
Camada responsável pela lógica de aplicação.
- Implementa serviços e casos de uso.
- Usa DTOs da camada Communication.
- Lança exceções da camada Exceptions.
- Depende da camada Domain para entidades e regras de negócio.


# Função:

- Contém a lógica de aplicação (serviços, casos de uso).
- Faz a ponte entre a API e o domínio.
- Usa DTOs da camada Communication para entrada/saída.
- Pode lançar exceções customizadas da camada Exceptions.
- Depende do Domain para acessar entidades e regras de negócio.