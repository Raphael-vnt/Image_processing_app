using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetImage
{
    /// <summary>
    ///  Traitement des images  
    /// </summary>
    public class Traitement
    {

        private MyImage UneImage;


        public Traitement(string file)
        {
            this.UneImage = new MyImage(file);

        }

        /// <summary>
        /// Passage de l'image en Noir en Blanc
        /// </summary>
        /// <param name="file"> le nom du fichier crée</param>
        public void ImageNoirEtBlanc(string file)
        {
            Pixel[,] Image = UneImage.MesPixels;
            Pixel[,] nouvelleImage = new Pixel[Image.GetLength(0), Image.GetLength(1)];

            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(1); j++)
                {
                    nouvelleImage[i, j] = new Pixel(Image[i, j].Red, Image[i, j].Green, Image[i, j].Blue);

                    nouvelleImage[i, j].PixelGris();
                }
            }

            UneImage.ModifiedImage(nouvelleImage, file);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public void Negatif(string file)
        {
            Pixel[,] Image = UneImage.MesPixels;
            Pixel[,] nouvelleImage = new Pixel[Image.GetLength(0), Image.GetLength(1)];

            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(1); j++)
                {
                    nouvelleImage[i, j] = new Pixel(Image[i, j].Red, Image[i, j].Green, Image[i, j].Blue);

                    nouvelleImage[i, j].PixelInverse();
                }
            }

            UneImage.ModifiedImage(nouvelleImage, file);
        }

        /// <summary>
        /// Changement de luminosité de l'image
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        /// <param name="niveau"> coefficient de luminosité </param>
        public void Eclaircissement(string file, double niveau)
        {
            Pixel[,] Image = UneImage.MesPixels;
            Pixel[,] nouvelleImage = new Pixel[Image.GetLength(0), Image.GetLength(1)];

            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(1); j++)
                {
                    nouvelleImage[i, j] = new Pixel(Image[i, j].Red, Image[i, j].Green, Image[i, j].Blue);

                    nouvelleImage[i, j].Eclaircissement(niveau);
                }
            }

            UneImage.ModifiedImage(nouvelleImage, file);
        }

        /// <summary>
        /// Changement de dimensions de l'image
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        /// <param name="changement"> le nombre de pixel a agrandir ou retrecir </param>
        public void ChangementDimensions(string file, int changementx, int changementy)
        {

            // Ici on utilise l'interpolation bilineaire en faisant une moyenne des 8 pixels autour , avec des coeffs selon le facteur d'agrandissment/ retrecissement et le positionnement du nouveau pixel

            Pixel[,] image = UneImage.MesPixels;

            Pixel[,] nouvelleImage = new Pixel[image.GetLength(0) + changementx, image.GetLength(1) + changementy];


            double rapportImageHauteur = image.GetLength(0) * 1.0 / nouvelleImage.GetLength(0) * 1.0;
            double rapportImageLargeur = image.GetLength(1) * 1.0 / nouvelleImage.GetLength(1) * 1.0;

            for (int i = 0; i < nouvelleImage.GetLength(0); i++)
            {
                for (int j = 0; j < nouvelleImage.GetLength(1); j++)
                {
                    nouvelleImage[i, j] = new Pixel(2, 2, 2);

                    bool interieurLargeur = ((int)(j * rapportImageLargeur) == (int)((j + 1) * rapportImageLargeur)) || (j * rapportImageLargeur - (int)(j * rapportImageLargeur) == 0) || ((j + 1) * rapportImageLargeur - (int)((j + 1) * rapportImageLargeur) == 0);

                    bool interieurHauteur = ((int)(i * rapportImageHauteur) == (int)((i + 1) * rapportImageHauteur)) || (i * rapportImageHauteur - (int)(i * rapportImageHauteur) == 0) || ((i + 1) * rapportImageHauteur - (int)((i + 1) * rapportImageHauteur) == 0);

                    bool cas1 = (j == nouvelleImage.GetLength(1) - 1 && i == 0) || (i == nouvelleImage.GetLength(0) - 1 && j == nouvelleImage.GetLength(1) - 1) || (j == 0 && i == nouvelleImage.GetLength(0) - 1);

                    bool cas2 = j == nouvelleImage.GetLength(1) - 1;

                    bool cas3 = i == nouvelleImage.GetLength(0) - 1;

                    if (cas1 == true || (cas2 == true && interieurHauteur == true) || (cas3 == true && interieurLargeur == true))
                    {
                        nouvelleImage[i, j].CoeffNouveauPixel(new Pixel[] { image[(int)(i * rapportImageHauteur), (int)(j * rapportImageLargeur)] }, new double[] { 1 });
                    }

                    else if ((cas3 == true && interieurLargeur == false) || (interieurLargeur == false && interieurHauteur == true))
                    {
                        double aire1 = ((int)((j + 1) * rapportImageLargeur) - j * rapportImageLargeur);
                        double aire2 = ((j + 1) * rapportImageLargeur - (int)((j + 1) * rapportImageLargeur));

                        Pixel[] moyen = { image[(int)(i * rapportImageHauteur), (int)(j * rapportImageLargeur)], image[(int)(i * rapportImageHauteur), (int)((j + 1) * rapportImageLargeur)] };

                        nouvelleImage[i, j].CoeffNouveauPixel(moyen, new double[] { aire1, aire2 });

                    }

                    else if ((cas2 == true && interieurHauteur == false) || interieurHauteur == false && interieurLargeur == true)
                    {
                        double aire1 = ((int)((i + 1) * rapportImageHauteur) - i * rapportImageHauteur);
                        double aire2 = ((i + 1) * rapportImageHauteur - (int)(rapportImageHauteur * (i + 1)));

                        Pixel[] moyen = { image[(int)(i * rapportImageHauteur), (int)(j * rapportImageLargeur)], image[(int)((i + 1) * rapportImageHauteur), (int)(j * rapportImageLargeur)] };

                        nouvelleImage[i, j].CoeffNouveauPixel(moyen, new double[] { aire1, aire2 });
                    }

                    else
                    {
                        nouvelleImage[i, j].CoeffNouveauPixel(new Pixel[] { image[(int)(i * rapportImageHauteur), (int)(j * rapportImageLargeur)] }, new double[] { 1 });
                    }

                }

            }

            UneImage.ModifiedImage(nouvelleImage, file);
        }

        /// <summary>
        /// Rotation a n degres de l'image
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        /// <param name="degres"> l'angle de rotation (positif ou negatif) </param>
        public void Rotation(string file, double degres)
        {
            Pixel[,] image = UneImage.MesPixels;


            // On calcul les nouvelles dimension de l'image selon la rotation.

            double radian = degres * (Math.PI) / 180;

            double hauteur = 0;
            double largeur = 0;

            hauteur = image.GetLength(0) / (2.0 * Math.Abs(Math.Cos(radian))) + Math.Abs(Math.Sin(radian)) * (image.GetLength(1) / 2.0 - Math.Abs(Math.Tan(radian)) * (image.GetLength(0) / 2.0));

            largeur = image.GetLength(1) / (2.0 * Math.Abs(Math.Cos(radian))) + Math.Abs(Math.Sin(radian)) * (image.GetLength(0) / 2.0 - Math.Abs(Math.Tan(radian)) * (image.GetLength(1) / 2.0));

            hauteur = (int)(hauteur + 1) * 2 - 2 * (image.GetLength(0) / 2.0 - (int)(image.GetLength(0) / 2.0));
            largeur = (int)(largeur + 1) * 2 - 2 * (image.GetLength(1) / 2.0 - (int)(image.GetLength(1) / 2.0));



            if (degres % 90 == 0 && (degres / 90) % 2 == 1)
            {
                hauteur = image.GetLength(1);
                largeur = image.GetLength(0);

            }

            /* Ici on recupere la position de chaque pixel apres Rotation. Pour cela, on passe en coordonées polaire en placant une origine au centre de l'image. Enfin on recupere ces
            coordonnees dans une matrice de tableau correspondant a la nouvelle position x, y de chaque pixel */

            double[,][] matriceDecalee = PositionPolaire(image.GetLength(0), image.GetLength(1), hauteur, largeur, radian);



            Pixel[,] nouvelleImage = new Pixel[(int)hauteur, (int)largeur];

            for (int i = 0; i < nouvelleImage.GetLength(0); i++)
            {
                for (int j = 0; j < nouvelleImage.GetLength(1); j++)
                {
                    nouvelleImage[i, j] = new Pixel(255, 255, 255);
                }
            }


            // Enfin ici on rempli notre nouvelle image en la remplissant de facon oblique et en utilisant une interpolation des pixels voisins.

            int compteur = 0;
            int compteur2 = 0;

            for (int i = 1; i < matriceDecalee.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < matriceDecalee.GetLength(1) - 1; j++)
                {
                    double moyenneX = (matriceDecalee[i, j][0] + matriceDecalee[i, j + 1][0]) / 2;
                    double moyenneY = (matriceDecalee[i, j][1] + matriceDecalee[i + 1, j][1]) / 2;

                    nouvelleImage[(int)moyenneX, (int)moyenneY] = image[i, j];

                    if (j < matriceDecalee.GetLength(1) - 2 && i < matriceDecalee.GetLength(0) - 2 && i > 1 && j > 1)
                    {
                        nouvelleImage[(int)moyenneX + 1, (int)moyenneY + 1] = image[i, j];
                        nouvelleImage[(int)moyenneX, (int)moyenneY + 1] = image[i, j];
                        nouvelleImage[(int)moyenneX - 1, (int)moyenneY + 1] = image[i, j];
                        nouvelleImage[(int)moyenneX + 1, (int)moyenneY - 1] = image[i, j];
                        nouvelleImage[(int)moyenneX - 1, (int)moyenneY] = image[i, j];
                    }


                }
            }



            UneImage.ModifiedImage(nouvelleImage, file);
        }

        /// <summary>
        /// Effets miroirs sur l'image
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        public void EffetMiroir(string file)
        {
            Pixel[,] image = UneImage.MesPixels;

            Pixel[,] nouvelleImage = new Pixel[image.GetLength(0), image.GetLength(1)];


            for (int j = 0; j < image.GetLength(1); j++)
            {
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    nouvelleImage[i, j] = new Pixel(255, 255, 255);
                    nouvelleImage[i, j] = image[i, image.GetLength(1) - 1 - j];

                }
            }

            UneImage.ModifiedImage(nouvelleImage, file);
        }

        /// <summary>
        /// Effet kaleidoscope de l'image : symetrie selon la moitié en haut à gauche
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        public void EffetKaleidoscope(string file)
        {
            Pixel[,] image = UneImage.MesPixels;

            Pixel[,] nouvelleImage = new Pixel[image.GetLength(0), image.GetLength(1)];



            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    nouvelleImage[i, j] = new Pixel(255, 255, 255);

                    if (i < nouvelleImage.GetLength(0) / 2 && j < nouvelleImage.GetLength(1) / 2)
                    {
                        nouvelleImage[i, j] = image[i, j];
                    }

                    else if (i < nouvelleImage.GetLength(0) / 2 && j >= nouvelleImage.GetLength(1) / 2)
                    {
                        nouvelleImage[i, j] = image[i, nouvelleImage.GetLength(1) - j];
                    }

                    else if (i >= nouvelleImage.GetLength(0) / 2 && j < nouvelleImage.GetLength(1) / 2)
                    {
                        nouvelleImage[i, j] = image[nouvelleImage.GetLength(0) - i, j];
                    }

                    else
                    {
                        nouvelleImage[i, j] = image[nouvelleImage.GetLength(0) - i, nouvelleImage.GetLength(1) - j];
                    }



                }
            }


            UneImage.ModifiedImage(nouvelleImage, file);
        }

        /// <summary>
        /// Filtres appliquées sur l'image (matrice de convolution) de matrices a N dimensions
        /// </summary>
        /// <param name="file"> le nom du fichier crée </param>
        /// <param name="nb"> le choix de la matrice à appliquer</param>
        public void FiltresImage(string file, int nb)
        {
            Pixel[,] image = UneImage.MesPixels;

            double[,] convolution = Filtres(nb);
            int rebord = convolution.GetLength(0) / 2;

            Pixel[,] nouvelleImageBord = new Pixel[image.GetLength(0) + rebord * 2, image.GetLength(1) + rebord * 2];

            Pixel[,] nouvelleImage = new Pixel[image.GetLength(0), image.GetLength(1)];

            // On creer une nouvelle Image avec N bords pour gerer les calculs de matrices a N cotés sur les bords.

            for (int i = 0; i < nouvelleImageBord.GetLength(0); i++)
            {
                for (int j = 0; j < nouvelleImageBord.GetLength(1); j++)
                {
                    nouvelleImageBord[i, j] = new Pixel(0, 0, 0);

                    if (i - rebord >= 0 && j - rebord >= 0 && i - rebord < image.GetLength(0) && j - rebord < image.GetLength(1))
                    {
                        nouvelleImageBord[i, j] = new Pixel(image[i - rebord, j - rebord].Red, image[i - rebord, j - rebord].Green, image[i - rebord, j - rebord].Blue);

                    }

                    else
                    {

                        if (i - rebord < 0)
                        {
                            if (j - rebord < 0) nouvelleImageBord[i, j] = image[0, 0];

                            else if (j >= image.GetLength(1)) nouvelleImageBord[i, j] = image[0, image.GetLength(1) - 1];

                            else nouvelleImageBord[i, j] = image[0, j - rebord];

                        }

                        if (j - rebord < 0 && i >= rebord)
                        {
                            if (i >= image.GetLength(0)) nouvelleImageBord[i, j] = image[image.GetLength(0) - 1, 0];

                            else nouvelleImageBord[i, j] = image[i - rebord, 0];
                        }

                        if (j - rebord >= 0 && i >= image.GetLength(0))
                        {
                            if (j >= image.GetLength(1)) nouvelleImageBord[i, j] = image[image.GetLength(0) - 1, image.GetLength(1) - 1];

                            else nouvelleImageBord[i, j] = image[image.GetLength(0) - 1, j - rebord];
                        }

                        if (j >= image.GetLength(1) && i >= rebord && i < image.GetLength(0))
                        {
                            nouvelleImageBord[i, j] = image[i - rebord, image.GetLength(1) - 1];
                        }


                    }
                }
            }

            // On applique les coeffs de la matrice en question 



            for (int i = 0; i < nouvelleImage.GetLength(0); i++)
            {
                for (int j = 0; j < nouvelleImage.GetLength(1); j++)
                {
                    nouvelleImage[i, j] = new Pixel(image[i, j].Red, image[i, j].Green, image[i, j].Blue);

                    Pixel[,] matrice2 = new Pixel[convolution.GetLength(0), convolution.GetLength(1)];

                    for (int k = 0; k < matrice2.GetLength(0); k++)
                    {
                        for (int l = 0; l < matrice2.GetLength(1); l++)
                        {
                            matrice2[k, l] = new Pixel(0, 0, 0);
                            matrice2[k, l].NewPixel(nouvelleImageBord[k - rebord + rebord + i, l - rebord + rebord + j]);

                        }
                    }

                    nouvelleImage[i, j].MatricePixel(matrice2, convolution);

                }

            }
            UneImage.ModifiedImage(nouvelleImage, file);
        }

        /// <summary>
        /// Passage de coordonnées carésiennes de l'ensemble de  points d'une matrice vers les coordonnées polaires 
        /// </summary>
        /// <param name="hauteur"> hauteur de la matrice </param>
        /// <param name="largeur"> largeur de la matrice </param>
        /// <param name="nouvelleHauteur"> nouvelle hauteur de la matrice apres rotation </param>
        /// <param name="nouvelleLargeur"> nouvelle largeur de la matrice apres rotation </param>
        /// <param name="radian"> angle de rotation </param>
        /// <returns> matrice de tableau. Chaque tableau a successivement comme valeur la nouvelle position en x et y apres rotation de chaque points 
        /// (selon un syteme de coordonées defini avec les paramètres précédents)</returns>
        private double[,][] PositionPolaire(int hauteur, int largeur, double nouvelleHauteur, double nouvelleLargeur, double radian)
        {

            double[,][] matriceDecalee = new double[hauteur + 1, largeur + 1][];


            for (int i = 0; i < matriceDecalee.GetLength(0); i++)
            {
                for (int j = 0; j < matriceDecalee.GetLength(1); j++)
                {
                    matriceDecalee[i, j] = new double[2];
                }
            }

            for (int i = 0; i < matriceDecalee.GetLength(0); i++)
            {
                for (int j = 0; j < matriceDecalee.GetLength(1); j++)
                {
                    double positionx = j - (matriceDecalee.GetLength(1) - 1) / 2.0;
                    double positiony = (matriceDecalee.GetLength(0) - 1) / 2.0 - i;


                    double rayon = Math.Sqrt(positionx * positionx + positiony * positiony);

                    double angle = 0;

                    if (positionx != 0) angle = Math.Atan(positiony / positionx);

                    else if (positionx == 0 && positiony > 0) angle = Math.PI / 2;

                    else if (positionx == 0 && positiony < 0) angle = Math.PI / 2 + Math.PI;

                    else angle = 0;


                    if ((positionx < 0 && positiony >= 0) || (positiony <= 0 && positionx < 0)) angle = angle + Math.PI;

                    double nouvellePosx = rayon * Math.Cos(angle + radian) + nouvelleLargeur / 2;
                    double nouvellePosy = -rayon * Math.Sin(angle + radian) + nouvelleHauteur / 2;



                    matriceDecalee[i, j][1] = nouvellePosx;
                    matriceDecalee[i, j][0] = nouvellePosy;


                }

            }

            return matriceDecalee;
        }

        /// <summary>
        /// catalogue des filtres applicables
        /// </summary>
        /// <param name="nb"></param>
        /// <returns> le filtre voulu </returns>
        private double[,] Filtres(int nb)
        {


            switch (nb)
            {
                case 0:
                    double[,] detectionContour = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                    return detectionContour;

                case 1:
                    double[,] RenforcementsBords = { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
                    return RenforcementsBords;

                case 2:
                    double[,] Flou = { { 1, -1, 1 }, { -1, 1, -1 }, { 1, -1, 1 } };
                    return Flou;

                case 3:
                    double[,] Repoussage = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                    return Repoussage;


                case 4:
                    double[,] Contraste = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                    return Contraste;


                case 5:
                    double[,] FlouGaussien = { { 1/256.0, 4/256.0, 6 / 256.0 , 4 / 256.0 , 1 / 256.0 },
                        { 4 / 256.0, 16 / 256.0, 24 / 256.0, 16 / 256.0, 4 / 256.0 },
                        { 6 / 256.0, 24 / 256.0, 36 / 256.0, 24 / 256.0, 6 / 256.0 },
                        { 4/256.0, 16/256.0, 24 / 256.0 , 16 / 256.0 , 4 / 256.0 },
                        { 1/256.0, 4/256.0, 6 / 256.0 , 4 / 256.0 , 1 / 256.0 }
                    };
                    return FlouGaussien;


                default:

                    double[,] imageNormal = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
                    return imageNormal;


            }


        }

        /// <summary>
        /// Ajoute des bords à l'image en copiant les pixels de bords
        /// </summary>
        /// <param name="image"> l'image a traiter </param>
        /// <param name="agrandissement"> le nombre de pixels a ajouter sur chaque bord</param>
        /// <returns></returns>
        public Pixel[,] RebordsImage(Pixel[,] image, int agrandissement)
        {
            Pixel[,] nouvelleImageBord = new Pixel[image.GetLength(0) + agrandissement, image.GetLength(1) + agrandissement];

            int rebord = agrandissement / 2;

            for (int i = 0; i < nouvelleImageBord.GetLength(0); i++)
            {
                for (int j = 0; j < nouvelleImageBord.GetLength(1); j++)
                {
                    nouvelleImageBord[i, j] = new Pixel(0, 0, 0);

                    if (i - rebord >= 0 && j - rebord >= 0 && i - rebord < image.GetLength(0) && j - rebord < image.GetLength(1))
                    {
                        nouvelleImageBord[i, j] = new Pixel(image[i - rebord, j - rebord].Red, image[i - rebord, j - rebord].Green, image[i - rebord, j - rebord].Blue);

                    }

                    else
                    {

                        if (i - rebord < 0)
                        {
                            if (j - rebord < 0) nouvelleImageBord[i, j] = image[0, 0];

                            else if (j >= image.GetLength(1)) nouvelleImageBord[i, j] = image[0, image.GetLength(1) - 1];

                            else nouvelleImageBord[i, j] = image[0, j - rebord];

                        }

                        if (j - rebord < 0 && i >= rebord)
                        {
                            if (i >= image.GetLength(0)) nouvelleImageBord[i, j] = image[image.GetLength(0) - 1, 0];

                            else nouvelleImageBord[i, j] = image[i - rebord, 0];
                        }

                        if (j - rebord >= 0 && i >= image.GetLength(0))
                        {
                            if (j >= image.GetLength(1)) nouvelleImageBord[i, j] = image[image.GetLength(0) - 1, image.GetLength(1) - 1];

                            else nouvelleImageBord[i, j] = image[image.GetLength(0) - 1, j - rebord];
                        }

                        if (j >= image.GetLength(1) && i >= rebord && i < image.GetLength(0))
                        {
                            nouvelleImageBord[i, j] = image[i - rebord, image.GetLength(1) - 1];
                        }


                    }
                }
            }

            return nouvelleImageBord;
        }


    }
}
