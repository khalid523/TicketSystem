using businesslogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketinDataAccess.Entity.data;
namespace businesslogic.Services
{
   public interface IHistoryService
    {
        List<HistoryDto> LoadAll();
    }
}
