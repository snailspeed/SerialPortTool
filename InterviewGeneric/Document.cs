using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewGeneric
{
    public class Document : IDocument
    {
        public Document(string title, string content)
        {
            Title = title;
            Contnet = content;
        }

        public string Title { get; }
        public string Contnet { get; }
    }
}
