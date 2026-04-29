using System.Text.Json;

namespace MiniStore.Web.Models;

public static class CartSessionHelper
{
    private const string Key = "Cart";

    public static List<CartItem> GetCart(ISession session)
    {
        var json = session.GetString(Key);
        return json is null ? [] : JsonSerializer.Deserialize<List<CartItem>>(json) ?? [];
    }

    public static void SaveCart(ISession session, List<CartItem> cart)
    {
        session.SetString(Key, JsonSerializer.Serialize(cart));
    }

    public static void AddItem(ISession session, CartItem item)
    {
        var cart = GetCart(session);
        var existing = cart.FirstOrDefault(c => c.ProductId == item.ProductId);
        if (existing is not null)
            existing.Quantity += item.Quantity;
        else
            cart.Add(item);
        SaveCart(session, cart);
    }

    public static void UpdateQuantity(ISession session, int productId, int quantity)
    {
        var cart = GetCart(session);
        var item = cart.FirstOrDefault(c => c.ProductId == productId);
        if (item is not null)
        {
            if (quantity <= 0)
                cart.Remove(item);
            else
                item.Quantity = quantity;
        }
        SaveCart(session, cart);
    }

    public static void RemoveItem(ISession session, int productId)
    {
        var cart = GetCart(session);
        cart.RemoveAll(c => c.ProductId == productId);
        SaveCart(session, cart);
    }

    public static void Clear(ISession session) => session.Remove(Key);
}
