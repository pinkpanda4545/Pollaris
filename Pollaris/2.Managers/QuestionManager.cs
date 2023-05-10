﻿using Microsoft.Extensions.Options;
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

        public QuestionInfo GetQuestionFromId(int questionId)
        {
            SQLAccessor sql = new SQLAccessor();
            QuestionInfo question = sql.GetQuestionFromId(questionId);
            List<int> optionIds = sql.GetOptionIdsFromQuestionId(question.Id);
            List<OptionInfo> options = sql.GetOptionsFromIds(optionIds);
            question.Options = options;
            return question;
        }

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
                } else
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
            } else
            {
                return sql.SubmitSAResponse(userId, question.Id, answers[0]);
            }
        }

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

        public void ChangeGraded(int questionId, bool isGraded)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeGraded(questionId, isGraded); 
        }

        public void ChangeAnonymous(int questionId, bool isAnonymous)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeAnonymous(questionId, isAnonymous);
        }

        public void ChangeQuestionName(int questionId, string questionName)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeQuestionName(questionId, questionName); 
        }
    }
}
