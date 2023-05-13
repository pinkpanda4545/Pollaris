using Microsoft.Extensions.Options;
using Pollaris._3.Accessors;
using Pollaris.Models;
using System.Diagnostics;

namespace Pollaris.Managers
{
    public class QuestionManager
    {
        // This function retrieves a list of QuestionInfo objects from a SQL database based on a given setId.
        // Inputs:
        // - int setId: the id of the set of questions to retrieve from the SQL database.
        // Returns: list of QuestionInfo objects retrieved.
        public List<QuestionInfo> GetQuestionsFromSetId(int setId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> questionIds = sql.GetQuestionIdsFromSetId(setId);
            List<QuestionInfo> questions = sql.GetQuestionsFromIds(questionIds);
            foreach (QuestionInfo question in questions)
            {
                List<int> optionIds = sql.GetOptionIdsFromQuestionId(question.Id);
                List<OptionInfo> options = sql.GetOptionsFromIds(optionIds);
                question.Options = options;
            }
            return questions;
        }

        // This function retrieves a QuestionInfo object from a SQL database based on a given questionId.
        // Inputs:
        // - int questionId: the id of the question to retrieve from the SQL database.
        // Returns: QuestionInfo object retrieved.
        public QuestionInfo GetQuestionFromId(int questionId)
        {
            SQLAccessor sql = new SQLAccessor();
            QuestionInfo question = sql.GetQuestionFromId(questionId);
            List<int> optionIds = sql.GetOptionIdsFromQuestionId(question.Id);
            List<OptionInfo> options = sql.GetOptionsFromIds(optionIds);
            question.Options = options;
            return question;
        }

        // This function submits a user's answer to a given question to a SQL database.
        // Inputs:
        // - int userId: the id of the user submitting the answer.
        // - QuestionInfo question: the question being answered.
        // - List<string> answers: a list of strings representing the user's answers.
        // Returns: boolean indicating whether or not the submission was successful.
        public bool SubmitAnswer(int userId, QuestionInfo question, List<string> answers)
        {
            SQLAccessor sql = new SQLAccessor();
            if (question.Type != "SA")
            {
                List<int> optionIds = sql.GetOptionIdsFromQuestionId(question.Id);
                List<OptionInfo> options = sql.GetOptionsFromIds(optionIds);
                if (question.Type != "R")
                {
                    int optionId = options.Where(x => x.Name == answers[0]).First().Id;
                    return sql.SubmitMCorTFResponse(userId, optionId);
                }
                else
                {
                    if (answers.Count != options.Count) return false;

                    for (int i = 0; i < answers.Count; i++)
                    {
                        int optionId = options.Where(x => x.Name == answers[i]).First().Id;
                        bool result = sql.SubmitRankingResponse(userId, optionId, i);
                        if (!result) return false;
                    }
                    return true;
                }
            }
            else
            {
                return sql.SubmitSAResponse(userId, question.Id, answers[0]);
            }
        }

        // Create a new question of the specified type and add it to the specified set. 
        // Inputs:
        //  - setId: int representing the ID of the set to add the question to
        //  - type: string representing the type of question to create
        // Returns: newly created QuestionInfo object.
        public QuestionInfo CreateQuestion(int setId, string type)
        {
            SQLAccessor sql = new SQLAccessor();
            QuestionInfo question = sql.CreateQuestion(type);
            sql.SetQuestionConnection(setId, question.Id);
            if (sql.GetQuestionIdsFromSetId(setId).Count == 1)
            {
                sql.ChangeActiveQuestion(setId, question.Id);
            }
            return question;
        }

        // Delete the question with the specified ID and all associated options and responses from the database.
        // Inputs:
        //  - id: int representing the ID of the question to delete
        // Returns: void
        public void DeleteQuestion(int id)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> optionIds = sql.GetOptionIdsFromQuestionId(id);
            sql.DeleteOptionsFromQuestionOptions(optionIds);
            sql.DeleteOptionsFromResponse(optionIds);
            sql.DeleteOptionsFromIds(optionIds);
            sql.DeleteQuestionsFromSet(new List<int> { id });
            sql.DeleteQuestionsFromIds(new List<int> { id });
        }

        // Change whether the question with the specified ID is graded or not.
        // Inputs:
        //  - questionId: int representing the ID of the question to modify
        //  - isGraded: bool representing whether the question should be graded or not
        // Returns: void
        public void ChangeGraded(int questionId, bool isGraded)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeGraded(questionId, isGraded);
        }

        // Change whether the question with the specified ID is anonymous or not.
        // Inputs:
        //  - questionId: int representing the ID of the question to modify
        //  - isAnonymous: bool representing whether the question should be anonymous or not
        // Returns: void
        public void ChangeAnonymous(int questionId, bool isAnonymous)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeAnonymous(questionId, isAnonymous);
        }

        // Change the name of the question with the specified ID.
        // Inputs:
        //  - questionId: int representing the ID of the question to modify
        //  - questionName: string representing the new name for the question
        // Returns: void
        public void ChangeQuestionName(int questionId, string questionName)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeQuestionName(questionId, questionName);
        }
    }
}
