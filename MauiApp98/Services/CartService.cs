using MauiApp98.Data;
using MauiApp98.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var cart = database.GetAll<Cart>()
                               .FirstOrDefault(c => c.UserId == UserId && c.GameId == game.Id);

            if (cart != null)
            {
                database.Delete(cart);
            }
        }

        public void EmptyCart(int UserId)
        {
            var cartsToRemove = database.GetAll<Cart>().Where(c => c.UserId == UserId).ToList();

            foreach (var cart in cartsToRemove)
            {
                database.Delete(cart);
            }
        }
    }




}
