using System.Web.Mvc;


namespace KarrenCupCakes.Controllers
{
    
    
    public class PortfolioController : Controller
    {
        
        public ActionResult Cakes()
        {
            return View(); 
        }

        public ActionResult Desserts()
        {
            return View();
        }

        public ActionResult Pasta()
        {
            return View();
        }
    }
}
