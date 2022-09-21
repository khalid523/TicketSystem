using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Repository;
using Ticketinsystems.data;

namespace businesslogic.Services
{
    public class permissionsService : IpermissionsService
    {
        private readonly IRepository<permissions> repositoryPermission;
        public permissionsService(IRepository<permissions> _repositoryPermission)
        {
            repositoryPermission = _repositoryPermission;
        }
        public List<permissionsDto> Load()
        {

            List<permissionsDto> liDeto = new List<permissionsDto>();
            List<permissions> lidepartment = repositoryPermission.LoadAll().ToList();
            foreach (var item in lidepartment)
            {
                permissionsDto permissionsDto = new permissionsDto();
                permissionsDto.Id = item.Id;
                permissionsDto.Name = item.Name;
                liDeto.Add(permissionsDto);
            }
            return liDeto;

        }
     
          
    }
}
