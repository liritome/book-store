namespace BookStore.ViewModels
{
    public interface ISelectInputViewModel
    {
        string Name { get; }
        string Label { get; }
        string Placeholder { get; }
        IEnumerable<object> Items { get; }
        IEnumerable<int>? SelectedIds { get; }
        int GetId(object item);
        string GetText(object item);
    }

    public class SelectInputViewModel<T> : ISelectInputViewModel
    {
        public string Name { get; set; } = null!;
        public string Label { get; set; } = "";
        public string Placeholder { get; set; } = "";
        public IEnumerable<T> Items { get; set; } = new List<T>();
        IEnumerable<object> ISelectInputViewModel.Items => Items.Cast<object>();
        public IEnumerable<int>? SelectedIds { get; set; } = new List<int>();
        public Func<T, int> GetIdFunc { get; set; } = null!;
        public int GetId(object item) => GetIdFunc((T)item);
        public Func<T, string> GetTextFunc { get; set; } = null!;
        public string GetText(object item) => GetTextFunc((T)item);
    }
}