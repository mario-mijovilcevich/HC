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
    public class AntecedentesController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /Antecedentes/
        public async Task<ActionResult> Index()
        {
            return View(await db.Antecedentes.ToListAsync());
        }

        // GET: /Antecedentes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Antecedentes antecedentes = await db.Antecedentes.FindAsync(id);
            if (antecedentes == null)
            {
                return HttpNotFound();
            }
            return View(antecedentes);
        }

        // GET: /Antecedentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Antecedentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="AntecedenteID,Descripcion")] Antecedentes antecedentes)
        {
            if (ModelState.IsValid)
            {
                db.Antecedentes.Add(antecedentes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(antecedentes);
        }

        // GET: /Antecedentes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Antecedentes antecedentes = await db.Antecedentes.FindAsync(id);
            if (antecedentes == null)
            {
                return HttpNotFound();
            }
            return View(antecedentes);
        }

        // POST: /Antecedentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="AntecedenteID,Descripcion")] Antecedentes antecedentes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(antecedentes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(antecedentes);
        }

        // GET: /Antecedentes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Antecedentes antecedentes = await db.Antecedentes.FindAsync(id);
            if (antecedentes == null)
            {
                return HttpNotFound();
            }
            return View(antecedentes);
        }

        // POST: /Antecedentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Antecedentes antecedentes = await db.Antecedentes.FindAsync(id);
            db.Antecedentes.Remove(antecedentes);
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
