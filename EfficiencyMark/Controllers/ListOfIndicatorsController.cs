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
    public class ListOfIndicatorsController : Controller
    {
        private readonly ApplicationContext _context;

        public ListOfIndicatorsController(ApplicationContext context)
        {
            _context = context;
        }



        public IActionResult Index(string Name, string NameOfTheIndicator, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;


            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["NameOfTheIndicatorSort"] = sortOrder == SortState.NameOfTheIndicatorAsc ? SortState.NameOfTheIndicatorDesc : SortState.NameOfTheIndicatorAsc;


            IQueryable<ListOfIndicators> source = _context.ListOfIndicators.Include(l => l.Employee);

            if (Name != null)
            {
                source = source.Where(x => x.Employee.Name == Name);
            }

            if (NameOfTheIndicator != null)
            {
                source = source.Where(x => x.NameOfTheIndicator == NameOfTheIndicator);
            }



            switch (sortOrder)
            {

                case SortState.NameDesc:
                    source = source.OrderByDescending(x => x.Employee.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(x => x.Employee.Name);
                    break;

                case SortState.NameOfTheIndicatorAsc:
                    source = source.OrderByDescending(x => x.NameOfTheIndicator);
                    break;
                case SortState.NameOfTheIndicatorDesc:
                    source = source.OrderBy(x => x.NameOfTheIndicator);
                    break;
            }


            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = pageView,
                FilterListViewModel = new FilterListViewModel(Name,NameOfTheIndicator),
                ListOfIndicators = items
            };
            return View(ivm);

        }





        // GET: ListOfIndicators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listOfIndicators = await _context.ListOfIndicators
                .Include(l => l.Employee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (listOfIndicators == null)
            {
                return NotFound();
            }

            return View(listOfIndicators);
        }

        // GET: ListOfIndicators/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: ListOfIndicators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,NameOfTheIndicator")] ListOfIndicators listOfIndicators)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listOfIndicators);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", listOfIndicators.EmployeeId);
            return View(listOfIndicators);
        }

        // GET: ListOfIndicators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listOfIndicators = await _context.ListOfIndicators.SingleOrDefaultAsync(m => m.Id == id);
            if (listOfIndicators == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", listOfIndicators.EmployeeId);
            return View(listOfIndicators);
        }

        // POST: ListOfIndicators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,NameOfTheIndicator")] ListOfIndicators listOfIndicators)
        {
            if (id != listOfIndicators.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listOfIndicators);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListOfIndicatorsExists(listOfIndicators.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", listOfIndicators.EmployeeId);
            return View(listOfIndicators);
        }

        // GET: ListOfIndicators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listOfIndicators = await _context.ListOfIndicators
                .Include(l => l.Employee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (listOfIndicators == null)
            {
                return NotFound();
            }

            return View(listOfIndicators);
        }

        // POST: ListOfIndicators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listOfIndicators = await _context.ListOfIndicators.SingleOrDefaultAsync(m => m.Id == id);
            _context.ListOfIndicators.Remove(listOfIndicators);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListOfIndicatorsExists(int id)
        {
            return _context.ListOfIndicators.Any(e => e.Id == id);
        }
    }
}
