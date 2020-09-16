using System.Collections.Generic;

namespace BOTA.API.GraphQL.Dtos
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }

        //public JObject Variables { get; set; }
        //public IDictionary<string,int> Variables { get; set; }
        //[JsonConverter(typeof(CustomJsonDictionaryConverter))]
        public IDictionary<string,object> Variables { get; set; }
    }
}
