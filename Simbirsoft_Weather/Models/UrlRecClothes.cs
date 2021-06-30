using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Simbirsoft_Weather.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    [ViewComponent]
    public class UrlRecClothes
    {
        private readonly IRecPatternWritter rec;

        public UrlRecClothes(IRecPatternWritter rechtml)
        {
            rec = rechtml;
        }

        public IViewComponentResult Invoke(Clothes clothes)
        {
            return new HtmlContentViewComponentResult(
                new HtmlString(rec.WriteRec(clothes,clothes.ForWhom))
            );
        }

    }
}
