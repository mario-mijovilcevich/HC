using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HC.Models;

namespace HC.Controllers
{
    public class FactoresRiesgoController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /FactoresRiesgo/
        public async Task<ActionResult> Index()
        {
            return View(await db.FactoresRiesgo.ToListAsync());
        }

        // GET: /FactoresRiesgo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactoresRiesgo factoresriesgo = await db.FactoresRiesgo.FindAsync(id);
            if (factoresriesgo == null)
            {
                return HttpNotFound();
            }
            return View(factoresriesgo);
        }

        // GET: /FactoresRiesgo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /FactoresRiesgo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="FactorRiesgoID,Descripcion")] FactoresRiesgo factoresriesgo)
        {
            if (ModelState.IsValid)
            {
                db.FactoresRiesgo.Add(factoresriesgo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(factoresriesgo);
        }

        // GET: /FactoresRiesgo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactoresRiesgo factoresriesgo = await db.FactoresRiesgo.FindAsync(id);
            if (factoresriesgo == null)
            {
                return HttpNotFound();
            }
            return View(factoresriesgo);
        }

        // POST: /FactoresRiesgo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="FactorRiesgoID,Descripcion")] FactoresRiesgo factoresriesgo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factoresriesgo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(factoresriesgo);
        }

        // GET: /FactoresRiesgo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactoresRiesgo factoresriesgo = await db.FactoresRiesgo.FindAsync(id);
            if (factoresriesgo == null)
            {
                return HttpNotFound();
            }
            return View(factoresriesgo);
        }

        // POST: /FactoresRiesgo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FactoresRiesgo factoresriesgo = await db.FactoresRiesgo.FindAsync(id);
            db.FactoresRiesgo.Remove(factoresriesgo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
