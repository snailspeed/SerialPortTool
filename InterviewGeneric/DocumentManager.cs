using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewGeneric
{
    public class DocumentManager<TDocument> where TDocument : IDocument
    {
        private Queue<TDocument> documentQueue = new Queue<TDocument>();

        public void AddDocument(TDocument doc)
        {
            documentQueue.Enqueue(doc);
        }

        public void DisplayAllDocuments()
        {
            foreach (TDocument doc in documentQueue)
            {
                Console.WriteLine(doc.Title);
            }
        }
    }
}
