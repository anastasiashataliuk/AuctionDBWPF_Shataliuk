using AutoMapper;
using DAL.Concrete;
using DAL.Interface;
using DALEF.Context;
using DALEF.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DALEF.Concrete
{
    public class AuctionDalEf : IAuctionDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public AuctionDalEf(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public List<Auction> GetAll()
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                return _mapper.Map<List<Auction>>(context.Auction.ToList());
            }
        }

        public Auction GetById(int id)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblAuction = context.Auction.FirstOrDefault(a => a.Auction_Id == id);
                return _mapper.Map<Auction>(tblAuction);


            }
        }

        public Auction Insert(Auction auction)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblAuction = _mapper.Map<TblAuction>(auction);
                context.Auction.Add(tblAuction);
                context.SaveChanges();

                auction.Auction_Id = tblAuction.Auction_Id; // Присвоюємо ID новому аукціону
                return auction;
            }
        }

        public void Update(Auction auction)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblAuction = _mapper.Map<TblAuction>(auction);
                context.Auction.Update(tblAuction);
                context.SaveChanges();

            }


        }

        public void Delete(int id)
        {
            using (var context = new AuctiondbContext(_connectionString))
            {
                var tblAuction = context.Auction.Find(id);
                if (tblAuction != null)
                {
                    context.Auction.Remove(tblAuction);
                    context.SaveChanges();
                }
            }
        }

    }
}
