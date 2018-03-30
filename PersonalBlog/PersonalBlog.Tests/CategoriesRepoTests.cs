using NUnit.Framework;
using PersonalBlog.Data;
using PersonalBlog.Models.Reponses;
using PersonalBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace PersonalBlog.Tests
{
    [TestFixture]
    public class CategoriesRepoTests
    {
        CategoriesRepo repo = new CategoriesRepo();

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
        public void GetAllCats()
        {
            CategoryResponse response = repo.GetAll();
            Category actual = response.Categories.Where(c => c.CategoryId == 1).First();

            Assert.AreEqual(true, response.Success);
            Assert.AreEqual("Tech", actual.CategoryName);
        }

        [TestCase("Tech", 1, true)]
        [TestCase("", 0, false)]
        public void GetCatByName(string expected, int id, bool valid)
        {
            CategoryResponse response = repo.GetById(id);

            Assert.AreEqual(valid, response.Success);
            if (valid == true)
            {
                Category actual = response.Categories.First();
                Assert.AreEqual(expected, actual.CategoryName);
            }
        }

        [Test]
        public void AddCat()
        {
            Category junkCat = repo.GetAll().Categories.First();
            junkCat.CategoryId = 0;
            junkCat.CategoryName="Pr0n";

            CategoryResponse response = repo.Add(junkCat);
            Assert.AreEqual(true, response.Success);
        }

        [Test]
        public void EditCat()
        {
            Category junkCat = repo.GetAll().Categories.First();
            junkCat.CategoryName = "Pr0n";

            CategoryResponse response = repo.Add(junkCat);
            Category actual = repo.GetById(junkCat.CategoryId).Categories.First();

            Assert.AreEqual(true, response.Success);
            Assert.AreEqual("Pr0n", actual.CategoryName);
        }

        [Test]
        public void DeleteCat()
        {
            CategoryResponse response = repo.Delete(1);
            CategoryResponse actual = repo.GetById(1);

            Assert.AreEqual(true, response.Success);
            Assert.AreEqual(false, actual.Success);
        }
    }
}
