using NHibernate;
using NHibernateDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;

// reference: http://www.dotnetjalps.com/2013/09/asp-net-mvc-nhibernate-crud-getting-started.html
namespace NHibernateDemo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var employees = session.Query<Employee>().ToList();
                return View(employees);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                using (ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(employee);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            }
            catch ( Exception exception)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return View(employee);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                using (ISession session = NHibernateSession.OpenSession())
                {
                    var employeeToUpdate = session.Get<Employee>(id);

                    employeeToUpdate.Designation = employee.Designation;
                    employeeToUpdate.FirstName = employee.FirstName;
                    employeeToUpdate.LastName = employee.LastName;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(employeeToUpdate);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return View(employee);
            }
        }

        public ActionResult Delete(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return View(employee);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                using (ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(employee);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
    }
}