# API Athenas Academy
Descrição

A API API Athenas Academy fornece acesso a um sistema educacional que gerencia informações relacionadas a Alunos, Cursos, Certificados e Disciplinas. Ela oferece endpoints para consulta e manipulação desses dados, permitindo que desenvolvedores integrem facilmente as funcionalidades educacionais em suas aplicações.

## Recursos Disponíveis

### A API oferece os seguintes recursos principais:

**Alunos**
- [x] Listar todos os alunos cadastrados.
- [x] Cadastrar um novo aluno.
 
**Cursos**
- [x] Listar todos os cursos disponíveis.
- [x] Listar todos os cursos disponíveis.
- [x] Cadastrar um novo curso.
- [x] Consultar detalhes de um curso específico.
- [x] Atualizar informações de um curso existente.
- [x] Excluir um curso do sistema.


**Certificado**

- [x] Listar todos os certificados emitidos.
- [x] Emitir um novo certificado para um aluno e um curso específico.
- [x] Consultar detalhes de um certificado emitido.
- [x] Revogar um certificado previamente emitido.

**Disciplina**
- [x] Listar todas as disciplinas cadastradas.
- [x] Cadastrar uma nova disciplina.
- [x] Consultar detalhes de uma disciplina específica.
- [x] Atualizar informações de uma disciplina existente.
- [x] Excluir uma disciplina do sistema.

**Autenticação**
Para acessar os endpoints da API, é necessário autenticar-se usando Header de autenticação JWT - Schema Bearer. 


**Principais end-points da API**

➡️ Alunos
- GET /api/v1/alunos:                                           _Obter a lista de todos os alunos._
- POST /api/v1/alunos:                                          _Cadastrar um novo aluno._
   

➡️ Cursos

- GET/api/v1/curso/{id}                                          _Obtém um curso pelo seu ID._

- GET/api/v1/curso/todos                                         _Obtém todos os cursos cadastrados._

- POST/api/v1/curso/registrar                                    _Cadastra um novo curso._

- PUT /api/v1/curso/atualizar                                    _Atualiza os dados de um curso existente._

- DELETE /api/v1/curso/desativar/{id}                            _Desativa um curso existente._

- GET /api/v1/curso/disciplina/{id}                               _Obtém uma disciplina pelo seu ID._

- DELETE /api/v1/curso/disciplina/{id}                            _Desativa uma disciplina existente._

- GET /api/v1/curso/disciplina/todos                              _Obtém todas as disciplinas cadastradas._

- POST /api/v1/curso/disciplina/registrar                         _Cadastra uma nova disciplina._

- PUT /api/v1/curso/disciplina/atualizar                          _Atualiza os dados de uma disciplina existente._

- GET /api/v1/curso/area-conhecimento/{id}                        _Obtém uma área de conhecimento pelo seu ID._

- GET /api/v1/curso/area-conhecimento/todos                       _Obtém todas as áreas de conhecimento cadastradas._

- POST /api/v1/curso/area-conhecimento/registrar                  _Cadastra uma nova área de conhecimento._

- PUT /api/v1/curso/area-conhecimento/atualizar                   _Atualiza os dados de uma área de conhecimento existente._

- DELETE /api/v1/curso/area-conhecimento/desativar/{id}           _Desativa uma área de conhecimento existente._

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


# Exemplos de Requisições

Aqui estão alguns exemplos de como fazer solicitações para os endpoints da API usando diferentes métodos HTTP (GET, POST, PUT, DELETE) e parâmetros.
Respostas da API

A API retornará respostas com códigos de status HTTP padrão para indicar o resultado de uma solicitação. 

Código 500: Servidor não encontrado
![image](https://github.com/athenasacademy/athena-backend/assets/106875411/7450167c-83ef-439d-b5db-0101ff6c0f42) 

Código 404 arquivo não encontrado
![image](https://github.com/athenasacademy/athena-backend/assets/106875411/a0923853-f59d-487d-bcd6-11986470c96d)


Código 200 - Sucesso
![image](https://github.com/athenasacademy/athena-backend/assets/106875411/3f1d18b8-4dda-481c-9a48-ecb6d491e839)



# Uso da API

[Inserir instruções ou exemplos de como integrar e usar a API em diferentes tecnologias e linguagens de programação.]
Limitações e Considerações

[Inserir informações sobre quaisquer limitações conhecidas da API, como taxa de limite de solicitações, recursos restritos, etc.]
Contribuições

Para qualquer dúvida, problema ou sugestão, entre em contato com [inserir endereço de e-mail ou link para o suporte aqui].
.
