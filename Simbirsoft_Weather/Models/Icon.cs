using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{


    public class IconTagHelper : TagHelper
    {

        public string Icon { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "img";

            if (Icon == null)
            {
                output.Attributes.SetAttribute("src", " ");
            }
            else
            {
                Icon = Icon.Replace('n','d');
                output.Attributes.SetAttribute("src", $"https://openweathermap.org/img/wn/{Icon}@2x.png");
            }
            output.Attributes.SetAttribute("width", "90");

            output.TagMode = TagMode.StartTagAndEndTag;

        }
    }
}
