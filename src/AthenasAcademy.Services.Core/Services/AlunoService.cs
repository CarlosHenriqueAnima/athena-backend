<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AutoMapper;
using AthenasAcademy.Services.Domain.Responses;
=======
﻿using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

>>>>>>> 7f24942145c284dece18f132624604ac986811b1

namespace AthenasAcademy.Services.Core.Services
{
<<<<<<< HEAD
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;
        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
            _mapper = AutoMapperConfig.Configure();
        }

        public async Task<AlunoModel> AtualizarAluno(AlunoModel aluno)
        {
            if (aluno == null)
            {
                throw new ArgumentNullException(nameof(aluno), "O aluno não pode ser nulo.");
            }

            // Implemente aqui a validação dos dados do aluno, se necessário.

            await _alunoRepository.AtualizarAlunoAsync(aluno);
            return aluno;
        }

        public async Task<AlunoModel> AdicionarAluno(AlunoModel aluno)
        {
            if (aluno == null)
            {
                throw new ArgumentNullException(nameof(aluno), "O aluno não pode ser nulo.");
            }

            // Implemente aqui a validação dos dados do aluno, se necessário.

            try
            {
                await _alunoRepository.AdicionarAlunoAsync(aluno);
                return aluno;
            }
            catch (Exception ex)
            {
                // Aqui você pode fazer algum tratamento ou log da exceção, se necessário.
                throw new Exception("Ocorreu um erro ao adicionar o aluno.", ex);
            }
        }

        public async Task<bool> DesativarAluno(int id)
        {
            // Verifique se o aluno existe antes de desativá-lo
            var alunoExistente = await _alunoRepository.ObterAlunoPorIdAsync(id);
            if (alunoExistente == null)
            {
                return false; // Indica que o aluno não foi encontrado
            }

            await _alunoRepository.RemoverAlunoAsync(id);
            return true; // Indica que o aluno foi desativado com sucesso
        }

        public async Task<AlunoModel> ObterAlunoPorId(int id)
        {

            return await _alunoRepository.ObterAlunoPorIdAsync(id);
            //if (alunoModel == null)
            //{
              //  return null;
            //}
            //var alunoResponse = _mapper.Map<AlunoResponse>(alunoModel);
            //var alunoModelFromResponse = _mapper.Map<AlunoModel>(alunoResponse);
           // return alunoModelFromResponse;
        }

        public async Task<AlunoModel> ObterAlunoPorIdAsync(int id)
        {
            return await _alunoRepository.ObterAlunoPorIdAsync(id);
        }

        public async Task<IEnumerable<AlunoModel>> ObterTodosAlunos()
        {
            return await _alunoRepository.ObterTodosAlunosAsync();
        }
=======
    private readonly IAlunoRepository _alunoRepository;

    public AlunoService(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<AlunoModel> AtualizarAluno(AlunoModel aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno), "O aluno não pode ser nulo.");
        }

        // Implemente aqui a validação dos dados do aluno, se necessário.

        await _alunoRepository.AtualizarAlunoAsync(aluno);
        return aluno;
    }

    public async Task<AlunoModel> AdicionarAluno(AlunoModel aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno), "O aluno não pode ser nulo.");
        }

        // Implemente aqui a validação dos dados do aluno, se necessário.

        await _alunoRepository.AdicionarAlunoAsync(aluno);
        return aluno;
    }

    public async Task<bool> DesativarAluno(int id)
    {
        // Verifique se o aluno existe antes de desativá-lo
        var alunoExistente = await _alunoRepository.ObterAlunoPorIdAsync(id);
        if (alunoExistente == null)
        {
            return false; // Indica que o aluno não foi encontrado
        }

        await _alunoRepository.RemoverAlunoAsync(id);
        return true; // Indica que o aluno foi desativado com sucesso
    }

    public async Task<AlunoModel> ObterAlunoPorId(int id)
    {
        return await _alunoRepository.ObterAlunoPorIdAsync(id);
    }

    public async Task<IEnumerable<AlunoModel>> ObterTodosAlunos()
    {
        return await _alunoRepository.ObterTodosAlunosAsync();
    }

    Task IAlunoService.ObterAlunoPorIdAsync(int id)
    {
        throw new NotImplementedException();
>>>>>>> 7f24942145c284dece18f132624604ac986811b1
    }
}
