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

        int Max_x = 0;//Максимальные координаты по X,Y
        int Max_y = 0;

        public Triangle[] Triangles;//Массив треугольников
        public struct Triangle//Структура треугольника
        {
            public int x1;
            public int y1;
            public int x2;
            public int y2;
            public int x3;
            public int y3;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Max();
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
                String[] Data = FileContent[i + 1].Split(' ');//Разбиваем строку на данные
                //Создаём треугольник
                Triangles[i].x1 = Convert.ToInt32(Data[0]);
                Triangles[i].y1 = Convert.ToInt32(Data[1]);
                Triangles[i].x2 = Convert.ToInt32(Data[2]);
                Triangles[i].y2 = Convert.ToInt32(Data[3]);
                Triangles[i].x3 = Convert.ToInt32(Data[4]);
                Triangles[i].y3 = Convert.ToInt32(Data[5]);
            }

        }

        void Draw()
        {
            Bitmap TrianglesImage = new Bitmap(Max_x, Max_y);
            Graphics flagGraphics = Graphics.FromImage(TrianglesImage);

            var _Pen = new Pen(Color.Red, 1);
            var g = pictureBox1.CreateGraphics();


            for (int i = 0; i < TriangleCount; i++)//Проходимся по всем треугольникам
            {
                var Point1 = new Point(Triangles[i].x1, Triangles[i].y1);//Задаём координаты 3х точек
                var Point2 = new Point(Triangles[i].x2, Triangles[i].y2);
                var Point3 = new Point(Triangles[i].x3, Triangles[i].y3);

                DrawLineInt(TrianglesImage, Point1, Point2);//Рисуем
                DrawLineInt(TrianglesImage, Point1, Point3);
                DrawLineInt(TrianglesImage, Point2, Point3);


            }
            pictureBox1.Image = TrianglesImage;

        }

        void Max()
        {
            for (int i = 0; i < TriangleCount; i++)//Проходимся по всем строкам и вытаскиваем треугольники
            {
                String[] Data = FileContent[i + 1].Split(' ');//Разбиваем строку на данные
                //Ищем максимальный X
                if (Convert.ToInt32(Data[0]) > Max_x) Max_x = Convert.ToInt32(Data[0]);
                if (Convert.ToInt32(Data[2]) > Max_x) Max_x = Convert.ToInt32(Data[2]);
                if (Convert.ToInt32(Data[4]) > Max_x) Max_x = Convert.ToInt32(Data[4]);
                //Ищем максимальный Y
                if (Convert.ToInt32(Data[1]) > Max_y) Max_y = Convert.ToInt32(Data[1]);
                if (Convert.ToInt32(Data[3]) > Max_y) Max_y = Convert.ToInt32(Data[3]);
                if (Convert.ToInt32(Data[5]) > Max_y) Max_y = Convert.ToInt32(Data[5]);
            }

        }


        public void DrawLineInt(Bitmap bmp, Point a, Point b)//Функция отрисовки линии
        {
            Pen blackPen = new Pen(Color.Black, 1);

            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.DrawLine(blackPen, a.X, a.Y, b.X, b.Y);
            }
        }


    }
}
