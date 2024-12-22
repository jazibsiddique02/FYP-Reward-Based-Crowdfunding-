using FYP_Reward_Based_Crowdfunding_.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FYP_Reward_Based_Crowdfunding_.Models
{
    public class CampaignViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int campaign_id { get; set; }

        // Foreign key referencing AspNetUsers.Id
        [Required]
        [ForeignKey("User")]
        public string user_id { get; set; } // Use string since IdentityUser.Id is a string by default

        // Navigation property for IdentityUser
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public int target_amount { get; set; }

        [Required]
        public int raised_amount { get; set; }

        [Required]
        public string category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime deadline { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime created_at { get; set; }

        [Required]
        public IFormFile picture { get; set; }
    }
}
