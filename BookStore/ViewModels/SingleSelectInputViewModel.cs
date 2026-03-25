namespace BookStore.ViewModels
{
    public class SingleSelectInputViewModel
    {
        public string EntityName { get; set; } = "";
        public string Header { get; set; } = "";
        public int? SelectedId { get; set; }
        public string InputContainerPlaceholder { get; set; } = "";

        public string FieldName { get; set; } = "";
        public string SearchByQueryUrl { get; set; } = "";
        public string SearchByIdUrl { get; set; } = "";
    }
}
