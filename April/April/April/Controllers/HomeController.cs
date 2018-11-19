using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using April.Common.AutoMap;
using April.Uow.Repositories;
using Domain.Core.Auditings;
using Domain.Core.Permissions.Users;

namespace April.Controllers
{
    public class HomeController : AprilWebControllerBase
    {
        private readonly IBaseRepository<UserBase, long> _baseRepository;
        private readonly IBaseRepository<AuditLog, long> _logRepository;

        public HomeController(IBaseRepository<UserBase, long> baseRepository, IBaseRepository<AuditLog, long> logRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
        }

        [Authorize]
        public ActionResult Index()
        {            
            var userId = AprilSession.BelongUserId;
            var user = _baseRepository.FirstOrDefault(u=>u.Id==1);
            var info = user.MapTo<UserBaseViewModel>();                      
            return View();
        }
    }
}