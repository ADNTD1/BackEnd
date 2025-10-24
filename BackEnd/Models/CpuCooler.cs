namespace BackEnd.Models
{
    public class CpuCooler : Product
    {
        public string Type { get; set; }

        public int TDP { get; set; }

        public int FanSize { get; set; }

        public int Fancount { get; set; }

        public bool Rgb { get; set; }

        public int Height { get; set; }

        public int MaxRpm { get; set; }

        public int NoiseLevel { get; set; }


    }
}
