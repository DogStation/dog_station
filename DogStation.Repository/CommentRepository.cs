using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogStation.Entity.Models;
using System.Data.Entity;
using DogStation.Utils;

namespace DogStation.Repository
{
    public class CommentRepository : IRepository<Comment>
    {
        public readonly RescueDogEntities db = RescueDog.Instance();

        public Comment Add(Comment t)
        {
            db.Entry(t).State = EntityState.Added;
            db.SaveChanges();
            return t;
        }

        public Comment Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Comment Get(long id)
        {
            return db.Comment.Find(id);
        }

        public List<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAll(long idDog, int page)
        {
            return db.Comment
                .Where(c => c.dog == idDog)
                .OrderByDescending(c => c.commentTime)
                .Skip(DefaultUtil.DefaultCommentPageSize * (page - 1))
                .Take(DefaultUtil.DefaultCommentPageSize)
                .ToList();
        }

        public bool Update(Comment t)
        {
            throw new NotImplementedException();
        }
    }
}
