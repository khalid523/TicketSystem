using businesslogic.Dto;
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
    public class EmpolyeeServices : IEmpolyeeServices
    {
        private readonly IRepository<Employee> repository;
        private readonly IRepositoryEmpolyee repositoryEmpolyee;
        private readonly IPmService pmService;
        private readonly IRepository<ProjectMember> repositoryPM;
        private readonly IRepository<Users> repositoryUser;
        private readonly IRepository<Tickets> repositoryTickets;
        public EmpolyeeServices(IRepository<Employee> _repository, IRepositoryEmpolyee _repositoryEmpolyee,IPmService _pmService , IRepository<ProjectMember> _repositoryPM, IRepository<Users> _repositoryUser, IRepository<Tickets> _repositoryTickets)
        {
            this.repository = _repository;
            repositoryEmpolyee = _repositoryEmpolyee;
            pmService = _pmService;
            repositoryPM = _repositoryPM;
            repositoryUser = _repositoryUser;
            repositoryTickets = _repositoryTickets;
        }
            public List<EmpolyeeDto> Load()
             {

            List<EmpolyeeDto> liDetoEmp = new List<EmpolyeeDto>();
            IEnumerable<Employee> liEmpolyee = repositoryEmpolyee.GetALLD();
            foreach (var item in liEmpolyee)
            {
                EmpolyeeDto Emp = new EmpolyeeDto();
                Emp.department = new DepartmentDto();
                Emp.projects = new ProjectsDto();
                Emp.Role = new UserRoleDto();
                Emp.Id = item.Id;
                Emp.Name = item.Name;
                if (item.department != null)
                {
                    Emp.department.Name = item.department.Name;
                }
                if (item.projects != null)
                {
                    Emp.projects.Name = item.projects.Name;
                }
                if (item.Role != null)
                {
                    Emp.Role.RoleName = item.Role.RoleName;
                }
                Emp.departmentid = item.departmentid;
                Emp.Email = item.Email;
                Emp.Password = item.Password;

                Emp.projectsId = item.projectsId;

                Emp.RoleId = item.RoleId;
                liDetoEmp.Add(Emp);
            }


            return liDetoEmp;

        }

        public List<EmpolyeeDto> empolyeeDtos()
        {
            List<EmpolyeeDto> liDeto = new List<EmpolyeeDto>();
            IEnumerable<Employee> li = repositoryEmpolyee.GetALL().Where(x =>x.isDelete== false);
            foreach (var item in li) {

                EmpolyeeDto EmpDto = new EmpolyeeDto();
                EmpDto.department = new DepartmentDto();
                EmpDto.projects = new ProjectsDto();
                EmpDto.Role = new UserRoleDto();
                EmpDto.Id = item.Id;
                EmpDto.Name = item.Name;
                EmpDto.Salary = item.Salary;
                EmpDto.Email = item.Email;
                EmpDto.Password = item.Password;
            
               
           
                if (item.Role != null)
                {
                    EmpDto.Role.RoleName = item.Role.RoleName;
                    EmpDto.Role.RoleId = item.Role.RoleId;
          
                    EmpDto.RoleId = item.RoleId;
                }
                if (item.department != null)
                {
                    EmpDto.department.Name = item.department.Name;
                    EmpDto.department.Id = item.department.Id;
                    EmpDto.departmentid = item.departmentid;
                
                }
                if (item.projects != null)
                {
                    EmpDto.projects.Id = item.projects.Id;
                    EmpDto.projects.Name = item.projects.Name;
             
                    EmpDto.projectsId = item.projectsId;
                }
                EmpDto.User = new UsersDato();
                EmpDto.User.Role = new UserRoleDto();
                
                if (item.User != null)
                {
                    EmpDto.UserId = item.UserId;
                    EmpDto.User.Name = item.User.Name;
                    EmpDto.User.Password = item.User.Password;
                    EmpDto.User.Phone = item.User.Phone;
                    EmpDto.User.Id = item.User.Id;
                    EmpDto.User.Role.RoleId = item.User.Role.RoleId;
                    EmpDto.User.Role.RoleName = item.User.Role.RoleName;
                    EmpDto.User.ProjectMembers = new List<ProjectMemberDto>();
                  

                    foreach (var item1 in item.User.ProjectMembers)
                    {
                        ProjectMemberDto projectMemberDto = new ProjectMemberDto();
                        projectMemberDto.Projects = new ProjectsDto();
                        projectMemberDto.ProjectsId = item1.ProjectsId;

                        projectMemberDto.UserId = item1.UserId;
                        projectMemberDto.Projects.Name = item1.Projects.Name;
                        EmpDto.User.ProjectMembers.Add(projectMemberDto);
                    }

                    }
                    liDeto.Add(EmpDto);

            }
            return (liDeto);
        }
            public void Insert(EmpolyeeDto EmpDto, int[] projectsId, int RoleId, int departmentid)
           {

            Users users = new Users();
            users.Name = EmpDto.Name;
            users.Password = encrypt(EmpDto.Password);
            users.RoleId = EmpDto.RoleId;
            users.Email = EmpDto.Email;
            repositoryUser.Insert(users);
            Employee Emp = new Employee();
            Emp.Name = EmpDto.Name;
            Emp.Salary = EmpDto.Salary;
            Emp.Email = EmpDto.Email;
            Emp.projectsId = EmpDto.projectsId;
            Emp.departmentid = EmpDto.departmentid;
            Emp.RoleId = EmpDto.RoleId;
            Emp.UserId = users.Id;
            Emp.Password = encrypt(EmpDto.Password);
            foreach (var item1 in projectsId)
            {
                EmpDto.projectsId = item1;
            }
            repository.Insert(Emp);
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
            Employee employee = repository.Load(Id);
            var Tickets = repositoryTickets.LoadAll().Where(tt => tt.AssgintoId == employee.UserId).ToList();

            foreach (var item in Tickets)
            {
                item.AssgintoId = null;
                repositoryTickets.Update(item);
            }
    
            //repository.Delete(Id);
            employee.isDelete = true;
            repository.Update(employee);
        }

        public EmpolyeeDto Edit(int Id)
        {
            var x = repository.Load(Id);
            EmpolyeeDto EmpDto = new EmpolyeeDto();
            EmpDto.Name= x.Name;
            EmpDto.Salary = x.Salary;
            EmpDto.Email = x.Email;
            EmpDto.projectsId = x.projectsId;
            EmpDto.departmentid = x.departmentid;
            EmpDto.RoleId = x.RoleId;
            EmpDto.UserId = x.UserId;
            EmpDto.Password =Decrypt( x.Password);
            return EmpDto;
        }
        public void update(EmpolyeeDto empolyeeDto, int[] projectsId, int RoleId, int departmentid)
        {
            if (RoleId != 0)
            {
                var EmployeeForID = repositoryPM.LoadAll().Where(p => p.UserId == empolyeeDto.UserId).FirstOrDefault();
                var ForprojectMembers = repositoryPM.LoadAll().Where(p => p.UserId == EmployeeForID.UserId).ToList();

                foreach (var item in ForprojectMembers)
                {

                    repositoryPM.Deletet(item);
                }


                ProjectMemberDto projectMemberDto = new ProjectMemberDto();
                foreach (var item in projectsId)
                {
                    projectMemberDto.ProjectsId = item;
                    projectMemberDto.UserId = empolyeeDto.UserId;

                    pmService.Insert(projectMemberDto);

                }

            }
            else
            {


            }
        

            if (empolyeeDto.UserId.HasValue)
            {
                Users u = repositoryUser.Load(empolyeeDto.UserId.Value);
                u.Email = empolyeeDto.Email;
                u.Name = empolyeeDto.Name;
                u.Password =encrypt( empolyeeDto.Password);
                u.RoleId = empolyeeDto.RoleId;
                repositoryUser.Update(u);
                Employee Emp = new Employee();
                Emp.Id = empolyeeDto.Id;
                Emp.Email = empolyeeDto.Email;
                Emp.Name = empolyeeDto.Name;
                Emp.Password =encrypt( empolyeeDto.Password);
                Emp.RoleId = empolyeeDto.RoleId;
                Emp.departmentid = empolyeeDto.departmentid;
                Emp.projectsId = empolyeeDto.projectsId;
                Emp.UserId = u.Id;
                Emp.Salary = empolyeeDto.Salary;
                repository.Update(Emp);
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

    }
}