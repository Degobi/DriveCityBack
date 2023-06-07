namespace DriveOfCity.Models.Pagamento
{
    public class LinkRequisicao
    {
        public decimal Price { get; set; }
        public string? Produto { get; set; }
        public string? SuccessUrl { get; internal set; }
        public string? CancelUrl { get; internal set; }
    }
}
