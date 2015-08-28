using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QueueAndHi.BL.Authentication;
using DAL;

namespace QueueAndHi.BL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PostQueries" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PostQueries.svc or PostQueries.svc.cs at the Solution Explorer and start debugging.
    public class PostQueries : IPostQueries
    {
        private PostOps postOps = new PostOps();
        private IAuthTokenSerializer authTokenSerializer;

        public PostQueries()
        {
            this.authTokenSerializer = new AuthTokenSerializer();
        }

        public IEnumerable<Question> FreeSearch(string searchString)
        {
            return postOps.FreeSearch(searchString);
        }

        public IEnumerable<Question> TagsSearch(string tag)
        {
            return postOps.TagsSearch(tag);
        }

        public IEnumerable<Question> GetMyQuestions(AuthenticationToken authToken)
        {
            int userid = authTokenSerializer.Deserialize(authToken);
            return postOps.GetQuestionsByUser(userid);
        }

        public DiscussionThread GetDiscussionThreadById(int id)
        {
            DiscussionThread dt = postOps.GetDiscussionThreadById(id);
            dt.Answers = dt.Answers.OrderByDescending(answer => answer.Ranking.OverallRanking).ToList();
            if (dt.Question.RightAnswerId.HasValue)
            {
                Answer rightAnswer = dt.Answers.Single(ans => ans.ID == dt.Question.RightAnswerId.Value);
                dt.Answers.Remove(rightAnswer);
                dt.Answers.Insert(0, rightAnswer);
            }

            return dt;
        }

        public IEnumerable<Question> GetLatestQuestions()
        {
            return postOps.GetLatestQuestions();
        }
    }
}
