using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjetImage
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLittleEndianToEntier()
        {
            MyImage uneImage = new MyImage();
            byte[] endian = { 26, 5, 2, 0 };
            int entier = uneImage.Convertir_Endian_To_Int(endian);
            Assert.AreEqual(132378, entier);

        }  // Test si le tableau de byte en little endian passé en paramètre retourne la bonne valeur d'entier
           // Sert pour recuper les datas de l'image

        [TestMethod]
        public void DecimalToLittleEndian()
        {


            MyImage uneIMage = new MyImage();
            byte[] endian = uneIMage.Convertir_Int_To_Endian(1543);
            byte[] result = { 7, 6, 0, 0 };


            for (int i = 0; i < endian.Length; i++)
            {
                Assert.AreEqual(result[i], endian[i]);
            }

        }  // Test si l'entier passé en paramètre retourne la bonne valeur de byte en little endian
           // Sert pour ecrire les datas de l'image

        [TestMethod]
        public void RGBToHSV()
        {
            Pixel unPixel = new Pixel(123, 12, 234);
            double[] rgb = unPixel.RGBtoHSV();
            double[] result = { 270, 0.948717948717949, 0.917647058823529 };


            for (int i = 0; i < rgb.Length; i++)
            {
                Assert.AreEqual(Math.Round(rgb[i]), Math.Round(result[i]));
            }


        } // Test si la conversion RGB to HSV est correcte

        [TestMethod]
        public void TestLoiNormal()
        {
            Creation maCreation = new Creation();
            double alea = maCreation.ProbabiliteNormal(0.1, 0);
            Assert.IsTrue(alea < 1);
            Assert.IsTrue(alea > -1);



        }  // Test si la loi normale fonctionne : si les valeurs ont une très faible dispersion si on diminue l'ecart type (esperance centrée en 0) 
                                         // Probabilité de 1/10000 d'echouer

        [TestMethod]
        public void TestCalculComplex()
        {
            Complexe complexe = new Complexe(1.2, -3.5);
            Complexe complexe2 = new Complexe(-2.1, 9.6);
            double norme = complexe.Norme();

            double resultNorme = 3.7;
            Assert.AreEqual(Math.Round(norme), Math.Round(resultNorme));

            complexe.Produit(complexe2);
            double reel = 31.08;
            double imaginaire = 18.87;



            Assert.AreEqual(Math.Round(complexe.Reel), Math.Round(reel));
            Assert.AreEqual(Math.Round(complexe.Imaginaire), Math.Round(imaginaire));




        } // Test si les operations basiques d'un nombre complexe fonctionnent




    }
}
