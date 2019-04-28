using System;
using App.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Data.Test
{
    [TestClass]
    public class ArtistTXLocalDapperDAUnitTest
    {
        [TestMethod]
        public void Count()
        {
            var da = new ArtistTXLocalDapperDA();

            Assert.IsTrue(da.GetCount()>0);
        }

        [TestMethod]
        public void GetAll()
        {
            var da = new ArtistTXLocalDapperDA();
            var listado = da.GetAll();
            Assert.IsTrue(listado.Count > 0);
        }

        [TestMethod]
        public void Get()
        {
            var da = new ArtistTXLocalDapperDA();
            var entity = da.Get(10);
            Assert.IsTrue(entity.ArtistId > 0);
        }

        [TestMethod]
        public void GetAllSP()
        {
            var da = new ArtistTXLocalDapperDA();
            var listado = da.GetAllSP("Aerosmith");
            Assert.IsTrue(listado.Count > 0);
        }

        [TestMethod]
        public void InsertSP()
        {
            var da = new ArtistTXLocalDapperDA();
            var artist = new Artist();
            artist.Name = "Sebastian";
            var id = da.Insert(artist);
            Assert.IsTrue(id > 0,"Nombre del artista ya existe");
        }


        [TestMethod]
        public void UpdateSP()
        {
            var da = new ArtistTXLocalDapperDA();
            var artist = new Artist
            {
                Name = "eduardo",
                ArtistId = 278
            };
           
            var registrosAfectados = da.Update(artist);
            Assert.IsTrue(registrosAfectados > 0, "Nombre del artista ya existe");
        }

        [TestMethod]
        public void DeleteSP()
        {
            var da = new ArtistTXLocalDapperDA();
           /* var artist = new Artist
            {
                ArtistId = 276
            };*/

            var registrosAfectados = da.DeleteSp(278);
            Assert.IsTrue(registrosAfectados > 0, "Artista no fue eliminado");
        }
    }
}
