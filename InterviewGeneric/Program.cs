using System;

namespace InterviewGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            var dm = new DocumentManager<IDocument>();
            dm.AddDocument(new Document("Title A", "Sample A"));
            dm.AddDocument(new Document("Title B", "Sample B"));

            dm.DisplayAllDocuments();

            Console.ReadLine();
        }
    }
}
