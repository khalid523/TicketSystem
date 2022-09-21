using businesslogic.Dto;
using businesslogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService _departmentService)
        {
            departmentService = _departmentService;
        }
        [HttpGet]
        [Route("GetDepartment")]
        public List<DepartmentDto> Get()
        {
            List<DepartmentDto> departmentDtos = departmentService.Load();
            return departmentDtos;

        }
        [HttpPost]
        [Route("InsertDepartment")]
        public IHttpActionResult Insert(DepartmentDto departmentDto)
        {
            


            var DepartmentName = departmentService.Load().Where(u => u.Name == departmentDto.Name).FirstOrDefault();

            if (DepartmentName == null)
            {
                departmentService.Insert(departmentDto);
                return StatusCode(HttpStatusCode.OK);

            }
            else
            {

                return StatusCode(HttpStatusCode.NotModified);

            }
        }
        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(int Id) 
        {
            if (Id <= 0)
                return BadRequest("Not a valid student id");

            departmentService.Delete(Id);
            return Ok();
         

        }
        [HttpPut]
        [Route("Update")]
        public void Update(DepartmentDto departmentDto)
        {
            departmentService.update(departmentDto);
        }

        [HttpGet]
        [Route("loadBLoadId")]
        public DepartmentDto loadBLoadId(int Id)
        {

            return departmentService.Edit(Id);
        }
    }
}
