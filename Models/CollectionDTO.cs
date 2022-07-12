using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CreateCollectionDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Collection name is too long")]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        [StringLength(maximumLength: 200, ErrorMessage = "Less then 200 characters")]

        public string Description { get; set; }

    }
    public class CollectionDTO : CreateCollectionDTO
    {
        public long Id {get; set;}
    }

    public class UpdateCollectionDTO : CreateCollectionDTO
    {
    }
    
}
