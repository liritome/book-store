namespace BookStore.ViewModels
{
    public class MultiSelectInputViewModel
    {
        public string EntityName { get; set; } = "";
        public string Header { get; set; } = "";
        public List<int> SelectedIds { get; set; } = new();
        public string InputContainerPlaceholder { get; set; } = "";

        public string FieldName { get; set; } = "";
        public string SearchByQueryUrl { get; set; } = "";
        public string SearchByIdUrl { get; set; } = "";
    }
}