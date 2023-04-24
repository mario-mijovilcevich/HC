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
    public class HistoriasClinicasController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /HistoriasClinicas/
        public async Task<ActionResult> Index()
        {
            var historiasclinicas = db.HistoriasClinicas.Include(h => h.FactoresRiesgoList);
            return View(await historiasclinicas.ToListAsync());
        }

        // GET: /HistoriasClinicas/Details/5
        public async Task<ActionResult> Details()
        {
            int? id = Convert.ToInt32(Session["HistoriaClinicaID"]);
 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriasClinicas historiasclinicas = await db.HistoriasClinicas.FindAsync(id);
            if (historiasclinicas == null)
            {
                return HttpNotFound();
            }
            return View(historiasclinicas);
        }

        // GET: /HistoriasClinicas/Create
        public ActionResult Create()
        {
            var paciente = (Pacientes)Session["Paciente"];
            if (paciente == null) return RedirectToAction("Create","Pacientes");

            var historiaClinica = (HistoriasClinicas)Session["HistoriaClinica"];
            
            ViewBag.FactoresRiesgo = db.FactoresRiesgo.ToList();
            ViewBag.Antecedentes = db.Antecedentes.ToList();
            ViewBag.Medicaciones = db.Medicaciones.ToList();
            ViewBag.Instituciones = db.Instituciones.ToList();

            return View(historiaClinica);
        }

        // POST: /HistoriasClinicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="HistoriaClinicaID,PacienteID,FactoresRiesgo,Antecedentes,Medicaciones,EstudiosComplementarios,FechaCreacion,FechaActualizacion")] HistoriasClinicas historiasclinicas, string submit)
        {
            if (ModelState.IsValid)
            {
                historiasclinicas.FechaCreacion = DateTime.Now;

                var factoresRiesgo = Request.Form["FactoresRiesgoCheckBoxes"];
                var antecedentes = Request.Form["AntecedentesCheckBoxes"];
                var medicaciones = Request.Form["MedicacionesCheckBoxes"];
                var instituciones = Request.Form["InstitucionesCheckBoxes"];

                foreach (var item in factoresRiesgo.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "false").ToArray())
                {
                    historiasclinicas.FactoresRiesgoList.Add(db.FactoresRiesgo.Find(Convert.ToInt32(item)));
                }

                foreach (var item in antecedentes.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "false").ToArray())
                {
                    historiasclinicas.AntecedentesList.Add(db.Antecedentes.Find(Convert.ToInt32(item)));
                }

                foreach (var item in medicaciones.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "false").ToArray())
                {
                    historiasclinicas.MedicacionesList.Add(db.Medicaciones.Find(Convert.ToInt32(item)));
                }

                foreach (var item in instituciones.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "false").ToArray())
                {
                    historiasclinicas.InstitucionesList.Add(db.Instituciones.Find(Convert.ToInt32(item)));
                }

                if (submit == "Volver")
                {
                    Session["HistoriaClinica"] = historiasclinicas;
                    return RedirectToAction("Create", "Pacientes");
                }

                var paciente = db.Pacientes.Add((Pacientes)Session["Paciente"]);
                await db.SaveChangesAsync();
                historiasclinicas.PacienteID = paciente.PacienteID;
                db.HistoriasClinicas.Add(historiasclinicas);
                await db.SaveChangesAsync();
                Session.Remove("Paciente");
                Session.Remove("HistoriaClinica");
                return RedirectToAction("Index");
            }

            return View(historiasclinicas);
        }

        // GET: /HistoriasClinicas/Edit/5
        public async Task<ActionResult> Edit()
        {
            int? id = Convert.ToInt32(Session["HistoriaClinicaID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriasClinicas historiasclinicas = await db.HistoriasClinicas.FindAsync(id);

            ViewBag.FactoresRiesgo = db.FactoresRiesgo.ToList();
            ViewBag.Antecedentes = db.Antecedentes.ToList();
            ViewBag.Medicaciones = db.Medicaciones.ToList();
            ViewBag.Instituciones = db.Instituciones.ToList();

            if (historiasclinicas == null)
            {
                return HttpNotFound();
            }
            return View(historiasclinicas);
        }

        // POST: /HistoriasClinicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="HistoriaClinicaID,PacienteID,FactoresRiesgo,Antecedentes,Medicaciones,EstudiosComplementarios,FechaCreacion,FechaActualizacion")] HistoriasClinicas historiasclinicas)
        {
            if (ModelState.IsValid)
            {
                db.HistoriasClinicas.Attach(historiasclinicas);
                db.Entry(historiasclinicas).Collection(f => f.FactoresRiesgoList).Load();
                db.Entry(historiasclinicas).Collection(f => f.AntecedentesList).Load();
                db.Entry(historiasclinicas).Collection(f => f.MedicacionesList).Load();
                db.Entry(historiasclinicas).Collection(f => f.InstitucionesList).Load();
                historiasclinicas.FactoresRiesgoList.Clear();
                historiasclinicas.AntecedentesList.Clear();
                historiasclinicas.MedicacionesList.Clear();
                historiasclinicas.InstitucionesList.Clear();

                var factoresRiesgo = Request.Form["FactoresRiesgoCheckBoxes"];
                var antecedentes = Request.Form["AntecedentesCheckBoxes"];
                var medicaciones = Request.Form["MedicacionesCheckBoxes"];
                var instituciones = Request.Form["InstitucionesCheckBoxes"];

                foreach (var item in factoresRiesgo.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "false").ToArray())
                {
                    historiasclinicas.FactoresRiesgoList.Add(db.FactoresRiesgo.Find(Convert.ToInt32(item)));
                }

                foreach (var item in antecedentes.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "false").ToArray())
                {
                    historiasclinicas.AntecedentesList.Add(db.Antecedentes.Find(Convert.ToInt32(item)));
                }

                foreach (var item in medicaciones.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "false").ToArray())
                {
                    historiasclinicas.MedicacionesList.Add(db.Medicaciones.Find(Convert.ToInt32(item)));
                }

                foreach (var item in instituciones.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x) && x != "false").ToArray())
                {
                    historiasclinicas.InstitucionesList.Add(db.Instituciones.Find(Convert.ToInt32(item)));
                }

                historiasclinicas.FechaActualizacion = DateTime.Now;

                db.Entry(historiasclinicas).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return RedirectToAction("Details", "HistoriasClinicas", new { id = historiasclinicas.HistoriaClinicaID });
            }
            return View(historiasclinicas);
        }

        // GET: /HistoriasClinicas/Delete/5
        public async Task<ActionResult> Delete()
        {
            int? id = Convert.ToInt32(Session["HistoriaClinicaID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriasClinicas historiasclinicas = await db.HistoriasClinicas.FindAsync(id);
            if (historiasclinicas == null)
            {
                return HttpNotFound();
            }
            return View(historiasclinicas);
        }

        // POST: /HistoriasClinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HistoriasClinicas historiasclinicas = await db.HistoriasClinicas.FindAsync(id);
            db.HistoriasClinicas.Remove(historiasclinicas);
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
