using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Services
{
   public interface IClientService
    {
        List<ClientDto> ClientDtos();
        void Insert(ClientDto clientDto, int[] projectsId, int RoleId);
        void Delete(int Id);
        ClientDto Edit(int Id);
        void update(ClientDto clientDto, int[] projectsId, int RoleId);
    }
}