using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetImage
{
    /// <summary>
    /// Classe traitant la lecture et ecriture d'image ( en .CSV et .BMP) 
    /// </summary>
    public class MyImage
    {
        /// <summary>
        /// L'image composé de matrice de Pixel
        /// </summary>
        public Pixel[,] MesPixels { get; set; }

        string emplacementFichier;

        private string typeImage;
        private int tailleFichier;
        private int tailleOffset;
        private int hauteur;
        private int largeur;
        private int nbBitsCouleurs = 24;

        /// <summary>
        /// On lit l'image(bmp ou csv) puis recupère ses données pour compléter nos attributs(MesPixels, taille, largeur, hauteur...)
        /// </summary>
        /// <param name="file"> </param>
        public MyImage(string file)
        {

            this.emplacementFichier = file;

            byte[] data;

            try
            {

                if (file[file.Length - 1] == 'v' && file[file.Length - 2] == 's' && file[file.Length - 3] == 'c')
                {
                    data = DataFromCSV();
                }


                else
                {
                    FileInfo fileInfo = new FileInfo(file);

                    data = new byte[fileInfo.Length];

                    using (FileStream fs = fileInfo.OpenRead())
                    {
                        fs.Read(data, 0, data.Length);
                        fs.Close();

                    }

                }




                int compteur = 54;

                string s = null;
                s += (char)data[0];
                s += (char)data[1];
                this.typeImage = s;

                byte[] tabTailleFichier = { data[2], data[3], data[4], data[5] };
                this.tailleFichier = Convertir_Endian_To_Int(tabTailleFichier);

                byte[] tabTailleOffset = { data[10], data[11], data[12], data[13] };
                this.tailleOffset = Convertir_Endian_To_Int(tabTailleOffset);

                byte[] tabLargeur = { data[18], data[19], data[20], data[21] };
                this.largeur = Convertir_Endian_To_Int(tabLargeur);

                byte[] tabHauteur = { data[22], data[23], data[24], data[25] };
                this.hauteur = Convertir_Endian_To_Int(tabHauteur);

                byte[] tabCouleurs = { data[28], data[29] };
                this.nbBitsCouleurs = Convertir_Endian_To_Int(tabCouleurs);

                MesPixels = new Pixel[hauteur, largeur];

                int bits = Exception(MesPixels);

                for (int i = hauteur - 1; i >= 0; i--)
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        this.MesPixels[i, j] = new Pixel(data[compteur + 2], data[compteur + 1], data[compteur]);
                        compteur += 3;
                    }

                    compteur += bits;
                }

            }

            catch
            {

            }


        }

        /// <summary>
        ///  Constructeur nul dans le cas ou on souhaite creer une image a partir de rien, dans ce cas pas besoin d'utiliser les attributs
        /// </summary>
        public MyImage()
        {

        }



        /// <summary>
        /// On lit le fichier CSV et stocke ses données en tableau de byte
        /// </summary>
        /// <returns> le tableau de byte </returns>
        public byte[] DataFromCSV()
        {


            StreamReader sr = new StreamReader(this.emplacementFichier);

            char ch = (char)sr.Read();
            int compteurx = 0;


            List<string> listA = new List<string>();


            while (!sr.EndOfStream)
            {

                string num = null;

                while (ch != ';' && sr.EndOfStream == false && (int)ch != 13)
                {
                    num += ch;
                    ch = (char)sr.Read();

                }

                while (ch == ';')
                {

                    ch = (char)sr.Read();

                }

                if ((int)ch == 13)
                {
                    ch = (char)sr.Read();

                    // Console.WriteLine();

                    if ((int)ch == 10) ch = (char)sr.Read();
                }

                if (num != null) { listA.Add(num); compteurx++; }
                // Console.Write("[" + (int)listA[compteurx][0] + "] ");



            }

            sr.Close();

            byte[] data = new byte[listA.Count];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)Int32.Parse(listA[i]);
                // Console.Write(data[i] + " ");
            }

            return data;
        }

        /// <summary>
        /// Conversion d'un tableau de byte sous forme de little endian , en entier
        /// </summary>
        /// <param name="tab"> le tableau de byte (ici sur 2 ou 4 octets)</param>
        /// <returns> l'entier converti </returns>
        public int Convertir_Endian_To_Int(byte[] tab)
        {
            int entier = 0;

            for (int i = 0; i < tab.Length; i++)
            {
                entier |= tab[i] << (i * 8);
            }
            return entier;
        }

        /// <summary>
        /// Operation inverse : conversion d'un entier en un tableau de byte sous forme de little endian
        /// </summary>
        /// <param name="val"> l'entier à convertir</param>
        /// <returns> le tableau de byte </returns>
        public byte[] Convertir_Int_To_Endian(int val)
        {

            byte[] tab = new byte[4];

            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = (byte)(val >> i * 8);
            }
            return tab;
        }

        /// <summary>
        /// Transformation en fichier binaire de l'image (sous forme de bmp) puis création d'un nouveau fichier 
        /// </summary>
        /// <param name="file"> le fichier crée </param>
        public void FromImageToFile(string file)
        {
            if (File.Exists(file)) File.Delete(file);

            byte[] data;


            if (this.emplacementFichier[this.emplacementFichier.Length - 1] == 'v' && this.emplacementFichier[this.emplacementFichier.Length - 2] == 's' && this.emplacementFichier[this.emplacementFichier.Length - 3] == 'c')
            {
                data = DataFromCSV();
                File.WriteAllBytes(file, data);

            }

            else
            {
                FileInfo fileInfo = new FileInfo(this.emplacementFichier);
                data = new byte[fileInfo.Length];

                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                    fs.Close();

                }

                using (FileStream fs = File.Create(file))
                {
                    fs.Write(data, 0, data.Length);
                    fs.Close();

                }
            }

        }

        /// <summary>
        /// gestion des images dont le nombre total d'octets n'est pas un multiple de 4 (ajout de 0 pour respecter cette condition)
        /// </summary>
        /// <param name="Image"> l'image passée en paramètre </param>
        /// <returns>le nombre de bits a rajouter a la fin de chaque ligne </returns>
        public int Exception(Pixel[,] Image)
        {
            int nbDeBitsAjoute = 0;

            while ((Image.GetLength(1) * 3 + nbDeBitsAjoute) % 4 != 0)
            {
                nbDeBitsAjoute++;
            }

            return nbDeBitsAjoute;
        }

        /// <summary>
        /// Recupere l'image passé en paramètre et la transforme en instance puis crer un fichier bmp de celle ci 
        /// </summary>
        /// <param name="Image"> l'image en para </param>
        /// <param name="file"> le fichier crée </param>
        public void ModifiedImage(Pixel[,] Image, string file)
        {
            byte[] imageModifie = DataImage(Image);

            File.WriteAllBytes(file, imageModifie);

        }

        /// <summary>
        /// Prends en paramètre une image et creer un fichier CSV associé a cette image
        /// </summary>
        /// <param name="Image"> l'image</param>
        /// <param name="file"> le fichier crée </param>
        public void ImageToCSV(Pixel[,] Image, string file)
        {
            StreamWriter sw = new StreamWriter(file);


            byte[] data = DataImage(Image);

            string[] myData = new string[data.Length];

            string ing = null;

            for (int i = 0; i < myData.Length; i++)
            {

                myData[i] = data[i].ToString() + ";";

                if (i == 20 || i == 54 || i % Image.GetLength(1) == 0)
                {
                    sw.WriteLine();

                }

                sw.Write(myData[i]);

            }

            sw.Close();

        }

        /// <summary>
        /// return le tableau de byte associé à l'image 
        /// </summary>
        /// <param name="Image"> l'image en paramètre </param>
        /// <returns> le tableau de byte </returns>
        public byte[] DataImage(Pixel[,] Image)
        {
            int bits = Exception(Image);
            int taille = 54 + 3 * Image.GetLength(0) * Image.GetLength(1) + bits * Image.GetLength(0);

            //  Console.WriteLine(taille);


            byte[] imageModifie = new byte[taille];

            try
            {
                if (typeImage != null)
                {
                    imageModifie[0] = (byte)typeImage[0];
                    imageModifie[1] = (byte)typeImage[1];
                }
                
            }
            finally
            {
                imageModifie[0] = 66;
                imageModifie[1] = 77;
            }



            for (int i = 2; i < 6; i++)
            {
                imageModifie[i] = Convertir_Int_To_Endian(taille)[i - 2];
            }

            for (int i = 6; i < 18; i++)
            {
                imageModifie[i] = 0;

                if (i == 10) imageModifie[i] = 54;
                if (i == 14) imageModifie[i] = 40;
            }

            for (int i = 18; i < 22; i++)
            {
                imageModifie[i] = Convertir_Int_To_Endian(Image.GetLength(1))[i - 18];
            }

            for (int i = 22; i < 26; i++)
            {
                imageModifie[i] = Convertir_Int_To_Endian(Image.GetLength(0))[i - 22];
            }

            for (int i = 26; i < 34; i++)
            {
                imageModifie[i] = 0;

                if (i == 26) imageModifie[i] = 1;


                if (i == 28) imageModifie[i] = 24;


            }

            for (int i = 34; i < 38; i++)
            {
                imageModifie[i] = Convertir_Int_To_Endian(taille - 54)[i - 34];
            }

            for (int i = 38; i < 54; i++)
            {
                imageModifie[i] = 0;
            }


            int j = Image.GetLength(0) - 1;
            int k = 0;


            /*  for (int i = 0; i < Image.GetLength(0); i++)
              {
                  for (int z = 0; z < Image.GetLength(1); z++)
                  {
                     Console.Write(imageModifie[i] + " "+ i+" " );
                  }

                  Console.WriteLine();
              }*/

            for (int i = 54; i < imageModifie.Length; i += 3)
            {
                if (k == Image.GetLength(1))
                {
                    for (int l = 0; l < bits; l++)
                    {
                        imageModifie[l + i] = 0;

                    }

                    i = i - 3 + bits;
                    k = 0;
                    j--;
                }

                else
                {

                    imageModifie[i] = (byte)Image[j, k].Blue;
                    imageModifie[i + 1] = (byte)Image[j, k].Green;
                    imageModifie[i + 2] = (byte)Image[j, k].Red;

                    k++;
                }

            }

            return imageModifie;
        }

        /// <summary>
        /// affiche les caractéristiques de l'instance de l'image 
        /// </summary>
        public void AfficherMonImage()
        {
            Console.WriteLine("taille : " + tailleFichier);
            Console.WriteLine("hauteur : " + this.hauteur + " ; largeur : " + this.largeur);
            Console.WriteLine("bits ajoutés : " + Exception(MesPixels));


        }




    }
}
