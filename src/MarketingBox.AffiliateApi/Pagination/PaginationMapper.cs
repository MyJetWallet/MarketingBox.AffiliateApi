using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AffiliateApi.Pagination
{
    public static class PaginationMapper
    {
        public static Paginated<TItem, TId> Paginate<TItem, TId>(this IReadOnlyCollection<TItem> source,
            PaginationRequest<TId> request,
            IUrlHelper url,
            Func<TItem, TId> idProjection)
        {

            return new Paginated<TItem, TId>
            {
                Items = source,
                Pagination = MapPaginationModel(request,
                    url,
                    source,
                    idProjection)
            };
        }

        private static Pagination<TId> MapPaginationModel<TId, TItem>(PaginationRequest<TId> request,
            IUrlHelper url,
            IReadOnlyCollection<TItem> items,
            Func<TItem, TId> idProjection)
        {
            var result = new Pagination<TId>
            {
                Count = items.Count,
                Order = request.Order,
                Cursor = request.Cursor,
            };

            if (items.Any() && items.Count == request.Limit)
            {
                request.Cursor = idProjection(items.Last());
                result.NextUrl = BuildUrl(url, request);
            }

            return result;
        }

        private static string BuildUrl<T>(IUrlHelper url, PaginationRequest<T> request)
        {
            var controller = url.ActionContext.RouteData.Values["controller"].ToString();
            var action = url.ActionContext.RouteData.Values["action"].ToString();

            return url.Action(action, controller, request);
        }
    }
}
