using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjetImage
{
    /// <summary>
    /// Creation d'image 
    /// </summary>
    public class Creation
    {
        private MyImage UneImage;

        private Complexe UneFractale { get; set; } = new Complexe(0, 0);

        private string fichier;

        public Creation(string file)
        {
            this.UneImage = new MyImage(file);
            this.fichier = file;

        }

        /// <summary>
        /// Constructeur nul dans le cas ou on souhaite creer une image a partir de rien
        /// </summary>
        public Creation()
        {
            this.UneImage = new MyImage();
        }

        /// <summary>
        /// Creatin d'une fractale de MandelBrot
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        public void FractaleMandelBrot(string file)
        {
            Pixel[,] Fractal = new Pixel[700, 700];

            for (int i = 0; i < Fractal.GetLength(0); i++)
            {
                for (int j = 0; j < Fractal.GetLength(1); j++)
                {
                    Fractal[i, j] = new Pixel(255, 255, 255);

                    double a = (double)(i - Fractal.GetLength(0) / 2) / (double)(Fractal.GetLength(0) / 4);
                    double b = (double)(j - Fractal.GetLength(1) / 2) / (double)(Fractal.GetLength(1) / 4);

                    int iterations = 0;
                    double teinte, saturation, valeur;


                    Complexe c = new Complexe(b, a);

                    Complexe z = new Complexe(0, 0);

                    while (iterations < 100)
                    {
                        iterations++;
                        z.Produit(z);
                        z.Somme(c);

                        if (z.Norme() > 2.0) break;
                    }

                    teinte = 50;

                    // On utilise le HSV pour donner un rendu couleur sympa


                    valeur = (iterations* iterations / 100.0) + 0.3;
                    saturation = 1 - iterations / 100.0;

                    if (valeur > 1) valeur = 1;

                    if (z.Norme() <= 2.0)
                    {
                        teinte = 180 + z.Norme() * 100.0;

                        valeur = 0.5;
                        saturation = 1;

                    }

                    Fractal[i, j].HSVtoRGB(teinte, saturation, valeur);


                }
            }


            UneImage.ModifiedImage(Fractal, file);

        }

        /// <summary>
        /// Creation d'une fractale quelconque en modifiant la valeur de l'équation initiale
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        /// <param name="nb"> la fractale demandée selon le catalogue demandée</param>
        public void FractaleQuelconque(string file, int nb)
        {
            Pixel[,] Fractal = new Pixel[1000, 1000];

            FractalesCommunes(nb);

            Complexe z = new Complexe(UneFractale.Reel, UneFractale.Imaginaire);

            for (int i = 0; i < Fractal.GetLength(0); i++)
            {
                for (int j = 0; j < Fractal.GetLength(1); j++)
                {
                    Fractal[i, j] = new Pixel(255, 255, 255);

                    double a = (double)(i - Fractal.GetLength(0) / 2) / (double)(Fractal.GetLength(0) / 4);
                    double b = (double)(j - Fractal.GetLength(1) / 2) / (double)(Fractal.GetLength(1) / 4);

                    int iterations = 0;
                    double teinte, saturation, valeur;



                    Complexe c = new Complexe(b, a);


                    while (iterations < 100)
                    {
                        iterations++;
                        c.Produit(c);
                        c.Somme(z);

                        if (c.Norme() > 2.0) break;
                    }

                    teinte = 50;
                    valeur = (iterations* iterations / 100.0) + 0.3;
                    saturation = 1 - iterations / 100.0;

                    if (valeur > 1) valeur = 1;

                    // On utilise le HSV pour donner un rendu couleur sympa

                    if (c.Norme() <= 2.0)
                    {
                        teinte = 180 + c.Norme() * 100.0;

                        valeur = 0.5;
                        saturation = 1;

                    }

                    Fractal[i, j].HSVtoRGB(teinte, saturation, valeur);
                }
            }


            UneImage.ModifiedImage(Fractal, file);
        }

        /// <summary>
        /// Catalogue des fractales communes
        /// </summary>
        /// <param name="nb"> la fractale voulu </param>
        public void FractalesCommunes(int nb)
        {
            switch (nb)
            {
                case 1:  // La basilique
                    UneFractale.Reel = -1;
                    UneFractale.Imaginaire = 0;

                    break;

                case 2:  // L'avion
                    UneFractale.Reel = -1.7548;
                    UneFractale.Imaginaire = 0;

                    break;

                case 3:  // Le lapin
                    UneFractale.Reel = 0.28;
                    UneFractale.Imaginaire = 0.53;

                    break;

                case 4:  // Le lapin triple
                    UneFractale.Reel = -0.122;
                    UneFractale.Imaginaire = 0.744;

                    break;

                case 5:  // spirale
                    UneFractale.Reel = 0.3;
                    UneFractale.Imaginaire = 0.5;

                    break;

                case 6:  // grand avion
                    UneFractale.Reel = -1.310;
                    UneFractale.Imaginaire = 0;

                    break;

                case 7:  // Chou fleur
                    UneFractale.Reel = 0.25;
                    UneFractale.Imaginaire = 0;

                    break;

                case 8:  // Fleur
                    UneFractale.Reel = 0.5 * Math.Cos(Math.PI / 3.0) - 0.25 * Math.Cos(((Math.PI / 3.0) * 2));
                    UneFractale.Imaginaire = 0.5 * Math.Sin(Math.PI / 3.0) - 0.25 * Math.Sin(((Math.PI / 3.0) * 2));

                    break;

                case 9:  // San Marco
                    UneFractale.Reel = -0.75;
                    UneFractale.Imaginaire = 0;

                    break;

                case 10:  // disque de Siegel
                    UneFractale.Reel = -1;
                    UneFractale.Imaginaire = 0.33333;

                    break;

                case 11:  // lemnicastde de Bernoulli
                    UneFractale.Reel = -0.7777;
                    UneFractale.Imaginaire = 0.1111;
                    break;

                case 12:  // la dendrite
                    UneFractale.Reel = 0;
                    UneFractale.Imaginaire = 1;
                    break;

                case 13:  //  la cote
                    UneFractale.Reel = -0.101;
                    UneFractale.Imaginaire = 0.956;

                    break;

                case 14:  //  poussiere de Fatou n1
                    UneFractale.Reel = -0.63;
                    UneFractale.Imaginaire = 0.67;

                    break;

                case 15:  //  poussiere de Fatou n2
                    UneFractale.Reel = 0.35;
                    UneFractale.Imaginaire = 0.05;

                    break;

                case 16:  //  poussiere de Fatou n3
                    UneFractale.Reel = -0.76;
                    UneFractale.Imaginaire = 0.12;

                    break;

                case 17:  //  galaxy
                    UneFractale.Reel = -0.71954;
                    UneFractale.Imaginaire = 0.2134;

                    break;


                default:
                    UneFractale.Reel = Convert.ToDouble(Console.ReadLine());
                    UneFractale.Imaginaire = Convert.ToDouble(Console.ReadLine());

                    break;







            }
        }

        /// <summary>
        ///  Autre type de fractale 2D 
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        public void TapisDeSierpinski(string file)
        {
            Pixel[,] Tapis = new Pixel[1000, 1000];

            for (int i = 0; i < Tapis.GetLength(0); i++)
            {
                for (int j = 0; j < Tapis.GetLength(1); j++)
                {
                    if (Sierpinski((int)(i / (1.39)), (int)(j / (1.37))) == 0) Tapis[i, j] = new Pixel(0, 0, 0);

                    else Tapis[i, j] = new Pixel(255, 255, 25);
                }
            }

            UneImage.ModifiedImage(Tapis, file);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int Sierpinski(int x, int y)
        {
            while (x > 0 || y > 0) // when either of these reaches zero the pixel is determined to be on the edge 
                                   // at that square level and must be filled
            {
                if (x % 3 == 1 && y % 3 == 1)
                {               //checks if the pixel is in the center for the current square level
                    return 0;
                }

                x /= 3; //x and y are decremented to check the next larger square level
                y /= 3;
            }
            return 1; // if all possible square levels are checked and the pixel is not determined 
                      // to be open it must be filled
        }

        /// <summary>
        /// Création d'un histogramme Noir et Blanc de l'image en paramètre
        /// </summary>
        /// <param name="file"></param>
        public void HistogrammeClassique(string file)
        {
            Pixel[,] Image = this.UneImage.MesPixels;

            Pixel[,] Histo = new Pixel[700, 900];

            int[] value = new int[256];

            int max = 0;
            int limite = (Histo.GetLength(1) - 256 * 3) / 2;

            for (int i = 0; i < value.Length; i++)
            {
                value[i] = 0;
            }


            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(1); j++)
                {
                    double moyenne = (Image[i, j].Red + Image[i, j].Green + Image[i, j].Blue) / 3;

                    value[(int)moyenne]++;

                }
            }


            for (int i = 0; i < value.Length; i++)
            {
                if (max < value[i])
                {
                    max = value[i];
                }

                //Console.WriteLine(i + " : " + value[i]);
            }


            for (int j = 0; j < Histo.GetLength(1); j++)
            {
                for (int i = 0; i < Histo.GetLength(0); i++)
                {
                    Histo[i, j] = new Pixel(255, 255, 255);

                    if (j == limite) Histo[i, j] = new Pixel(0, 0, 0);



                }
            }


            for (int j = limite; j < limite + 768; j++)
            {
                int compteur = Histo.GetLength(0) - 1;

                double pixelMax = (Histo.GetLength(0)) * 1.0 / (max * 1.0);

                double pixelRempli = (pixelMax * value[(int)(j - limite) / 3]) * 1.0;

                // Console.WriteLine(limite);

                while (pixelRempli > 0)
                {
                    Histo[compteur, j] = new Pixel((j - limite) / 3, (j - limite) / 3, (j - limite) / 3);

                    if (compteur == Histo.GetLength(0) - 1) Histo[compteur, j] = new Pixel(0, 0, 0);



                    pixelRempli--;
                    compteur--;
                }

            }


            UneImage.ModifiedImage(Histo, file);

        }

        /// <summary>
        /// Création d'un histogramme rouge vert ou bleu de l'image passée en paramètre
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        /// <param name="rgb"> choix de l'histogramme </param>
        public void HistogrammeRGB(string file, string rgb)
        {
            Pixel[,] Image = this.UneImage.MesPixels;

            Pixel[,] Histo = new Pixel[700, 900];

            int[] value = new int[256];
            int max = 0;
            int limite = (Histo.GetLength(1) - 256 * 3) / 2;
            int curseur = 0;
            string coul = "";

            for (int i = 0; i < value.Length; i++)
            {
                value[i] = 0;
            }


            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(1); j++)
                {
                    double couleur = 0;

                    if (rgb == "R") { couleur = Image[i, j].Red; coul = "rouge"; }

                    if (rgb == "G") { couleur = Image[i, j].Green; coul = "vert"; }

                    if (rgb == "B") { couleur = Image[i, j].Blue; coul = "bleu"; }




                    value[(int)couleur]++;

                }
            }


            for (int i = 0; i < value.Length; i++)
            {
                if (max < value[i])
                {
                    max = value[i];
                    curseur = i;
                }

                //Console.WriteLine(i + " : " + value[i]);
            }



            for (int j = 0; j < Histo.GetLength(1); j++)
            {
                for (int i = 0; i < Histo.GetLength(0); i++)
                {
                    double pixelMax = (max * 1.0) / (Histo.GetLength(0)) * 1.0;
                    double pixel = pixelMax * j;

                    //  Console.WriteLine(pixelMax);

                    Histo[i, j] = new Pixel(255, 255, 255);

                    if (j == limite) Histo[i, j] = new Pixel(0, 0, 0);

                    if (j == limite && (pixel % 100 == 0)) Histo[i, j - 1] = new Pixel(0, 0, 0);





                }
            }


            for (int j = limite; j < limite + 768; j++)
            {
                int compteur = Histo.GetLength(0) - 1;

                double pixelMax = (Histo.GetLength(0)) * 1.0 / (max * 1.0);

                double pixelRempli = (pixelMax * value[(int)(j - limite) / 3]) * 1.0;



                while (pixelRempli > 0)
                {
                    if (rgb == "R") Histo[compteur, j] = new Pixel((j - limite) / 3, 0, 0);

                    if (rgb == "G") Histo[compteur, j] = new Pixel(0, (j - limite) / 3, 0);

                    if (rgb == "B") Histo[compteur, j] = new Pixel(0, 0, (j - limite) / 3); ;



                    if (compteur == Histo.GetLength(0) - 1) Histo[compteur, j] = new Pixel(0, 0, 0);



                    pixelRempli--;
                    compteur--;
                }

                if (value[(j - limite) / 3] == 0) Histo[compteur, j] = new Pixel(0, 0, 0);

            }



            Console.WriteLine("le pic maximal est atteint en la valeur " + curseur + " de " + coul + " avec " + max + " pixels");

            UneImage.ModifiedImage(Histo, file);
        }

        /// <summary>
        /// Autre type d'histogramme basée sur le HSV 
        /// </summary>
        /// <param name="file"></param>
        public void HistogrammeTeinte(string file)
        {
            Pixel[,] Image = this.UneImage.MesPixels;

            Pixel[,] Histo = new Pixel[700, 900];

            int[] value = new int[361];

            int max = 0;
            int limite = (Histo.GetLength(1) - 361 * 2) / 2;
            int curseur = 0;

            for (int i = 0; i < value.Length; i++)
            {
                value[i] = 0;
            }


            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(1); j++)
                {
                    double teinte = Image[i, j].RGBtoHSV()[0];

                    //  if(teinte>70)  Console.WriteLine(teinte);

                    value[(int)teinte]++;

                }
            }


            for (int i = 0; i < value.Length; i++)
            {
                if (max < value[i])
                {
                    max = value[i];
                    curseur = i;
                }

                //Console.WriteLine(i + " : " + value[i]);
            }


            for (int j = 0; j < Histo.GetLength(1); j++)
            {
                for (int i = 0; i < Histo.GetLength(0); i++)
                {
                    Histo[i, j] = new Pixel(255, 255, 255);

                    if (j == limite) Histo[i, j] = new Pixel(0, 0, 0);



                }
            }


            for (int j = limite; j < limite + 361 * 2; j++)
            {
                int compteur = Histo.GetLength(0) - 1;

                double pixelMax = (Histo.GetLength(0)) * 1.0 / (max * 1.0);

                double pixelRempli = (pixelMax * value[(int)(j - limite) / 2]) * 1.0;

                // Console.WriteLine(limite);

                while (pixelRempli > 0)
                {
                    // Histo[compteur, j] = new Pixel((j - limite) / 3, (j - limite) / 3, (j - limite) / 3);

                    Histo[compteur, j].HSVtoRGB((j - limite) / 2, 1, 1);

                    if (compteur == Histo.GetLength(0) - 1) Histo[compteur, j] = new Pixel(0, 0, 0);



                    pixelRempli--;
                    compteur--;
                }

                if (value[(j - limite) / 2] == 0) Histo[compteur, j] = new Pixel(0, 0, 0);

            }

            Console.WriteLine("le pic maximal est atteint en la valeur " + curseur + " avec " + max + " pixels");

            UneImage.ModifiedImage(Histo, file);
        }

        /// <summary>
        /// Cache une image sur une autre 
        /// </summary>
        /// <param name="imageCachee"></param>
        /// <param name="file"></param>
        public void CoderImage(string imageCachee, string file)
        {

            MyImage ImageCachee = new MyImage(imageCachee);
            Pixel[,] Image = this.UneImage.MesPixels;
            Pixel[,] HiddenImage = ImageCachee.MesPixels;

            Pixel[,] NewImage = new Pixel[Image.GetLength(0), Image.GetLength(1)];

            int debutligne = Image.GetLength(0) / 2 - HiddenImage.GetLength(0) / 2;
            int finligne = debutligne + HiddenImage.GetLength(0);

            int debutcolonne = Image.GetLength(1) / 2 - HiddenImage.GetLength(1) / 2;
            int fincolonne = debutcolonne + HiddenImage.GetLength(1);

            int ligne = 0;
            int colonne = 0;

            for (int i = 0; i < NewImage.GetLength(0); i++)
            {
                for (int j = 0; j < NewImage.GetLength(1); j++)
                {
                    NewImage[i, j] = new Pixel(0, 0, 0);

                    if (i >= debutligne && i < finligne && j >= debutcolonne && j < fincolonne)
                    {
                        if (colonne == HiddenImage.GetLength(1))
                        {
                            colonne = 0;
                            ligne++;
                        }

                        int newRed = (HiddenImage[ligne, colonne].Red >> 4) | (Image[i, j].Red & 240);  // on utilise le decalage de bits et les operateurs & et | pour modifier la valeur des 4 premiers et derniers bits
                        int newGreen = (HiddenImage[ligne, colonne].Green >> 4) | (Image[i, j].Green & 240);
                        int newBlue = (HiddenImage[ligne, colonne].Blue >> 4) | (Image[i, j].Blue & 240);

                        NewImage[i, j] = new Pixel(newRed, newGreen, newBlue);

                        colonne++;

                    }

                    else
                    {
                        NewImage[i, j] = Image[i, j];
                    }
                }
            }

            UneImage.ModifiedImage(NewImage, file);


        }

        /// <summary>
        /// Retrouve les images d'un image deja codée
        /// </summary>
        /// <param name="file"> le nom du fichier crée pour l'image 1</param>
        /// <param name="file2"> le nom du fichier crée pour l'image 2</param>
        public void DecoderImage(string file, string file2)
        {
            Pixel[,] ImageADecoder = this.UneImage.MesPixels;

            Pixel[,] ImageDecodee1 = new Pixel[ImageADecoder.GetLength(0), ImageADecoder.GetLength(1)];

            Pixel[,] ImageDecodee2 = new Pixel[ImageADecoder.GetLength(0), ImageADecoder.GetLength(1)];

            for (int i = 0; i < ImageDecodee1.GetLength(0); i++)
            {
                for (int j = 0; j < ImageDecodee1.GetLength(1); j++)
                {
                    // on utilise le decalage de bits et les operateurs & et | pour modifier la valeur des 4 premiers et derniers bits

                    int red1 = ImageADecoder[i, j].Red & 240;
                    int green1 = ImageADecoder[i, j].Green & 240;
                    int blue1 = ImageADecoder[i, j].Blue & 240;

                    int red2 = (ImageADecoder[i, j].Red & 15) << 4;
                    int green2 = (ImageADecoder[i, j].Green & 15) << 4;
                    int blue2 = (ImageADecoder[i, j].Blue & 15) << 4;

                    ImageDecodee1[i, j] = new Pixel(red1, green1, blue1);
                    ImageDecodee2[i, j] = new Pixel(red2, green2, blue2);


                }
            }

            UneImage.ModifiedImage(ImageDecodee1, file);
            UneImage.ModifiedImage(ImageDecodee2, file2);

        }

        /// <summary>
        /// Creation personnelle melangeant des effets existants et d'autre nouveaux en utilisant l'aleatoire (loi normale,..)
        /// </summary>
        /// <param name="file"></param>
        public void CreationPersonnelle(string file)
        {
            Pixel[,] Image = this.UneImage.MesPixels;
            Pixel[,] GlitchedImage = new Pixel[Image.GetLength(0), Image.GetLength(1)];
            double pivot = 1;

            /* 1ere partie ou on decoupe aleatoirement l'image en N morceaux de 10 a 30 morceaux puis on decale aleatoirement encore l'image de N pixels pour chaque decoupage via une loi normale
            Ce decoupage permet de creer de couper l'image en  "bandes" qui se decalent aleatoirement plus ou  moins  a droite ou a gauche
            Toutes ces donnees sont stockées sous forme de teableau
            * 
            */

            int morceauxGlitches = getrandom.Next(10, 30); // On coupe l'image verticalement l'image horizontalement entre 10 et 30 morceaux
            int[] ImageCoupee = new int[morceauxGlitches];
            int somme = 0;




            while (somme != 100)
            {
                somme = 0;

                for (int i = 0; i < ImageCoupee.Length; i++)
                {
                    int longueurCoupee = getrandom.Next((100 / ImageCoupee.Length) / 3, (100 / ImageCoupee.Length) * 3);  // On definit  la longueur de chaque morceaux coupées pour que leur somme vaut la hauteur de l'image
                    ImageCoupee[i] = longueurCoupee;  // Encore une fois de maniere aleatoire

                    somme += longueurCoupee;

                }

            }

            somme = 0;

            for (int i = 0; i < ImageCoupee.Length; i++)
            {
                ImageCoupee[i] = (int)((ImageCoupee[i] / 100.0) * GlitchedImage.GetLength(0));

                somme += ImageCoupee[i];

            }


            int[] decalageMorceauxCoupes = new int[ImageCoupee.Length];   /* Ici on choisi aleatoirement le decalage de chaque morceaux selon la valeur de la probabilité normale
            
             Si elle est negative, l'image se decale a gauche, a droite sinon
             */

            for (int i = 0; i < decalageMorceauxCoupes.Length; i++)
            {
                decalageMorceauxCoupes[i] = (int)(ProbabiliteNormal(0.1, 0) * GlitchedImage.GetLength(1));
            }

            double luminosite = 0;
            int index = 0;
            int compteur = 0;
            int part = ImageCoupee[0];
            int alea = 87;





            // Ici on commence a remplir notre image

            for (int i = 0; i < GlitchedImage.GetLength(0); i++)
            {

                if (i > part)
                {
                    compteur++;
                    alea = getrandom.Next(0, 100);
                    if (compteur >= ImageCoupee.Length) compteur = ImageCoupee.Length - 1;  // on applique les decalages de chaque morceaux
                    part += ImageCoupee[compteur];


                }

                if (decalageMorceauxCoupes[compteur] > 0)
                {

                    index = GlitchedImage.GetLength(1) - decalageMorceauxCoupes[compteur];

                }

                else index = Math.Abs(decalageMorceauxCoupes[compteur]);




                for (int j = 0; j < GlitchedImage.GetLength(1); j++)
                {
                    GlitchedImage[i, j] = new Pixel(Image[i, j].Red, Image[i, j].Green, Image[i, j].Blue);

                    if (index >= GlitchedImage.GetLength(1) || index < 0) index = 0;

                    GlitchedImage[i, j].NewPixel(Image[i, index]);

                    index++;

                    // Ici on creer un effet de decalage de l'image sur elle meme (comme un fondu sur elle meme ) pour creer un effet double de l'image

                    GlitchedImage[i, j].Red = (int)((Image[i, j].Red + GlitchedImage[i, j].Red * 1.5) / 2.5);
                    GlitchedImage[i, j].Green = (int)((Image[i, j].Green + GlitchedImage[i, j].Green * 1.5) / 2.5);
                    GlitchedImage[i, j].Blue = (int)((Image[i, j].Blue + GlitchedImage[i, j].Blue * 1.4) / 2.5);


                    GlitchedImage[i, j].CreaAlea(alea);  // voir la méthode Pixel (utlise un effet random (noir et blanc, negatif,...)) 

                    if (((i + 1) / 4) % 2 == 0)  // ici on creer les bandes noires de l'image
                    {
                        luminosite = ProbabiliteNormal(0.1, 0.75);

                        while (Math.Abs(pivot - luminosite) > 0.2)
                        {
                            luminosite = ProbabiliteNormal(0.15, 0.85);
                        }

                        pivot = luminosite;
                        GlitchedImage[i, j].Eclaircissement(Math.Abs(luminosite));

                    }

                    else
                    {
                        luminosite = ProbabiliteNormal(0.04, 1);
                        GlitchedImage[i, j].Eclaircissement(Math.Abs(luminosite));

                    }


                }
            }

            UneImage.ModifiedImage(ImageOndulee(GlitchedImage), file);   // Enfin On ondule l'image.

        }

        /// <summary>
        /// Fonction utilisé dans la creation pour ondulée l'image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Pixel[,] ImageOndulee(Pixel[,] image)
        {
            Pixel[,] NewImage = new Pixel[image.GetLength(0), image.GetLength(1)];

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    NewImage[i, j] = new Pixel(0, 0, 0);
                    NewImage[i, j].NewPixel(image[i, j]);
                }
            }

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    int hauteur = (int)(j + 10 * Math.Sin(i * 1.0 / 30.0));

                    if (hauteur >= 0 && hauteur < image.GetLength(1))
                        NewImage[i, hauteur] = image[i, j];
                }
            }

            return NewImage;

        }

        /// <summary>
        ///  Effet fondu de 2 images
        /// </summary>
        /// <param name="image"></param>
        /// <param name="image2"></param>
        /// <param name="file"></param>
        public void ImagesFondu(Pixel[,] image, Pixel[,] image2, string file)
        {
            Pixel[,] NewImage = new Pixel[image.GetLength(0), image.GetLength(1)];

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    NewImage[i, j] = new Pixel(0, 0, 0);
                    NewImage[i, j].NewPixel(image[i, j]);

                }
            }

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {


                    NewImage[i, j].Red = (i * image2[i, j].Red + (image.GetLength(0) - i) * image[i, j].Red) / image.GetLength(0);
                    NewImage[i, j].Green = (i * image2[i, j].Green + (image.GetLength(0) - i) * image[i, j].Green) / image.GetLength(0);
                    NewImage[i, j].Blue = (i * image2[i, j].Blue + (image.GetLength(0) - i) * image[i, j].Blue) / image.GetLength(0);

                }
            }
            UneImage.ModifiedImage(NewImage, file);

        }

        /// <summary>
        ///  methode random
        /// </summary>
        private static readonly Random getrandom = new Random();

        /// <summary>
        ///  methode loi normale (approximation)
        /// </summary>
        /// <param name="ecartType"> ecart type de la loi </param>
        /// <param name="esperance">esperance</param>
        /// <returns> le double de la loi </returns>
        public double ProbabiliteNormal(double ecartType, double esperance)
        {

            lock (getrandom)
            {
                double u1 = 1.0 - getrandom.NextDouble();
                double u2 = 1.0 - getrandom.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                             Math.Sin(2.0 * Math.PI * u2);
                double randNormal = esperance + ecartType * randStdNormal;

                return randNormal;
            }

        }

    }
}
