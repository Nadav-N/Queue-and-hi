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
            using (var db = new qnhdb())
            {
                question intQuestion = Converter.toQuestion(question);
                intQuestion.version = 1;
                db.questions.Add(intQuestion);
                db.SaveChanges();
                // When does the saved question gets his id?
                // if its part of the saving to the DB process, then how can I know what is the id which was generated for the question?
                // if its before the saving to he DB (Manual set) then it cannot be int and should be replaced with Uniqueidentifier (guid)
                //question.Tags.Select(t => Converter.toTag(t, intQuestion.id));
                UserInfo ui = userOps.GetUserInfo(intQuestion.author_id);
                return Converter.toExtQuestion(intQuestion, ui, new RankingHistory(), question.Tags);
            }
        }

        public void DeleteQuestion(int questionId)
        {
            using (var db = new qnhdb())
            {
                foreach (var ranking in db.question_rankings.Where(r => r.question_id == questionId))
                {
                    db.question_rankings.Remove(ranking);
                }
                question question = db.questions.Single(q => q.id == questionId);
                db.questions.Remove(question);
                db.SaveChanges();
            }
        }

        public Answer AddAnswer(Answer answer)
        {
            using (var db = new qnhdb())
            {
                answer intAnswer = Converter.toAnswer(answer);
                db.answers.Add(intAnswer);
                db.SaveChanges();
                return answer;
            }
        }

        public void DeleteAnswer(int answerId)
        {
            using (var db = new qnhdb())
            {
                foreach (var ranking in db.answer_rankings.Where(r => r.answer_id == answerId))
                {
                    db.answer_rankings.Remove(ranking);
                }

                answer answer = db.answers.Single(a => a.id == answerId);
                db.answers.Remove(answer);
                db.SaveChanges();
            }
        }

        public void RecommendQuestion(int questionId)
        {
            ChangeQuestionRecommendation(questionId, true);
        }

        public void UnrecommendQuestion(int questionId)
        {
            ChangeQuestionRecommendation(questionId, false);
        }

        private void ChangeQuestionRecommendation(int questionId, bool isRecommended)
        {
            using (var db = new qnhdb())
            {
                question question = db.questions.Single(q => q.id == questionId);
                question.recommended = Convert.ToByte(isRecommended);
                db.SaveChanges();
            }
        }

        public void IncrementVersion(int questionId)
        {
            using (var db = new qnhdb())
            {
                question question = db.questions.First(q => q.id == questionId);
                question.version++;
                db.SaveChanges();
            }
        }

        public IEnumerable<Question> FreeSearch(string searchString)
        {
            List<Question> questions = new List<Question>();
            using (var db = new qnhdb())
            {
                foreach (question question in db.questions.Where(q => q.title.Contains(searchString) || q.contents.Contains(searchString)))
                {
                    UserInfo ui = userOps.GetUserInfo(question.author_id);
                    RankingHistory rh = GetQuestionRankingHistory(question.id);
                    questions.Add(Converter.toExtQuestion(question, ui, rh, getTags(question.id)));
                }

                return questions;
            }
        }

        public IEnumerable<Question> TagsSearch(string tag)
        {
            List<Question> questions = new List<Question>();
            using (var db = new qnhdb())
            {
                foreach (tag intTag in db.tags.Where(t => String.Equals(t.tag1, tag, StringComparison.InvariantCultureIgnoreCase)))
                {
                    questions.Add(GetQuestionById(intTag.question_id));
                }

                return questions;
            }
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
                            new RankingHistory(),//GetQuestionRankingHistory(q.id), 
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
            DiscussionThread discussionThread = new DiscussionThread();
            int dtVersion;
            Question question = GetInternalQuestionById(id, out dtVersion);
            discussionThread.Version = dtVersion;
            discussionThread.Question = question;
            discussionThread.Answers = GetAnswers(question);

            return discussionThread;
        }

        public Question GetQuestionById(int id)
        {
            int version;
            return GetInternalQuestionById(id, out version);
        }

        private Question GetInternalQuestionById(int id, out int version)
        {
            using (var db = new qnhdb())
            {
                question question = db.questions.First(x => x.id == id);
                version = question.version;
                UserInfo ui = userOps.GetUserInfo(question.author_id);
                return Converter.toExtQuestion(question, ui, GetQuestionRankingHistory(question.id), getTags(question.id));
            }
        }

        public Answer GetAnswerById(int id)
        {
            using (var db = new qnhdb())
            {
                answer answer = db.answers.First(x => x.id == id);
                UserInfo ui = userOps.GetUserInfo(answer.author_id);
                return Converter.toExtAnswer(answer, ui, GetAnswerRankingHistory(answer.id));
            }
        }

        private IEnumerable<Answer> GetAnswers(Question question)
        {
            List<Answer> externalAnswers = new List<Answer>();
            using (var db = new qnhdb())
            {
                foreach (var answer in db.answers.Where(a => a.question_id == question.ID))
                {
                    UserInfo ui = userOps.GetUserInfo(answer.author_id);
                    RankingHistory rh = GetAnswerRankingHistory(answer.id);
                    externalAnswers.Add(Converter.toExtAnswer(answer, ui, rh));
                }
            }

            return externalAnswers;
        }

        public void RankUpQuestion(int questionId, int userId)
        {
            throw new NotImplementedException();
        }

        public void RankDownQuestion(int questionId, int userId)
        {
            throw new NotImplementedException();
        }

        public void RankUpAnswer(int answerId, int userId)
        {
            throw new NotImplementedException();
        }

        public void RankDownAnswer(int answerId, int userId)
        {
            throw new NotImplementedException();
        }

        public void CancelRankUpQuestion(int questionId, int userId)
        {
            throw new NotImplementedException();
        }

        public void CancelRankDownQuestion(int questionId, int userId)
        {
            throw new NotImplementedException();
        }

        public void CancelRankUpAnswer(int answerId, int userId)
        {
            throw new NotImplementedException();
        }

        public void CancelRankDownAnswer(int answerId, int userId)
        {
            throw new NotImplementedException();
        }

        public RankingHistory GetQuestionRankingHistory(int questionId)
        {
            using (var db = new qnhdb())
            {
                IQueryable<question_rankings> questionRanking = db.question_rankings.Where(ranking => ranking.question_id == questionId);
                return Converter.toExtRankingHistory(questionRanking);
            }
        }

        public RankingHistory GetAnswerRankingHistory(int answerId)
        {
            using (var db = new qnhdb())
            {
                IQueryable<answer_rankings> answerRankings = db.answer_rankings.Where(ranking => ranking.answer_id == answerId);
                return Converter.toExtRankingHistory(answerRankings);
            }
        }

        public void MarkAsRightAnswer(int answerId)
        {
            using (var db = new qnhdb())
            {
                answer answer = db.answers.Single(a => a.id == answerId);
                question question = db.questions.Single(q => q.id == answer.question_id);
                question.right_answer_id = answerId;
                db.SaveChanges();
            }
        }

        public void UnmarkAsRightAnswer(int answerId)
        {
            using (var db = new qnhdb())
            {
                answer answer = db.answers.Single(a => a.id == answerId);
                question question = db.questions.Single(q => q.id == answer.question_id);
                question.right_answer_id = null;
                db.SaveChanges();
            }
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
