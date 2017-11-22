using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfficiencyMark.Models;
using EfficiencyMark.ViewModels;

namespace EfficiencyMark.Controllers
{
    public class AchievementsOfAnEmployeesController : Controller
    {
        private readonly ApplicationContext _context;

        public AchievementsOfAnEmployeesController(ApplicationContext context)
        {
            _context = context;
        }


        public IActionResult Index(string Name, string NameOfAchievement,  int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;


            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["NameOfAchievementSort"] = sortOrder == SortState.NameOfAchievementAsc ? SortState.NameOfAchievementDesc : SortState.NameOfAchievementAsc;

            IQueryable<AchievementsOfAnEmployee> source = _context.AchievementsOfAnEmployee.Include(a => a.Employee);

            if (Name != null)
            {
                source = source.Where(x => x.Employee.Name == Name);
            }

            if (NameOfAchievement!= null )
            {
                source = source.Where(x => x.NameOfAchievement== NameOfAchievement);
            }



            switch (sortOrder)
            {

                case SortState.NameDesc:
                    source = source.OrderByDescending(x => x.Employee.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(x => x.Employee.Name);
                    break;

                case SortState.NameOfAchievementAsc:
                    source = source.OrderByDescending(x => x.NameOfAchievement);
                    break;
                case SortState.NameOfAchievementDesc:
                    source = source.OrderBy(x => x.NameOfAchievement);
                    break;
            }


            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = pageView,
                FilterAchivViewModel = new FilterAchivViewModel(NameOfAchievement, Name),
                AchievementsOfAnEmployee = items
            };
            return View(ivm);

        }


        // GET: AchievementsOfAnEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievementsOfAnEmployee = await _context.AchievementsOfAnEmployee
                .Include(a => a.Employee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (achievementsOfAnEmployee == null)
            {
                return NotFound();
            }

            return View(achievementsOfAnEmployee);
        }

        // GET: AchievementsOfAnEmployees/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: AchievementsOfAnEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,NameOfAchievement")] AchievementsOfAnEmployee achievementsOfAnEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(achievementsOfAnEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", achievementsOfAnEmployee.EmployeeId);
            return View(achievementsOfAnEmployee);
        }

        // GET: AchievementsOfAnEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievementsOfAnEmployee = await _context.AchievementsOfAnEmployee.SingleOrDefaultAsync(m => m.Id == id);
            if (achievementsOfAnEmployee == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", achievementsOfAnEmployee.EmployeeId);
            return View(achievementsOfAnEmployee);
        }

        // POST: AchievementsOfAnEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,NameOfAchievement")] AchievementsOfAnEmployee achievementsOfAnEmployee)
        {
            if (id != achievementsOfAnEmployee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achievementsOfAnEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchievementsOfAnEmployeeExists(achievementsOfAnEmployee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", achievementsOfAnEmployee.EmployeeId);
            return View(achievementsOfAnEmployee);
        }

        // GET: AchievementsOfAnEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievementsOfAnEmployee = await _context.AchievementsOfAnEmployee
                .Include(a => a.Employee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (achievementsOfAnEmployee == null)
            {
                return NotFound();
            }

            return View(achievementsOfAnEmployee);
        }

        // POST: AchievementsOfAnEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var achievementsOfAnEmployee = await _context.AchievementsOfAnEmployee.SingleOrDefaultAsync(m => m.Id == id);
            _context.AchievementsOfAnEmployee.Remove(achievementsOfAnEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AchievementsOfAnEmployeeExists(int id)
        {
            return _context.AchievementsOfAnEmployee.Any(e => e.Id == id);
        }
    }
}
