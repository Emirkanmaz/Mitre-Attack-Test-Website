using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using mitredeneme.Models.Entity;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace mitredeneme.Controllers
{
    public class AttackController : Controller
    {
        // GET: Attack
        public ActionResult Index()
        {
            using (dbdenemeEntities2 dbModel = new dbdenemeEntities2())
            {
                return View(dbModel.TBL_sonuc.ToList());
            }
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult Test1()
        {
            return View();
        }
        public ActionResult Test2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RunPowerShellScriptTest1()
        {
            // Create a PowerShell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            // Create a pipeline and execute the script
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript("C:\\Users\\emirk\\OneDrive\\Masaüstü\\son\\mitredeneme\\Content\\tests\\testOne.bat");
            pipeline.Commands.Add("Out-String");
            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace
            runspace.Close();

            // convert the script result into a single string
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());   
            }

            using (dbdenemeEntities2 dbModel = new dbdenemeEntities2())
            {
                TBL_sonuc p1 = new TBL_sonuc();
                p1.kullaniciAdi = (string)Session["username"];
                p1.test1 = "Başarılı";
                p1.time = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                dbModel.TBL_sonuc.Add(p1);
                dbModel.SaveChanges();
                // return the results of the script execution
            }
            
            return RedirectToAction("Index", "Attack");
        }
        [HttpPost]
        public ActionResult RunPowerShellScriptTest2()
        {
            // Create a PowerShell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            // Create a pipeline and execute the script
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript("C:\\Users\\emirk\\OneDrive\\Masaüstü\\son\\mitredeneme\\Content\\tests\\testTwo.bat");
            pipeline.Commands.Add("Out-String");
            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace
            runspace.Close();

            // convert the script result into a single string
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }

            using (dbdenemeEntities2 dbModel = new dbdenemeEntities2())
            {
                TBL_sonuc p1 = new TBL_sonuc();
                p1.kullaniciAdi = (string)Session["username"];
                p1.test2 = "Başarılı";
                p1.time = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                dbModel.TBL_sonuc.Add(p1);
                dbModel.SaveChanges();
                // return the results of the script execution
            }

            return RedirectToAction("Index", "Attack");
        }

    }
}