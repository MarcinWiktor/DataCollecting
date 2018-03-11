using System.Collections.Generic;
using WpfBasics.Enums;

namespace WpfBasics.Models
{
    public class Car
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Revision { get; set; }
        public int IdentificationNumber { get; set; }
        public MaterialEnum Material { get; set; }
        public List<ProductionOptionEnum> ProductionOptions { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public FinishEnum Finish { get; set; }
        public PurchaseInformationEnum PurchaseInformation { get; set; }
        public string SupplierName { get; set; }
        public int SupplierCode { get; set; }
        public string Note { get; set; }
    }
}
