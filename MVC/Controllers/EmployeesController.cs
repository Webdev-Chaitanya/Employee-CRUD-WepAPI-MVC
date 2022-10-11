using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            IEnumerable<MvcEmployee> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<MvcEmployee>>().Result;
            return View(empList);
        }

        public ActionResult AddOrEdit(int id =0)
        {
            if (id == 0)
            {
                return View(new MvcEmployee());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcEmployee>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(MvcEmployee emp)
        {
            if (emp.EmployeeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employees", emp).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Employees/"+emp.EmployeeID , emp).Result;
                TempData["SuccessMessage"] = "Update Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Employees/" +id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully"; 
            return RedirectToAction("Index");
        }
    }
}