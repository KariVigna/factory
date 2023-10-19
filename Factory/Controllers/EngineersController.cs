using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
    public class EngineersController : Controller
    {
        private readonly FactoryContext _db;

        public EngineersController(FactoryContext db)
        {
        _db = db;
        }

        public ActionResult Index()
        {
        List<Engineer> model = _db.Engineers.ToList();
        return View(model);
        }

        public ActionResult Create()
        {
        return View();
        }

        [HttpPost]
        public ActionResult Create(Engineer engineer)
        {
        if (!ModelState.IsValid)
        {
            return View(engineer);
        }
        else
        {
            _db.Engineers.Add(engineer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        }

        public ActionResult Details(int id)
        {
        Engineer thisEngineer = _db.Engineers
            .Include(engineer => engineer.JoinEntities)
            .ThenInclude(join => join.Machine)
            .FirstOrDefault(engineer => engineer.EngineerId == id);
        return View(thisEngineer);
        }

        public ActionResult Edit(int id)
        {
        Enginner thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
        return View(thisEngineer);
        }

        [HttpPost]
        public ActionResult Edit(Engineer engineer)
        {
        _db.Engineers.Update(engineer);
        _db.SaveChanges();
        return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
        Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
        return View(thisEngineer);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
        Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
        _db.Engineers.Remove(thisEngineer);
        _db.SaveChanges();
        return RedirectToAction("Index");
        }

        public ActionResult AddTag(int id)
        {
        Engineer thisEngineer = _db.Engineers.FirstOrDefault(items => items.EngineerId == id);
        ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Description");
        return View(thisEngineer);
        }

        [HttpPost]
        public ActionResult AddTag(Engineer engineer, int machineId)
        {
    #nullable enable
        EngineerMachine? joinEntity = _db.EnginerMachines.FirstOrDefault(join => (join.MachineId == machineId && join.EngineerId == engineer.EngineerId));
    #nullable disable
        if (joinEntity == null && machineId != 0)
        {
            _db.EngineerMachines.Add(new EngineerMachine() { MachineId = machineId, EngineerId = engineer.EngineerId });
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = engineer.EngineerId });
        }

        [HttpPost]
        public ActionResult DeleteJoin(int joinId)
        {
        EngineerMachine joinEntry = _db.EngineerMachiens.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
        _db.EngineerMachiens.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
        }
    }
}