using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections;

namespace BookStore.TagHelpers
{
    public class PageTagHelper : TagHelper
    {
        public int PageNumber { get; set; }
        public Enum? SortBy { get; set; }
        public object? Filter { get; set; }
        public string Area { get; set; } = "";
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
            queryParams.Add($"Page={PageNumber}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join('&', queryParams) : "";
            string areaHrefSegment = string.IsNullOrEmpty(Area) ? "" : $"/{Area}";
            var href = $"{areaHrefSegment}/{Controller}/{Action}{queryString}";
            output.Attributes.SetAttribute("href", href);
        }
    }
}
