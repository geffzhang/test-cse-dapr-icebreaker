using Newtonsoft.Json;

namespace Vigilantes.DaprWorkshop.LoyaltyService.Models
{
    public class OrderItemSummary
    {
        [JsonProperty("menuItemId")]
        public int MenuItemId { get; set; }
        [JsonProperty("menuItemName")]
        public string MenuItemName { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}