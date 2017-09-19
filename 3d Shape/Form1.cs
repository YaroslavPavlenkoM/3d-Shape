using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _3d_Shape.ListFigures;
using _3d_Shape.Figures;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace _3d_Shape
{
    public partial class Form1 : Form
    {
        List<Figure> figures = new List<Figure>();
        int chosenFigureIndex = -1;
        string typeOfFigure;

        Bitmap bitmap;
        Graphics field;
        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            field = Graphics.FromImage(bitmap);

            timer1.Start();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Items[2].Enabled = false;
            contextMenuStrip1.Items[3].Enabled = false;
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < figures.Count; i++)
                {
                    if (figures[i].IsPointBelongFigure(new Point(e.X, e.Y)))
                    {
                        chosenFigureIndex = i;

                        contextMenuStrip1.Items[2].Enabled = true;
                        contextMenuStrip1.Items[3].Enabled = true;

                        if (figures[i].IsRotating)
                            contextMenuStrip1.Items[3].Text = "Остановить";
                        else
                            contextMenuStrip1.Items[3].Text = "Вращать";
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int angle;
            pictureBox1.Refresh();
            try
            {
                bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }
            catch(ArgumentException) 
            {
                bitmap = new Bitmap(1350, pictureBox1.Height);
            }

            field = Graphics.FromImage(bitmap);

            foreach (var figure in figures)
            {
                figure.RotatingFigure(1);
                if (RelationshipFigure.IntersectFigures(figures, figure))
                {
                    angle = 0;
                }
                else
                {
                    angle = 1;
                }
                figure.RotatingFigure(-1);

                if (figure.IsRotating)
                    figure.DrownFigure(ref field, angle);
                else
                    figure.DrownFigure(ref field, 0);
            }

            pictureBox1.Image = bitmap;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            typeOfFigure = "Square";
            label1.Text = "Вы выбрали квадрат";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            typeOfFigure = "Circle";
            label1.Text = "Вы выбрали круг";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            typeOfFigure = "Tringle";
            label1.Text = "Вы выбрали треугольник";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            typeOfFigure = "Trapeze";
            label1.Text = "Вы выбрали трапецию";
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (typeOfFigure)
            {
                case "Square":
                    Quadrilateral square = new Quadrilateral(e.X, e.Y, new Point(e.X - 46, e.Y - 46), new Point(e.X + 46, e.Y - 46),
                                                           new Point(e.X + 46, e.Y + 46), new Point(e.X - 46, e.Y + 46));
                    if (RelationshipFigure.IntersectFigures(figures, square))
                    {
                        MessageBox.Show("Выберите другое место!");
                    }
                    else
                    {
                        square.DrownFigure(ref field, 0);
                        pictureBox1.Image = bitmap;

                        figures.Add(square);
                    }
                    break;
                case "Trapeze":
                    Quadrilateral quadrilateral = new Quadrilateral(e.X, e.Y, new Point(e.X - 30, e.Y - 40), new Point(e.X + 30, e.Y - 40),
                                                           new Point(e.X + 55, e.Y + 40), new Point(e.X - 55, e.Y + 40));
                    if (RelationshipFigure.IntersectFigures(figures, quadrilateral))
                    {
                        MessageBox.Show("Выберите другое место!");
                    }
                    else
                    {
                        quadrilateral.DrownFigure(ref field, 0);
                        pictureBox1.Image = bitmap;

                        figures.Add(quadrilateral);
                    }
                    break;
                case "Tringle":
                    Tringle tringle = new Tringle(e.X, e.Y, new Point(e.X, e.Y - 40), new Point(e.X - 40, e.Y + 30), new Point(e.X + 40, e.Y + 30));
                    if (RelationshipFigure.IntersectFigures(figures, tringle))
                    {
                        MessageBox.Show("Выберите другое место!");
                    }
                    else
                    {
                        tringle.DrownFigure(ref field, 0);
                        pictureBox1.Image = bitmap;

                        figures.Add(tringle);
                    }

                    break;
                case "Circle":
                    Circle circle = new Circle(e.X, e.Y, 48);
                    if (RelationshipFigure.IntersectFigures(figures, circle))
                    {
                        MessageBox.Show("Выберите другое место!");
                    }
                    else
                    {
                        circle.DrownFigure(ref field, 0);
                        pictureBox1.Image = bitmap;

                        figures.Add(circle);
                    }
                    break;
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chosenFigureIndex >= 0)
            {
                figures.RemoveAt(chosenFigureIndex);
                pictureBox1.Refresh();

                try
                {
                    bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                }
                catch (ArgumentException)
                {
                    bitmap = new Bitmap(1350, pictureBox1.Height);
                }

                field = Graphics.FromImage(bitmap);

                foreach (var figure in figures)
                {
                    figure.DrownFigure(ref field, 0);
                }

                pictureBox1.Image = bitmap;
            }

            chosenFigureIndex = -1;
        }

        private void вращатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chosenFigureIndex >= 0)
            {
                figures[chosenFigureIndex].IsRotating = !figures[chosenFigureIndex].IsRotating;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog savefile1 = new SaveFileDialog();
            

            savefile1.Filter = "xml files |*.xml";
            savefile1.FilterIndex = 2;
            savefile1.RestoreDirectory = true;

            if (savefile1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = savefile1.OpenFile()) != null)
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Figure>));
                        serializer.Serialize(myStream, figures);
                    }
                    finally
                    {
                        myStream.Close();
                    }       
                }
                else
                {
                    throw new Exception("Файл не открылся");
                } 
            }

        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Figure>));
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "xml files |*.xml";
            openFileDialog1.Title = "Select a Xml File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader fs = new StreamReader(openFileDialog1.FileName))
                {
                    XmlReader reader = XmlReader.Create(fs);
                    figures = (List<Figure>)serializer.Deserialize(reader);
                }
            }
            
        }

    }
}
