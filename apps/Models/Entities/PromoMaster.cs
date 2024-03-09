
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("promo_master")]
    public class PromoMaster
    {
        [Key]
        [Column("code", Order = 0), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("qty", Order = 1)]
        public int? Qty { get; set; } = 0;

        [Required]
        [Column("qty_remaining", Order = 2)]
        public int? QtyRemaining { get; set; } = 0;

        [Required]
        [Column("balance", Order = 3)]
        public decimal? Balance { get; set; } = 0;

        [Required]
        [Column("balance_remaining", Order = 4)]
        public decimal? BalanceRemaining { get; set; } = 0;

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
