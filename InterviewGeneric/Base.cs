using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewGeneric
{
    public class Base<T>
    {
    }

    class Derived<T> : Base<int> where T : class, new()
    {

    }
}
