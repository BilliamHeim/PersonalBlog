using NUnit.Framework;
using PersonalBlog.Data;
using PersonalBlog.Models.Reponses;
using PersonalBlog.Models.Tables;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace PersonalBlog.Tests
{
    [TestFixture]
    public class TagRepoTests
    {
        TagsRepo repo = new TagsRepo();

        [SetUp]
        public void Setup()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.DbReset";

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
                cmd.CommandText = "dbo.DbReset";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void GetAll()
        {
            TagsResponse response = repo.GetAll();
            Tag actual = response.Tags.Where(t => t.TagId == 3).First();

            Assert.AreEqual(true, response.Success);
            Assert.AreEqual("#Neature", actual.TagName);
        }

        [TestCase("#Lit", 9, true)]
        [TestCase("#CmonMan", 6, true)]
        [TestCase(null, 0, false)]
        [TestCase("", 0, false)]
        [TestCase("TofurkeyDinner", 0, false)]
        public void GetByName(string name, int expected, bool valid)
        {
            TagsResponse response = repo.GetByName(name);

            Assert.AreEqual(valid, response.Success);
            if (valid == true)
            {
                Tag actual = response.Tags.First();
                Assert.AreEqual(expected, actual.TagId);
            }
        }

        [TestCase("#Lit", 9, true)]
        [TestCase("#CmonMan", 6, true)]
        [TestCase(null, 0, false)]
        [TestCase("", 0, false)]
        [TestCase("TofurkeyDinner", 9000, false)]
        public void GetById(string expected, int id, bool valid)
        {
            TagsResponse response = repo.GetById(id);

            Assert.AreEqual(valid, response.Success);
            if (valid == true)
            {
                Tag actual = response.Tags.First();
                Assert.AreEqual(expected, actual.TagName);
            }
        }

        [Test]
        public void Add()
        {
            PostsRepo postGet = new PostsRepo();
            List<Post> junkPost = new List<Post>();
            junkPost.Add(postGet.GetAll().Posts.FirstOrDefault());
            Tag toAdd = new Tag { TagName = "#Testerized", Posts = junkPost };
            TagsResponse response = repo.Add(toAdd);

            Assert.AreEqual(true, response.Success);
        }

        [Test]
        public void Edit()
        {
            PostsRepo postGet = new PostsRepo();
            List<Post> junkPost = new List<Post>();
            junkPost.Add(postGet.GetAll().Posts.FirstOrDefault());
            Tag toAdd = new Tag { TagName = "#Testerized", Posts = junkPost };
            repo.Add(toAdd);

            Tag toEdit = repo.GetByName("#Testerized")
                .Tags.First();
            toEdit.TagName = "#MrTsTea";
            TagsResponse response=repo.Edit(toEdit);

            Assert.AreEqual(true, response.Success);
        }

        [Test]
        public void Delete()
        {
            TagsResponse response=repo.Delete(1);
            TagsResponse actual = repo.GetById(1);

            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(false, actual.Success);
        }
    }
}
