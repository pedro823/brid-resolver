using Newtonsoft.Json;

namespace IDResolver.Models
{
    public class BridResolverException 
    {
        public string Message;
        
        public BridResolverException(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}