using MyAspNetCore.Web.Models;

namespace MyAspNetCore.Web.Helpers
{
    public class Helper : IHelper
    {
        // private bool _isConfiguration;
        private readonly AppDbContext _context;

        //public Helper(bool isConfiguration)
        //{
        //    _isConfiguration=isConfiguration;   
        //}

        public Helper(AppDbContext context)
        {
            _context = context;
        }
        public string Upper(string text)
        {
            return text.ToUpper();
        }
    }
}
