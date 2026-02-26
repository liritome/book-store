using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections;
using System.Text;

namespace BookStore.TagHelpers
{
    [HtmlTargetElement("sortable-header")]
    public class SortableHeaderTagHelper : TagHelper
    {
        public string DisplayName { get; set; } = "";
        public Enum? SortBy { get; set; }
        public Enum? CurrentSort { get; set; }
        public Enum? SortAsc { get; set; }
        public Enum? SortDesc { get; set; }
        public object? Filter { get; set; }
        public string Controller { get; set; } = "";
        public string Action { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "form";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("method", "get");
            output.Attributes.SetAttribute("action", $"/Admin/{Controller}/{Action}");

            var sb = new StringBuilder();
            sb.Append($"<input type='hidden' name='Sort' value='{SortBy}'/>");

            if (Filter != null)
            {
                var props = Filter.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var value = prop.GetValue(Filter);
                    if (value == null) continue;
                    if (value is IEnumerable enumerable && value is not string)
                    {
                        foreach (var item in enumerable)
                        {
                            sb.Append($"<input type='hidden' name='{prop.Name}' value='{item}'/>");
                        }
                    }
                    else
                    {
                        sb.Append($"<input type='hidden' name='{prop.Name}' value='{value}'/>");
                    }
                }
            }

            string icon = "";
            if (CurrentSort != null)
            {
                if (SortAsc != null && CurrentSort.Equals(SortAsc))
                {
                    icon = "<i class='bi bi-caret-down-fill'></i>";
                }
                else if (SortDesc != null && CurrentSort.Equals(SortDesc))
                {
                    icon = "<i class='bi bi-caret-up-fill'></i>";
                }
            }
            sb.Append($"<button type='submit' class='btn fw-bold p-0'>{DisplayName}{icon}</button>");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
