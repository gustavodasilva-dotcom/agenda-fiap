# Agenda FIAP: Sistema de Gestão de Eventos e Contatos

Este repositório contém o código-fonte da terceira atividade do Tech Challenge do curso **Arquitetura de Sistemas .NET com Azure**, da Pós Tech da FIAP.

O objetivo deste projeto foi implementar uma arquitetura de microsserviços e aplicar conceitos de mensageria, refatorando uma API de contatos existente (proveniente de desafios anteriores) e adicionando outros dois microsserviços para gerenciar eventos e notificar usuários por e-mail. O sistema agora é composto por três microsserviços: **Contatos**, **Eventos** e **Notificações**.

## Visão Geral do Projeto
A ideia central da aplicação é permitir que os usuários criem e gerenciem eventos, associem contatos a esses eventos e enviem notificações. Cada evento representa uma atividade ou tarefa futura que pode envolver múltiplos participantes. Nos inspiramos em ferramentas como o **Google Calendar** para desenvolver este conceito.

A interface _frontend_ oferece uma experiência de usuário fluida, permitindo a gestão de contatos e eventos de maneira intuitiva.

### Microsserviços
O projeto está estruturado em três principais microsserviços, localizados na pasta `src/Modules`:

1. **Contatos**: Gerencia os dados de contato, incluindo operações de CRUD.
2. **Eventos**: Gerencia os dados de eventos, incluindo operações de CRUD, além de lidar com a criação, modificação e associação de contatos a eventos.
3. **Notificações**: Responsável por enviar notificações por e-mail para os contatos associados aos eventos.

Utilizamos **RabbitMQ** em conjunto com **MassTransit** para realizar a troca de mensagens entre os serviços. As principais interações incluem:

- Ao criar um evento no serviço `Eventos`, são enviadas mensagens ao serviço `Contatos` para registrar as associações entre contatos e eventos.
- Uma mensagem é enviada ao serviço `Notificações` para disparar e-mails de notificação para os contatos envolvidos.
- Quando um contato é removido do sistema, o `Contatos` envia uma mensagem ao `Eventos`, que remove o contato dos eventos futuros aos quais estava vinculado.

Essa arquitetura permite que a mensageria seja aplicada a diferentes cenários de negócios, garantindo o desacoplamento e a escalabilidade do sistema.

### Frontend
A aplicação web, localizada na pasta `src/Web` e desenvolvida com o _framework_ **Blazor**, oferece interfaces para a gestão de contatos e eventos. Ela interage com os microsserviços para persistir os dados e proporcionar aos usuários uma experiência simplificada para organizar seus contatos e eventos.

### Monitoramento
Para garantir a confiabilidade do sistema, integramos o **Prometheus** e o **Grafana** para monitorar o desempenho e a saúde dos microsserviços, assim como acompanhar a gestão e o fluxo de mensagens do **RabbitMQ**.