namespace CnpjLibrary.Test
{
    using CpfCnpjLibrary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CnpjValidarTests
    {
        [TestMethod]
        public void ValidWithMask()
        {
            Assert.IsTrue(Cnpj.Validar("67.419.649/0001-12"));
            Assert.IsTrue(Cnpj.Validar("67419649000112"));
            Assert.IsTrue(Cnpj.Validar("02.944.203/0001-61"));
            Assert.IsTrue(Cnpj.Validar("2.944.203/0001-61"));
            Assert.IsTrue(Cnpj.Validar("02089275000179"));
            Assert.IsTrue(Cnpj.Validar("2089275000179"));
        }

        [TestMethod]
        public void ValidAlphanumeric()
        {
            Assert.IsTrue(Cnpj.Validar("12.ABC.345/01DE-35"));
            Assert.IsTrue(Cnpj.Validar("12abc34501de35"));
            Assert.IsTrue(Cnpj.Validar("Z8.1RG.51Z/0001-41"));
            Assert.IsTrue(Cnpj.Validar("B0.5XL.TB5/0001-80"));
            Assert.IsTrue(Cnpj.Validar("0C.P2R.LHM/0001-69"));
            Assert.IsTrue(Cnpj.Validar("Z8.1RG.51Z/0001-41"));
            Assert.IsTrue(Cnpj.Validar("6L.B1T.GP3/0001-71"));
            Assert.IsTrue(Cnpj.Validar("6W9ATTLB000119"));
            Assert.IsTrue(Cnpj.Validar("PZVJLGY3000166"));
            Assert.IsTrue(Cnpj.Validar("ZAWTX14J000122"));
        }

        [TestMethod]
        public void InvalidAlphanumeric()
        {
            Assert.IsFalse(Cnpj.Validar("12.ABC.345/01DE-36"));
            Assert.IsFalse(Cnpj.Validar("12.ABC.345/01DE-XX"));
            Assert.IsFalse(Cnpj.Validar("AA.AAA.AAA/AAAA-AA"));
        }
    }
}
