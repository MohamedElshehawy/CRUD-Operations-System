using AutoMapper;
using Demo.DAL.Entities;
using Demo.Pl.ViweModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pl.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<IdentityRole> userManager, IMapper mapper)
        {
            _roleManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                // appuser => UserViewModel
                // select operator {transformation operator}
                var roles = await _roleManager.Roles.Select(r => new RoleViewModel  
                {
                    Id = r.Id,
                    Name = r.Name,
                   

                }).ToListAsync();
                return View(roles);
            }
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null) return View(Enumerable.Empty<RoleViewModel>());
            var mappedRole = new RoleViewModel
            {
                Name = role.Name,
                Id = role.Id

            };
            return View(new List<RoleViewModel> { mappedRole});
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<IdentityRole>(model);
                var result = await _roleManager.CreateAsync(mappedRole);
                if(result.Succeeded) return RedirectToAction(nameof(Index));
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            var user = await _roleManager.FindByIdAsync(id);
            if (user is null) return NotFound();

            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(user);

            return View(viewName, mappedRole);

        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, nameof(Edit));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleViewModel  model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid) return View(model);


            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return NotFound();
                role.Name = model.Name;

                await _roleManager.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);

            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, nameof(Delete));
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {

            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return NotFound();

                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);

            }
            return View();
        }
    }
}

