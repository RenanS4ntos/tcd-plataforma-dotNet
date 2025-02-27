using ProjetoAcademico.Domain.DTOs.Common;
using ProjetoAcademico.Domain.DTOs.ProfessorDto.Adicionar;
using ProjetoAcademico.Domain.DTOs.ProfessorDto.Atualizar;
using ProjetoAcademico.Domain.DTOs.ProfessorDto.Listar;
using ProjetoAcademico.Domain.DTOs.ProfessorDto.Obter;
using ProjetoAcademico.Domain.Entities;
using ProjetoAcademico.Domain.Enumerators;
using ProjetoAcademico.Domain.Interfaces.Repositories;
using ProjetoAcademico.Domain.Interfaces.Services;
using ProjetoAcademico.Infra.CrossCutting.NotificationPattern;

namespace ProjetoAcademico.Domain.Services
{
    public class ServiceProfessor(IRepositoryProfessor repositoryProfessor) : Notifiable, IServiceProfessor
    {
        public ServiceResponse<IEnumerable<ProfessorListarDto>> Listar()
        {
            var Professors = repositoryProfessor.Listar();
            var ProfessorsDto = Professors.Select(Professor => new ProfessorListarDto
            {
                Id = Professor.Id,
                Nome = Professor.Nome,
                Biografia = Professor.Biografia
            });

            return new ServiceResponse<IEnumerable<ProfessorListarDto>>(ProfessorsDto, this);
        }

        public ServiceResponse<ProfessorObterDto> Obter(Guid id)
        {
            var Professor = repositoryProfessor.Obter(id);
            if (Professor is null) // ou if (Professor == null)
            {
                AddNotification("Professor", "Professor não encontrado");
                return new ServiceResponse<ProfessorObterDto>(this);
            }

            var ProfessorDto = new ProfessorObterDto()
            {
                Id = Professor.Id,
                Nome = Professor.Nome,
                Biografia = Professor.Biografia
            };

            return new ServiceResponse<ProfessorObterDto>(ProfessorDto, this);
        }

        public ServiceResponse<BaseResponse> Adicionar(ProfessorAdicionarDto ProfessorDto)
        {
            var validation = new ProfessorAdicionarDtoValidator().Validate(ProfessorDto);
            if (!validation.IsValid)
            {
                AddNotifications(validation.Errors);
                return new ServiceResponse<BaseResponse>(this);
            }

            var Professor = new Professor(
                ProfessorDto.Nome,
                ProfessorDto.Biografia
                );

            repositoryProfessor.Adicionar(Professor);
            repositoryProfessor.Commit();

            return new ServiceResponse<BaseResponse>(
                new BaseResponse(Professor.Id, "Professor Adicionado com Sucesso."), 
                this);
        }

        public ServiceResponse<BaseResponse> Atualizar(ProfessorAtualizarDto ProfessorDto)
        {
            var validation = new ProfessorAtualizarDtoValidator().Validate(ProfessorDto);
            if (!validation.IsValid)
            {
                AddNotifications(validation.Errors);
                return new ServiceResponse<BaseResponse>(this);
            }

            var Professor = repositoryProfessor.Obter(ProfessorDto.Id);
            if (Professor is null)
            {
                AddNotification("Professor", "Professor não encontrado");
                return new ServiceResponse<BaseResponse>(this);
            }

            Professor.Atualizar(
                ProfessorDto.Nome,
                ProfessorDto.Biografia
            );

            repositoryProfessor.Commit();

            return new ServiceResponse<BaseResponse>(new BaseResponse(Professor.Id, "Professor Atualizado com Sucesso."), this);
        }

        public ServiceResponse<BaseResponse> Remover(Guid id)
        {
            var Professor = repositoryProfessor.Obter(id);
            if (Professor is null)
            {
                AddNotification("Professor", "Professor não encontrado");
                return new ServiceResponse<BaseResponse>(this);
            }

            repositoryProfessor.Remover(Professor);
            repositoryProfessor.Commit();

            return new ServiceResponse<BaseResponse>(new BaseResponse(id, "Professor Removido com Sucesso."), this);
        }
    }
}
