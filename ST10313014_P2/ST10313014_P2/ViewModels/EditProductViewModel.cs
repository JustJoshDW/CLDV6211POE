namespace ST10313014_P2.ViewModels
{
    public class EditProductViewModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int CategoryId { get; set; }

        public double Price { get; set; }

        public string ArtistName { get; set; }

        public IFormFile ProductImage { get; set; }

        public string ExistingImagePath { get; set; }
    }
}


