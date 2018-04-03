using NUnit.Framework;
using PersonalBlog.Data;
using PersonalBlog.Models.Models;
using PersonalBlog.Models.Reponses;
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
    public class TagsRepoTests
    {
        //RUNNING TESTS WILL DELETE NON-SEED DATA
        //DO NOT RUN POST-PRODUCTION STAGE

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
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DbReset";

                conn.Open();
            }
        }

        TagsRepo _repo = new TagsRepo();

        [Test]
        public void GetAll()
        {
            TagsResponse actual = _repo.GetAll();

            Assert.AreEqual(true,actual.Success);
            Assert.AreEqual("#MeToo",actual.Tags.Where(t=>t.TagId==1));
            Assert.AreEqual("#Neature", actual.Tags.Where(t => t.TagId == 3));
        }

        public void GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Tags tag)
        {
            throw new NotImplementedException();
        }

        public void Edit(Tags tag)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {

        }
    }
}
