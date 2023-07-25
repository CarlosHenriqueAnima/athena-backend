# API Athenas Academy
Descrição

A API API Athenas Academy fornece acesso a um sistema educacional que gerencia informações relacionadas a Alunos, Cursos, Certificados e Disciplinas. Ela oferece endpoints para consulta e manipulação desses dados, permitindo que desenvolvedores integrem facilmente as funcionalidades educacionais em suas aplicações.

## Recursos Disponíveis

### A API oferece os seguintes recursos principais:
Alunos
    Listar todos os alunos cadastrados.
    Cadastrar um novo aluno.
 
Cursos

    Listar todos os cursos disponíveis.
    Cadastrar um novo curso.
    Consultar detalhes de um curso específico.
    Atualizar informações de um curso existente.
    Excluir um curso do sistema.

Certificados

    Listar todos os certificados emitidos.
    Emitir um novo certificado para um aluno e um curso específico.
    Consultar detalhes de um certificado emitido.
    Revogar um certificado previamente emitido.

Disciplinas

    Listar todas as disciplinas cadastradas.
    Cadastrar uma nova disciplina.
    Consultar detalhes de uma disciplina específica.
    Atualizar informações de uma disciplina existente.
    Excluir uma disciplina do sistema.

Autenticação

Para acessar os endpoints da API, é necessário autenticar-se usando Header de autenticação JWT - Schema Bearer. 

Endpoints
A seguir estão os endpoints principais da API


➡️ Alunos
    GET /api/v1/alunos: Obter a lista de todos os alunos.
    
    POST /api/v1/alunos: Cadastrar um novo aluno.
   

➡️ Cursos

GET/api/v1/curso/{id} Obtém um curso pelo seu ID.

GET/api/v1/curso/todos Obtém todos os cursos cadastrados.

POST/api/v1/curso/registrar Cadastra um novo curso.

PUT /api/v1/curso/atualizar Atualiza os dados de um curso existente.

DELETE /api/v1/curso/desativar/{id} Desativa um curso existente.

GET /api/v1/curso/disciplina/{id}Obtém uma disciplina pelo seu ID.

DELETE /api/v1/curso/disciplina/{id} Desativa uma disciplina existente.

GET /api/v1/curso/disciplina/todos Obtém todas as disciplinas cadastradas.

POST /api/v1/curso/disciplina/registrar Cadastra uma nova disciplina.

PUT /api/v1/curso/disciplina/atualizar Atualiza os dados de uma disciplina existente.

GET /api/v1/curso/area-conhecimento/{id} Obtém uma área de conhecimento pelo seu ID.

GET /api/v1/curso/area-conhecimento/todos Obtém todas as áreas de conhecimento cadastradas.

POST /api/v1/curso/area-conhecimento/registrar Cadastra uma nova área de conhecimento.

PUT /api/v1/curso/area-conhecimento/atualizar Atualiza os dados de uma área de conhecimento existente.

DELETE /api/v1/curso/area-conhecimento/desativar/{id} Desativa uma área de conhecimento existente.

➡️ Certificado

GET /api/v1/certificado
   
➡️ Inscricao

GET /api/v1/inscricao

➡️ Matricula

GET /api/v1/matricula

➡️ Pagamento

GET /api/v1/pagamento

➡️Usuario

POST /api/v1/usuario/registrar Registra um novo usuário.
POST /api/v1/usuario/loginRealiza o login de um usuário.
GET /api/v1/usuario/todosObtém informações de todos os usuários.


Exemplos de Requisições

Aqui estão alguns exemplos de como fazer solicitações para os endpoints da API usando diferentes métodos HTTP (GET, POST, PUT, DELETE) e parâmetros.
Respostas da API

A API retornará respostas com códigos de status HTTP padrão para indicar o resultado de uma solicitação. 

Código 500: Servidor não encontrado
![image](https://github.com/athenasacademy/athena-backend/assets/106875411/7450167c-83ef-439d-b5db-0101ff6c0f42) 

Código 404 arquivo não encontrado
![image](https://github.com/athenasacademy/athena-backend/assets/106875411/a0923853-f59d-487d-bcd6-11986470c96d)


Código 200 - Sucesso
![image](https://github.com/athenasacademy/athena-backend/assets/106875411/3f1d18b8-4dda-481c-9a48-ecb6d491e839)



Uso da API

[Inserir instruções ou exemplos de como integrar e usar a API em diferentes tecnologias e linguagens de programação.]
Limitações e Considerações

[Inserir informações sobre quaisquer limitações conhecidas da API, como taxa de limite de solicitações, recursos restritos, etc.]
Contribuições

Para qualquer dúvida, problema ou sugestão, entre em contato com [inserir endereço de e-mail ou link para o suporte aqui].
