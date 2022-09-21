using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace businesslogic.Services
{
   public interface ITicketService
    {

        void Insert(TicketsDto ticketsDto, int userId, int ProjectsId);
        void update(Tickets T);
        List<TicketsDto> ticketsDtos(int? userId);
        void Done(TicketsDto ticketsDto, int Id, int userId);
        void Pull(TicketsDto ticketsDto, int Id, int userId);
        void urgent(HistoryDto historyDto, int ProjectsId, string ProjectName);
        void Reject(TicketsDto ticketsDto, int Id);
        void escalation(HistoryDto historyDto, int Id);
        void EscalationToLeader(HistoryDto historyDto,int Id);
    }
}
