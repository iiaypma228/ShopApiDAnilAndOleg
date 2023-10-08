using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models
{
    public class ContractProviderProduct
    {

        public string Id { get; set; }

        [ForeignKey("ContractProvider")]
        public string ContractProviderId { get; set; }

        [NotMapped] 
        public ContractProvider? ContractProvider { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [NotMapped]
        public Product? Product { get; set; }

        public int Amount { get; set; }
    }
}
