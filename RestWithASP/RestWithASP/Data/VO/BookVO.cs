using RestWithASP.Hypermedia;
using RestWithASP.Hypermedia.Abstract;

namespace RestWithASP.Data.VO
{
    public class BookVO : ISupportsHypermedia
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

        public DateTime Launch_date { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>(); 
    }
}
