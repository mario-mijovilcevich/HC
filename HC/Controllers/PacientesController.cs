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
    public class PacientesController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /Pacientes/
        public async Task<ActionResult> Index()
        {
            Session.Remove("PacienteID");
            Session.Remove("HistoriaClinicaID");

            var pacientes = db.Pacientes.Include(p => p.ObrasSociales).Include(p => p.TiposDocumentos);
            return View(await pacientes.ToListAsync());
        }

        // GET: /Pacientes/Details/5
        public ActionResult Details()
        {
            int? id = Convert.ToInt32(Session["PacienteID"]);

            Pacientes pacientes = db.Pacientes.Include(h => h.HistoriasClinicas).Where(h => h.PacienteID == id).Single();
                       
            if (pacientes == null)
            {
                return HttpNotFound();
            }

            HistoriasClinicas historiaClinica = db.HistoriasClinicas.Where(h => h.PacienteID == id).Single();
            
            Session["HistoriaClinicaID"] = historiaClinica.HistoriaClinicaID; 

            return View(pacientes);
        }

        // GET: /Pacientes/Create
        public ActionResult Create()
        {
            var paciente = (Pacientes)Session["Paciente"];

            var selectedObraSocial = 0;
            var selectedTipoDocumento = 0;

            if (paciente != null)
            {
                selectedObraSocial = paciente.ObraSocialID;
                selectedTipoDocumento = paciente.TipoDocumentoID;
            }

            ViewBag.ObraSocialID = new SelectList(db.ObrasSociales, "ObraSocialID", "Nombre", selectedObraSocial);
            ViewBag.TipoDocumentoID = new SelectList(db.TiposDocumentos, "TipoDocumentoID", "Descripcion", selectedTipoDocumento);

            return View(paciente);
        }

        // POST: /Pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PacienteID,TipoDocumentoID,NumeroDocumento,Nombre,Apellido,Telefono,Email,Direccion,FechaNacimiento,ObraSocialID")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                //db.Pacientes.Add(pacientes);
                //await db.SaveChangesAsync();
                Session["Paciente"] = pacientes;
                return RedirectToAction("Create", "HistoriasClinicas");
               //return RedirectToAction("Index");
            }

            ViewBag.ObraSocialID = new SelectList(db.ObrasSociales, "ObraSocialID", "Nombre", pacientes.ObraSocialID);
            ViewBag.TipoDocumentoID = new SelectList(db.TiposDocumentos, "TipoDocumentoID", "Descripcion", pacientes.TipoDocumentoID);
            return View(pacientes);
        }

        // GET: /Pacientes/Edit/5
        public async Task<ActionResult> Edit()
        {
            int? id = Convert.ToInt32(Session["PacienteID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = await db.Pacientes.FindAsync(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.ObraSocialID = new SelectList(db.ObrasSociales, "ObraSocialID", "Nombre", pacientes.ObraSocialID);
            ViewBag.TipoDocumentoID = new SelectList(db.TiposDocumentos, "TipoDocumentoID", "Descripcion", pacientes.TipoDocumentoID);
            return View(pacientes);
        }

        // POST: /Pacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="PacienteID,TipoDocumentoID,NumeroDocumento,Nombre,Apellido,Telefono,Email,Direccion,FechaNacimiento,ObraSocialID")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacientes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Pacientes", new { id = pacientes.PacienteID });
            }
            ViewBag.ObraSocialID = new SelectList(db.ObrasSociales, "ObraSocialID", "Nombre", pacientes.ObraSocialID);
            ViewBag.TipoDocumentoID = new SelectList(db.TiposDocumentos, "TipoDocumentoID", "Descripcion", pacientes.TipoDocumentoID);
            return View(pacientes);
        }

        // GET: /Pacientes/Delete/5
        public async Task<ActionResult> Delete()
        {
            int? id = Convert.ToInt32(Session["PacienteID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = await db.Pacientes.FindAsync(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: /Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pacientes pacientes = await db.Pacientes.FindAsync(id);
            db.Pacientes.Remove(pacientes);
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

        public PartialViewResult SearchPaciente(string searchString)
        {
            //System.Threading.Thread.Sleep(4000);
            Session.Remove("PacienteID");
            Session.Remove("HistoriaClinicaID");
            var pacientes = db.Pacientes.Include(p => p.ObrasSociales).Include(p => p.TiposDocumentos).ToList();
            var result = pacientes.Where(c => c.Apellido.StartsWith(searchString, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Apellido).ToList();

           return PartialView("_SearchPacientePartial", result);
        }

        public ActionResult SetPacienteID(int id, char op)
        {
            Session["PacienteID"] = id;

            if (op == 'B') return RedirectToAction("Delete");
            else return RedirectToAction("Details");
        }
    }
}
