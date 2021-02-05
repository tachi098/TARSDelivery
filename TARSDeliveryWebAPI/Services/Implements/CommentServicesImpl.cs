using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;
using TARSDeliveryWebAPI.Repositories;
using TARSDeliveryWebAPI.Services.Interfaces;

namespace TARSDeliveryWebAPI.Services.Implements
{
    public class CommentServicesImpl : ICommentServices
    {
        private readonly ApplicationContext context;
        public CommentServicesImpl(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Comment>> comments()
        {
            return await context.GetComments.Where(m => m.Delete_at == null).ToListAsync();
        }

        public async Task<bool> Create(Comment comment)
        {
            comment.Create_at = DateTime.Now;
            context.GetComments.Add(comment);
            var created = await context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteComment(int id)
        {
            var model = await context.GetComments.FindAsync(id);

            model.Delete_at = DateTime.Now;

            context.GetComments.Update(model);
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Comment> GetComment(int id)
        {
            return await context.GetComments.SingleOrDefaultAsync(m => m.Id.Equals(id));
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await context.GetComments.ToListAsync();
        }
    }
}
