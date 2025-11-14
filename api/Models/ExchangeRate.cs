using System.Collections.Generic;

namespace api.Models
{
    public class ExchangeRate
    {
        public string Name { get; set; }
        public List<Currency> Currencies { get; set; }
    }
}