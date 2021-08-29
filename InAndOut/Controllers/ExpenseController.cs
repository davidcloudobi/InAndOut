using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //var qry = from d in _db.Expenses
            //         .Include(x => x.ExpenseType)
            //         //.Include(x => x.Tables.Select(c => c.Fields))
            //         //.Include(x => x.Tables.Select(f => f.ForeingKeys))
            //          select d;

            IList<Expense> objList = _db.Expenses.Include(x=> x.ExpenseType).ToList();
            return View(objList);
        }

        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //}) ;
            //ViewBag.TypeDropDown = TypeDropDown;

            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

        };
            return View(expenseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM data)
        {
            if (!ModelState.IsValid)
            {
                //IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                //{
                //    Text = i.Name,
                //    Value = i.Id.ToString()
                //});
                //ViewBag.TypeDropDown = TypeDropDown;
                data.TypeDropDown  = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(data);
            }
            _db.Expenses.Add(data.Expense);
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



            var obj = _db.Expenses.Find(id);
            if (obj is null)
            {
                return NotFound();
            }

            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = obj,
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };

            return View(expenseVM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(ExpenseVM item)
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
            _db.Update(item.Expense);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Expenses.Find(id);
            if (obj is null)
            {
                return NotFound();
            }

            return View(obj);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost( int? id)
        {

            var obj = _db.Expenses.Find(id);
            if (obj is null)
            {
                return NotFound();
            }


            _db.Expenses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
      
        }

        
    }
}

