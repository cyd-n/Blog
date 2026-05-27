using BackEnd.Models;
using BackEnd.Requests;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services {

    public interface IArticleService {
        Task<Artical?> GetById(int _id);
        Task<Artical> Create(ArticalRequest _req);
    }
}