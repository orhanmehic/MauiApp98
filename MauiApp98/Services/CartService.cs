using MauiApp98.Data;
using MauiApp98.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiApp98.Services
{
    internal class CartService
    {

        private readonly SqliteData database;

        public CartService(SqliteData database)
        {
            this.database = database;
        }

        public void AddGameToCart(int UserId, Games game)
        {

            Cart cart = new Cart();
            cart.UserId = UserId;
            cart.GameId = game.Id;

            database.Insert<Cart>(cart);

            
        }

        public List<Games> GetGamesInCart(int UserId)
        {
            var gameIdsInCart = database.GetAll<Cart>()
                                .Where(cart => cart.UserId == UserId)
                                .Select(cart => cart.GameId)
                                .ToList();

            var gamesInCart = database.GetAll<Games>()
                                      .Where(game => gameIdsInCart.Contains(game.Id))
                                      .ToList();

            return gamesInCart;


        }

        public void RemoveGameFromCart(int UserId, Games game)
        {
            var cartRecordToRemove = database.GetAll<Cart>()
                                     .FirstOrDefault(cart => cart.UserId == UserId && cart.GameId == game.Id);

            if (cartRecordToRemove != null)
            {
                database.Delete(cartRecordToRemove);
            }

        }

        public void EmptyCart(int UserId)
        {
            var cartRecordsToRemove = database.GetAll<Cart>()
                                              .Where(cart => cart.UserId == UserId)
                                              .ToList();

            foreach (var cartRecord in cartRecordsToRemove)
            {
                database.Delete(cartRecord);
            }
        }


    }




}
