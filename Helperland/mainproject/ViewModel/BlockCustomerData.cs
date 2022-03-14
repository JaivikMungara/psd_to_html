using mainproject.Models;
using Newtonsoft.Json;

namespace mainproject.ViewModel
{
    public class BlockCustomerData
    {

        public User user { get; set; }

        public FavoriteAndBlocked favoriteAndBlocked { get; set; }
    }
}