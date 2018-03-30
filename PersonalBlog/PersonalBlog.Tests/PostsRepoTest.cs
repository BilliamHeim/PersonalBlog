using NUnit.Framework;
using PersonalBlog.Data;
using PersonalBlog.Models.Reponses;
using PersonalBlog.Models.Tables;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace PersonalBlog.Tests
{
    [TestFixture]
    public class PostsRepoTest
    {
        PostsRepo repo = new PostsRepo();

        [SetUp]
        public void Setup()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DbReset";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        [TearDown]
        public void Teardown()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DbReset";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestCase("Self Driving Cars Wanna Kill Us All", 2, true)]
        [TestCase("See Here's the Thing with Jews", 3, true)]
        [TestCase("Biscuit", 0, false)]
        public void GetPostById(string title, int postId, bool expected)
        {
            PostsResponse response = repo.GetById(postId);

            Assert.AreEqual(expected, response.Success);
            if (expected == true)
            {
                Assert.AreEqual(title, response.Posts.First().PostTitle);
            }
        }

        [TestCase(3, 5, true)]
        [TestCase(1, 7, true)]
        [TestCase(0, 1, false)]
        [TestCase(0, 0, false)]
        public void GetPostByTag(int postCount, int tagId, bool expected)
        {
            PostsResponse response = repo.GetByTag(tagId);

            Assert.AreEqual(expected, response.Success);
            if (expected == true)
            {
                Assert.AreEqual(postCount, response.Posts.Count());
            }
        }

        [TestCase(1, false, true)]
        [TestCase(3, true, true)]
        public void GetPostByApproval(int postCount, bool isApproved, bool expected)
        {
            PostsResponse response = repo.GetByApproval(isApproved);

            Assert.AreEqual(expected, response.Success);
            if (expected == true)
            {
                Assert.AreEqual(postCount, response.Posts.Count());
            }
        }

        [TestCase("Self Driving", 2, true)]
        [TestCase("jews", 3, true)]
        [TestCase("Biscuit", 0, false)]
        [TestCase("", 0, false)]
        [TestCase(null, 0, false)]
        public void GetPostByTitle(string title, int postId, bool expected)
        {
            PostsResponse response = repo.GetByTitle(title);

            Assert.AreEqual(expected, response.Success);
            if (expected == true)
            {
                Assert.AreEqual(postId, response.Posts.First().PostId);
            }
        }

        [TestCase(1, 1, true)]
        [TestCase(5, 1, true)]
        [TestCase(7, 0, false)]
        public void GetPostByCat(int catId, int postCount, bool expected)
        {
            PostsResponse response = repo.GetByCategory(catId);

            Assert.AreEqual(expected, response.Success);
            if (expected == true)
            {
                Assert.AreEqual(postCount, response.Posts.Count());
            }
        }

        [Test]
        public void AddPost()
        {
            Post test = repo.GetByCategory(1).Posts.First();
            test.PostId = 0;
            test.PostTitle = "HurrMurmuhFur";

            PostsResponse response = repo.Add(test);

            Assert.AreEqual(true, response.Success);
        }

        [Test]
        public void EditPost()
        {
            Post test = repo.GetByCategory(1).Posts.First();
            test.PostTitle = "HurrMurmuhFur";

            PostsResponse response = repo.Edit(test);
            Post edited = repo.GetByCategory(1).Posts.First();

            Assert.AreEqual(true, response.Success);
            Assert.AreEqual("HurrMurmuhFur", edited.PostTitle);
        }

        public void DeletePost()
        {
            PostsResponse response = repo.Delete(1);
            PostsResponse actual = repo.GetById(1);
        }
    }
}
