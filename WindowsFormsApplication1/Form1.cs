using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int TriangleCount;//Количество треугольников
        String filePath;
        String[] FileContent;//Файл построчно

        public Triangle[] Triangles;//Массив треугольников
        public struct Triangle//Структура треугольника
        {
            public Single x1;
            public Single y1;
            public Single x2;
            public Single y2;
            public Single x3;
            public Single y3;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Draw();//Вызов отрисовки

            //g.DrawLines(new Pen(Color.Red, 3), new[] { point1, point2, point3, point1 } );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();//Вызов диалога с выбором файла
            filePath = openFileDialog1.FileName;//Получаем путь к файлу
            FileContent = File.ReadAllLines(filePath);//Загружаем файл, в массив строк
            Parser();//Получаем данные из файла

        }

        void Parser()
        {
            TriangleCount = Convert.ToInt32(FileContent[0]);//Вытаскиваем из 1 строки, количество треугольников
            Triangles = new Triangle[TriangleCount];

            int FileLength = FileContent.Length;//Количество строк в файле
            for (int i = 0; i < TriangleCount; i++)//Проходимся по всем строкам и вытаскиваем треугольники
            {
                String[] Data = FileContent[i+1].Split(' ');//Разбиваем строку на данные
                //Создаём треугольник
                Triangles[i].x1 = Single.Parse(Data[0]);
                Triangles[i].y1 = Single.Parse(Data[1]);
                Triangles[i].x2 = Single.Parse(Data[2]);
                Triangles[i].y2 = Single.Parse(Data[3]);
                Triangles[i].x3 = Single.Parse(Data[4]);
                Triangles[i].y3 = Single.Parse(Data[5]);
            }

        }

        void Draw()
        {
            var _Pen = new Pen(Color.Red, 1);
            var g = pictureBox1.CreateGraphics();


            for (int i = 0; i < TriangleCount; i++)//Проходимся по всем треугольникам
            {
                var Point1 = new PointF(Triangles[i].x1, Triangles[i].y1);//Задаём координаты 3х точек
                var Point2 = new PointF(Triangles[i].x2, Triangles[i].y2);
                var Point3 = new PointF(Triangles[i].x3, Triangles[i].y3);
                g.DrawLines((_Pen), new[] { Point1, Point2 });//рисуем
                g.DrawLines((_Pen), new[] { Point1, Point3 });
                g.DrawLines((_Pen), new[] { Point2, Point3 });
            }

        }


    }
}
