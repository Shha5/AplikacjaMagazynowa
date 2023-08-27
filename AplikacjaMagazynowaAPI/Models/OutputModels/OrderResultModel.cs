namespace AplikacjaMagazynowaAPI.Models.OutputModels
{
    public class OrderResultModel
    {
        public bool Success { get; set; }
        public string? OrderNumber { get; set; }
        public string? OrderSignature { get; set; }
        public List<string> Errors { get; set; }
    }
}
