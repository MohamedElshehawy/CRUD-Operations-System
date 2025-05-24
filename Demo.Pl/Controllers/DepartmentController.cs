using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Pl.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            //ViewData["Mm"] = "Hellow From View Data";
            var departments = await  _repository.GetAllAsync();
            return View(model:departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(department);
               //if ( >0)
               // {
               //     TempData[key:"Message"] = "Department Created Sucessfully";
               // }
                
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        public async Task<IActionResult> Details(int? id) => await RetuenViewWithDepartment(id,nameof(Details));
        public async Task<IActionResult> Edit(int? id) => await RetuenViewWithDepartment(id, nameof(Edit));
        [HttpPost]
        public IActionResult Edit(Department department ,[FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("",ex.Message);
                }
            }
            return View(department);
        }
        public async Task<IActionResult> Delete(int? id) => await RetuenViewWithDepartment(id, nameof(Delete));
        [HttpPost]
        public IActionResult Delete(Department department ,[FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
           
                try
                {
                    _repository.Delete(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message);
                }
            
            return View(department);
        }

        private async Task<IActionResult> RetuenViewWithDepartment(int? id , string ViewName)
        {
            if (!id.HasValue)
                return BadRequest();
            var department = await _repository.GetAsync(id.Value);
            if (department is null)
                return NotFound();

            return View(ViewName, department);
        }
    }
}
