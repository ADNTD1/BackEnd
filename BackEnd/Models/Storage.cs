namespace BackEnd.Models
{
    public class Storage : Product
    {
        public string Type { get; set; }
        public string Interface { get; set; }
        public int CapacityGB { get; set; }
        public int ReadSpeedMBps { get; set; }
        public int WriteSpeedMBps { get; set; }
        public string FormFactor { get; set; }
        public int Warranty { get; set; }
    }
}
