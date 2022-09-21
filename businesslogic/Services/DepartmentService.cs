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
    public class DepartmentService: IDepartmentService
    {
        
        //Repository<Department> repository = new Repository<Department>();
        //Repository<Employee> repositoryEmp = new Repository<Employee>();
        private readonly IRepository<Department> repository;
        private readonly IRepository<Employee> repositoryEmp;
        private readonly IEmpolyeeServices empolyeeServices;

        public DepartmentService(IRepository<Department> _repository, IRepository<Employee> _repositoryEmp, IEmpolyeeServices _empolyeeServices)
        {
            this.repository = _repository;
            this.empolyeeServices = _empolyeeServices;
            this.repositoryEmp = _repositoryEmp;
        }

        ////private readonly IRepository<Department> repository;
        ////private readonly IRepository<Employee> repositoryEmp;
        //private readonly IEmpolyeeServices empolyeeServices;

        //public DepartmentService(/*IRepository<Department> _repository, IRepository<Employee> _repositoryEmp,*/ IEmpolyeeServices _empolyeeServices)
        //{
        //    //this.repository = _repository;
        //    this.empolyeeServices = _empolyeeServices;
        //    //this.repositoryEmp = _repositoryEmp;
        //}

        public List<DepartmentDto> Load()
        {

            List<DepartmentDto> liDeto = new List<DepartmentDto>();
            List<Department> lidepartment = repository.LoadAll().ToList();
            foreach (var item in lidepartment)
            {
                DepartmentDto  departmentDto = new DepartmentDto();
                departmentDto.Id = item.Id;
                departmentDto.Name = item.Name;

                liDeto.Add(departmentDto);
            }
            return liDeto;

        }
        public void Insert(DepartmentDto DeptDto)
        {
            Department Dept = new Department();
            Dept.Id = DeptDto.Id;
            Dept.Name = DeptDto.Name;
            repository.Insert(Dept);
        }

        public void Delete(int Id)
        {
            var EmpID = empolyeeServices.Load().Where(e => e.departmentid == Id).Select(e => e.Id).ToList();
            var RoleID = empolyeeServices.Load().Where(e => e.departmentid == Id).Select(e => e.RoleId).ToList();

            foreach (var item in EmpID)
            {
                if (item != 0)
                {
                    Employee employee = new Employee();
                    employee = repositoryEmp.Load(item);
                    employee.departmentid = null;
                    employee.isDelete = true;
                    repositoryEmp.Update(employee);
                }
            }
            Department department = repository.Load(Id);
            repository.Delete(Id);
        }


        public DepartmentDto Edit(int Id)
        {
            Department department = new Department();
            department = repository.Load(Id);
            DepartmentDto departmentDto = new DepartmentDto();
            departmentDto.Id= department.Id;
            departmentDto.Name = department.Name;
            return departmentDto;
        }
        public void update(DepartmentDto DeptDto)
        {
            Department Dept = new Department();
            Dept.Id = DeptDto.Id;
            Dept.Name = DeptDto.Name;
            repository.Update(Dept);
        }

      
    }
}