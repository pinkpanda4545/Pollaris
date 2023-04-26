using Pollaris.Models;
using System.Diagnostics;

namespace Pollaris.Managers
{
    public class QuestionManager
    {
        public static List<QuestionInfo> staticQuestions = new List<QuestionInfo>();
        public List<QuestionInfo> GetQuestionsFromSetId(int setId, int roomId)
        {
            //TAKE THIS WHOLE THING OUT!!
            List<OptionInfo> options = new List<OptionInfo>();
            options.Add(new OptionInfo(1, "A", false));
            options.Add(new OptionInfo(2, "B", true));
            options.Add(new OptionInfo(3, "C", false));
            options.Add(new OptionInfo(4, "D", false));

            List<OptionInfo> options2 = new List<OptionInfo>();
            options.Add(new OptionInfo(1, "True", false));
            options.Add(new OptionInfo(2, "False", true));

            List<QuestionInfo> questions = new List<QuestionInfo>();
            questions.Add(new QuestionInfo(1, "Describe how your day is going.", "SA"));
            questions.Add(new QuestionInfo(2, "What is the color of the sun?", "MC", options));
            questions.Add(new QuestionInfo(3, "There are 7 continents on this planet.", "TF", options2));
            questions.Add(new QuestionInfo(4, "Rank the following options from the most time intensive to the least time intensive.", "R", options));
            questions.Add(new QuestionInfo(5, "What is the meaning of life?", "SA"));
            switch (roomId)
            {
                case 3:
                    questions[1].IsActive = true;
                    break;
                case 4:
                    questions[2].IsActive = true;
                    break;
                case 5:
                    questions[3].IsActive = true;
                    break;
                case 6:
                    questions[4].IsActive = true;
                    break;
            }
            staticQuestions = questions;
            return questions;
        }

        public List<QuestionInfo> GetQuestionsFromSetId(int setId)
        {
            List<OptionInfo> options = new List<OptionInfo>();
            options.Add(new OptionInfo(1, "A", false));
            options.Add(new OptionInfo(2, "B", true));
            options.Add(new OptionInfo(3, "C", false));
            options.Add(new OptionInfo(4, "D", false));

            List<OptionInfo> options2 = new List<OptionInfo>();
            options.Add(new OptionInfo(1, "True", false));
            options.Add(new OptionInfo(2, "False", true));

            List<QuestionInfo> questions = new List<QuestionInfo>();
            questions.Add(new QuestionInfo(1, "Describe how your day is going.", "SA", true));
            questions.Add(new QuestionInfo(2, "What is the color of the sun?", "MC", options));
            questions.Add(new QuestionInfo(3, "There are 7 continents on this planet.", "TF", options2));
            questions.Add(new QuestionInfo(4, "Rank the following options from the most time intensive to the least time intensive.", "R", options));
            questions.Add(new QuestionInfo(5, "What is the meaning of life?", "SA"));
            staticQuestions = questions;
            return questions;
        }

        public List<QuestionInfo> ChangeActiveQuestion(int questionId)
        {
            staticQuestions[questionId].IsActive = false;
            staticQuestions[questionId + 1].IsActive = true;
            return staticQuestions;
        }

        public int? GetActiveQuestionIndex(int setId, int roomId)
        {
            List<QuestionInfo> questions = GetQuestionsFromSetId(setId, roomId);
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
            List<OptionInfo> options = new List<OptionInfo>();
            options.Add(new OptionInfo(1, "A", false));
            options.Add(new OptionInfo(2, "B", true));
            options.Add(new OptionInfo(3, "C", false));
            options.Add(new OptionInfo(4, "D", false));
            QuestionInfo q = new QuestionInfo(questionId, "Dummy SA question here.", "MC", options);
            return q; 
        }
    }
}
