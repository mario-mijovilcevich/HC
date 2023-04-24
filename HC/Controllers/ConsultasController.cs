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
    public class ConsultasController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /Consultas/
        public async Task<ActionResult> Index()
        {
            Session.Remove("ConsutaID");
            int? id = Convert.ToInt32(Session["HistoriaClinicaID"]);
            
            var consultas = db.Consultas.Include(c => c.HistoriasClinicas).OrderBy(f => f.Fecha).Where(h => h.HistoriaClinicaID == id);
            return View(await consultas.ToListAsync());
        }

        // GET: /Consultas/Details/5
        public async Task<ActionResult> Details()
        {
            int? id = Convert.ToInt32(Session["ConsultaID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultas consultas = await db.Consultas.FindAsync(id);
            if (consultas == null)
            {
                return HttpNotFound();
            }
            return View(consultas);
        }

        // GET: /Consultas/Create
        public ActionResult Create()
        {
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View();
        }

        // POST: /Consultas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ConsultaID,HistoriaClinicaID,MotivoConsulta,EstadoFisico,Indicaciones,Fecha")] Consultas consultas)
        {
            if (ModelState.IsValid)
            {
                db.Consultas.Add(consultas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", consultas.HistoriaClinicaID);
            return View(consultas);
        }

        // GET: /Consultas/Edit/5
        public async Task<ActionResult> Edit()
        {
            int? id = Convert.ToInt32(Session["ConsultaID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultas consultas = await db.Consultas.FindAsync(id);
            if (consultas == null)
            {
                return HttpNotFound();
            }
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View(consultas);
        }

        // POST: /Consultas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ConsultaID,HistoriaClinicaID,MotivoConsulta,EstadoFisico,Indicaciones,Fecha")] Consultas consultas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", consultas.HistoriaClinicaID);
            return View(consultas);
        }

        // GET: /Consultas/Delete/5
        public async Task<ActionResult> Delete()
        {
            int? id = Convert.ToInt32(Session["ConsultaID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultas consultas = await db.Consultas.FindAsync(id);
            if (consultas == null)
            {
                return HttpNotFound();
            }
            return View(consultas);
        }

        // POST: /Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Consultas consultas = await db.Consultas.FindAsync(id);
            db.Consultas.Remove(consultas);
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

        public ActionResult SetConsultaID(int id, char op)
        {
            Session["ConsultaID"] = id;

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
