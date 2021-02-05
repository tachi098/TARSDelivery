using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;

namespace TARSDeliveryWebAPI.Services.Interfaces
{
    public interface ICommentServices
    {
        Task<IEnumerable<Comment>> comments();

        Task<Comment> GetComment(int id);

        Task<IEnumerable<Comment>> GetComments();

        Task<bool> Create(Comment comment);

        Task<bool> DeleteComment(int id);
    }
}
