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
            List<string> correctAnswers = sql.GetAnswersFromQuestionId(question.Id);
            if (correctAnswers.SequenceEqual(answers))
            {
                //student answered correctly
                sql.SubmitStudentAnswer(userId, question.Id, answers);
                return true;
            }
            else
            {
                //student answered incorrectly
                sql.SubmitStudentAnswer(userId, question.Id, answers);
                return false;
            }
        }

        public QuestionInfo CreateQuestion(int setId, string type)
        {
            SQLAccessor sql = new SQLAccessor();
            QuestionInfo question = sql.CreateQuestion(type);
            sql.SetQuestionConnection(setId, question.Id);
            return question;
        }

        public QuestionInfo ChangeActiveQuestion(int setId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetQuestionIdsFromSetId(setId);
            List<QuestionInfo> questions = sql.GetQuestionsFromIds(ids);
            int activeQuestionId = questions.Where(x => x.IsActive).First().Id;
            int nextQuestionId = 0;
            int nextQuestionIndex = -1;
            for (int i = 0; i < questions.Count; i++)
            {
                if (questions[i].IsActive)
                {
                    nextQuestionId = questions[i + 1].Id;
                    nextQuestionIndex = i;
                }
            }
            sql.ChangeActiveQuestion(activeQuestionId, nextQuestionId);
            return questions[nextQuestionIndex];
        }

        public void SaveQuestionEdits(int questionId)
        {

        }
    }
}
