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
                //foreach(var tag in 
                db.SaveChanges();
                return Converter.toExtQuestion(intQuestion, question.Author, new RankingHistory(), question.Tags);
            }
        }

        public void DeleteQuestion(int questionId)
        {
            using (var db = new qnhdb())
            {
                //remove rankings
                foreach (var ranking in db.question_rankings.Where(r => r.question_id == questionId))
                {
                    //change user's rank based on ranking of question
                    var que = db.questions.Where(x => x.id == ranking.question_id).Single();
                    user rankee = db.users.Single(x => x.id == que.author_id);
                    rankee.ranking = rankee.ranking + (ranking.rank == 0 ? 1 : -1);
                    db.question_rankings.Remove(ranking);
                }

                //remove answer ranking and answers
                var answers = db.answers.Where(x => x.question_id == questionId);
                foreach (var answer in answers)
                {
                    foreach (var ranking in db.answer_rankings.Where(x => x.answer_id == answer.id))
                    {
                        //change user's rank based on ranking of answer
                        var ans = db.answers.Where(x => x.id == ranking.answer_id).Single();
                        user rankee = db.users.Single(x => x.id == ans.author_id);
                        rankee.ranking = rankee.ranking + (ranking.rank == 0 ? 1 : -1);
                        db.answer_rankings.Remove(ranking);
                    }
                    db.answers.Remove(answer);
                }

                //remove tags? nope. tags are remove by the entity framework for us. this is buggy!
                foreach (var tag in db.tags.Where(x => x.question_id == questionId))
                {
                    db.tags.Remove(tag);
                }
                //remove question
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
                    //get the question id
                    var ans = db.answers.Where(x => x.id == ranking.answer_id).Single();
                    user rankee = db.users.Single(x => x.id == ans.author_id);
                    rankee.ranking = rankee.ranking + (ranking.rank == 0 ? 1 : -1);
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


                foreach (question question in db.questions.Where(q => q.title.Contains(searchString) || q.contents.Contains(searchString)
                    || db.users.Any(x => x.id == q.author_id && x.name.Contains(searchString))))
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
                foreach (tag intTag in db.tags.Where(t => t.tag1 == tag))
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
                            GetQuestionRankingHistory(q.id),
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
            using (var db = new qnhdb())
            {
                var oldrankings = db.question_rankings.SingleOrDefault(x => x.author_id == userId && x.question_id == questionId);
                if(oldrankings != null)
                    db.question_rankings.Remove(oldrankings);
                db.question_rankings.Add(new question_rankings { author_id = userId, question_id = questionId, rank = Convert.ToByte(true) });
                question ranked_question = db.questions.Single(x => x.id == questionId);
                user author = db.users.First(user => user.id == ranked_question.author_id);
                author.ranking++;
                db.SaveChanges();
            }
        }

        public void RankDownQuestion(int questionId, int userId)
        {
            using (var db = new qnhdb())
            {
                var oldrankings = db.question_rankings.SingleOrDefault(x => x.author_id == userId && x.question_id == questionId);
                if (oldrankings != null)
                    db.question_rankings.Remove(oldrankings); 
                db.question_rankings.Add(new question_rankings { author_id = userId, question_id = questionId, rank = Convert.ToByte(false) });
                question ranked_question = db.questions.Single(x => x.id == questionId);
                user author = db.users.First(user => user.id == ranked_question.author_id);
                author.ranking--;
                db.SaveChanges();
            }
        }

        public void RankUpAnswer(int answerId, int userId)
        {
            using (var db = new qnhdb())
            {
                var oldrankings = db.answer_rankings.SingleOrDefault(x => x.author_id == userId && x.answer_id== answerId);
                if (oldrankings != null)
                    db.answer_rankings.Remove(oldrankings); 
                db.answer_rankings.Add(new answer_rankings { author_id = userId, answer_id = answerId, rank = Convert.ToByte(true) });
                answer ranked_answer = db.answers.Single(x => x.id == answerId);
                user author = db.users.First(user => user.id == ranked_answer.author_id);
                author.ranking++;
                db.SaveChanges();
            }
        }

        public void RankDownAnswer(int answerId, int userId)
        {
            using (var db = new qnhdb())
            {
                var oldrankings = db.answer_rankings.SingleOrDefault(x => x.author_id == userId && x.answer_id == answerId);
                if (oldrankings != null)
                    db.answer_rankings.Remove(oldrankings); 
                db.answer_rankings.Add(new answer_rankings { author_id = userId, answer_id = answerId, rank = Convert.ToByte(false) });
                answer ranked_answer = db.answers.Single(x => x.id == answerId);
                user author = db.users.First(user => user.id == ranked_answer.author_id);
                author.ranking--;
                db.SaveChanges();
            }
        }

        public void CancelQuestionRank(int questionId, int userId)
        {
            using (var db = new qnhdb())
            {
                question_rankings ranking = db.question_rankings.Single(qr => qr.author_id == userId && qr.question_id == questionId);
                question ranked_question = db.questions.Single(x => x.id == questionId);
                user author = db.users.First(user => user.id == ranked_question.author_id);
                if (Convert.ToBoolean(ranking.rank))
                {
                    author.ranking--;
                }
                else
                {
                    author.ranking++;
                }

                db.question_rankings.Remove(ranking);
                db.SaveChanges();
            }
        }

        public void CancelAnswerRanking(int answerId, int userId)
        {
            using (var db = new qnhdb())
            {
                answer_rankings ranking = db.answer_rankings.Single(ar => ar.author_id == userId && ar.answer_id == answerId);
                answer ranked_answer = db.answers.Single(x => x.id == answerId);
                user author = db.users.First(user => user.id == ranked_answer.author_id);
                if (Convert.ToBoolean(ranking.rank))
                {
                    author.ranking--;
                }
                else
                {
                    author.ranking++;
                }

                db.answer_rankings.Remove(ranking);
                db.SaveChanges();
            }
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
