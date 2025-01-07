using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FYP_Reward_Based_Crowdfunding_.Areas.Identity.Data;

namespace FYP_Reward_Based_Crowdfunding_.Models
{
    public class Rewards
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reward_id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public int reward_contribution_amount { get; set; }

        [Required]
        [ForeignKey("Campaign")]
        public int campaign_id { get; set; }

        public virtual Campaigns Campaign { get; set; }
    }
}
