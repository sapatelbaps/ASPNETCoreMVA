using System.Threading.Tasks;

namespace WebApplication24
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    public class RepeatTagHelper : TagHelper
    {
        public int Count { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            for (int i = 0; i < Count; i++)
            {
                output.Content.AppendHtml(await output.GetChildContentAsync(useCachedResult:false));
            }
        }

    }
}
