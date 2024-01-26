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

        public void AddGameToCart(Cart cart, Games game)
        {
      
            database.Insert(cart.User);

   
            var cartGame = new CartGames { CartId = cart.Id, GameId = game.Id };
            database.Insert(cartGame);
        }

        public List<Games> GetGamesInCart(Cart cart)
        {
            var cartGameIds = database.GetAll<CartGames>()
                                      .Where(cg => cg.CartId == cart.Id)
                                      .Select(cg => cg.GameId)
                                      .ToList();

            var gamesInCart = database.GetAll<Games>()
                                      .Where(game => cartGameIds.Contains(game.Id))
                                      .ToList();

            return gamesInCart;
        }

        public void RemoveGameFromCart(Cart cart, Games game)
        {


            var cartGame = database.GetAll<CartGames>()
                           .FirstOrDefault(cg => cg.CartId == cart.Id && cg.GameId == game.Id);

            if (cartGame != null)
            {
                database.Delete(cartGame);
            }
        }

    }




}
