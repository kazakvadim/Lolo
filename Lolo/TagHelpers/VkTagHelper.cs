using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Lolo.TagHelpers
{
    public class VkTagHelper : TagHelper
    {
        private const string address = "https://vk.com/id__kazak";

        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public VkTagHelper(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer; 
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", address);
            output.Attributes.SetAttribute("class", "icons-href");
            output.Content.SetContent(_sharedLocalizer["Created"]);
      
        }

    }
   
}
