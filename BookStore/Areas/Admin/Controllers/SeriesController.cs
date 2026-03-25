using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeriesController : Controller
    {
        private readonly SeriesService seriesService;

        public SeriesController(SeriesService seriesService)
        {
            this.seriesService = seriesService;
        }

        [HttpGet]
        public async Task<JsonResult> SearchSeriesJson(string query)
        {
            var series = await seriesService.SearchSeriesAsync(query);
            var result = series.Select(s => new
            {
                id = s.Id,
                name = s.Name
            });
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> SearchSeriesByIdJson(int id)
        {
            var series = await seriesService.GetSeriesByIdAsync(id);
            if(series == null)
            {
                return Json(null);
            }
            var result = new
            {
                id = series.Id,
                name = series.Name
            };
            return Json(result);
        }
    }
}
