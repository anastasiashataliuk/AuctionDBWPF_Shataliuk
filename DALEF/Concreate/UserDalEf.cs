using AutoMapper;
using DAL.Interface;
using DALEF.Context;
using DALEF.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DALEF.Concrete
{
    public class UserDalEf : IUserDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public UserDalEf(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public List<User> GetAll()
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                return _mapper.Map<List<User>>(context.Users.ToList());
            }
        }

        public User GetById(int id)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblUser = context.Users.FirstOrDefault(u => u.User_Id == id);
                return _mapper.Map<User>(tblUser);
            }
        }

        public User Insert(User user)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblUser = _mapper.Map<TblUser>(user);
                context.Users.Add(tblUser);
                context.SaveChanges();

                user.User_Id = tblUser.User_Id; // Assign the generated ID
                return user;
            }
        }

        public void Update(User user)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblUser = _mapper.Map<TblUser>(user);
                context.Users.Update(tblUser);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblUser = context.Users.Find(id);
                if (tblUser != null)
                {
                    context.Users.Remove(tblUser);
                    context.SaveChanges();
                }
            }
        }
    }
}
