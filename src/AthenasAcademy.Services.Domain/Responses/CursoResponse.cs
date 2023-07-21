﻿using AthenasAcademy.Services.Domain.Requests;

namespace AthenasAcademy.Services.Domain.Responses;

public class CursoResponse
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public int CargaHoraria { get; set; }

    public List<DisciplinaRequest> Disciplinas { get; set; }

    public AreaConhecimentoRequest AreaConhecimento { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime DataAtualizacao { get; set; }
}