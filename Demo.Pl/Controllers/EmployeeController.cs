using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repository;
using Demo.DAL.Entities;
using Demo.Pl.Utility;
using Demo.Pl.ViweModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Demo.Pl.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _repository;
        //private readonly IDepartmentRepository _department;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(SearchValue))
            {
                employees = await _unitOfWork.Employees.GetAllAsync();
                return View(_mapper.Map<IEnumerable<EmployeeVM>>(employees));
            }
             employees = await _unitOfWork.Employees.GetAllAsync(e=>e.Name.ToLower().Contains(SearchValue.ToLower()));
            return View(_mapper.Map<IEnumerable<EmployeeVM>>(employees));

        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Department= await _unitOfWork.Departments.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employeeVm)
        {
            if (ModelState.IsValid)
            {
                if (employeeVm.Image is not null)
                {
                    employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");

                }
                var employee = _mapper.Map<EmployeeVM, Employee>(employeeVm);
                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Department = await _unitOfWork.Departments.GetAllAsync();
            return View(employeeVm);
        }
        public async Task<IActionResult> Details(int? id) => await RetuenViewWithEmployee(id, nameof(Details));
        public async Task<IActionResult> Edit(int? id) => await RetuenViewWithEmployee(id, nameof(Edit));
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeVM employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVm.Image is not null)
                        employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");

                    
                    _unitOfWork.Employees.Update(_mapper.Map<EmployeeVM, Employee>(employeeVm));
                   await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employeeVm);
        }
        public async Task<IActionResult> Delete(int? id) => await RetuenViewWithEmployee(id, nameof(Delete));
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeVM employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.Employees.Delete(_mapper.Map<EmployeeVM, Employee>(employeeVm));

                if ( await _unitOfWork.CompleteAsync() > 0 && employeeVm.ImageName is not null)
                    DocumentSettings.DeleteFile(employeeVm.ImageName, "Images");
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }

            return View(employeeVm);
        }

        private async Task<IActionResult> RetuenViewWithEmployee(int? id, string ViewName)
        {
            if (!id.HasValue)
                return BadRequest();
            var employee = await _unitOfWork.Employees.GetAsync(id.Value);
            if (employee is null)
                return NotFound();
            ViewBag.Department = await _unitOfWork.Departments.GetAllAsync(); 

            return View(ViewName, _mapper.Map<EmployeeVM>(employee));
        }
    }
}
