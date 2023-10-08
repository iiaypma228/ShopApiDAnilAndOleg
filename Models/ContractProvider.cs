using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models
{
    public class ContractProvider
    {

        public string Id { get; set; }

        [ForeignKey("Provider")]
        public int ProviderId { get; set; }

        [NotMapped] 
        public Provider Provider { get; set;}

        public DateTime Date { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        [NotMapped]
        public List<ContractProviderProduct>? ContractProviders { get; set; }
    }

    public enum ShippingMethod
    {
        LabelByLabel, //помарочная;
        ByMarkingCode, //по коду маркировки с объединением в палеты или короба;
        ByBlocksAndMonoboxes //по блокам и монокоробам.
    }
}
