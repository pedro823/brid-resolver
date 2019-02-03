using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IDResolver.Interface
{
    public class Element
    {
        [BindRequired]
        public string Id { set; get; }
        
        [BindRequired]
        public string CallbackUrl { set; get; }

        public bool HasNullValues()
        {
            return Id == null || CallbackUrl == null;
        }
    }
}