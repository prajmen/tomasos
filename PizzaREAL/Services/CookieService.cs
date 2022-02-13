using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaREAL.Models;

namespace PizzaREAL.Services
{
    public interface ICookieService
    {
        //Order AddToOrder(int id);
    }
    public class CookieService : ICookieService
    {
        //public Order AddToOrder(int id)
        //{
        //    Order order;
        //    string cartJSON;

        //    if (HttpContext.Session.GetString("cart") == null)
        //    {
        //        products = new List<Product>();
        //    }
        //    else
        //    {
        //        cartJSON = HttpContext.Session.GetString("cart");


        //        //Konverterar JSON från session till en lista 
        //        products = JsonConvert.DeserializeObject<List<Product>>(cartJSON);

        //    }

        //    var product = ProductList().SingleOrDefault(p => p.ID == id);
        //    products.Add(product);

        //    cartJSON = JsonConvert.SerializeObject(products);

        //    HttpContext.Session.SetString("cart", cartJSON);
        //}

        
    }
}
