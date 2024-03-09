
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("promo_transaction_detail")]
    public class PromoTransactionDetail
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("promo_transaction")]
        [Column("promo_transaction_id", Order = 1)]
        public Guid? PromoTransactionId { get; set; }

        [Column("code", Order = 2), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("qty", Order = 3)]
        public int? Qty { get; set; } = 0;

        [Required]
        [Column("balance", Order = 4)]
        public decimal? Balance { get; set; } = 0;

        [Required]
        [Column("created_date", Order = 5)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("updated_date", Order = 6)]
        public DateTime UpdatedDate { get; set; }

        [Required]
        [Column("active_flag", Order = 7)]
        public bool ActiveFlag { get; set; } = true;
    }
}
