using System;
using System.Collections.Generic;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;

namespace AthenasAcademy.Services.Core.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public MatriculaModel CreateMatricula(int contratoId, int detalheContratoId, string codigoMatricula)
        {
            var matricula = new MatriculaModel
            {
                ContratoId = contratoId,
                DetalheContratoId = detalheContratoId,
                Ativo = true,
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now,
                CodigoMatricula = codigoMatricula
            };

            _matriculaRepository.CreateMatricula(matricula);
            return matricula;
        }

        public MatriculaModel GetMatriculaById(int id)
        {
            return _matriculaRepository.GetMatriculaById(id);
        }

        public IEnumerable<MatriculaModel> GetAllMatriculas()
        {
            return _matriculaRepository.GetAllMatriculas();
        }

        public void UpdateMatricula(MatriculaModel matricula)
        {
            matricula.DataAlteracao = DateTime.Now;
            _matriculaRepository.UpdateMatricula(matricula);
        }

        public void DeleteMatricula(int id)
        {
            _matriculaRepository.DeleteMatricula(id);
        }
    }
}