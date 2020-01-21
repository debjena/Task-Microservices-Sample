using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubTask_Api
{
    public class Link
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string method { get; set; }
    }

    public class LinkHelper<T> where T : class
    {
        public T _value { get; set; }
        public T _schema { get; set; }
        public List<Link> _links { get; set; }

        public LinkHelper()
        {
            _links = new List<Link>();
        }

        public LinkHelper(T item) : base()
        {
            _value = item;
            _links = new List<Link>();
            _schema = Activator.CreateInstance<T>();
        }
    }
}
