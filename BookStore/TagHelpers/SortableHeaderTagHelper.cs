using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections;

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
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;

            var queryParams = new List<string>();
            if (SortBy != null)
            {
                queryParams.Add($"Sort={SortBy}");
            }
            if (Filter != null)
            {
                var props = Filter.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var value = prop.GetValue(Filter);
                    if (value == null) continue;
                    if (value is IEnumerable enumerable && value is not string)
                    {
                        var values = new List<string>();
                        foreach (var item in enumerable)
                        {
                            if (item != null)
                            {
                                queryParams.Add($"{prop.Name}={item}");
                            }
                        }
                    }
                    else
                    {
                        queryParams.Add($"{prop.Name}={value}");
                    }
                }
            }

            var queryString = queryParams.Count > 0 ? "?" + string.Join('&', queryParams) : "";
            var href = $"/Admin/{Controller}/{Action}{queryString}";
            output.Attributes.SetAttribute("href", href);

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

            output.Content.SetHtmlContent($"{DisplayName}{icon}");
        }
    }
}
