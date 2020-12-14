using System.Collections.Generic;

namespace ClientNamespace
{
    public class History<T>
    {
        public List<Call<T>> historyOfCalls = new List<Call<T>>();
    }
}
