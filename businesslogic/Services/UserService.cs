using businesslogic.Dto;
using businesslogic.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketinDataAccess.Repository;
using Ticketinsystems.data;

namespace businesslogic.Services
{
    public class UserService: IUserService
    {

        public UserService(IRepository<Users> _repository,IRepositoryUser _repositoryUser)
        {
            repository = _repository;
            repositoryUser = _repositoryUser;
        }
 
        private readonly IRepository<Users> repository;
        private readonly IRepositoryUser repositoryUser;

        public List<UsersDato> LoadAll()
        { 
            List<UsersDato> liDeto = new List<UsersDato>();
            List<Users> liusers = repositoryUser.GetALL().Where(x => x.isDelete == false).ToList();
            foreach (var item in liusers)
            {  
               UsersDato usersDato = new UsersDato();
                usersDato.Role = new UserRoleDto();
                usersDato.Name = item.Name;
                usersDato.Password = item.Password;
                usersDato.Id = item.Id;
                usersDato.Phone = item.Phone;
                usersDato.Email = item.Email;
                usersDato.RoleId = item.RoleId;
                usersDato.Role.RoleName = item.Role.RoleName;
                liDeto.Add(usersDato);
            }
            return liDeto;
        }
        public void Insert(UsersDato user)
        {
            //db.users.Add(user);
            //db.SaveChanges();
        }

        public void Delete(int Id)
        {
            Users users = repository.Load(Id);
            users.isDelete = true;
            repository.Update(users);
            //repository.Delete(Id);
        }
    }
}