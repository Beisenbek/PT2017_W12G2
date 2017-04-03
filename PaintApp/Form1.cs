using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintApp
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(pictureBox1.Image);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        Point prevPoint;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
        }

        Graphics g;
        Point curPoint;

        GraphicsPath gp = new GraphicsPath();

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                curPoint = e.Location;

                switch (currentShape)
                {
                    case Shapes.Free:
                        g.DrawLine(new Pen(color), prevPoint, curPoint);
                        prevPoint = curPoint;
                        break;
                    case Shapes.Line:
                        gp.Reset();
                        gp.AddLine(prevPoint, curPoint);
                        break;
                    case Shapes.Rectangle:
                        gp.Reset();
                        gp.AddRectangle(GenerateRectangle(prevPoint,curPoint));
                        break;
                    case Shapes.Triangle:
                        break;
                    default:
                        break;
                }
            }

            pictureBox1.Refresh();
            mouseLocationLabel.Text = string.Format("X:{0},Y:{1}", e.X, e.Y);
        }

        private RectangleF GenerateRectangle(Point prevPoint, Point curPoint)
        {
            //new Rectangle(Math.Min(prevPoint.X,curPoint.X),
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            g.DrawPath(new Pen(color), gp);
        }

        Color color = Color.Red;
        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                color = dlg.Color;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //if (System.IO.File.Exists(sfd.FileName))
                  //  System.IO.File.Delete(sfd.FileName);

                pictureBox1.Image.Save(sfd.FileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Bitmap newimage = new Bitmap(ofd.FileName);
                Bitmap cloneimage = newimage.Clone() as Bitmap;
                newimage.Dispose();
                pictureBox1.Image = cloneimage;
                g = Graphics.FromImage(pictureBox1.Image);
            }
        }

        Shapes currentShape = Shapes.Free;
        private void button2_Click(object sender, EventArgs e)
        {
            currentShape = Shapes.Free;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentShape = Shapes.Line;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawPath(new Pen(color), gp);
        }
    }

    enum Shapes
    {
        Free,
        Line,
        Rectangle,
        Triangle
    }
}
