using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using April.Core.Ioc;
using April.Uow.Repositories;
using April.Web;
using April.Web.Auditing;
using WebTest.Entities;

namespace WebTest.Controllers
{
    public class HomeController : TestBaseController
    {
        //public IBaseRepository<Person, long> PersonRep { get; set; }
        //public IBaseRepository<Student, long> StudentRep { get; set; }
        private readonly IThrowException _throwException;

        public HomeController(IThrowException throwException)
        {
            _throwException = throwException;
        }

        // GET: Home        
        public ActionResult Index()
        {
            var person = new Person
            {
                Name = "Joe",
                Age = 18
            };
            //var id = PersonRep.InsertAndGetId(person);
            //var student = new Student
            //{
            //    Name = "Peter",
            //    Age = 19
            //};
            //StudentRep.InsertAndGetId(student);

            _throwException.Throw();

            return View(person);
        }
    }
}