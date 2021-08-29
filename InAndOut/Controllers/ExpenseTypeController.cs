using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IList<ExpenseType> objList = _db.ExpenseTypes.ToList();
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseType data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            _db.ExpenseTypes.Add(data);
            _db.SaveChanges();
            return RedirectToAction("Index");
            //return View();
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ExpenseTypes.Find(id);
            if (obj is null)
            {
                return NotFound();
            }

            return View(obj);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(ExpenseType item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            //var obj = _db.Expenses.Find(item.Id);
            //if (obj is null)
            //{
            //    return NotFound();
            //}

            //obj.Amount = item.Amount;
            //obj.ExpenseName = item.ExpenseName;

            //_db.Expenses.Update(obj);
            _db.Update(item);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ExpenseTypes.Find(id);
            if (obj is null)
            {
                return NotFound();
            }

            return View(obj);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var obj = _db.ExpenseTypes.Find(id);
            if (obj is null)
            {
                return NotFound();
            }


            _db.ExpenseTypes.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
