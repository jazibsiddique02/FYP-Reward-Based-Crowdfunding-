using FYP_Reward_Based_Crowdfunding_.Areas.Identity.Data;
using FYP_Reward_Based_Crowdfunding_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;



namespace FYP_Reward_Based_Crowdfunding_.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ApplicationDbContext context;
        IWebHostEnvironment env;
        private readonly UserManager<ApplicationUser> userManager;


        public CampaignController(ApplicationDbContext _context, IWebHostEnvironment _env, UserManager<ApplicationUser> _userManager)
        {
            context = _context;
            env = _env;
            userManager = _userManager;
        }





        public async Task<IActionResult> GetAllCampaigns()
        {

            var campaigns = await context.Campaigns.ToListAsync();
            return View(campaigns);
        }


        [Authorize]
        public async Task<IActionResult> CreateCampaign()
        {
            var userId = userManager.GetUserId(User); // Get the logged-in user's ID
            ViewBag.UserId = userId;
            return View();
        }





        [HttpPost]
        public IActionResult CreateCampaign(CampaignViewModel campaign)
        {
            if (!ModelState.IsValid)
            {

                string filename = "";

                campaign.deadline = campaign.deadline.Date;
                campaign.created_at = campaign.deadline.Date;



                if (campaign.picture != null)
                {
                    string folder = Path.Combine(env.WebRootPath, "images");
                    filename = Guid.NewGuid().ToString() + "_" + campaign.picture.FileName;
                    string filePath = Path.Combine(folder, filename);
                    campaign.picture.CopyTo(new FileStream(filePath, FileMode.Create));

                    Campaigns newCampaign = new Campaigns
                    {
                        user_id = userManager.GetUserId(User),
                        title = campaign.title,
                        description = campaign.description,
                        target_amount = campaign.target_amount,
                        raised_amount = campaign.raised_amount,
                        category = campaign.category,
                        deadline = campaign.deadline,
                        created_at = campaign.created_at,
                        image_url = filename,
                        contribution_amount = campaign.contribution_amount
                    };

                    context.Campaigns.Add(newCampaign);
                    context.SaveChanges();





                    // Find the campaign where the title matches
                    var campaignData = context.Campaigns.FirstOrDefault(c => c.title == newCampaign.title);

                    


                    // Store the campaign object in session (serialized as a JSON string)
                    HttpContext.Session.SetString("newCampaign", JsonConvert.SerializeObject(campaignData));

                    
                    return RedirectToAction("CreateReward");



                }
            }
            return View();
        }




        public async Task<IActionResult> CreateReward()
        {
            // Retrieve the serialized campaign data from session
            var campaignData = HttpContext.Session.GetString("newCampaign");

            if (!string.IsNullOrEmpty(campaignData))
            {
                // Deserialize the data back to a Campaigns object
                var newCampaign =JsonConvert.DeserializeObject<Campaigns>(campaignData);
                ViewBag.CampaignId = newCampaign.campaign_id;

                // Pass the object to the view
                return View();
            }

            //// If no campaign data in session, return an error or redirect to another page
            //return BadRequest("Campaign data is not available.");

            // Find the campaign where the title matches

            return View();
        }





        public IActionResult FinalizeCampaign()
        {
            return RedirectToAction("GetAllCampaigns");
        }

        [HttpPost]
        public IActionResult CreateReward(Rewards reward)
        {
            if (!ModelState.IsValid)
            {
                Rewards newReward = new Rewards
                {
                    title = reward.title,
                    description = reward.description,
                    reward_contribution_amount = reward.reward_contribution_amount,
                    campaign_id = reward.campaign_id
                };

                context.Rewards.Add(newReward);
                context.SaveChanges();


                TempData["Message"] = "Reward created successfully!";

                return RedirectToAction("CreateReward");
            }
            return View();
        }

    }
}








