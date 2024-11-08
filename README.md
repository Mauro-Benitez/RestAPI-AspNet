# Nome do Projeto

API RESful com ASP.NET Core 8.0 e C#

## Índice

- [Visão Geral](#visão-geral)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)

## Visão Geral
Este projeto é uma API RESTful construída com foco nas boas práticas e nos princípios REST. Ela oferece funcionalidades como paginação, versionamento de API, upload e download de arquivos, HATEOAS e autenticação com JWT, além de ser organizada em camadas para facilitar a manutenção. Testada com o Postman, a API utiliza SQL e migrations para gerenciar o banco de dados de forma eficiente.

## Funcionalidades

- Requisições e Respostas: Suporte a operações utilizando diferentes métodos HTTP, incluindo GET (com consultas via query string), PATCH (para atualizações parciais), POST, PUT, e DELETE, com manipulação de cabeçalhos e corpos de requisição conforme necessário.
- Paginação: Implementação de paginação para otimizar consultas de dados volumosos e melhorar a eficiência do uso da API.
- Versionamento de API: Estruturação e controle de versões para facilitar a evolução da API sem comprometer compatibilidade com clientes existentes.
- Upload e Download de Arquivos: Suporte a operações de upload e download de arquivos, oferecendo flexibilidade e segurança nas transações de dados binários.
- HATEOAS: Integração do princípio de HATEOAS (Hypermedia as the Engine of Application State), fornecendo links de navegação em respostas JSON para uma API verdadeiramente RESTful.
- Migrations e SQL: Utilização de SQL básico para operações CRUD e aplicação de migrations para garantir controle e evolução estruturada do banco de dados.
- Autenticação REST com JWT: Implementação de autenticação baseada em JSON Web Tokens (JWT), garantindo segurança e integridade nas operações de autenticação e autorização.
- Arquitetura em Camadas: Estruturação do código em camadas (controllers, services, repositories, etc.), promovendo separação de responsabilidades, escalabilidade e manutenção do projeto.

## Tecnologias Utilizadas

- ASP.NET Core - Framework para construção de APIs e aplicações web em .NET.
- MySQL - Bancos de dados relacionais para armazenamento de dados.
- JWT (JSON Web Token) - Autenticação e controle de acesso seguro.
- HATEOAS - Implementação de hipermídia para conformidade com os princípios REST.
- Postman - Ferramenta de teste e documentação de APIs.
- Swagger - Documentação automática dos endpoints da API.
