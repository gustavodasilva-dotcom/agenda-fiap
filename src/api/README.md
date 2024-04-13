# api

Esta pasta contém os projetos que constituem a API.

Segue a organização das pastas:

## /src

Contém os projetos que estruturam as camadas da aplicação:

- `Api`: camada inicial com endpoints que recebem e retornam dados requisitados;
- `Application`: camada que processa as requisições e realiza as chamadas ao `Domain`;
- `Domain`: camada que contém as principais regras de negócio; e
- `Infrastructure`: camada que realiza o acesso ao banco de dados.

## /tests

Contém o projeto `UnitTests` -- o projeto de testes unitários da API.