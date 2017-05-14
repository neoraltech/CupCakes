using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System.Linq;
using KarrenCupCakes.Models;
using KarrenCupCakes.Core;
using System.Web;

namespace KarrenCupCakes.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Connect(string name, string email, string number)
        {
            UserData userData = new UserData();
            userData.Name = name;
            userData.Email = email;
            userData.Phone = number;

            Session.Add("UserData", userData);

            bool result = SignalRConnector.AddThisUser(userData);

            return Json(result);
        }

        public ActionResult SendMessage(string message)
        {
            UserData userData = (UserData)Session["UserData"];

            bool result = SignalRConnector.SendMessage(userData, message);

            return Json(result);
        }

        public static void MessageReceived(string message)
        {
            
        }
	}
}