namespace INTEXAPP2.Models.ViewModels
{
    public class BurialViewModel
    {
        public IQueryable<Burialmain> Burials { get; set; }
        public PageDetails PageDetails { get; set; }
    }
}
