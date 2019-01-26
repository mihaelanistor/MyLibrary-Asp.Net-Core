using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace MyLibrary.Repositories
{
    [Authorize]
    public class HelperRepository : Controller
    {

        //private readonly ApplicationDbContext _context;

        //public HelperRepository(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public string Logat()
        {
            //    //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //    //var hei = User.Identity.IsAuthenticated.ToString();

            //    return "hello";


            //AspNetUserManager<UserLoginInfo>
            //    var user = _context.Users.FirstOrDefault().Id;
            return "hei";
        }







    //private readonly UserManager<ApplicationDbContext> _userManager;

    //    public HelperRepository(UserManager<ApplicationDbContext> userManager)
    //    {
    //        _userManager = userManager;
    //    }

    //    [HttpGet]
    //    public async Task<string> GetCurrentUserId()
    //    {
    //        ApplicationDbContext usr = await GetCurrentUserAsync();
    //        return usr?.;
    //    }

    //    private Task<ApplicationDbContext> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


    }
}
