using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Entity.data;
using TicketinDataAccess.Repository;

namespace businesslogic.Services
{
  public class HistoryService:IHistoryService
    {
        private readonly IRepository<History> repository;
        private readonly IRepositoryHistory repositoryHistory;

        public HistoryService(IRepository<History> _repository,IRepositoryHistory _repositoryHistory)
        {
            repository = _repository;
            repositoryHistory = _repositoryHistory;
        }

        public List<HistoryDto> LoadAll()
        {
            List<HistoryDto> liDeto = new List<HistoryDto>();
            List<History> liHistory = repositoryHistory.GetALL().ToList();
            foreach (var item in liHistory)
            {
                HistoryDto historyDto = new HistoryDto();
                historyDto.Projects = new ProjectsDto();
                historyDto.Id = item.Id;
                historyDto.Name = item.Name;
                historyDto.ProjectsId = item.ProjectsId;
                historyDto.Projects.Name =item.ProjectsId.HasValue?   item.Projects.Name:null;
                historyDto.IsUrgent = item.IsUrgent;
                historyDto.Description = item.Description;
                historyDto.Status = item.Status;
                historyDto.EscLeader = item.EscLeader;
                liDeto.Add(historyDto);
            }
            return liDeto;
        }
    }
}
