using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetImage
{
    /// <summary>
    ///  Creation d'une classe complexe pour la gestion d'operations sur C ( fractales)
    /// </summary>
    public class Complexe
    {

        private double reel;
        private double imaginaire;

        public double Reel
        {
            get { return this.reel; }
            set { this.reel = value; }
        }

        public double Imaginaire
        {
            get { return this.imaginaire; }
            set { this.imaginaire = value; }
        }


        public Complexe(double a, double b)
        {
            this.reel = a;
            this.imaginaire = b;

        }

        /// <summary>
        ///  somme de 2 complexes
        /// </summary>
        /// <param name="unComplexe"> le complexe passé en paramètre </param>
        public void Somme(Complexe unComplexe)
        {
            this.reel = reel + unComplexe.reel;
            this.imaginaire = imaginaire + unComplexe.imaginaire;

        }

        /// <summary>
        /// Produit de 2 complexes
        /// </summary>
        /// <param name="unComplexe"> le complexe passé en paramètre </param>
        public void Produit(Complexe unComplexe)
        {
            double newReel = this.reel * unComplexe.reel - imaginaire * unComplexe.imaginaire;
            double newIma = this.reel * unComplexe.imaginaire + unComplexe.reel * imaginaire;

            this.reel = newReel;
            this.imaginaire = newIma;

        }

        /// <summary>
        /// calcul de la norme du complexe
        /// </summary>
        /// <returns> la norme du complexe en double </returns>
        public double Norme()
        {
            return Math.Sqrt(imaginaire * imaginaire + reel * reel);
        }




    }
}
