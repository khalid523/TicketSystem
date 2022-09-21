using businesslogic.Dto;
using businesslogic.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Ticketinsystems.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService DepartmentService;
        private readonly IEmpolyeeServices empolyeeServices;
        public DepartmentController(IDepartmentService _departmentService, IEmpolyeeServices _empolyeeServices)
        {
            DepartmentService = _departmentService;
            empolyeeServices = _empolyeeServices;
        }
        [Authorize(Roles = "LoadDepartment")]
        public ActionResult Index1()
        {
         
            return View();
        }
        [Authorize(Roles = "LoadDepartment")]
        //public ActionResult List()
        //{
        //    return Json(DepartmentService.Load(), JsonRequestBehavior.AllowGet);
        //    //return View("List", DepartmentService.Load());
        //}
        //[Authorize(Roles = "LoadDepartment")]
        //public ActionResult List()
        //{

        //    return View("Index", DepartmentService.Load());
        //}

        public async Task<ActionResult> List()
        {
            IEnumerable<DepartmentDto> departmentDtos = null;

            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri("https://localhost:44380/api/");
                var result = await client.GetAsync("Department/GetDepartment");

                if (result.IsSuccessStatusCode)
                {
                    departmentDtos = await result.Content.ReadAsAsync<IList<DepartmentDto>>();
                }
                else
                {
                    departmentDtos = Enumerable.Empty<DepartmentDto>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            //return View( "Index",departmentDtos);
            return Json(departmentDtos, JsonRequestBehavior.AllowGet);
        }

        //[Authorize]
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[Authorize(Roles = "AddDepartment")]
        //[HttpPost]
        //public ActionResult Create(DepartmentDto DeptDto)
        //{
        //    List<DepartmentDto> li = new List<DepartmentDto>();
        //    var DepartmentName = DepartmentService.Load().Where(u => u.Name == DeptDto.Name).FirstOrDefault();

        //    if (DepartmentName == null)
        //    {
        //        DepartmentService.Insert(DeptDto);

        //    }
        //    else
        //    {
        //        //ViewBag.Messagealret = String.Format("the Name has existed");
        //        string msg = "the Name has existed";

        //        //li = DepartmentService.Load();
        //        return Json(new { Message = msg,falied=false});
        //        //return Json(li, JsonRequestBehavior.AllowGet);
        //    }
         
        //    //ViewBag.Message = String.Format("Department Has add");
        //    //li = DepartmentService.Load();
        //    return Json(new { falied = true} );

        //}
        [HttpPost]
        public ActionResult create(DepartmentDto DeptDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<DepartmentDto>("Department/InsertDepartment/", DeptDto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { falied = true });
                }
                else
                {
                    return Json(new {MS= "TJHGF", falied = false });

                }
            }

            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
        }
        public ActionResult Delete(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/");

                var deleteTask = client.DeleteAsync("Department/Delete?Id=" + Id.ToString());
                //var responseTask = client.GetAsync("student?id=" + id.ToString());

                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return Json(new { ssuccess=true });
                }

            }

            return Json(new { MS = "error"});
        }
        //[Authorize]
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[Authorize(Roles = "AddDepartment")]
        //[HttpPost]
        //public JsonResult CreateAjax(DepartmentDto DeptDto)
        //{
        //    DepartmentService.Insert(DeptDto);
        //    ViewBag.Message = String.Format("Department Has add");
        //    List<DepartmentDto> li = new List<DepartmentDto>();
        //    li = DepartmentService.Load();
        //    //return Json(empDB.Add(emp), JsonRequestBehavior.AllowGet);

        //}
        public ActionResult Find(int Id)
        {
          
            return View("View", DepartmentService.Edit(Id));
        }
        //[Authorize(Roles = "DeleteDepartment")]
 
        //public ActionResult Delete(int Id)
        //{ 
        //    DepartmentService.Delete(Id);
        //    List <DepartmentDto> li = new List<DepartmentDto>();
        //    li = DepartmentService.Load();
        //    ViewBag.MessageDelete = String.Format("Department Has Delete");
        //    return Json( JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public ActionResult EditAjax(int Id)
        //{
        //    TempData["IdEmp"] = empolyeeServices.Load().Where(e => e.departmentid == Id).Select(e => e.Id).FirstOrDefault();
        //    TempData["IdRole"] = empolyeeServices.Load().Where(e => e.departmentid == Id).Select(e => e.RoleId).FirstOrDefault();
        //    var IDRole = Convert.ToInt32(TempData["IdRole"]);
          
        //    DepartmentService.Edit(Id);
            
        //    //return View(DepartmentService.Edit(Id));
        //    //var Employee = empDB.ListAll().Find(x => x.EmployeeID.Equals(ID));
        //    return Json(DepartmentService.Edit(Id), JsonRequestBehavior.AllowGet);
        //}






        [HttpGet]
        public async Task< ActionResult >EditAjax(int Id)
        {
            DepartmentDto departmentDtos = null;

            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri("https://localhost:44380/api/");
                var result = await client.GetAsync("Department/loadBLoadId?Id="+Id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    departmentDtos = await result.Content.ReadAsAsync<DepartmentDto>();
                    return Json(departmentDtos, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //departmentDtos = Empty(DepartmentDto)();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    return Json(departmentDtos, JsonRequestBehavior.AllowGet);
                }
            }
            //return View( "Index",departmentDtos);




            //TempData["IdEmp"] = empolyeeServices.Load().Where(e => e.departmentid == Id).Select(e => e.Id).FirstOrDefault();
            //TempData["IdRole"] = empolyeeServices.Load().Where(e => e.departmentid == Id).Select(e => e.RoleId).FirstOrDefault();
            //var IDRole = Convert.ToInt32(TempData["IdRole"]);

            //DepartmentService.Edit(Id);

            ////return View(DepartmentService.Edit(Id));
            ////var Employee = empDB.ListAll().Find(x => x.EmployeeID.Equals(ID));
            //return Json(DepartmentService.Edit(Id), JsonRequestBehavior.AllowGet);


        }


        //[Authorize(Roles = "EditDepartment")]
        //[HttpPost]
        //public ActionResult Edit(DepartmentDto DeptDto)
        //{
        //    var IdEmp = Convert.ToInt32(TempData["IdEmp"]);
        //    DepartmentService.update(DeptDto);
        //    if (IdEmp != 0)
        //    {
        //        var emp = empolyeeServices.Load().Where(y => y.Id == IdEmp).FirstOrDefault();
        //    }
        //    List<DepartmentDto> li = new List<DepartmentDto>();
        //    li = DepartmentService.Load();
        //    return View("Index", li);
        ////}
        ///



        //public IHttpActionResult Put(StudentViewModel student)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Not a valid model");

        //    using (var ctx = new SchoolDBEntities())
        //    {
        //        var existingStudent = ctx.Students.Where(s => s.StudentID == student.Id)
        //                                                .FirstOrDefault<Student>();

        //        if (existingStudent != null)
        //        {
        //            existingStudent.FirstName = student.FirstName;
        //            existingStudent.LastName = student.LastName;

        //            ctx.SaveChanges();
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }

        //    return Ok();
        //}
        //[Authorize(Roles = "EditDepartment")]
        //[HttpPost]
        //public ActionResult Update(DepartmentDto DeptDto)
        //{

        //    var IdEmp = Convert.ToInt32(TempData["IdEmp"]);
        //    DepartmentService.update(DeptDto);
        //    if (IdEmp != 0)
        //    {
        //        var emp = empolyeeServices.Load().Where(y => y.Id == IdEmp).FirstOrDefault();
        //    }
        //    List<DepartmentDto> li = new List<DepartmentDto>();
        //    li = DepartmentService.Load();
        //    return Json(li, JsonRequestBehavior.AllowGet);
        //}

        [Authorize(Roles = "EditDepartment")]
        [HttpPost]
        public async Task<ActionResult> Update(DepartmentDto DeptDto)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44380/api/");
                    var response = await client.PutAsJsonAsync("Department/Update", DeptDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { falied = true });
                    }
                    else
                    {
                        return Json(new { MS = "TJHGF", falied = false });

                    }
                }

            }
            return Json( JsonRequestBehavior.AllowGet);
        }



    }
}