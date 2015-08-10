using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueAndHi.Common;

namespace DAL
{
    public class PostOps
    {
        private UserOps userOps = new UserOps();
        
        public Question AddQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestion(int questionId)
        {
            throw new NotImplementedException();
        }

        public Answer AddAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }

        public void DeleteAnswer(int answerId)
        {
            throw new NotImplementedException();
        }

        public void RecommendQuestion(int questionId)
        {
            throw new NotImplementedException();
        }

        public void VoteUpQuestion(int questionId)
        {
            throw new NotImplementedException();
        }

        public void VoteDownQuestion(int questionId)
        {
            throw new NotImplementedException();
        }

        public void VoteUpAnswer(int answerId)
        {
            throw new NotImplementedException();
        }

        public void VoteDownAnswer(int answerId)
        {
            throw new NotImplementedException();
        }

        public void IncrementVersion(int questionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> FreeSearch(string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> TagsSearch(string tag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetLatestQuestions()
        {
            List<Question> result = new List<Question>();
            IEnumerable<question> tmpResult;

            using (var db = new qnhdb())
            {
                tmpResult = db.questions.OrderByDescending(x => x.created).Take(10);
                foreach (question q in tmpResult)
                {
                    //get members
                    UserInfo ui = userOps.GetUserInfo(q.author_id);

                    result.Add(
                        Converter.toExtQuestion(
                            q, 
                            ui,
                            new RankingHistory(),//GetQuestionRankingHistory(q.id, q.author_id), 
                            getTags(q.id)
                            )
                        );
                }
            }
            return result;
        }
        public IEnumerable<Question> GetQuestionsByUser(int userId)
        {
            List<Question> result = new List<Question>();
            IEnumerable<question> tmpResult;

            using (var db = new qnhdb())
            {
                tmpResult = db.questions.Where(x => x.author_id == userId);


                foreach (question q in tmpResult)
                {
                    //get members
                    UserInfo ui = userOps.GetUserInfo(q.author_id);

                    result.Add(Converter.toExtQuestion(
                            q,
                            ui,
                            new RankingHistory(),//GetQuestionRankingHistory(q.id, q.author_id), 
                            getTags(q.id)
                            )
                       );
                }
            }
            return result;
        }

        public DiscussionThread GetDiscussionThreadById(int id)
        {
            throw new NotImplementedException();
        }

        private Question GetQuestion(int it)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Answer> GetAnswers(Question question)
        {
            throw new NotImplementedException();
        }

        public void RankQuestion(int questionId, int userId, int rank)
        {
            throw new NotImplementedException();
        }

        public void RankAnswer(int answerId, int userId, int rank)
        {
            throw new NotImplementedException();
        }

        public int GetQuestionRankingHistory(int questionId, int userId)
        {
            throw new NotImplementedException();
        }

        public int GetAnswerRankingHistory(int answerId, int userId)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> getTags(int questionId)
        {
            using (var db = new qnhdb())
            {
                return db.tags.Where(x => x.question_id == questionId).Select(row => row.tag1).ToList<String>();
            }
        }
    }
}
