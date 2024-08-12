using System;
using System.Collections.Generic;           // VINTELER Louis-Raphael TD D 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProjetImage
{
    /// <summary>
    /// Sans paramètres, n’effectue aucunes actions particulières mis à part le lancement du Forms. Sert aussi d’endroit de phases de tests pour les méthodes.
    /// </summary>
    public class Program
    {
        [STAThread]

        static void Main(string[] args)
        {
            string fichier = "renek.bmp";

            MyImage image = new MyImage(fichier);


            // MyImage image2 = new MyImage("image1.csv");
            // image.FromImageToFile("test.bmp");
            // image.ImageToCSV(image.MesPixels, "fichier.csv");

            // MyImage image2 = new MyImage("abdoul.csv");

            // image.FromImageToFile("test.bmp");

            string fichier2 = "renek.bmp";

            MyImage image2 = new MyImage(fichier2);

            MyImage image3 = new MyImage();

            Traitement traitement = new Traitement(fichier);

            // traitement.ChangementDimensions("test.bmp", 100);


            // traitement.Eclaircissement("test.bmp", 0.5);

            //  traitement.ImageNoirEtBlanc("test.bmp");

            //  traitement.Rotation("test.bmp", -6);

            // traitement.EffetMiroir("test.bmp");

            // traitement.EffetKaleidoscope("test.bmp");

            //  traitement.FiltresImage("test.bmp", 5);


            //  Creation crea = new Creation(fichier);


            Creation crea2 = new Creation();

            // crea2.FractaleMandelBrot("test.bmp");

            // crea.FractaleQuelconque("test.bmp", 2);

            // crea.TapisDeSierpinski("test.bmp");


            // crea.HistogrammeClassique("test.bmp");

            // crea.HistogrammeRGB("test.bmp", "G");

            // crea.HistogrammeTeinte("test.bmp");


            // crea.CoderImage("lac.bmp", "test.bmp");

            // crea.DecoderImage("Imager1.bmp", "Image2.bmp");

            //   crea.CreationPersonnelle("test.bmp");

            // crea.ImageOndulee(image.MesPixels, "test2.bmp");

            //  crea.Test("test.bmp");

            //  crea.Image3D(image.MesPixels, "test.bmp");

            //  Process.Start("test.bmp");


            Console.WriteLine("process terminé");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Interface());




            Console.ReadKey();


        }
    }
}
