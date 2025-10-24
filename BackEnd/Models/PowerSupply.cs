namespace BackEnd.Models
{
    public class PowerSupply : Product
    {
        public bool Modular { get; set; }

        public string Certidication { get; set; }

        public string FormFactor { get; set; }

        public int Wattage { get; set; }

        public bool Rgb { get; set; }

    }
}
