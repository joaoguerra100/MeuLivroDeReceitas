# MyRecipeBook.Infrastructure
Camada responsável pela infraestrutura da aplicação.
- Implementa persistência e integrações externas.
- Contém repositórios concretos e configurações de banco de dados.
- Depende da camada Domain para entidades.

# Função:

- Implementa detalhes técnicos (persistência, banco de dados, serviços externos).
- Contém repositórios concretos, configurações de ORM (ex.: EF Core).
- Depende do Domain para saber quais entidades salvar.
- Pode ser injetada na Application via interfaces.