using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Data
{
    public class Collection
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(ApiUser))]
        private string UserId { get; set; }   
        public ApiUser ApiUser { get; set; }
    }
}
