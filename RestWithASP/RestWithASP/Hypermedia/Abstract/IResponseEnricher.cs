using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithASP.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnricher(ResultExecutingContext context);

        Task Enrich(ResultExecutingContext conte);
    }
}
