using businesslogic.Dto;
using businesslogic.Enum;
using businesslogic.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TicketinDataAccess.Repository;
using Ticketinsystems.data;

namespace businesslogic.Services
{
    public class ClientService: IClientService
    {
        private readonly IRepository<Client> repository;
        private readonly IPmService pmService;
        private readonly IRepository<ProjectMember> rrepository;
        private readonly IRepository<Users> repositoryUser;
        private readonly IRepositoryClient repositoryClient;
        private readonly IRepository<Tickets> repositorytickets;
        public ClientService(IRepository<Client> _repository, IPmService _pmService, IRepository<ProjectMember> _rrepository, IRepository<Users> _repositoryUser, IRepositoryClient _repositoryClient, IRepository<Tickets> _repositorytickets)
        {
            pmService = _pmService;
            rrepository = _rrepository;
            repositoryUser = _repositoryUser;
            repository = _repository;
            repositoryClient = _repositoryClient;
            repositorytickets = _repositorytickets;
        }
        public List<ClientDto> ClientDtos()
        {
            List<ClientDto> liDeto = new List<ClientDto>();
            IEnumerable<Client> li = repositoryClient.GetALL().Where(x => x.isDelete == false);
            foreach (var item in li)
            {

                ClientDto CDto = new ClientDto();
                CDto.projects = new ProjectsDto();
                CDto.Role = new UserRoleDto();
                CDto.User = new UsersDato();
                CDto.User.Role = new UserRoleDto();
                CDto.Id = item.Id;
                CDto.Name = item.Name;
                CDto.Email = item.Email;
                CDto.Password = item.Password;
                CDto.Informations = item.Informations;
                if (item.Role != null)
                {
                    CDto.Role.RoleName = item.Role.RoleName;
                    CDto.Role.RoleId = item.Role.RoleId;
                    CDto.RoleId = item.RoleId;
                }
              
                if (item.projects != null)
                {
                    CDto.projects.Name = item.projects.Name;
                    CDto.projects.Id = item.projects.Id;
                    CDto.projectsId = item.projectsId;
                }
                if (item.User != null)
                {
                    CDto.UserId = item.UserId;
                    CDto.User.Name = item.User.Name;
                    CDto.User.Password = item.User.Password;
                    CDto.User.Phone = item.User.Phone;
                    CDto.User.Role.RoleName = item.User.Role.RoleName;
                    CDto.User.RoleId = item.User.RoleId;
                    CDto.User.Role.RoleId = item.User.Role.RoleId;
                }
                CDto.User.ProjectMembers = new List<ProjectMemberDto>();
                foreach (var item1 in item.User.ProjectMembers)
                {
                    ProjectMemberDto projectMemberDto = new ProjectMemberDto();
                    projectMemberDto.Projects = new ProjectsDto();
                    projectMemberDto.ProjectsId = item1.ProjectsId;
                    projectMemberDto.UserId = item1.UserId;
                    projectMemberDto.Projects.Name = item1.Projects.Name;
                    CDto.User.ProjectMembers.Add(projectMemberDto);
                }

                liDeto.Add(CDto);
            }
            return (liDeto);
        }
        public void Insert(ClientDto clientDto, int[] projectsId, int RoleId)
        {
            Users users = new Users();
            users.Name = clientDto.Name;
            users.Password = encrypt(clientDto.Password);
            users.RoleId = clientDto.RoleId;
            users.Email = clientDto.Email;
            repositoryUser.Insert(users);
            Client client = new Client();
            client.Name = clientDto.Name;
            client.Email = clientDto.Email;
            client.projectsId = clientDto.projectsId;
            client.RoleId = clientDto.RoleId;
            client.UserId = users.Id;
            client.Password = encrypt(clientDto.Password);
            client.Informations = clientDto.Informations;
            foreach (var item1 in projectsId)
            {
                clientDto.projectsId = item1;
            }
            repository.Insert(client);
            ProjectMemberDto pm = new ProjectMemberDto();
            foreach (var item in projectsId)
            {
                pm.ProjectsId = item;
                pm.UserId = users.Id;
                pmService.Insert(pm);
            }

        }
        public void Delete(int Id)
        {
            var client = repository.LoadAll().Where(x=>x.Id== Id).Select(x=>x.UserId).FirstOrDefault();
            var t = repositorytickets.LoadAll().Where(tt =>tt.UserId == client).ToList();
            if (t.Any())
            {
                foreach (var item in t)
                {
                    item.UserId = null;
                    item.Status = Status.Done.ToString();
                    repositorytickets.Update(item);
                }
            }
            Client client1 = repository.Load(Id);
            client1.isDelete = true;
            repository.Update(client1);
        }

        public ClientDto Edit(int Id)
        {
            var Client = repository.Load(Id);
            ClientDto clientDto = new ClientDto();
            clientDto.Name = Client.Name;  
            clientDto.Email = Client.Email;
            clientDto.projectsId = Client.projectsId;
            clientDto.RoleId = Client.RoleId;
            clientDto.UserId = Client.UserId;
            clientDto.Password = Decrypt(Client.Password);
            clientDto.Informations = Client.Informations;
            return clientDto;
        }
        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            var cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public void update(ClientDto clientDto, int[] projectsId, int RoleId)
        {
            if (RoleId != 0)
            {
                var b = rrepository.LoadAll().Where(p => p.UserId == clientDto.UserId).FirstOrDefault();
                var pm = rrepository.LoadAll().Where(p => p.UserId == b.UserId).ToList();
                foreach (var item in pm)
                {
                    rrepository.Deletet(item);
                }
                ProjectMemberDto pmm = new ProjectMemberDto();
                foreach (var item in projectsId)
                {
                    pmm.ProjectsId = item;
                    pmm.UserId = clientDto.UserId;
                    pmService.Insert(pmm);
                }
            }
            else
            {
            }
            if (clientDto.UserId.HasValue)
            {
                Users u = repositoryUser.Load(clientDto.UserId.Value);
                u.Email = clientDto.Email;
                u.Name = clientDto.Name;
                u.Password = encrypt(clientDto.Password);
                u.RoleId = clientDto.RoleId;
                repositoryUser.Update(u);
                Client client = new Client();
                client.Id = clientDto.Id;
                client.Email = clientDto.Email;
                client.Name = clientDto.Name;
                client.Password = encrypt(clientDto.Password);
                client.RoleId = clientDto.RoleId;
                client.Informations = clientDto.Informations;
                client.projectsId = clientDto.projectsId;
                client.UserId = u.Id;
                repository.Update(client);
            }
        }
        public string encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}