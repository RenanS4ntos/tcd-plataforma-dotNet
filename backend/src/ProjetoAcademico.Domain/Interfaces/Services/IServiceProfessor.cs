using ProjetoAcademico.Domain.DTOs.Common;
using ProjetoAcademico.Domain.DTOs.ProfessorDto.Adicionar;
using ProjetoAcademico.Domain.DTOs.ProfessorDto.Atualizar;
using ProjetoAcademico.Domain.DTOs.ProfessorDto.Listar;
using ProjetoAcademico.Domain.DTOs.ProfessorDto.Obter;

namespace ProjetoAcademico.Domain.Interfaces.Services
{
    public interface IServiceProfessor
    {
        ServiceResponse<IEnumerable<ProfessorListarDto>> Listar();
        ServiceResponse<ProfessorObterDto> Obter(Guid id);
        ServiceResponse<BaseResponse> Adicionar(ProfessorAdicionarDto ProfessorDto);
        ServiceResponse<BaseResponse> Atualizar(ProfessorAtualizarDto ProfessorDto);
        ServiceResponse<BaseResponse> Remover(Guid id);
    }
}
