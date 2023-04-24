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
    public class ECOsController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /ECOs/
        public async Task<ActionResult> Index()
        {
            Session.Remove("ECOID");
            int? id = Convert.ToInt32(Session["HistoriaClinicaID"]);

            var ecos = db.ECOs.Include(e => e.HistoriasClinicas).Where(h => h.HistoriaClinicaID == id);
            return View(await ecos.ToListAsync());
        }

        // GET: /ECOs/Details/5
        public async Task<ActionResult> Details()
        {
            int? id = Convert.ToInt32(Session["ECOID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ECOs ecos = await db.ECOs.FindAsync(id);
            if (ecos == null)
            {
                return HttpNotFound();
            }
            return View(ecos);
        }

        // GET: /ECOs/Create
        public ActionResult Create()
        {
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View();
        }

        // POST: /ECOs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ECOID,HistoriaClinicaID,Fecha,Vi,S,PP,Ao,Ai,Fey,Conclusion")] ECOs ecos)
        {
            if (ModelState.IsValid)
            {
                db.ECOs.Add(ecos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", ecos.HistoriaClinicaID);
            return View(ecos);
        }

        // GET: /ECOs/Edit/5
        public async Task<ActionResult> Edit()
        {
            int? id = Convert.ToInt32(Session["ECOID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ECOs ecos = await db.ECOs.FindAsync(id);
            if (ecos == null)
            {
                return HttpNotFound();
            }
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View(ecos);
        }

        // POST: /ECOs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ECOID,HistoriaClinicaID,Fecha,Vi,S,PP,Ao,Ai,Fey,Conclusion")] ECOs ecos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ecos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", ecos.HistoriaClinicaID);
            return View(ecos);
        }

        // GET: /ECOs/Delete/5
        public async Task<ActionResult> Delete()
        {
            int? id = Convert.ToInt32(Session["ECOID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ECOs ecos = await db.ECOs.FindAsync(id);
            if (ecos == null)
            {
                return HttpNotFound();
            }
            return View(ecos);
        }

        // POST: /ECOs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ECOs ecos = await db.ECOs.FindAsync(id);
            db.ECOs.Remove(ecos);
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

        public ActionResult SetECOID(int id, char op)
        {
            Session["ECOID"] = id;

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
