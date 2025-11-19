namespace BackEnd.Models
{
    public class Motherboard : Product
    {
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public int MaxRamGB { get; set; }
        public string FormFactor { get; set; }
        public int RamSlots { get; set; }
        public bool HasWiFi { get; set; }

        public int M2Slots { get; set; }

        public string SupportedMemType { get; set; }
    }
}
