using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using System.Dynamic;
using System.Text.Json;

namespace Pollaris.Controllers
{
    public class OptionController : Controller
    {
        // MakeOptionCorrect function sets the correctness status of an option.
        // Inputs:
        // - optionId: an integer representing the ID of the option
        // - isCorrect: a boolean indicating whether the option is correct or not
        // Returns: void
        [HttpPost]
        [Route("/Option/MakeOptionCorrect")]
        public void MakeOptionCorrect(int optionId,bool isCorrect)
        {
            OptionManager oM = new OptionManager();
            oM.MakeOptionCorrect(optionId, isCorrect); 
        }

        // CreateNewOption function creates a new option for a question.
        // Inputs:
        // - questionId: an integer representing the ID of the question
        // Returns: an integer representing the ID of the newly created option
        [HttpPost]
        [Route("/Option/CreateNewOption")]
        public int CreateNewOption(int questionId)
        {
            OptionManager oM = new OptionManager();
            return oM.CreateNewOption(questionId); 
        }

        // DeleteOption function deletes an option.
        // Inputs:
        // - optionId: an integer representing the ID of the option
        // Returns: void
        [HttpPost]
        [Route("/Option/DeleteOption")]
        public void DeleteOption(int optionId)
        {
            OptionManager oM = new OptionManager();
            oM.DeleteOption(optionId);
        }

        // ChangeOptionName function changes the name of an option.
        // Inputs:
        // - optionId: an integer representing the ID of the option
        // - optionName: a string representing the new name of the option
        // Returns: void
        [HttpPost]
        [Route("/Option/ChangeOptionName")]
        public void ChangeOptionName(int optionId, string optionName)
        {
            OptionManager oM = new OptionManager(); 
            oM.ChangeOptionName(optionId, optionName);
        }
    }
}
