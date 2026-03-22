using BookStore.InputModels;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class BookBaseIndexViewModel
    {
        public IEnumerable<BookBase> BookBases { get; set; } = null!;

        public BookBaseFilter Filter { get; set; } = new();

        public PageViewModel PageViewModel { get; set; } = null!;

        public BookBaseSortState Sort {  get; set; }

        public BookBaseSortState IdSort => Sort == BookBaseSortState.IdAsc
            ? BookBaseSortState.IdDesc : BookBaseSortState.IdAsc;

        public BookBaseSortState TitleSort => Sort == BookBaseSortState.TitleAsc
            ? BookBaseSortState.TitleDesc : BookBaseSortState.TitleAsc;

        public BookBaseSortState SeriesIdSort => Sort == BookBaseSortState.SeriesIdAsc
            ? BookBaseSortState.SeriesIdDesc : BookBaseSortState.SeriesIdAsc;

        public BookBaseSortState YearSort => Sort == BookBaseSortState.YearAsc
            ? BookBaseSortState.YearDesc : BookBaseSortState.YearAsc;

        public BookBaseSortState EditionsCountSort => Sort == BookBaseSortState.EditionsCountAsc
            ? BookBaseSortState.EditionsCountDesc : BookBaseSortState.EditionsCountAsc;
    }
}
