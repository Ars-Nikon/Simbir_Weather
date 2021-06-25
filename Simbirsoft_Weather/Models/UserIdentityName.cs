using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class UserIdentityName : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        public UserIdentityName(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync(string UserName)
        {
            var result = await _userManager.FindByNameAsync(UserName);
            if (result == null)
            {
                return new HtmlContentViewComponentResult(new HtmlString("No Name"));
            }
            if (result.Name == null)
            {
                return new HtmlContentViewComponentResult(new HtmlString("No Name"));
            }
            return new HtmlContentViewComponentResult(new HtmlString(result.Name));
        }
    }
}
