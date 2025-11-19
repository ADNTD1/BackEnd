namespace BackEnd.Models
{
    public class Ram : Product
    {
        public bool Rgb { get; set; }

        public int CapacityGB { get; set; }

        public int SpeedMTs { get; set; }

        public string MemType { get; set; }

        public int Slots { get; set; }

    }
}
