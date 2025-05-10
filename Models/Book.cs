
namespace SiemensBookAPI.Models
{
    public class Book
    { 
        public int Bid { get; set; }
        public string Title { get; set; }="";
        public string Author { get; set; }="";
        public string Publisher { get; set; }="";
        public int NumberGiven { get; set; }
        public int NumberTotal { get; set; }
        
    }
}
