using Microsoft.AspNet.Identity.EntityFramework;

namespace TikiCrawler.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class  ProductTiki
    {
        private string ProductID { get; set; }
        private string LinkProduct { get; set; }
        private string ProductName { get; set; }
        private string Brand { get; set; }
        private string Price { get; set; }
        private string TotalReviewPoint { get; set; }


        public void ConnetToNoSQL(string url)
        {
    
        }

    }


   
    
}