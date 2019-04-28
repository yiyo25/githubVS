using System;
using App.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Data.Test
{
    [TestClass]
    public class ArtistTXDistribuidasDAUnitTest
    {
        [TestMethod]
        public void Count()
        {
            var da = new ArtistTXDistribuidasDA();

            Assert.IsTrue(da.GetCount()>0);
        }

        [TestMethod]
        public void GetAll()
        {
            var da = new ArtistTXDistribuidasDA();
            var listado = da.GetAll();
            Assert.IsTrue(listado.Count > 0);
        }

        [TestMethod]
        public void Get()
        {
            var da = new ArtistTXDistribuidasDA();
            var entity = da.Get(10);
            Assert.IsTrue(entity.ArtistId > 0);
        }

        [TestMethod]
        public void GetAllSP()
        {
            var da = new ArtistTXDistribuidasDA();
            var listado = da.GetAllSP("Aerosmith");
            Assert.IsTrue(listado.Count > 0);
        }

        [TestMethod]
        public void InsertSP()
        {
            var da = new ArtistTXDistribuidasDA();
            var artist = new Artist();
            artist.Name = "edi";
            var id = da.Insert(artist);
            Assert.IsTrue(id > 0,"Nombre del artista ya existe");
        }


        [TestMethod]
        public void UpdateSP()
        {
            var da = new ArtistTXDistribuidasDA();
            var artist = new Artist
            {
                Name = "jhon",
                ArtistId = 278
            };
           
            var registrosAfectados = da.Update(artist);
            Assert.IsTrue(registrosAfectados > 0, "Nombre del artista ya existe");
        }

        [TestMethod]
        public void DeleteSP()
        {
            var da = new ArtistTXDistribuidasDA();
           /* var artist = new Artist
            {
                ArtistId = 276
            };*/

            var registrosAfectados = da.DeleteSp(278);
            Assert.IsTrue(registrosAfectados > 0, "Artista no fue eliminado");
        }
    }
}
