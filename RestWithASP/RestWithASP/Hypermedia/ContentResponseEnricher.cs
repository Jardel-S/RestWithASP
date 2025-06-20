﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWithASP.Hypermedia.Abstract;
using RestWithASP.Hypermedia.Utils;
using System.Collections.Concurrent;

namespace RestWithASP.Hypermedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHypermedia
    {

        public ContentResponseEnricher()
        { 
        
        }

        public virtual bool CanEnricher(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>) || contentType == typeof(PagedSearchVO<T>);
        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        bool IResponseEnricher.CanEnricher(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
            {
                return CanEnricher(okObjectResult.Value.GetType());
            }

            return false;
        }

        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);

            if (response.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);

                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
                else if (okObjectResult.Value is PagedSearchVO<T> pageSearch)
                {
                    Parallel.ForEach(pageSearch.List.ToList(), (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
            }            
            await Task.FromResult<object>(null);
        }
    }
}
