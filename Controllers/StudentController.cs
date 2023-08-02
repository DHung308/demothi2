using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AAAATHIDEMO.Data;
using AAAATHIDEMO.Models;
using AAAATHIDEMO.Models.Process;

namespace AAAATHIDEMO.Controllers
{
    public class StudentController : Controller
    {
        private readonly AAAATHIDEMOContext _context;
        private readonly StringProcess _StringProcess;

        public StudentController(AAAATHIDEMOContext context)
        {
            _context = context;
            _StringProcess = new StringProcess();
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var aAAATHIDEMOContext = _context.Student.Include(s => s.Faculty);
            return View(await aAAATHIDEMOContext.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["FaID"] = new SelectList(_context.Faculty, "FaID", "FaName");
            string newStdID;
            if(_context.Student.Any())
            {
                var lastStdID = _context.Student.OrderByDescending(s => s.ID).First();
                newStdID = _StringProcess.AutoGenerateCode(lastStdID.ID);
            }
            else
            {
                newStdID = "STD001";
            }
            ViewBag.StdID = newStdID;
            return View(new Student { ID = newStdID});
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,FaID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FaID"] = new SelectList(_context.Faculty, "FaID", "FaName", student.FaID);
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["FaID"] = new SelectList(_context.Faculty, "FaID", "FaID", student.FaID);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name,FaID")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
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
            ViewData["FaID"] = new SelectList(_context.Faculty, "FaID", "FaID", student.FaID);
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'AAAATHIDEMOContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
          return (_context.Student?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
