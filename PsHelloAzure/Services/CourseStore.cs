using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using PsHelloAzure.Models;

namespace PsHelloAzure.Services
{
    public class CourseStore
    {
        private DocumentClient client;
        private Uri coursesLink;

        public CourseStore()
        {
            var uri = new Uri("https://pshelloazure25.documents.azure.com:443/");
            var key = "48RqRi5a6h7zqQNrFQgfWNZsymgml2NfAHMpfBpCyalArozvr2TfbUN2Ab1NYwWglAKgFGNHWQsicabdJVW0FQ==";
            client = new DocumentClient(uri, key);
            coursesLink = UriFactory.CreateDocumentCollectionUri("pshelloazure", "courses");
        }

        public async Task InsertCourses(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                await client.CreateDocumentAsync(coursesLink, course);
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            var courses = client.CreateDocumentQuery<Course>(coursesLink,
                new FeedOptions { EnableCrossPartitionQuery = true })
                                .OrderBy(c => c.Title);

            return courses;
        }
    }
}
