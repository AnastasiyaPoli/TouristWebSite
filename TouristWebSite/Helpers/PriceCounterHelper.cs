using DAL.DBHelpers;

namespace TouristWebSite.Helpers
{
    public class PriceCounterHelper
    {
        public static long CountPrice(long routeId, bool isBusiness, long backRouteId, bool isBackBusiness, long hotelId, bool isLux, long ex1, long ex2, long ex3, long ex4, long ex5, long peopleCount)
        {
            long price = 0;

            if (isBusiness)
            {
                price += RoutesDBHelper.GetRouteById(routeId).PriceBusiness;
            }
            else
            {
                price += RoutesDBHelper.GetRouteById(routeId).PriceEconom;
            }

            if (isBackBusiness)
            {
                price += BackRoutesDBHelper.GetBackRouteById(backRouteId).PriceBusiness;
            }
            else
            {
                price += BackRoutesDBHelper.GetBackRouteById(backRouteId).PriceEconom;
            }

            if (isLux)
            {
                price += HotelsDBHelper.GetHotelById(hotelId).PriceLux;
            }
            else
            {
                price += HotelsDBHelper.GetHotelById(hotelId).PriceStandart;
            }

            if (ex1 != 0)
            {
                price += ExcursionsDBHelper.GetExcursionById(ex1).Price;
            }

            if (ex2 != 0)
            {
                price += ExcursionsDBHelper.GetExcursionById(ex2).Price;
            }

            if (ex3 != 0)
            {
                price += ExcursionsDBHelper.GetExcursionById(ex3).Price;
            }

            if (ex4 != 0)
            {
                price += ExcursionsDBHelper.GetExcursionById(ex4).Price;
            }

            if (ex5 != 0)
            {
                price += ExcursionsDBHelper.GetExcursionById(ex5).Price;
            }

            return price * peopleCount;
        }
    }
}