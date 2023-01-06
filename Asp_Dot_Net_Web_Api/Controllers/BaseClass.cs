using System;

namespace Asp_Dot_Net_Web_Api.Controllers
{
    public class BaseClass : Controller
    {
        public readonly ApplicationDbContext _db;

        public BaseClass(ApplicationDbContext db)
        {
            _db = db;
        }

    }
}

