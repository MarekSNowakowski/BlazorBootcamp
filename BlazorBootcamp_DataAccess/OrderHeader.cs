using System.ComponentModel.DataAnnotations;

namespace BlazorBootcamp_DataAccess
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        // todo: add navigation property for user

        [Required]
        public double OrderTotal { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime ShippingDate { get; set; }
        [Required]
        public string Status { get; set; }

        // stripe payment
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        // Customer details
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string City {  get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Email { get; set; }

        // Carrier
        public string? Tracking { get; set; }
        public string? Carrier { get; set; }
    }
}
