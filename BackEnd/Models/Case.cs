namespace BackEnd.Models
{
    public class Case : Product
    {
        public string FormFactorSupport { get; set; }
        public int MaxGpuLengthMM { get; set; }
        public int MaxCoolerHeightMM { get; set; }
        public string PsuFormFactor { get; set; }
        public int DriveBays { get; set; }
        public bool HasGlassPanel { get; set; }
        public string CaseType { get; set; }
        public bool Rgb { get; set; }
    }
}
