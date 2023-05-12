using Pollaris._3.Accessors;
using Pollaris.Models;

namespace Pollaris.Managers
{
    public class OptionManager
    {
        // Makes an option with the given ID either correct or incorrect for its corresponding question.
        // Inputs:
        // - optionId: the ID of the option to modify
        // - isCorrect: true if the option should be marked as correct, false otherwise
        // Returns: void
        public void MakeOptionCorrect(int optionId, bool isCorrect)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeOptionCorrect(optionId, isCorrect);
        }

        // Changes the name of an option with the given ID.
        // Inputs:
        // - optionId: the ID of the option to modify
        // - newName: the new name for the option
        // Returns: void
        public void ChangeOptionName(int optionId, string newName)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeOptionName(optionId, newName);
        }

        // Creates a new option for a question with the given ID.
        // Inputs:
        // - questionId: the ID of the question to create the option for
        // Returns: the ID of the newly created option
        public int CreateNewOption(int questionId)
        {
            SQLAccessor sql = new SQLAccessor();
            OptionInfo option = sql.CreateOption();
            sql.QuestionOptionConnection(questionId, option.Id);
            return option.Id;
        }

        // Deletes an option with the given ID, as well as its corresponding entries in the questionOptions and response tables.
        // Inputs:
        // - optionId: the ID of the option to delete
        // Returns: void
        public void DeleteOption(int optionId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> optionIds = new List<int> { optionId };
            sql.DeleteOptionsFromQuestionOptions(optionIds);
            sql.DeleteOptionsFromResponse(optionIds);
            sql.DeleteOptionsFromIds(optionIds);
        }
    }
}
