using Microsoft.Extensions.Options;
using Pollaris._3.Accessors;
using Pollaris.Models;
using System.Diagnostics;

namespace Pollaris.Managers
{
    public class QuestionManager
    {
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

        public void ChangeActiveQuestion(int questionId)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeActiveQuestion(questionId); 
        }

        public int? GetActiveQuestionIndex(int setId, int roomId)
        {
            SQLAccessor sql = new SQLAccessor(); 
            List<int> questionIds = sql.GetQuestionIdsFromSetId(setId);
            List<QuestionInfo> questions = sql.GetQuestionsFromIds(questionIds); 
            for (int i = 0; i < questions.Count; i++)
            {
                if (questions[i].IsActive) return i;
            }
            return null;
        }

        public QuestionInfo? GetActiveQuestionFromSet(SetInfo set)
        {
            foreach (QuestionInfo q in set.Questions)
            {
                if (q.IsActive) return q;
            }
            return null;
        }

        public QuestionInfo GetQuestionFromId(int questionId)
        {
            SQLAccessor sql = new SQLAccessor();
            QuestionInfo question = sql.GetQuestionFromId(questionId);
            List<int> optionIds = sql.GetOptionIdsFromQuestionId(question.Id);
            List<OptionInfo> options = sql.GetOptionsFromIds(optionIds);
            question.Options = options;
            return question;
        }

        public bool SubmitAnswer(int userId, int roomId, int setId, QuestionInfo question, List<string> answers)
        {
            SQLAccessor sql = new SQLAccessor();
            List<string> correctAnswers = sql.GetAnswers(roomId, setId, question.Id);
            if (correctAnswers.SequenceEqual(answers))
            {
                //student answered correctly
                sql.SubmitStudentAnswer(userId, roomId, setId, question.Id, true);
                return true;
            }
            else
            {
                //student answered incorrectly
                sql.SubmitStudentAnswer(userId, roomId, setId, question.Id, false);
                return false;
            }
        }
    }
}
