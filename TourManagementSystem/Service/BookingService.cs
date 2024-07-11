using TourManagementSystem.Models;

namespace TourManagementSystem.Services
{
    public class BookingService
    {
        public decimal CalculateTotalCost(Service service, Destination destination)
        {
            decimal totalCost = 0;

            if (!string.IsNullOrEmpty(service.NoOfRoom))
            {
                totalCost += Convert.ToDecimal(service.NoOfRoom) * destination.HotelCosrPerDay.Value;
            }
            else
            {
                totalCost += destination.BaseCost;
            }

            if (service.IsRequiredPersonalGuide)
            {
                totalCost += 5000;
            }

            totalCost += destination.BaseCost;

            return totalCost;
        }
    }
}
