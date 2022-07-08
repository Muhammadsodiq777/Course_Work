using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CreateHotelDTO
    {
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Range(1,5)]
        public double Rating { get; set; }

        public long CountryId { get; set; }
    }

    public class UpdateHotelDTO : CreateHotelDTO
    {

    }
    public class HotelDTO: CreateHotelDTO
    {
        public long Id { get; set; }

        public CountryDTO Country { get; set; }
    }

}
