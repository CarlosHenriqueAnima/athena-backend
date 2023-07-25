using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IAlunoRepository _alunoRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository, IAlunoRepository alunoRepository)
        {
            _matriculaRepository = matriculaRepository;
            _alunoRepository = alunoRepository;
        }

        public async Task<MatriculaResponse> ObterMatricula(int id)
        {
            var matricula = await _matriculaRepository.ObterMatriculaPorIdAsync(id);
            if (matricula == null)
            {
                return null;
            }

            // Faça o mapeamento do modelo de matrícula (MatriculaModel) para a classe de resposta (MatriculaResponse) aqui, se necessário.
            var matriculaResponse = new MatriculaResponse
            {
                Id = matricula.Id,
                AlunoId = matricula.AlunoId,
                DataMatricula = matricula.DataMatricula,
                Curso = matricula.Curso
            };

            return matriculaResponse;
        }

        public async Task<IEnumerable<MatriculaResponse>> ObterTodasMatriculas()
        {
            /// Implemente a lógica para obter todas as matrículas usando o repositório.
            var matriculas = await _matriculaRepository.ObterTodasMatriculasAsync();

            // Faça o mapeamento de cada modelo de matrícula (MatriculaModel) para a classe de resposta (MatriculaResponse) aqui.
            var matriculasResponse = matriculas.Select(matricula => new MatriculaResponse
            {
                Id = matricula.Id,
                AlunoId = matricula.AlunoId,
                DataMatricula = matricula.DataMatricula,
                Curso = matricula.Curso
            });

            return matriculasResponse;
        }

        public async Task<NovaMatriculaResponse> CadastrarMatricula(MatriculaModel request)
        {
            
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "A solicitação de matrícula não pode ser nula.");
            }

            // Implemente a lógica para validar os dados da matrícula, se necessário.

            try
            {
                // Faça o mapeamento dos dados da solicitação (NovaMatriculaRequest) para o modelo de matrícula (MatriculaModel)
                var matriculaModel = new MatriculaModel
                {
                    AlunoId = request.AlunoId,
                    DataMatricula = DateTime.Now, // Defina a data de matrícula como a data atual
                    Curso = request.Curso // Defina o curso com base na solicitação
                };

                // Adicione a nova matrícula usando o repositório
                await _matriculaRepository.AdicionarMatriculaAsync(matriculaModel);

                // Mapeamento do modelo de matrícula (MatriculaModel) para a resposta (NovaMatriculaResponse)
                var matriculaResponse = new NovaMatriculaResponse
                {
                    Id = matriculaModel.Id,
                    AlunoId = matriculaModel.AlunoId,
                    DataMatricula = matriculaModel.DataMatricula,
                    Curso = matriculaModel.Curso
                };

                return matriculaResponse;
            }
            catch (Exception ex)
            {
                // Aqui você pode fazer algum tratamento ou log da exceção, se necessário.
                throw new Exception("Ocorreu um erro ao cadastrar a matrícula.", ex);
            }
        }

        public async Task<MatriculaResponse> AtualizarMatricula(MatriculaModel request) // Alterar aqui
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "A solicitação de matrícula não pode ser nula.");
            }

            // Implemente a lógica para atualizar uma matrícula usando o repositório e o mapeamento para o modelo de matrícula (MatriculaModel) aqui, se necessário.
            try
            {
                // Obtém a matrícula existente pelo ID
                var matriculaExistente = await _matriculaRepository.ObterMatriculaPorIdAsync(request.Id);
                if (matriculaExistente == null)
                {
                    throw new Exception($"Matrícula com ID {request.Id} não encontrada.");
                }

                // Faça o mapeamento dos dados da solicitação (MatriculaRequest) para o modelo de matrícula (MatriculaModel)
                matriculaExistente.AlunoId = request.AlunoId;
                matriculaExistente.Curso = request.Curso;

                // Atualiza a matrícula usando o repositório
                await _matriculaRepository.AtualizarMatriculaAsync(matriculaExistente);

                // Faça o mapeamento do modelo de matrícula (MatriculaModel) para a classe de resposta (MatriculaResponse)
                var matriculaResponse = new MatriculaResponse
                {
                    Id = matriculaExistente.Id,
                    AlunoId = matriculaExistente.AlunoId,
                    DataMatricula = matriculaExistente.DataMatricula,
                    Curso = matriculaExistente.Curso
                };

                return matriculaResponse;
            }
            catch (Exception ex)
            {
                // Aqui você pode fazer algum tratamento ou log da exceção, se necessário.
                throw new Exception("Ocorreu um erro ao atualizar a matrícula.", ex);
            }
        }

        public async Task<bool> DesativarMatricula(int id)
        {
            // Implemente a lógica para desativar uma matrícula usando o repositório.
            // Exemplo:
            // await _matriculaRepository.RemoverMatriculaAsync(id);
            // Faça o tratamento do retorno da remoção e retorne true ou false, conforme a necessidade.
            return true; // ou false
        }
    }
}
