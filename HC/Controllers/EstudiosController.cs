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
    public class EstudiosController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /Estudios/
        public async Task<ActionResult> Index()
        {
            Session.Remove("EstudioID");
            int id = Convert.ToInt32(Session["HistoriaClinicaID"]);

            var estudios = db.Estudios.Include(e => e.HistoriasClinicas).Include(e => e.TiposEstudios).Where(h => h.HistoriaClinicaID == id);
            return View(await estudios.ToListAsync());
        }

        // GET: /Estudios/Details/5
        public async Task<ActionResult> Details()
        {
            int? id = Convert.ToInt32(Session["EstudioID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudios estudios = await db.Estudios.FindAsync(id);
            if (estudios == null)
            {
                return HttpNotFound();
            }
            return View(estudios);
        }

        // GET: /Estudios/Create
        public ActionResult Create()
        {
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            ViewBag.TipoEstudioID = new SelectList(db.TiposEstudios, "TipoEstudioID", "Descripcion");
            return View();
        }

        // POST: /Estudios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="EstudioID,HistoriaClinicaID,TipoEstudioID,Fecha,Conclusion")] Estudios estudios)
        {
            if (ModelState.IsValid)
            {
                db.Estudios.Add(estudios);
                await db.SaveChangesAsync();
                return RedirectToAction("Index",Session["HistoriaClinicaID"]);
            }

            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", estudios.HistoriaClinicaID);
            ViewBag.TipoEstudioID = new SelectList(db.TiposEstudios, "TipoEstudioID", "Descripcion", estudios.TipoEstudioID);
            return View(estudios);
        }

        // GET: /Estudios/Edit/5
        public async Task<ActionResult> Edit()
        {
            int? id = Convert.ToInt32(Session["EstudioID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudios estudios = await db.Estudios.FindAsync(id);
            if (estudios == null)
            {
                return HttpNotFound();
            }
            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", estudios.HistoriaClinicaID);
            ViewBag.TipoEstudioID = new SelectList(db.TiposEstudios, "TipoEstudioID", "Descripcion", estudios.TipoEstudioID);
            return View(estudios);
        }

        // POST: /Estudios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="EstudioID,HistoriaClinicaID,TipoEstudioID,Fecha,Conclusion")] Estudios estudios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TipoEstudioID = new SelectList(db.TiposEstudios, "TipoEstudioID", "Descripcion", estudios.TipoEstudioID);
            return View(estudios);
        }

        // GET: /Estudios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudios estudios = await db.Estudios.FindAsync(id);
            if (estudios == null)
            {
                return HttpNotFound();
            }
            return View(estudios);
        }

        // POST: /Estudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Estudios estudios = await db.Estudios.FindAsync(id);
            db.Estudios.Remove(estudios);
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

        public ActionResult SetEstudioID(int id, char op)
        {
            Session["EstudioID"] = id;

            switch (op)
            {
                case 'B':
                    return RedirectToAction("Delete");
                case 'D':
                    return RedirectToAction("Details");
                case 'E':
                    return RedirectToAction("Edit");
                default:
                    return RedirectToAction("Index");
            }
        }
    }
}
