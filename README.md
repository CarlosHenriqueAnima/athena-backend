# athena-backend

Em nossa arquitetura de microservices, a parte de infraestrutura do banco de dados desempenha um papel fundamental para garantir a escalabilidade, a disponibilidade e o desempenho adequado do sistema. 

# Exemplos de Uso:
Nesta infraestrutura de banco de dados, existe nossa base de persistência do sistema, armazenando todos os dados relevantes para o funcionamento da plataforma. Além disso, essa infraestrutura também serve como ponto de acesso para sistemas legados, como o sistema de certificados, que consomem os dados necessários para suas operações.

# Teste:

Apesar de termos realizado os testes apenas em produção, para mitigar qualquer risco e garantir a confiabilidade do código, utilizamos no GitHub o conceito de "branch". Um branch é uma ramificação do desenvolvimento, uma linha independente que nos permite trabalhar em novos recursos ou correções de bugs sem afetar diretamente o código principal. Dessa forma, conseguimos isolar o trabalho em progresso e permitir que várias pessoas trabalhem em diferentes funcionalidades simultaneamente. Ao utilizar branches, podemos desenvolver e testar com segurança antes de incorporar as alterações ao código principal, evitando problemas e bugs que poderiam impactar diretamente a produção.
 
# Requisitos do Sistema:
PostgreSQL

