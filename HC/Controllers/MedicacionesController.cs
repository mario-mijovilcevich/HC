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
    public class MedicacionesController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /Medicaciones/
        public async Task<ActionResult> Index()
        {
            return View(await db.Medicaciones.ToListAsync());
        }

        // GET: /Medicaciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicaciones medicaciones = await db.Medicaciones.FindAsync(id);
            if (medicaciones == null)
            {
                return HttpNotFound();
            }
            return View(medicaciones);
        }

        // GET: /Medicaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Medicaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="MedicacionID,Nombre")] Medicaciones medicaciones)
        {
            if (ModelState.IsValid)
            {
                db.Medicaciones.Add(medicaciones);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicaciones);
        }

        // GET: /Medicaciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicaciones medicaciones = await db.Medicaciones.FindAsync(id);
            if (medicaciones == null)
            {
                return HttpNotFound();
            }
            return View(medicaciones);
        }

        // POST: /Medicaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="MedicacionID,Nombre")] Medicaciones medicaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicaciones).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(medicaciones);
        }

        // GET: /Medicaciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicaciones medicaciones = await db.Medicaciones.FindAsync(id);
            if (medicaciones == null)
            {
                return HttpNotFound();
            }
            return View(medicaciones);
        }

        // POST: /Medicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Medicaciones medicaciones = await db.Medicaciones.FindAsync(id);
            db.Medicaciones.Remove(medicaciones);
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
