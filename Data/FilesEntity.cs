using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Data
{
    public class FilesEntity
    {
        public long Id { get; set; }
        public string OriginalName { get; set; }
        public string FileURL { get; set; }
        public long? Size { get; set; }
        
        [ForeignKey(nameof(Collection))]
        public long CollectionId { get; set; }

        public Collection Collection { get; set; }

    }
}
