namespace BackEnd.Models
{
    public class GraphicsCard : Product
    {
        public string Chipset { get; set; }

        public int VRam { get; set; }

        public string MemoryType { get; set; }

        public int TDP { get; set; }

        public string PCIeVersion { get; set; }

        public int Warranty { get; set; }

        public string Lenght { get; set; }
    }
}
