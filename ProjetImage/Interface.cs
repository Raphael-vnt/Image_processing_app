using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProjetImage
{
    /// <summary>
    /// Class interface graphique du programme  
    /// </summary>
    public partial class Interface : Form
    {
        private MyImage UneImage;
        private Traitement traitement;
        private Creation creation;
        private string filePrincipal = "";
        private string fileEncours = "";
        bool hauteur = false;
        bool largeur = false;

        bool coderImage = false;
        string coder;

        bool decoder = false;

        bool fonduImage = false;
        string fondu;

        public Interface()
        {
            InitializeComponent();

            UneImage = new MyImage();
            creation = new Creation();

        }

        private void Interface_Load(object sender, EventArgs e)
        {

        }

        private void Titre_Click(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }


        /// <summary>
        /// /* boutton Principale qui recupere une image selectionné (manuellemnt ou depuis le repertoire), puis recupere ces données en remplissant les attributs de la classe (MyImage, Creation , traitement )  
        /// Affiche ensuite l'image via la classe Bitmap 
        /// Change aussi de nom et de specifités selon le traitement voulu(decoder, coder, fondu) */
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            StringBuilder stringBu = new StringBuilder();

            foreach (var item in listBox1.Items)
            {
                stringBu.Append(item.ToString());
                stringBu.Append(" ");

            }

            try
            {

                if (listBox1.SelectedItem.ToString() == "Autre")
                {
                    OpenFileDialog ofd = new OpenFileDialog();

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            if (ofd.FileName[ofd.FileName.Length - 1] == 'v')
                            {
                                this.UneImage = new MyImage(ofd.FileName);
                                this.UneImage.FromImageToFile("ImageCsvSample.bmp");
                                ofd.FileName = "ImageCsvSample.bmp";
                                filePrincipal = "ImageCsvSample.bmp";
                            }

                            Bitmap bit = new Bitmap(ofd.FileName);
                            pictureBox1.Image = bit;
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                            if (coderImage == true)
                            {
                                // button1.Text = "Selectionner image 2";
                                coder = ofd.FileName;
                                button3.Enabled = true;
                            }

                            else if (fonduImage == true)
                            {

                                fondu = ofd.FileName;
                                button3.Enabled = true;
                            }

                            else
                            {
                                filePrincipal = ofd.FileName;
                                this.UneImage = new MyImage(filePrincipal);
                                this.traitement = new Traitement(filePrincipal);
                                this.creation = new Creation(filePrincipal);

                                this.UneImage.FromImageToFile("test.bmp");
                                fileEncours = "test.bmp";
                            }


                            AnnulerEffets.Enabled = true;
                            Cumuler.Enabled = true;
                            enableControls();
                            Sauvegarde.Enabled = true;

                        }

                        catch
                        {
                            MessageBox.Show("Type de fichier non valide, seul les fichiers de type .bmp et .csv sont pris en compte.");
                        }

                    }

                }

                else
                {
                    try
                    {
                        Bitmap bit = new Bitmap(listBox1.SelectedItem.ToString().ToLower() + ".bmp");
                        pictureBox1.Image = bit;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                        if (coderImage == true)
                        {
                            // button1.Text = "Selectionner image 2";
                            coder = listBox1.SelectedItem.ToString().ToLower() + ".bmp";
                            button3.Enabled = true;
                        }

                        else if (fonduImage == true)
                        {
                            fondu = listBox1.SelectedItem.ToString().ToLower() + ".bmp";
                            button3.Enabled = true;
                        }

                        else
                        {
                            filePrincipal = listBox1.SelectedItem.ToString().ToLower() + ".bmp";
                            this.UneImage = new MyImage(filePrincipal);
                            this.traitement = new Traitement(filePrincipal);
                            this.creation = new Creation(filePrincipal);

                            this.UneImage.FromImageToFile("test.bmp");
                            fileEncours = "test.bmp";
                        }


                        AnnulerEffets.Enabled = true;
                        Cumuler.Enabled = true;
                        enableControls();
                        Sauvegarde.Enabled = true;




                    }

                    catch
                    {
                        MessageBox.Show("Type de fichier non valide");
                    }
                }
            }

            catch
            {
                MessageBox.Show("ATTENTION");
            }



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Effets_Basiques_Click(object sender, EventArgs e)
        {

        }

        // A partir d'ici, on fait simplement le traitemnent de l'image pour chaque demande utilisateurs, puis affichons l'image 

        private void noirEtBlancToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                traitement.ImageNoirEtBlanc("test.bmp");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";

            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }


        }

        private void negatifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                traitement.Negatif("test.bmp");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";

            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void luminositéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Enabled = true;
            panel3.Visible = true;
        }

        private void simpleMiroirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                traitement.EffetMiroir("test.bmp");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";

            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void kaleidoscopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                traitement.EffetKaleidoscope("test.bmp");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";

            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void DetectionContourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                traitement.FiltresImage("test.bmp", 0);
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void RenforcementBordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                traitement.FiltresImage("test.bmp", 1);
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void FlouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                traitement.FiltresImage("test.bmp", 2);
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void RepoussageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                traitement.FiltresImage("test.bmp", 3);
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("L'image n'a pas pu être chargée");
            }
        }

        private void ContrasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                traitement.FiltresImage("test.bmp", 4);
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("L'image n'a pas pu être chargée");
            }
        }








        private void NoirEtBlancToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                creation.HistogrammeClassique("test.bmp");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void RougeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                creation.HistogrammeRGB("test.bmp", "R");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void VertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                creation.HistogrammeRGB("test.bmp", "G");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void BleuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                creation.HistogrammeRGB("test.bmp", "B");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void TeinteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                creation.HistogrammeTeinte("test.bmp");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void MandelBrotToolStripMenuItem_Click(object sender, EventArgs e)
        {

            creation.FractaleMandelBrot("test2.bmp");
            CreaImage();

        }

        private void tapisDeSierpienskiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            creation.TapisDeSierpinski("test2.bmp");
            CreaImage();
        }

        private void GlitchedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                creation.CreationPersonnelle("test.bmp");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";

            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }

        }





        
        /// <summary>
        /// Methode permettant de rendre accessible tous les controles du menuStripTools
        /// </summary>
        private void enableControls()
        {

            foreach (ToolStripMenuItem toolItem in menuStrip1.Items)
            {

                foreach (ToolStripItem item in toolItem.DropDownItems)
                {
                    item.Enabled = true;

                }
            }


        }

       /// <summary>
       /// Ouvre l'image en cours de traitment 
       /// </summary>
       /// <param name="file"></param>

        private void OuvertureImage(string file)
        {

            if (Cumuler.Checked == true)
            {
                this.UneImage = new MyImage(file);
                this.traitement = new Traitement(file);
                this.creation = new Creation(file);

            }

            try
            {
                Image img;
                using (var bmpTemp = new Bitmap(file))
                {
                    img = new Bitmap(bmpTemp);
                }

                pictureBox1.Image = img;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch
            {
                MessageBox.Show("l'image n'a pas pu etre ouverte");
            }


        }

        /// <summary>
        /// gere l'accessibilié de chaque parametre selon le traitement selectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem ownerItem = e.ClickedItem.OwnerItem as ToolStripMenuItem;

           

            if (ownerItem != rotationToolStripMenuItem)
            {
                panel1.Enabled = false;
                panel1.Visible = false;
            }

            if (ownerItem != Changement_dimensions)
            {
                panel2.Enabled = false;
                panel2.Visible = false;
            }

            if (ownerItem != luminositéToolStripMenuItem)
            {
                panel3.Enabled = false;
                panel3.Visible = false;
            }

            if (ownerItem != coderUneImageToolStripMenuItem)
            {
                coderImage = false;
                button1.Text = "Selectionner";
                button3.Enabled = false;
                button3.Visible = false;
            }

            if (ownerItem != decoderUneImageToolStripMenuItem)
            {
                decoder = false;
                Next.Enabled = false;
                Next.Visible = false;
                back.Enabled = false;
                back.Visible = false;
                button3.Enabled = false;
                button3.Visible = false;


            }

            if (ownerItem != fondu2ImagesToolStripMenuItem)
            {
                fonduImage = false;
                button1.Text = "Selectionner";
                button3.Enabled = false;
                button3.Visible = false;
            }



        }

        /// <summary>
        /// Methode permettant d'annuler les effets appliqués sur l'image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnulerEffets_Click(object sender, EventArgs e)
        {
            this.UneImage = new MyImage(filePrincipal);
            this.traitement = new Traitement(filePrincipal);
            this.creation = new Creation(filePrincipal);



            OuvertureImage(filePrincipal);
        }

        /// <summary>
        /// Permet l'accumulation de divers effets en changeant la valeur des attributs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cumuler_CheckedChanged(object sender, EventArgs e)
        {
            if (Cumuler.Checked == true)
            {
                this.UneImage = new MyImage(fileEncours);
                this.traitement = new Traitement(fileEncours);
                this.creation = new Creation(fileEncours);
            }

            else
            {
                this.UneImage = new MyImage(filePrincipal);
                this.traitement = new Traitement(filePrincipal);
                this.creation = new Creation(filePrincipal);


            }

        }


        /// <summary>
        /// Bouton de sauvegarde en .bmp ou csv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sauvegarde_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Bitmap Image|*.bmp|CSV Image|*.csv";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.FileName = ".bmp";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MyImage saveImage = new MyImage(fileEncours);
                /*byte[] data;
                 * FileInfo fileInfo = new FileInfo(fileEncours);
                data = new byte[fileInfo.Length];

                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                    fs.Close();

                }

                using (FileStream fs = File.Create(saveFileDialog1.FileName))
                {
                    fs.Write(data, 0, data.Length);
                    fs.Close();

                }*/

                try
                {
                    if (saveFileDialog1.FileName[saveFileDialog1.FileName.Length - 1] == 'v')
                    {
                        saveImage.ImageToCSV(saveImage.MesPixels, saveFileDialog1.FileName);
                    }

                    else
                    {
                        saveImage.FromImageToFile(saveFileDialog1.FileName);
                    }
                }

                catch
                {
                    MessageBox.Show("L'image n'a pas pu etre enregistrée");
                }

                


            }



        }

        /// <summary>
        /// Creer l'image pour toutes les creations a partir de rien (fractales)
        /// </summary>
        private void CreaImage()
        {
            try
            {
                filePrincipal = ("test2.bmp");
                fileEncours = "test2.bmp";

                this.UneImage = new MyImage("test2.bmp");
                this.traitement = new Traitement("test2.bmp");
                this.creation = new Creation("test2.bmp");
                OuvertureImage(filePrincipal);


                enableControls();
                Cumuler.Enabled = true;
                AnnulerEffets.Enabled = true;
            }

            catch
            {
                MessageBox.Show("L'image n'a pas pu etre chargé");
            }

        }

        private void labeltrack_Click(object sender, EventArgs e)
        {

        }

        // Toutes les fractales relevées...

        private void BasiliqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 1);
            CreaImage();
        }

        private void AvionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 2);
            CreaImage();
        }

        private void LeLapinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 3);
            CreaImage();
        }

        private void LeLapinTripleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 4);
            CreaImage();
        }

        private void LaSpiraleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 5);
            CreaImage();
        }

        private void LeGrandAvionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 6);
            CreaImage();
        }

        private void LeChouFleurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 7);
            CreaImage();
        }

        private void LaFleurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 8);
            CreaImage();
        }

        private void SanMarcoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 9);
            CreaImage();
        }

        private void DisqueDeSiegelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 10);
            CreaImage();
        }

        private void LemnicasteDeBernouilliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 11);
            CreaImage();
        }

        private void DendriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 12);
            CreaImage();
        }

        private void CoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 13);
            CreaImage();
        }

        private void PoussiereDeFatou1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 14);
            CreaImage();
        }

        private void PoussiereDeFatou2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 15);
            CreaImage();
        }

        private void PoussiereDeFatou3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 16);
            CreaImage();
        }

        private void GalaxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creation.FractaleQuelconque("test2.bmp", 17);
            CreaImage();
        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Enabled = true;
        }

        // A partir d'ici les boutons/textbox/trackbar... demandant de renter des valeurs particulieres pour le traitement ou simplement appliquer l'effet


        /// <summary>
        /// permet de modifier la valeur de rotation souhaiter de la trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                trackRotation.Value = Convert.ToInt32(valeurRotation.Text);
            }
            catch
            {

            }

        }
        /// <summary>
        /// Applique l'effet de rotation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Apply_Click(object sender, EventArgs e)
        {
            try
            {

                traitement.Rotation("test.bmp", trackRotation.Value);
                OuvertureImage("test.bmp");
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void Degres_Click(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        ///  La trackbar relié au textBox1, toujours pour la rotation (de -360 deg a 360 deg)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackRotation_Scroll(object sender, EventArgs e)
        {
            valeurRotation.Text = trackRotation.Value.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dimensionsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel2.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

            AccesDimension();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            AccesDimension();
        }



        /// <summary> 
        ///  Verifie si les dimensions entrées pour le redimensionnement de l'image sont conformes et permet de debloquer ensuite le bouton apply
        /// </summary>
        private void AccesDimension()

        {
            try
            {
                if (Convert.ToDouble(textBox2.Text) <= 4.0 && Convert.ToDouble(textBox2.Text) >= 0.1)
                {
                    //  ApplyDim.Enabled = true;
                    largeur = true;

                }

                else
                {
                    ApplyDim.Enabled = false;
                    largeur = false;
                }
            }

            catch
            {
                ApplyDim.Enabled = false;
                largeur = false;
            }

            try
            {
                if (Convert.ToDouble(textBox1.Text) <= 4.0 && Convert.ToDouble(textBox1.Text) >= 0.1)
                {
                    //  ApplyDim.Enabled = true;
                    hauteur = true;

                }

                else
                {
                    ApplyDim.Enabled = false;
                    hauteur = false;
                }
            }

            catch
            {
                ApplyDim.Enabled = false;
                hauteur = false;
            }


            if (largeur == true && hauteur == true)
            {
                ApplyDim.Enabled = true;
            }
        }


        /// <summary>
        /// applique le changement de dimensions 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyDim_Click(object sender, EventArgs e)
        {
            double pixelHauteur = Convert.ToDouble(textBox1.Text) * this.UneImage.MesPixels.GetLength(0) - this.UneImage.MesPixels.GetLength(0);
            double pixelLargeur = Convert.ToDouble(textBox2.Text) * this.UneImage.MesPixels.GetLength(1) - this.UneImage.MesPixels.GetLength(1);
            string file = "test.bmp";

            try
            {

                traitement.ChangementDimensions("test.bmp", (int)pixelHauteur, (int)pixelLargeur);
                if (Cumuler.Checked == true)
                {
                    this.UneImage = new MyImage(file);
                    this.traitement = new Traitement(file);
                    this.creation = new Creation(file);

                }

                try
                {
                    Image img;
                    using (var bmpTemp = new Bitmap(file))
                    {
                        img = new Bitmap(bmpTemp);
                    }

                    pictureBox1.Image = img;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch
                {
                    MessageBox.Show("l'image n'a pas pu etre ouverte");
                }
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Traite l'eclaircissemnent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string value = unite.Value.ToString() + "," + numericUpDown2.Value.ToString();

            try
            {
                traitement.Eclaircissement("test.bmp", Convert.ToDouble(value));
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";

            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }


        }

        private void coderUneImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coderImage = true;
            button3.Text = "Coder Image";
            button3.Visible = true;


            button1.Text = "Selectionner Image à cacher";
        }


        /// <summary>
        /// Bouton appliquant le codage de l'image et le fondu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (coderImage == true)

            {

                MyImage imageCoder = new MyImage(coder);
                Traitement Code = new Traitement(coder);

                if (imageCoder.MesPixels.GetLength(0) > UneImage.MesPixels.GetLength(0) || imageCoder.MesPixels.GetLength(1) > UneImage.MesPixels.GetLength(1))
                {
                    MessageBox.Show("ATTENTION : L'image a cacher a des dimensions superieurs a celles de l'image de base, ses dimensions ont été retrecies");

                    int rapportx = UneImage.MesPixels.GetLength(0) - imageCoder.MesPixels.GetLength(0);
                    int rapporty = UneImage.MesPixels.GetLength(1) - imageCoder.MesPixels.GetLength(1);

                    double coeff = UneImage.MesPixels.GetLength(0) * 1.0 / imageCoder.MesPixels.GetLength(0) * 1.0;
                    if (rapporty < rapportx) coeff = UneImage.MesPixels.GetLength(1) * 1.0 / imageCoder.MesPixels.GetLength(1) * 1.0;

                    int newDimx = (int)(coeff * imageCoder.MesPixels.GetLength(0) - imageCoder.MesPixels.GetLength(0));
                    int newDimy = (int)(coeff * imageCoder.MesPixels.GetLength(1) - imageCoder.MesPixels.GetLength(1));

                    Code.ChangementDimensions("aya.bmp", newDimx, newDimy);
                    coder = "aya.bmp";


                }
                creation.CoderImage(this.coder, "test.bmp");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }


            if (decoder == true)
            {
                creation.DecoderImage("test.bmp", "test66.bmp");
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";

                Next.Enabled = true;

            }

            if (fonduImage == true)
            {
                MyImage imageFondu = new MyImage(this.fondu);
                Creation Fondu = new Creation(this.fondu);
                try
                {
                    try
                    {
                        Fondu.ImagesFondu(imageFondu.MesPixels, UneImage.MesPixels, "test.bmp");
                    }
                    catch
                    {
                        Fondu.ImagesFondu(UneImage.MesPixels, imageFondu.MesPixels,"test.bmp");
                    }


                    OuvertureImage("test.bmp");
                    fileEncours = "test.bmp";

                    Next.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Impossible de fondre les 2 images ");
                }



            }

        }


        /// <summary>
        /// Decode une image
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void decoderUneImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Text = "Decoder Image";
            button3.Enabled = true;
            button3.Visible = true;
            decoder = true;

            Next.Visible = true;
            back.Visible = true;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            OuvertureImage("test66.bmp");
            fileEncours = "test66.bmp";
            Next.Enabled = false;
            back.Enabled = true;
        }

        private void back_Click(object sender, EventArgs e)
        {
            OuvertureImage("test.bmp");
            fileEncours = "test.bmp";
            Next.Enabled = true;
            back.Enabled = false;
        }

        /// <summary>
        /// Fondu de 2 images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fondu2ImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fonduImage = true;
            button3.Text = "Fondre Images";
            button3.Visible = true;


            button1.Text = "Selectionner Image à fondre";
        }

        private void flouGaussienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                traitement.FiltresImage("test.bmp", 5);
                OuvertureImage("test.bmp");
                fileEncours = "test.bmp";
            }

            catch
            {
                MessageBox.Show("Aucune image n'est chargée");
            }
        }

        private void Fractales_Click(object sender, EventArgs e)
        {

        }
    }
}
