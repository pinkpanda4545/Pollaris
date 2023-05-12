using Pollaris._3.Accessors;
using Pollaris.Models;

namespace Pollaris.Managers
{
    public class OptionManager
    {
        public void MakeOptionCorrect(int optionId, bool isCorrect)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeOptionCorrect(optionId, isCorrect); 
        }

        public void ChangeOptionName(int optionId, string newName)
        {
            SQLAccessor sql = new SQLAccessor(); 
            sql.ChangeOptionName(optionId, newName);
        }

        public int CreateNewOption(int questionId)
        {
            SQLAccessor sql = new SQLAccessor();
            OptionInfo option = sql.CreateOption();
            sql.QuestionOptionConnection(questionId, option.Id); 
            return option.Id;
        }

        public void DeleteOption(int optionId)
        {
            //delete from questionOptions
            //delete from response
            //delete from Option
            SQLAccessor sql = new SQLAccessor();
            List<int> optionIds = new List<int> { optionId };
            sql.DeleteOptionsFromQuestionOptions(optionIds);
            sql.DeleteOptionsFromResponse(optionIds);
            sql.DeleteOptionsFromIds(optionIds);
        }
    }
}
