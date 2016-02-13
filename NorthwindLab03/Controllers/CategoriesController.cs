using NorthwindLab03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NorthwindLab03.Controllers
{
    public class CategoriesController : Controller
    {

        private Models.PayrollEntities5 Contexto =
            new Models.PayrollEntities5();
     

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            this.Contexto.Dispose(); 
        }
        private StringBuilder Trace = new StringBuilder(); 
    
        private void makeTrace(string message)
        {
            Trace.AppendLine(message);            
        }
        public ActionResult Index()
        {
            this.Contexto.Database.Log += makeTrace; 

            var query = from c in this.Contexto.tbpersonal
                        select c;

            var result = query.ToList();

            this.Contexto.Database.Log -= makeTrace;

            ViewBag.TraceMessage = Trace.ToString(); 
            return View(result);
        }
        
        [HttpPost]
        public ActionResult Index(string filter)
        {
            var query = from c in this.Contexto.tbpersonal
                        where c.nombre.Contains(filter)
                        select c;

            this.ViewBag.Filter = filter;

            return View(query);
        }

       
        public ActionResult Details(int id)
        {
            var query = (from c in this.Contexto.tbpersonal
                        where c.idpersonal == id
                        select c).FirstOrDefault();
            if (query == null)
                return this.HttpNotFound();
            return this.View(query); 
        }

   
        public ActionResult Create()
        {
            return this.View();
        }
        [HttpPost]
        public ActionResult Create(
            [Bind(Include="nombre,apellidop,apellidom,lugarnacimiento,nacionalidad,sexo,estadocivil")]tbpersonal Model)
        {
            
            if (!this.ModelState.IsValid)
            {
                return this.View(Model);
            }

            if (Model.nombre == Model.apellidop )
            {
                //this.ModelState.Clear();
                this.ModelState.AddModelError("", "Personal must not equal description");
                return this.View(Model);
            }
            try
            {
                this.Contexto.tbpersonal.Add(Model);
                this.Contexto.SaveChanges();
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", ex);
                return this.View(Model);
            }
            
        }

       

        public ActionResult Delete(int id)
        {
            var  category =  this.Contexto.tbpersonal.Find(id);
            if (category == null)
                return this.HttpNotFound();

            return this.View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(int id)
        {
            var category = this.Contexto.tbpersonal.Find(id);
            if (category != null)
            {
                try
                {
                    this.Contexto.tbpersonal.Remove(category);
                    this.Contexto.SaveChanges(); 
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("", ex);
                    return this.View(category);                    
                }               
            }
            return this.RedirectToAction("Index");
        }
       


        public ActionResult Edit(int id)
        {
            var category = this.Contexto.tbpersonal.Find(id);
            if (category == null)
            {
                return this.HttpNotFound(); 
            }
            return this.View(category); 
        }

       
        [HttpPost]        
        public ActionResult  Edit(int id, tbpersonal Model)
        {
            var category = this.Contexto.tbpersonal.Find(id);
            if( category == null)
                return this.HttpNotFound(); 

            if(!this.ModelState.IsValid)
            {
                return this.View(Model);
            }                       
            
            if (!this.TryUpdateModel(category,
                new string[] { "nombre","apellidop","apellidom","lugarnacimiento","nacionalidad","sexo","estadocivil" }))
            {
                return this.View(Model);
            }
            try
            {
                this.Contexto.SaveChanges();
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {                
                this.ModelState.AddModelError("", ex);
                return this.View(Model);           
            }
        }
        



    }
}