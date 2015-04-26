using MVC5Demo.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Demo.Models
{
    public class AuthorRepository
    {
            private static EFEntities _ctx;
            private static EFEntities Ctx
            {
                get { return _ctx ?? (_ctx = new EFEntities()); }
            }

            
            public static IEnumerable<Author> GetAuthors()
            {
                var query = from x in Ctx.Authors select x;
                return query.ToList();
            }

            public static void InsertAuthor(Author author)
            {
                Ctx.Authors.Add(author);
                Ctx.SaveChanges();
            }

            public static void DeleteAuthor(int authorId)
            {
                var deleteItem = Ctx.Authors.FirstOrDefault(c => c.Id == authorId);

                if (deleteItem != null)
                {
                    Ctx.Authors.Remove(deleteItem);
                    Ctx.SaveChanges();
                }
            }
    }
}