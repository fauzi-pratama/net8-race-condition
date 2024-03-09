
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("promo_transaction")]
    public class PromoTransaction
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("total_balance", Order = 1)]
        public decimal TotalBalance { get; set; }

        [Required]
        [Column("trans_date", Order = 2)]
        public DateTime? TransDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("commited", Order = 3)]
        public bool Commited { get; set; } = false;

        [Required]
        [Column("created_date", Order = 4)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("updated_date", Order = 5)]
        public DateTime UpdatedDate { get; set; }

        [Required]
        [Column("active_flag", Order = 6)]
        public bool ActiveFlag { get; set; } = true;

        public List<PromoTransactionDetail>? PromoTransactionDetail { get; set; }
    }
}
