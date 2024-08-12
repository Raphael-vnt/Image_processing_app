using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetImage
{
    /// <summary>
    /// Classe Pixel 
    /// </summary>
    public class Pixel
    {

        private int red;
        private int green;
        private int blue;

        public int Red
        {
            get { return this.red; }
            set { this.red = value; }
        }

        public int Green
        {
            get { return this.green; }
            set { this.green = value; }
        }

        public int Blue
        {
            get { return this.blue; }
            set { this.blue = value; }
        }


        public Pixel(int rouge, int vert, int bleu)
        {
            this.red = rouge;
            this.green = vert;
            this.blue = bleu;

        }

        /// <summary>
        /// Passage d'un pixel en gris via moyennation des valeurs RGB
        /// </summary>
        public void PixelGris()
        {
            double gris = (this.red + this.green + this.blue) / 3;

            this.red = (int)gris;
            this.green = (int)gris;
            this.blue = (int)gris;

        }

        /// <summary>
        /// Changement de la luminosité du pixel en multipliant les valeurs RGB par un coefficient
        /// </summary>
        /// <param name="nombre"> le coeff compris entre 0 et 1 </param>
        public void Eclaircissement(double nombre)
        {
            this.red = (int)(this.red * nombre);
            this.blue = (int)(this.blue * nombre);
            this.green = (int)(this.green * nombre);


            if (this.red > 255) this.red = 255;
            if (this.blue > 255) this.blue = 255;
            if (this.green > 255) this.green = 255;

        }

        /// <summary>
        /// copie d'un Pixel
        /// </summary>
        /// <param name="unPixel"> le pixel a copié </param>
        public void NewPixel(Pixel unPixel)
        {
            this.red = unPixel.red;
            this.green = unPixel.green;
            this.blue = unPixel.blue;
        }

        /// <summary>
        /// Modification du pixel via tableau de pixels avec des coefficients (moyennation)
        /// </summary>
        /// <param name="desPixels"> le tableau de pixel</param>
        /// <param name="coefficients"> les coeffs associés </param>
        public void CoeffNouveauPixel(Pixel[] desPixels, double[] coefficients)
        {
            double resultRed = 0;
            double resultGreen = 0;
            double resultBlue = 0;

            double diviseur = 0;

            for (int i = 0; i < desPixels.Length; i++)
            {
                resultRed += desPixels[i].red * coefficients[i];
                resultGreen += desPixels[i].green * coefficients[i];
                resultBlue += desPixels[i].blue * coefficients[i];

                diviseur += coefficients[i];

            }

            this.red = (int)(resultRed / diviseur);
            this.green = (int)(resultGreen / diviseur);
            this.blue = (int)(resultBlue / diviseur);
        }

        /// <summary>
        /// Modification du pixel via matrice de pixels avec des coefficients (moyennation)
        /// </summary>
        /// <param name="desPixels"> la matrice de Pixel</param>
        /// <param name="test"> les coeffs associés (int)</param>
        public void MatricePixel(Pixel[,] desPixels, double[,] test)
        {


            // Console.WriteLine(desPixels.GetLength(0));

            int resultRed = 0;
            int resultGreen = 0;
            int resultBlue = 0;

            for (int i = 0; i < desPixels.GetLength(0); i++)
            {
                for (int j = 0; j < desPixels.GetLength(1); j++)
                {
                    resultRed = (int)(resultRed + desPixels[i, j].red * test[i, j]); 
                    resultGreen = (int)(resultGreen + desPixels[i, j].green * test[i, j]);
                    resultBlue = (int)(resultBlue + desPixels[i, j].blue * test[i, j]);


                    //  Console.Write(" ["+desPixels[i,j].red+ " ; "+ desPixels[i, j].green+ " ; " + desPixels[i, j].blue+"] " );

                }

                // Console.WriteLine();
            }

            if (resultRed < 0) resultRed = 0;
            if (resultGreen < 0) resultGreen = 0;
            if (resultBlue < 0) resultBlue = 0;

            if (resultRed > 255) resultRed = 255;
            if (resultGreen > 255) resultGreen = 255;
            if (resultBlue > 255) resultBlue = 255;


            this.red = resultRed;
            this.green = resultGreen;
            this.blue = resultBlue;

            //Console.WriteLine("resultat : "+ resultRed+" ; "+ resultGreen+" ; "+ resultBlue);

        }

        /// <summary>
        /// passage du TSV (teinte/saturation/Valeur en RGB ) en RGB . Il sagit d'un système de gestion des couleurs alternatif au RGB 
        /// </summary>
        /// <param name="h"> teinte (de 0 à 360 degres) </param>
        /// <param name="S"> saturation (de 0 a 1) </param>
        /// <param name="V"> valeur (de 0 à 1) </param>
        public void HSVtoRGB(double h, double S, double V)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // couleur dominante rouge

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // couleur dominante vert

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // couleur dominante bleue

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // couleur dominante rouge

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // plusieurs couleurs dominante

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // autres cas

                    default:
                        
                        R = G = B = V; 
                        break;
                }
            }

            this.red = (int)(R * 255);
            this.green = (int)(G * 255);
            this.blue = (int)(B * 255);

            //  Console.WriteLine(this.red + "; " + this.green + "; " + this.blue + "; ");


        }

        /// <summary>
        /// retour des valeurs de RGB en TSV
        /// </summary>
        /// <returns> un tableau contenant successivement les valeurs T,S,V</returns>
        public double[] RGBtoHSV()
        {


            double max;
            double min;

            if (this.green >= this.red && this.green >= this.blue)
            {
                max = this.green;
            }

            else if (this.red >= this.green && this.red >= this.blue)
            {
                max = this.red;
            }

            else max = this.blue;


            if (this.red <= this.blue && this.red <= this.green)
            {
                min = this.red;
            }

            else if (this.blue <= this.green && this.blue <= this.red)
            {
                min = this.blue;
            }

            else min = this.green;

            // Console.WriteLine("min :" + min + "max : " + max);

            double valeur = 1.0 * max / 255;
            double saturation;
            double teinte;

            if (max == 0) saturation = 0;

            else saturation = 1 - (min * 1.0) / (max * 1.0);


            if (max == min)
            {
                teinte = 0;
            }

            else if (max == this.red)
            {
                teinte = 60 * ((this.green - this.blue) / (max - min));

                if (teinte < 0) teinte += 360;
            }

            else if (max == this.green)
            {
                teinte = 60 * ((this.blue - this.red) / (max - min)) + 120;
            }

            else
            {
                teinte = 60 * ((this.red - this.green) / (max - min)) + 240;
            }

            double[] tsv = { teinte, saturation, valeur };

            return tsv;
        }

        /// <summary>
        ///  Rend le pixel negatif a sa valeur initiale
        /// </summary>
        public void PixelInverse()
        {
            this.red = 255 - this.red;
            this.blue = 255 - this.blue;
            this.green = 255 - this.green;

        }

        /// <summary>
        ///  Ici on applique un traitement aleatoire du pixel, (couleurs, NetB, negatif...)
        /// </summary>
        /// <param name="nb"></param>
        public void CreaAlea(int nb)
        {



            if (nb > 80) { this.PixelGris(); }

            if (nb == 66) { this.PixelInverse(); }

            if (nb <= 80 && nb > 75) { this.Red = (int)(this.red * 1.2); this.Blue = (int)(this.blue * 1.2); }

            if (nb > 70 && nb <= 75) { this.Red = (int)(this.red * 1.2); this.Green = (int)(this.green * 1.2); }

            if (nb > 65 && nb <= 70) { this.Blue = (int)(this.blue * 1.2); this.Green = (int)(this.green * 1.2); }

            if (nb > 60 && nb <= 65) { this.Blue = (int)(this.blue * 1.2); }

            if (nb > 55 && nb <= 60) { this.Green = (int)(this.green * 1.2); }

            if (nb > 50 && nb <= 55) { this.red = (int)(this.red * 1.2); }




        }
    }
}
