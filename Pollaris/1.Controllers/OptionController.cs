using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using System.Dynamic;
using System.Text.Json;

namespace Pollaris.Controllers
{
    public class OptionController : Controller
    {
        [HttpPost]
        [Route("/Option/MakeOptionCorrect")]
        public void MakeOptionCorrect(int optionId,bool isCorrect)
        {
            OptionManager oM = new OptionManager();
            oM.MakeOptionCorrect(optionId, isCorrect); 
        }

        [HttpPost]
        [Route("/Option/CreateNewOption")]
        public int CreateNewOption(int questionId)
        {
            OptionManager oM = new OptionManager();
            return oM.CreateNewOption(questionId); 
        }

        [HttpPost]
        [Route("/Option/DeleteOption")]
        public void DeleteOption(int optionId)
        {
            OptionManager oM = new OptionManager();
            oM.DeleteOption(optionId);
        }

        [HttpPost]
        [Route("/Option/ChangeOptionName")]
        public void ChangeOptionName(int optionId, string optionName)
        {
            OptionManager oM = new OptionManager(); 
            oM.ChangeOptionName(optionId, optionName);
        }
    }
}
